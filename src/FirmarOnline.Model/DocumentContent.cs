using FirmarOnline.Model.Forms;
using FirmarOnline.Model.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FirmarOnline.Model
{
    /// <summary>
    /// Clase para definir un documento
    /// </summary>
    [CustomValidation(typeof(DocumentContent), nameof(ValidateDocumentContent),
        ErrorMessage = "The Document Content is not valid.")]
    public class DocumentContent
    {
        /// <summary>
        /// Nombre del documento
        /// </summary>
        [MaxLength(255)]
        public string Name { get; set; }

        /// <summary>
        /// Contenido del documento en base64
        /// </summary>
        [Base64PDF("El documento debe ser un fichero PDF válido.")]
        public string B64PDFContent { get; set; }

#if NET6_0_OR_GREATER
        /// <summary>
        /// Formulario de WebForms.
        /// </summary>
        public Form Form { get; set; }
#endif

        /// <summary>
        /// Valores para sustituir en el formulario.
        /// </summary>
        public IEnumerable<FormValue> FormValues { get; set; }

        /// <summary>
        /// Identificador de formulario de WebForms.
        /// </summary>
        public string FormId { get; set; }

        /// <summary>
        /// Validación de contenido de documento. Hay que especificar un único contenido. Puede ser
        /// un PDF en Base64, o un JSON (Form) o una plantilla de JSON (FormId).
        /// </summary>
        /// <param name="documentContent"></param>
        /// <returns></returns>
        public static ValidationResult ValidateDocumentContent(DocumentContent documentContent)
        {
            bool hasB64Content = !string.IsNullOrWhiteSpace(documentContent.B64PDFContent);
#if NET6_0_OR_GREATER
            bool hasForm = documentContent.Form != null;
#else
            bool hasForm = false;
#endif
            bool hasFormId = !string.IsNullOrWhiteSpace(documentContent.FormId);

            int informedCount = Convert.ToInt32(hasB64Content) + Convert.ToInt32(hasForm) + Convert.ToInt32(hasFormId);

            if (informedCount != 1)
            {
                return new ValidationResult(
                    "Exactly one of the following properties must be provided: B64PDFContent, Form, or FormId.");
            }

            return ValidationResult.Success;
        }
    }
}