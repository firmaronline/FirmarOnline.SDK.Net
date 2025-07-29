using FirmarOnline.Model.Certificates;
using FirmarOnline.Model.Validations;
using System.ComponentModel.DataAnnotations;

namespace FirmarOnline.Model.Verify
{
    /// <summary>
    /// Definición de verificación de firmas de un documento PDF
    /// </summary>
    [CustomValidation(typeof(VerifyPDFDocument), nameof(ValidateVerifyPDFDocument),
        ErrorMessage = "The verify pdf document definition is not valid.")]
    public class VerifyPDFDocument
    {
        /// <summary>
        /// Contenido del documento en base 64
        /// </summary>
        [Required]
        [Base64]
        public string B64PDFContent { get; set; }

        /// <summary>
        /// Modo de verificación de firma
        /// </summary>
        [EnumDataType(typeof(VerifyMode))]
        public VerifyMode Mode { get; set; }

        /// <summary>
        /// Certificado para descifrar los datos biométricos
        /// </summary>
        public PKCS12Certificate Certificate { get; set; }

        /// <summary>
        /// Validación de datos para añadir un sello de tiempo en un documento firmado
        /// </summary>
        /// <param name="verifyPdfDocument">Datos para la verificación</param>
        /// <returns>Un <see cref="ValidationResult"/> con el resultado de la validación</returns>
        public static ValidationResult ValidateVerifyPDFDocument(VerifyPDFDocument verifyPdfDocument)
        {
            // Certificado
            if (verifyPdfDocument.Certificate != null && string.IsNullOrEmpty(verifyPdfDocument.Certificate.P12Certificate))
            {
                return new ValidationResult("The parameter p12Certificate must be informed.",
                        [nameof(Certificate)]);
            }

            return ValidationResult.Success;
        }
    }
}
