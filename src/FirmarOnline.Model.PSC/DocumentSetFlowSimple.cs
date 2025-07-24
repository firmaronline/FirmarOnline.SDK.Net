using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Define un sobre de documentos a partir de un flujo para enviar a firma remota
    /// </summary>
    [CustomValidation(typeof(DocumentSetFlowSimple), nameof(ValidateDocumentsSortedByType), ErrorMessage = "The DocumentSetFlowSimple definition is not valid.")]
    [CustomValidation(typeof(DocumentSetFlowSimple), nameof(ValidateDocumentTypeByRecipients), ErrorMessage = "The DocumentSetFlowSimple definition is not valid.")]
    [CustomValidation(typeof(DocumentSetFlowSimple), nameof(ValidateOnlyOneFormId), ErrorMessage = "The documentSet definition is not valid.")]
    public class DocumentSetFlowSimple : DocumentSetFlowBase
    {
        /// <summary>
        /// Validación de que los documentos estén agrupados. Todos los PDFs juntos y todos los Forms juntos.
        /// </summary>
        /// <param name="documentSetFlowSimple">Sobre de flujo.</param>
        /// <returns></returns>
        public static ValidationResult ValidateDocumentsSortedByType(DocumentSetFlowSimple documentSetFlowSimple)
        {
            if (CheckDocumentsSortedByType(documentSetFlowSimple.Documents))
                return ValidationResult.Success;
            else
                return new ValidationResult("WebForms and PDFs must be grouped.", new string[] { nameof(documentSetFlowSimple.Documents) });
        }

        /// <summary>
        /// Validación de que si hay formularios no puede haber más de un destinatario.
        /// </summary>
        public static ValidationResult ValidateDocumentTypeByRecipients(DocumentSetFlowSimple documentSetFlowSimple)
        {
            if (CheckDocumentTypeByRecipients(documentSetFlowSimple.Documents, documentSetFlowSimple.Recipients.Cast<RecipientBase>()))
                return ValidationResult.Success;
            else
                return new ValidationResult("WebForms can only have one recipient.", new string[] { nameof(documentSetFlowSimple.Documents) });
        }


        /// <summary>
        /// Validación de que no puede haber más de un formulario definido mediante un identificador de formulario.
        /// </summary>
        public static ValidationResult ValidateOnlyOneFormId(DocumentSetFlowSimple documentSetFlowSimple)
        {
            if (CheckOnlyOneFormId(documentSetFlowSimple.Documents))
                return ValidationResult.Success;
            else
                return new ValidationResult("Only one WebForm can be defined by FormId.", new string[] { nameof(DocumentContent.FormId) });
        }
    }
}