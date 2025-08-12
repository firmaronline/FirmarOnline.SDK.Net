using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Define un sobre de documentos a partir de un flujo para enviar a firma remota
    /// </summary>
    [CustomValidation(typeof(DocumentSetFlowWithOverrides), nameof(ValidateDocumentSetFlow), ErrorMessage = "The documentSetFlow definition is not valid.")]
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
        ///   - que se indique el orden a todos o a ninguno de los destinatarios
        ///   - no se puede enviar mas de un mensaje SMS o WhatsApp por destinatario
        /// </summary>
        /// <param name="documentSetFlow">Definición del sobre</param>
        /// <returns>Un <see cref="ValidationResult"/> con el resultado de la validación</returns>
        public static ValidationResult ValidateDocumentSetFlow(DocumentSetFlowWithOverrides documentSetFlow)
        {
            // Si se ha indicado el orden de los destinatarios
            if (documentSetFlow.Recipients.Any(r => r.Order != null))
            {
                // No se puede usar el metodo de envio por URL
                if (documentSetFlow.SendMethod == PSC.SendMethod.None)
                {
                    return new ValidationResult("Cannot indicate the order of the recipients if the send method is not indicated.", [nameof(Recipients)]);
                }

                // Validamos que se indique un orden a todos los destinatarios y que no sea 0
                if (documentSetFlow.Recipients.Any(r => r.Order == null || r.Order == 0))
                {
                    return new ValidationResult("You must indicate the order to all recipients.", [nameof(Recipients)]);
                }
            }

            // Solo se puede enviar un mensaje por SMS o WhatsApp a cada destinatario
            if (((documentSetFlow.SendMethod.HasValue && documentSetFlow.SendMethod.Value.UseSMS() ? 1 : 0) +
                 (documentSetFlow.ActionType.HasValue && documentSetFlow.ActionType.Value.UseSMS() ? 1 : 0) +
                 (documentSetFlow.AuthenticationType.HasValue && documentSetFlow.AuthenticationType.Value.UseSMS() ? 1 : 0)) > 1)
            {
                return new ValidationResult("Only the sending of an SMS or WhatsApp by envelope generated to authenticate the recipient(s) is allowed.",
                        [nameof(SendMethod), nameof(ActionType), nameof(AuthenticationType)]);
            }

            return ValidationResult.Success;
        }
    }
}