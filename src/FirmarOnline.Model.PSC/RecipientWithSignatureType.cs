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
        /// Valida la definición del destinatario.
        /// Comprueba que
        ///   - se haya introducido la información para autenticación por código de acceso
        ///   - no se hayan definido anexos para el actionType de notificación
        /// </summary>
        /// <param name="recipient">Definición del firmante</param>
        /// <returns>Un <see cref="ValidationResult"/> con el resultado de la validación</returns>
            public static ValidationResult ValidateRecipient(RecipientWithSignatureType recipient)
        {
            if (recipient.AuthType == RecipientAuthenticationType.AccessCode
                && string.IsNullOrEmpty(recipient.AccessCode?.Challenge))
            {
                return new ValidationResult("The access code challenge data is required.", [nameof(AccessCode)]);
            }

            if (recipient.ActionType == RecipientActionType.CertifiedNotification
                && (recipient.Attachments?.Count() ?? 0) > 0)
            {
                return new ValidationResult($"The field {nameof(Attachments)} is not valid.", [nameof(Attachments)]);
            }

            return ValidationResult.Success;
        }
    }
}