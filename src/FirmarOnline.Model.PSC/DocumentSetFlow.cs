using System.ComponentModel.DataAnnotations;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Clase base para la definición de flujos
    /// </summary>
    [CustomValidation(typeof(DocumentSetFlow), nameof(ValidateDocumentTypeByRecipients), ErrorMessage = "The DocumentSetFlowSimple definition is not valid.")]
    public class DocumentSetFlow : DocumentSetBase
    {
        /// <summary>
        /// Identificador de token de flujo para cliente
        /// </summary>
        [Required]
        [MaxLength(80)]
        public string FlowTokenId { get; set; }

        /// <summary>
        /// Documentos
        /// </summary>
        public DocumentCollection Documents { get; set; }

        /// <summary>
        /// Destinatarios
        /// </summary>
        public RecipientCollection<RecipientFlow> Recipients { get; set; }

        /// <summary>
        /// Validación de que si hay formularios no puede haber más de un destinatario.
        /// </summary>
        public static ValidationResult ValidateDocumentTypeByRecipients(DocumentSetFlow documentSetFlowSimple)
        {
            if (DocumentSetRules.CheckDocumentTypeByRecipients(documentSetFlowSimple.Documents, documentSetFlowSimple.Recipients))
                return ValidationResult.Success;
            else
                return new ValidationResult("WebForms can only have one recipient.", [nameof(documentSetFlowSimple.Documents)]);
        }
    }
}