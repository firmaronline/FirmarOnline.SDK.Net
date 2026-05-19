using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Define un sobre de documentos a partir de un flujo para enviar a firma remota
    /// </summary>
    [CustomValidation(typeof(DocumentSetFlowWithOverrides), nameof(ValidateDocumentSetFlowWithOverrides), ErrorMessage = "The documentSetFlow definition is not valid.")]
    public class DocumentSetFlowWithOverrides : DocumentSetFlowUrlWithOverrides
    {
        /// <summary>
        /// Método de envío a destinatarios
        /// </summary>
        [EnumDataType(typeof(SendMethod))]
        public SendMethod? SendMethod { get; set; }

        /// <summary>
        /// Validación de definición del sobre.
        /// Comprueba
        ///   - que si se indica el orden de los destinatarios el metodo de envio no sea por URL.
        ///   - que el email sea opcional si el método de envió es a dispositivo
        ///   - no se puede enviar mas de un mensaje SMS o WhatsApp por destinatario
        /// </summary>
        /// <param name="documentSetFlow">Definición del sobre</param>
        /// <returns>Un <see cref="ValidationResult"/> con el resultado de la validación</returns>
        public static ValidationResult ValidateDocumentSetFlowWithOverrides(DocumentSetFlowWithOverrides documentSetFlow)
        {
            // Si se ha indicado el orden de los destinatarios
            if (documentSetFlow.Recipients.Any(r => r.Order != null))
            {
                // No se puede usar el metodo de envio por URL
                if (documentSetFlow.SendMethod == PSC.SendMethod.None)
                {
                    return new ValidationResult("Cannot indicate the order of the recipients if the send method is not indicated.", [nameof(Recipients)]);
                }
            }

            // El email solo es opcional si el método de envió es a dispositivo
            if (documentSetFlow.SendMethod != PSC.SendMethod.Device && documentSetFlow.Recipients.Any(r => r.Email == null))
            {
                return new ValidationResult("The Email field is required.", [nameof(documentSetFlow.Recipients)]);
            }

            // Solo se puede enviar un mensaje por SMS o WhatsApp a cada destinatario
            if (((documentSetFlow.SendMethod.HasValue && documentSetFlow.SendMethod.Value.RequiresPhoneVerification() ? 1 : 0) +
                 (documentSetFlow.ActionType.HasValue && documentSetFlow.ActionType.Value.RequiresPhoneVerification() ? 1 : 0) +
                 (documentSetFlow.AuthenticationType == RecipientAuthenticationType.Mfa
                    ? documentSetFlow.AuthSteps?.Count(s => s.Type.RequiresPhoneVerification()) ?? 0
                    : (documentSetFlow.AuthenticationType.HasValue && documentSetFlow.AuthenticationType.Value.RequiresPhoneVerification() ? 1 : 0))) > 1)
            {
                return new ValidationResult("Only the sending of an SMS or WhatsApp by envelope generated to authenticate the recipient(s) is allowed.",
                    [nameof(SendMethod), nameof(ActionType), nameof(AuthenticationType)]);
            }

            // No se puede usar envío por email y autenticación OTP Email a la vez
            if (documentSetFlow.SendMethod.HasValue && documentSetFlow.SendMethod.Value.RequiresEmailDelivery() &&
                (documentSetFlow.AuthenticationType == RecipientAuthenticationType.Mfa
                    ? documentSetFlow.AuthSteps?.Any(s => s.Type.RequiresEmailVerification()) ?? false
                    : (documentSetFlow.AuthenticationType.HasValue && documentSetFlow.AuthenticationType.Value.RequiresEmailVerification())))
            {
                return new ValidationResult("Email delivery and OTP Email authentication cannot be used together.",
                    [nameof(SendMethod), nameof(AuthenticationType)]);
            }

            return ValidationResult.Success;
        }
    }
}