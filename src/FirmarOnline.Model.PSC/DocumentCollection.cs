using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Define una colección de documentos a firmar
    /// </summary>
    [CustomValidation(typeof(DocumentCollection), nameof(ValidateOnlyOneFormId), ErrorMessage = "The documentSet definition is not valid.")]
    [CustomValidation(typeof(DocumentCollection), nameof(ValidateDocumentsSortedByType), ErrorMessage = "The documentSet definition is not valid.")]
    public class DocumentCollection : List<Document>
    {
        /// <summary>
        /// Validación de que no puede haber más de un formulario definido mediante un identificador de formulario.
        /// </summary>
        public static ValidationResult ValidateOnlyOneFormId(DocumentCollection documents)
        {
            if (documents.Count(d => d.FormId != null) > 1)
                return new ValidationResult("Only one WebForm can be defined by FormId.", [nameof(DocumentContent.FormId)]);
            else
                return ValidationResult.Success;
        }

        /// <summary>
        /// Validación de que los documentos estén agrupados. Todos los PDFs juntos y todos los Forms juntos.
        /// </summary>
        public static ValidationResult ValidateDocumentsSortedByType(DocumentCollection documents)
        {
            if (!documents.Any()) return ValidationResult.Success;

#if NET6_0_OR_GREATER
            var documentTypes = documents.Select(d => d.Form != null || d.FormId != null ? 0 : 1).ToList();
#else
            var documentTypes = documents.Select(d => d.FormId != null ? 0 : 1).ToList();
#endif

            bool result = false;

            // Orden ascendente
            if (documentTypes.FirstOrDefault() == 0)
                result = documentTypes.Zip(documentTypes.Skip(1), (curr, next) => curr <= next).All(x => x);
            else
                result = documentTypes.Zip(documentTypes.Skip(1), (curr, next) => curr >= next).All(x => x);

            if (result)
                return ValidationResult.Success;
            else
                return new ValidationResult("WebForms and PDFs must be grouped.", [nameof(documents)]);
        }
    }
}