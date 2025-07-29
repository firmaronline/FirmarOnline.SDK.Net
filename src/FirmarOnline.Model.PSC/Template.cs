using FirmarOnline.Model.Validations;
using System.ComponentModel.DataAnnotations;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Plantilla
    /// </summary>
    [CustomValidation(typeof(Template), nameof(ValidateTemplate), ErrorMessage = "The template is not valid.")]
    public class Template
    {
        /// <summary>
        /// Contenido de logo de cabecera en base 64
        /// </summary>
        [Base64]
        public string B64LogoContent { get; set; }

        /// <summary>
        /// Contenido de logo de pied de página en base 64
        /// </summary>
        [Base64]
        public string B64LogoFooterContent { get; set; }

        /// <summary>
        /// Pie de página en emai
        /// </summary>
        public string FooterEmail { get; set; }

        /// <summary>
        /// Asunto de notificación de solicitud de firma
        /// </summary>
        public string SignEmailSubject { get; set; }

        /// <summary>
        /// Cuerpo de notificación de solicitud de firma
        /// </summary>
        public string SignEmailBody { get; set; }

        /// <summary>
        /// Asunto de recordatorio de solicitud de firma
        /// </summary>
        public string RememberEmailSubject { get; set; }

        /// <summary>
        /// Cuerpo de recordatorio de solicitud de firma
        /// </summary>
        public string RememberEmailBody { get; set; }

        /// <summary>
        /// Asunto de notificación de documento firmado
        /// </summary>
        public string CompletedEmailSubject { get; set; }

        /// <summary>
        /// Cuerpo de notificación de documento firmado
        /// </summary>
        public string CompletedEmailBody { get; set; }

        /// <summary>
        /// Asunto de mensaje de notificación
        /// </summary>
        public string NotificationEmailSubject { get; set; }

        /// <summary>
        /// Cuerpo de mensaje de notificación
        /// </summary>
        public string NotificationEmailBody { get; set; }

        /// <summary>
        /// Asunto de notificación de cancelación del proceso de firma
        /// </summary>
        public string CanceledEmailSubject { get; set; }

        /// <summary>
        /// Cuerpo de notificación de cancelación del proceso de firma
        /// </summary>
        public string CanceledEmailBody { get; set; }

        /// <summary>
        /// Asunto de notificación de rechazo de la firma de un documento
        /// </summary>
        public string RejectedEmailSubject { get; set; }

        /// <summary>
        /// Cuerpo de notificación de rechazo de la firma de un documento
        /// </summary>
        public string RejectedEmailBody { get; set; }

        /// <summary>
        /// Asunto de notificación de proceso expirado
        /// </summary>
        public string ExpiredEmailSubject { get; set; }

        /// <summary>
        /// Cuerpo de notificación de proceso expirado
        /// </summary>
        public string ExpiredEmailBody { get; set; }

        /// <summary>
        /// Asunto de mensaje de Notificación Fehaciente.
        /// </summary>
        public string CertifiedNotificationEmailSubject { get; set; }

        /// <summary>
        /// Cuerpo de mensaje de Notificación Fehaciente.
        /// </summary>
        public string CertifiedNotificationEmailBody { get; set; }

        /// <summary>
        /// Origen de SMS
        /// </summary>
        public string FromSms { get; set; }

        /// <summary>
        /// SMS de solicitud de firma
        /// </summary>
        public string SignedSms { get; set; }

        /// <summary>
        /// Validación de template
        /// </summary>
        /// <param name="template">Template</param>
        /// <returns>Un <see cref="ValidationResult"/> con el resultado de la validación</returns>
        public static ValidationResult ValidateTemplate(Template template)
        {
            // Origen de SMS no puede contener más de 11 caracteres
            if (!string.IsNullOrEmpty(template?.FromSms) && template.FromSms.Length > 11)
            {
                return new ValidationResult("SMS source cannot contain more than 11 characters.", [nameof(FromSms)]);
            }

            if (!string.IsNullOrEmpty(template?.SignedSms))
            {
                // SMS de solicitud de firma no puede contener más de 90 caracteres"
                if (template.SignedSms.Length > 90)
                {
                    return new ValidationResult("Signature request SMS cannot contain more than 90 characters.", [nameof(SignedSms)]);
                }
                // SMS de solicitud de firma debe contener el patrón {url}
                if (!template.SignedSms.Contains("{url}"))
                {
                    return new ValidationResult("Signature request SMS must contain the pattern {url}", [nameof(SignedSms)]);
                }
            }

            return ValidationResult.Success;
        }
    }
}