using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Define un sobre de documentos para enviar a firma remota
    /// </summary>
    [CustomValidation(typeof(DocumentSet), nameof(ValidateDocumentSet), ErrorMessage = "The documentSet definition is not valid.")]
    [CustomValidation(typeof(DocumentSet), nameof(ValidateDocumentTypeByRecipients), ErrorMessage = "The documentSet definition is not valid.")]
    [CustomValidation(typeof(DocumentSet), nameof(ValidateDocumentTypeByActionType60), ErrorMessage = "The documentSet definition is not valid.")]
    [CustomValidation(typeof(DocumentSet), nameof(ValidateDocumentTypeByCorporateSignature), ErrorMessage = "The documentSet definition is not valid.")]
    public class DocumentSet : DocumentSetStandAloneBase
    {
        /// <summary>
        /// Firma corporativa
        /// </summary>
        public CorporateSignature CorporateSignature { get; set; }

        /// <summary>
        /// Documentos
        /// </summary>
        public DocumentCollection Documents { get; set; }

        /// <summary>
        /// Destinatarios
        /// </summary>
        public RecipientCollection<Recipient> Recipients { get; set; }

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
        ///   - que el email sea opcional si el método de envió es a dispositivo
        /// </summary>
        /// <param name="documentSet">Definición del sobre</param>
        /// <returns>Un <see cref="ValidationResult"/> con el resultado de la validación</returns>
        public static ValidationResult ValidateDocumentSet(DocumentSet documentSet)
        {
            if (documentSet.ReminderDays >= documentSet.ExpirationDaysTimeout)
            {
                return new ValidationResult($"The value of {nameof(documentSet.ReminderDays)} must be less than the value of {nameof(documentSet.ExpirationDaysTimeout)}.",
                    [nameof(documentSet.ReminderDays), nameof(documentSet.ExpirationDaysTimeout)]);
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
                    return new ValidationResult("Cannot indicate the order of the recipients if the send method is not indicated.", [nameof(documentSet.Recipients)]);
                }
            }

            // El email solo es opcional si el método de envió es a dispositivo
            if (documentSet.SendMethod != SendMethod.Device && documentSet.Recipients.Any(r => r.Email == null))
            {
                return new ValidationResult("The Email field is required.", [nameof(documentSet.Recipients)]);
            }

            return ValidationResult.Success;
        }

        /// <summary>
        /// Validación de que si hay formularios no puede haber más de un destinatario.
        /// </summary>
        public static ValidationResult ValidateDocumentTypeByRecipients(DocumentSet documentSet)
        {
            if (DocumentSetRules.CheckDocumentTypeByRecipients(documentSet.Documents, documentSet.Recipients))
                return ValidationResult.Success;
            else
                return new ValidationResult("WebForms can only have one recipient.", [nameof(documentSet.Documents)]);
        }

        /// <summary>
        /// Validación de que si hay un Action Type 60 no puede haber ningún WebForm.
        /// </summary>
        public static ValidationResult ValidateDocumentTypeByActionType60(DocumentSet documentSet)
        {
            if (DocumentSetRules.CheckDocumentTypeByActionType60(documentSet.Documents, documentSet.Recipients))
                return ValidationResult.Success;
            else
                return new ValidationResult("A document set cannot contain recipients with Action Type 60 and WebForms.", [nameof(documentSet.Documents)]);
        }

        /// <summary>
        /// Valida que si el documento es un WebForm, no se configure una firma corporativa al inicio.
        /// </summary>
        public static ValidationResult ValidateDocumentTypeByCorporateSignature(DocumentSet documentSet)
        {
            if (DocumentSetRules.CheckDocumentTypeByCorporateSignature(documentSet.CorporateSignature, documentSet.Documents))
                return ValidationResult.Success;
            else
                return new ValidationResult("It is not possible set a corporate signature at the beginning if the content of the document is a WebForm.");                
        }
    }
}