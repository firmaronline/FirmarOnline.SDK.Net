using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Define un sobre de documentos para enviar a firma remota
    /// </summary>
    [CustomValidation(typeof(DocumentSet), nameof(ValidateDocumentSet), ErrorMessage = "The documentSet definition is not valid.")]
    [CustomValidation(typeof(DocumentSet), nameof(ValidateDocumentsSortedByType), ErrorMessage = "The documentSet definition is not valid.")]
    [CustomValidation(typeof(DocumentSet), nameof(ValidateDocumentTypeByRecipients), ErrorMessage = "The documentSet definition is not valid.")]
    [CustomValidation(typeof(DocumentSet), nameof(ValidateOnlyOneFormId), ErrorMessage = "The documentSet definition is not valid.")]
    [CustomValidation(typeof(DocumentSet), nameof(ValidateParallelRecipientsWithActionType60), ErrorMessage = "The documentSet definition is not valid.")]
    [CustomValidation(typeof(DocumentSet), nameof(ValidateDocumentTypeByActionType60), ErrorMessage = "The documentSet definition is not valid.")]
    public class DocumentSet : DocumentSetStandAloneBase
    {
        /// <summary>
        /// Firma corporativa
        /// </summary>
        public CorporateSignature CorporateSignature { get; set; }

        /// <summary>
        /// Documentos
        /// </summary>
        public IEnumerable<Document> Documents { get; set; }

        /// <summary>
        /// Destinatarios
        /// </summary>
        public IEnumerable<Recipient> Recipients { get; set; }

        /// <summary>
        /// Notificaciones (envío de copia de documento firmado)
        /// </summary>
        public List<Notification> Notifications { get; set; }

        /// <summary>
        /// Tipo de envio (None, email,sms)
        /// </summary>
        [EnumDataType(typeof(SendMethod))]
        public SendMethod SendMethod { get; set; }

        /// <summary>
        /// Número de días tras los que se enviará un recordatorio
        /// </summary>
        [Range(0, 180)]
        public int ReminderDays { get; set; }

        /// <summary>
        /// Validación de definición del sobre.
        /// Comprueba
        ///   - que los días de validez del sobre sean más que los días para el envío de recordatorio
        ///   - que no haya más de un envío de SMS para ningún destinatario
        ///   - que si se indica el orden de los destinatarios el metodo de envío no sea por URL.
        ///   - que se indique el orden a todos o a ninguno de los destinatarios
        ///   - que el email sea opcional si el método de envió es a dispositivo
        /// </summary>
        /// <param name="documentSet">Definición del sobre</param>
        /// <returns>Un <see cref="ValidationResult"/> con el resultado de la validación</returns>
        public static ValidationResult ValidateDocumentSet(DocumentSet documentSet)
        {
            if (documentSet.ReminderDays >= documentSet.ExpirationDaysTimeout)
            {
                return new ValidationResult($"The value of {nameof(ReminderDays)} must be less than the value of {nameof(ExpirationDaysTimeout)}.",
                    [nameof(ReminderDays), nameof(ExpirationDaysTimeout)]);
            }

            if (documentSet.Recipients.Any(
                r => ((documentSet.SendMethod.UseSMS() ? 1 : 0) +
                    (r.ActionType.UseSMS() ? 1 : 0) + (r.AuthType?.UseSMS() ?? false ? 1 : 0)) > 1))
            {
                return new ValidationResult("Only one SMS or WhatsApp message per recipient is allowed.",
                    [nameof(SendMethod), nameof(Recipient.ActionType), nameof(Recipient.AuthType)]);
            }

            // Si se ha indicado el orden de los destinatarios
            if (documentSet.Recipients.Any(r => r.Order != null))
            {
                // No se puede usar el metodo de envio por URL
                if (documentSet.SendMethod == SendMethod.None)
                {
                    return new ValidationResult("Cannot indicate the order of the recipients if the send method is not indicated.", [nameof(Recipients)]);
                }

                // Validamos que se indique un orden a todos los destinatarios y que no sea 0
                if (documentSet.Recipients.Any(r => r.Order == null || r.Order == 0))
                {
                    return new ValidationResult("You must indicate the order to all recipients.", [nameof(Recipients)]);
                }
            }

            // El email solo es opcional si el método de envió es a dispositivo
            if (documentSet.SendMethod != SendMethod.Device && documentSet.Recipients.Any(r => r.Email == null))
            {
                return new ValidationResult("The Email field is required.", [nameof(Recipients)]);
            }

            return ValidationResult.Success;
        }

        /// <summary>
        /// Validación de que los documentos estén agrupados. Todos los PDFs juntos y todos los Forms juntos.
        /// </summary>
        public static ValidationResult ValidateDocumentsSortedByType(DocumentSet documentSet)
        {
            if (DocumentSetValidators.CheckDocumentsSortedByType(documentSet.Documents))
                return ValidationResult.Success;
            else
                return new ValidationResult("WebForms and PDFs must be grouped.", [nameof(documentSet.Documents)]);
        }

        /// <summary>
        /// Validación de que si hay formularios no puede haber más de un destinatario.
        /// </summary>
        public static ValidationResult ValidateDocumentTypeByRecipients(DocumentSet documentSet)
        {
            if (DocumentSetValidators.CheckDocumentTypeByRecipients(documentSet.Documents, documentSet.Recipients.Cast<RecipientBase>()))
                return ValidationResult.Success;
            else
                return new ValidationResult("WebForms can only have one recipient.", [nameof(documentSet.Documents)]);
        }

        /// <summary>
        /// Validación de que no puede haber más de un formulario definido mediante un identificador de formulario.
        /// </summary>
        public static ValidationResult ValidateOnlyOneFormId(DocumentSet documentSet)
        {
            if (DocumentSetValidators.CheckOnlyOneFormId(documentSet.Documents))
                return ValidationResult.Success;
            else
                return new ValidationResult("Only one WebForm can be defined by FormId.", [nameof(DocumentContent.FormId)]);
        }

        /// <summary>
        /// Validación de que si hay destinatarios en paralelo no puede haber ningún Action Type 60.
        /// </summary>
        public static ValidationResult ValidateParallelRecipientsWithActionType60(DocumentSet documentSet)
        {
            var recipientsCryptoAPISignature = documentSet.Recipients
                .Where(r => r.ActionType == RecipientActionType.CryptoAPISignature && r.Order != null);

            if (recipientsCryptoAPISignature.Any() && recipientsCryptoAPISignature.GroupBy(r => r.Order).Where(g => g.Count() > 1).Any())
            {
                return new ValidationResult("A document set cannot contain parallel recipients and Action Type 60.", [nameof(Recipients)]);
            }
            else
            {
                return ValidationResult.Success;
            }
        }

        /// <summary>
        /// Validación de que si hay un Action Type 60 no puede haber ningún WebForm.
        /// </summary>
        public static ValidationResult ValidateDocumentTypeByActionType60(DocumentSet documentSet)
        {
            if (DocumentSetValidators.CheckDocumentTypeByActionType60(documentSet.Documents, documentSet.Recipients))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("A document set cannot contain recipients with Action Type 60 and WebForms.", [nameof(documentSet.Documents)]);
            }
        }
    }
}