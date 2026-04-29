using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Destinatario de un sobre con información del tipo de autenticación y la acción a realizar
    /// </summary>
    [CustomValidation(typeof(RecipientWithSignatureType), nameof(ValidateRecipient),
        ErrorMessage = "The Recipient definitios is not valid.")]
    public abstract class RecipientWithSignatureType : RecipientBase
    {
        /// <summary>
        /// Tipo de autenticación que debe realizar el destinatario sobre los documentos
        /// </summary>
        [DefaultValue(RecipientAuthenticationType.Basic)]
        [EnumDataType(typeof(RecipientAuthenticationType))]
        public RecipientAuthenticationType? AuthType { get; set; }

        /// <summary>
        /// Tipo de acción que debe realizar el destinatario sobre los documentos
        /// </summary>
        [Required]
        [EnumDataType(typeof(RecipientActionType))]
        public RecipientActionType ActionType { get; set; }

        /// <summary>
        /// Secuencia ordenada de pasos de autenticación (requerido si AuthType = MFA)
        /// </summary>
        public AuthenticationStep[] AuthSteps { get; set; }

        /// <summary>
        /// Valida la definición del destinatario.
        /// Comprueba que
        ///   - si el AuthType es MFA, haya al menos dos AuthSteps
        ///   - si el AuthType es MFA, no haya AuthSteps con el mismo Type
        ///   - si el AuthType es MFA y existe un AuthStep con tipo AccessCode, su Challenge no esté vacío
        ///   - si el AuthType es AccessCode, se haya introducido la información para autenticación por código de acceso
        ///   - si el AuthType no es MFA, no se hayan definido AuthSteps
        ///   - no se hayan definido anexos para el actionType de notificación
        /// </summary>
        /// <param name="recipient">Definición del firmante</param>
        /// <returns>Un <see cref="ValidationResult"/> con el resultado de la validación</returns>
        public static ValidationResult ValidateRecipient(RecipientWithSignatureType recipient)
        {
            return ValidateAuthentication(recipient)
                ?? ValidateAttachments(recipient)
                ?? ValidationResult.Success;
        }

        private static ValidationResult ValidateAuthentication(RecipientWithSignatureType recipient) =>
            recipient.AuthType switch
            {
                RecipientAuthenticationType.Mfa => AuthenticationStepRules.ValidateMfaSteps(recipient.AuthSteps),
                RecipientAuthenticationType.AccessCode =>
                    AuthenticationStepRules.ValidateAccessCodeChallenge(recipient.AccessCode?.Challenge)
                        ?? AuthenticationStepRules.ValidateNoAuthSteps(recipient.AuthSteps, nameof(AuthType)),
                _ => AuthenticationStepRules.ValidateNoAuthSteps(recipient.AuthSteps, nameof(AuthType))
            };

        private static ValidationResult ValidateAttachments(RecipientWithSignatureType recipient)
        {
            return recipient.ActionType == RecipientActionType.CertifiedNotification
                && (recipient.Attachments?.Any() ?? false)
                ? new ValidationResult($"The field {nameof(Attachments)} is not valid.", [nameof(Attachments)])
                : null;
        }
    }
}