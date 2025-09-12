using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Clase base para la definición de flujos
    /// </summary>
    [CustomValidation(typeof(DocumentSetFlow), nameof(ValidateDocumentsSortedByType), ErrorMessage = "The DocumentSetFlowSimple definition is not valid.")]
    [CustomValidation(typeof(DocumentSetFlow), nameof(ValidateDocumentTypeByRecipients), ErrorMessage = "The DocumentSetFlowSimple definition is not valid.")]
    [CustomValidation(typeof(DocumentSetFlow), nameof(ValidateOnlyOneFormId), ErrorMessage = "The documentSet definition is not valid.")]
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
        public IEnumerable<Document> Documents { get; set; }

        /// <summary>
        /// Destinatarios
        /// </summary>
        public IEnumerable<RecipientFlow> Recipients { get; set; }

        /// <summary>
        /// Validación de que los documentos estén agrupados. Todos los PDFs juntos y todos los Forms juntos.
        /// </summary>
        /// <param name="documentSetFlow">Sobre de flujo.</param>
        /// <returns></returns>
        public static ValidationResult ValidateDocumentsSortedByType(DocumentSetFlow documentSetFlow)
        {
            if (DocumentSetValidators.CheckDocumentsSortedByType(documentSetFlow.Documents))
                return ValidationResult.Success;
            else
                return new ValidationResult("WebForms and PDFs must be grouped.", [nameof(documentSetFlow.Documents)]);
        }

        /// <summary>
        /// Validación de que si hay formularios no puede haber más de un destinatario.
        /// </summary>
        public static ValidationResult ValidateDocumentTypeByRecipients(DocumentSetFlow documentSetFlowSimple)
        {
            if (DocumentSetValidators.CheckDocumentTypeByRecipients(documentSetFlowSimple.Documents, documentSetFlowSimple.Recipients.Cast<RecipientBase>()))
                return ValidationResult.Success;
            else
                return new ValidationResult("WebForms can only have one recipient.", [nameof(documentSetFlowSimple.Documents)]);
        }


        /// <summary>
        /// Validación de que no puede haber más de un formulario definido mediante un identificador de formulario.
        /// </summary>
        public static ValidationResult ValidateOnlyOneFormId(DocumentSetFlow documentSetFlow)
        {
            if (DocumentSetValidators.CheckOnlyOneFormId(documentSetFlow.Documents))
                return ValidationResult.Success;
            else
                return new ValidationResult("Only one WebForm can be defined by FormId.", [nameof(DocumentContent.FormId)]);
        }

    }
}