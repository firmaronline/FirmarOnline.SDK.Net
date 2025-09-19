using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Define un sobre de documentos a partir de un flujo para enviar a firma remota
    /// </summary>
    [CustomValidation(typeof(DocumentSetFlowUrlWithOverrides), nameof(ValidateDocumentSetFlowUrlWithOverrides), ErrorMessage = "The documentSetFlowUrl definition is not valid.")]
    [CustomValidation(typeof(DocumentSetFlowUrlWithOverrides), nameof(ValidateParallelRecipientsWithActionType60), ErrorMessage = "The documentSetFlowUrl definition is not valid.")]
    [CustomValidation(typeof(DocumentSetFlowUrlWithOverrides), nameof(ValidateDocumentTypeByRecipients), ErrorMessage = "The documentSetFlowUrl definition is not valid.")]
    [CustomValidation(typeof(DocumentSetFlowUrlWithOverrides), nameof(ValidateDocumentTypeByActionType60), ErrorMessage = "The documentSetFlowUrl definition is not valid.")]    
    [CustomValidation(typeof(DocumentSetFlowUrlWithOverrides), nameof(ValidateDocumentTypeByCorporateSignature), ErrorMessage = "The documentSetFlowUrl definition is not valid.")]
    public class DocumentSetFlowUrlWithOverrides : DocumentSetFlow
    {
        /// <summary>
        /// Idioma de flujo
        /// </summary>
        [EnumDataType(typeof(LanguageCode))]
        public LanguageCode? Language { get; set; }

        /// <summary>
        /// Nombre del remitente
        /// </summary>
        [MaxLength(255)]
        public string SenderName { get; set; }

        /// <summary>
        /// Email del remitente
        /// </summary>
        [EmailAddress]
        [MaxLength(255)]
        public string SenderMail { get; set; }

        /// <summary>
        /// Días recordatorio de sobre
        /// </summary>
        [Range(0, 180)]
        public int? ReminderDays { get; set; }

        /// <summary>
        /// Días expiración de sobre
        /// </summary>
        [Range(1, 180)]
        public int? ExpirationDaysTimeout { get; set; }

        /// <summary>
        /// Envío a los destinatarios de los documentos del proceso al final del flujo
        /// </summary>
        public bool? SendDocToRecipient { get; set; }

        /// <summary>
        /// Envío al remitente de los documentos y el certificado de trazabilidad del proceso al final del flujo
        /// </summary>
        public bool? SendDocToSender { get; set; }

        /// <summary>
        /// Tipo de RecipientAction
        /// </summary>
        [EnumDataType(typeof(RecipientActionType))]
        public RecipientActionType? ActionType { get; set; }

        /// <summary>
        /// Tipo de autenticación a realizar por el destinatario
        /// </summary>
        [EnumDataType(typeof(RecipientAuthenticationType))]
        public RecipientAuthenticationType? AuthenticationType { get; set; }

        /// <summary>
        /// Código de acceso (para autenticación mediante código de acceso)
        /// </summary>
        public AccessCode AccessCode { get; set; }

        /// <summary>
        /// Notificaciones (envío de copia de documento firmado)
        /// </summary>
        public List<Notification> Notifications { get; set; }

        /// <summary>
        /// Habilitar LTV (validación a largo plazo) para firma
        /// </summary>
        public bool? Ltv { get; set; }

        /// <summary>
        /// Firma corporativa
        /// </summary>
        public CorporateSignature CorporateSignature { get; set; }

        /// <summary>
        /// Validación de definición del sobre, comprueba:
        ///   - no se puede indicar un orden a los destinatarios cuando el método de envío es por Url
        ///   - que los días de validez del sobre sean más que los días para el envío de recordatorio
        ///   - no se puede enviar mas de un mensaje SMS o WhatsApp por destinatario
        /// </summary>
        /// <param name="documentSetFlowUrl">Definición del sobre</param>
        /// <returns>Un <see cref="ValidationResult"/> con el resultado de la validación</returns>
        public static ValidationResult ValidateDocumentSetFlowUrlWithOverrides(DocumentSetFlowUrlWithOverrides documentSetFlowUrl)
        {
            if (documentSetFlowUrl.GetType() == typeof(DocumentSetFlowUrlWithOverrides) && documentSetFlowUrl.Recipients.Any(r => r.Order != null))
            {
                return new ValidationResult("It is not possible to indicate the order of the recipients in this send method", [nameof(documentSetFlowUrl.Recipients)]);
            }

            if (documentSetFlowUrl.ReminderDays >= documentSetFlowUrl.ExpirationDaysTimeout)
            {
                return new ValidationResult($"The value of {nameof(documentSetFlowUrl.ReminderDays)} must be less than the value of {nameof(documentSetFlowUrl.ExpirationDaysTimeout)}.",
                    [nameof(documentSetFlowUrl.ReminderDays), nameof(documentSetFlowUrl.ExpirationDaysTimeout)]);
            }

            // Solo se puede enviar un mensaje por SMS o WhatsApp a cada destinatario
            if (((documentSetFlowUrl.ActionType.HasValue && documentSetFlowUrl.ActionType.Value.UseSMS() ? 1 : 0) +
                (documentSetFlowUrl.AuthenticationType.HasValue && documentSetFlowUrl.AuthenticationType.Value.UseSMS() ? 1 : 0)) > 1)
            {
                return new ValidationResult("Only the sending of an SMS or WhatsApp by envelope generated to authenticate the recipient(s) is allowed.", [nameof(ActionType), nameof(AuthenticationType)]);
            }

            return ValidationResult.Success;
        }

        /// <summary>
        /// Validación de que si hay formularios no puede haber más de un destinatario.
        /// </summary>
        public static ValidationResult ValidateDocumentTypeByRecipients(DocumentSetFlowUrlWithOverrides documentSetFlowUrl)
        {
            if (DocumentSetRules.CheckDocumentTypeByRecipients(documentSetFlowUrl.Documents, documentSetFlowUrl.Recipients))
                return ValidationResult.Success;
            else
                return new ValidationResult("WebForms can only have one recipient.", [nameof(documentSetFlowUrl.Documents)]);
        }

        /// <summary>
        /// Validación de que si hay un Action Type 60 no puede haber ningún WebForm.
        /// </summary>
        public static ValidationResult ValidateDocumentTypeByActionType60(DocumentSetFlowUrlWithOverrides documentSetFlowUrl)
        {
            if (documentSetFlowUrl.ActionType == RecipientActionType.CryptoAPISignature &&
#if NET6_0_OR_GREATER
                documentSetFlowUrl.Documents.Any(doc => doc.Form != null || doc.FormId != null))
#else
                documentSetFlowUrl.Documents.Any(doc => doc.FormId != null))
#endif
            {
                return new ValidationResult("A document set cannot contain recipients with Action Type 60 and WebForms.", [nameof(documentSetFlowUrl.Documents)]);
            }
            else
            {
                return ValidationResult.Success;
            }
        }

        /// <summary>
        /// Validación de que no puede haber más de un formulario definido mediante un identificador de formulario.
        /// </summary>
        public static ValidationResult ValidateParallelRecipientsWithActionType60(DocumentSetFlowUrlWithOverrides documentSetFlowUrl)
        {
            if (documentSetFlowUrl.ActionType == RecipientActionType.CryptoAPISignature && 
                documentSetFlowUrl.Recipients.Where(r => r.Order != null).Any() &&
                documentSetFlowUrl.Recipients.Where(r => r.Order != null).GroupBy(r => r.Order).Where(g => g.Count() > 1).Any())
            {
                return new ValidationResult("A document set cannot contain parallel recipients and Action Type 60.", [nameof(documentSetFlowUrl.Recipients)]);
            }
            else
            {
                return ValidationResult.Success;
            }
        }

        /// <summary>
        /// Valida que si el documento es un WebForm, no se configure una firma corporativa al inicio.
        /// </summary>
        public static ValidationResult ValidateDocumentTypeByCorporateSignature(DocumentSetFlowUrlWithOverrides documentSetFlowUrl)
        {
            if (DocumentSetRules.CheckDocumentTypeByCorporateSignature(documentSetFlowUrl.CorporateSignature, documentSetFlowUrl.Documents))
                return ValidationResult.Success;
            else
                return new ValidationResult("It is not possible set a corporate signature at the beginning if the content of the document is a WebForm.");
        }
    }
}