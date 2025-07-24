using FirmarOnline.Types.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#if NET6_0_OR_GREATER
using FirmarOnline.Model.Forms;
#endif

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
            int countDocumentContent = 0;
            if (!string.IsNullOrWhiteSpace(documentContent.B64PDFContent))
            {
                countDocumentContent++;
            }

#if NET6_0_OR_GREATER
            if (documentContent.Form != null)
            {
                countDocumentContent++;
            }
#endif

            if (!string.IsNullOrWhiteSpace(documentContent.FormId))
            {
                countDocumentContent++;
            }

            if (countDocumentContent != 1)
            {
                return new ValidationResult("Only one document content property can be non-null and at least one document content property must be non-null");
            }

            return ValidationResult.Success;
        }
    }

    /// <summary>
    /// Valores de Formulario (WebForms).
    /// </summary>
    public class FormValue
    {
        /// <summary>
        /// Identificador.
        /// </summary>
        public String Id { get; set; }

        /// <summary>
        /// Valor.
        /// </summary>
        public object Value { get; set; }
    }
}