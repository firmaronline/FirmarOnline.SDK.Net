using FirmarOnline.Types;
using FirmarOnline.Types.Validations;
using System.ComponentModel.DataAnnotations;

namespace FirmarOnline.Model.eSign
{
    /// <summary>
    /// Definición de datos para añadir un sello de tiempo en un documento firmado
    /// </summary>
    [CustomValidation(typeof(Timestamp), nameof(ValidateTimestamp),
        ErrorMessage = "The timestamp definition is not valid.")]
    public class Timestamp
    {
        /// <summary>
        /// Contenido del documento en base64
        /// </summary>
        [Required]
        [Base64PDF]
        public string B64PDFContent { get; set; }
        /// <summary>
        /// Permite indicar un proveedor externo para el sellado de tiempo
        /// </summary>
        public ExternalProvider TimestampProvider { get; set; }

        /// <summary>
        /// Validación de datos para añadir un sello de tiempo en un documento firmado
        /// </summary>
        /// <param name="timestamp">Datos para el resellado</param>
        /// <returns>Un <see cref="ValidationResult"/> con el resultado de la validación</returns>
        public static ValidationResult ValidateTimestamp(Timestamp timestamp)
        {
            // Tsp
            if (timestamp.TimestampProvider != null && string.IsNullOrEmpty(timestamp.TimestampProvider.Url))
            {
                return new ValidationResult("The parameter url must be informed.",
                        new string[] { nameof(TimestampProvider) });
            }

            return ValidationResult.Success;
        }
    }
}
