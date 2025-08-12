using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Define un sobre de documentos a partir de un flujo para enviar a firma remota
    /// </summary>
    [CustomValidation(typeof(DocumentSetFlowUrlWithOverrides), nameof(ValidateDocumentSetFlowUrl), ErrorMessage = "The documentSetFlowUrl definition is not valid.")]
    [CustomValidation(typeof(DocumentSetFlowUrlWithOverrides), nameof(ValidateDocumentsSortedByType), ErrorMessage = "The documentSetFlowUrl definition is not valid.")]
    [CustomValidation(typeof(DocumentSetFlowUrlWithOverrides), nameof(ValidateDocumentTypeByRecipients), ErrorMessage = "The documentSetFlowUrl definition is not valid.")]
    [CustomValidation(typeof(DocumentSetFlowUrlWithOverrides), nameof(ValidateOnlyOneFormId), ErrorMessage = "The documentSet definition is not valid.")]
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
        /// Validación de definición del sobre.
        /// Comprueba
        ///   - no se puede indicar un orden a los destinatarios cuando el metod de envio es por Url
        ///   - no se puede enviar mas de un mensaje SMS o WhatsApp por destinatario
        /// </summary>
        /// <param name="documentSetFlowUrl">Definición del sobre</param>
        /// <returns>Un <see cref="ValidationResult"/> con el resultado de la validación</returns>
        public static ValidationResult ValidateDocumentSetFlowUrl(DocumentSetFlowUrlWithOverrides documentSetFlowUrl)
        {
            // Si se ha indicado un orden de destinatarios
            if (documentSetFlowUrl.GetType().Name.Equals("DocumentSetFlowUrl") && documentSetFlowUrl.Recipients.Any(r => r.Order != null))
            {
                return new ValidationResult("It is not possible to indicate the order of the recipients in this send method", [nameof(Recipients)]);
            }

            // Solo se puede enviar un mensaje por SMS o WhatsApp a cada destinatario
            if (((documentSetFlowUrl.ActionType.HasValue && documentSetFlowUrl.ActionType.Value.UseSMS() ? 1 : 0) +
                (documentSetFlowUrl.AuthenticationType.HasValue && documentSetFlowUrl.AuthenticationType.Value.UseSMS() ? 1 : 0)) > 1)
            {
                return new ValidationResult("Only the sending of an SMS or WhatsApp by envelope generated to authenticate the recipient(s) is allowed.",
                        [nameof(ActionType), nameof(AuthenticationType)]);
            }

            return ValidationResult.Success;
        }

        /// <summary>
        /// Validación de que los documentos estén agrupados. Todos los PDFs juntos y todos los Forms juntos.
        /// </summary>
        /// <param name="documentSetFlowUrl">Sobre de flujo Url.</param>
        /// <returns></returns>
        public static ValidationResult ValidateDocumentsSortedByType(DocumentSetFlowUrlWithOverrides documentSetFlowUrl)
        {
            if (CheckDocumentsSortedByType(documentSetFlowUrl.Documents))
                return ValidationResult.Success;
            else
                return new ValidationResult("WebForms and PDFs must be grouped.", [nameof(documentSetFlowUrl.Documents)]);
        }

        /// <summary>
        /// Validación de que si hay formularios no puede haber más de un destinatario.
        /// </summary>
        public static ValidationResult ValidateDocumentTypeByRecipients(DocumentSetFlowUrlWithOverrides documentSetFlowUrl)
        {
            if (CheckDocumentTypeByRecipients(documentSetFlowUrl.Documents, documentSetFlowUrl.Recipients.Cast<RecipientBase>()))
                return ValidationResult.Success;
            else
                return new ValidationResult("WebForms can only have one recipient.", [nameof(documentSetFlowUrl.Documents)]);
        }


        /// <summary>
        /// Validación de que no puede haber más de un formulario definido mediante un identificador de formulario.
        /// </summary>
        public static ValidationResult ValidateOnlyOneFormId(DocumentSetFlowUrlWithOverrides documentSetFlowUrl)
        {
            if (CheckOnlyOneFormId(documentSetFlowUrl.Documents))
                return ValidationResult.Success;
            else
                return new ValidationResult("Only one WebForm can be defined by FormId.", [nameof(DocumentContent.FormId)]);
        }
    }
}