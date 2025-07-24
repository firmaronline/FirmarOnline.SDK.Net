using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Clase base para la definición de una firma corporativa
    /// </summary>
    [CustomValidation(typeof(CorporateSignatureBase), nameof(ValidateCorporateSignature),
        ErrorMessage = "The Corporate Signature is not valid.")]
    public abstract class CorporateSignatureBase
    {
        /// <summary>
        /// Tipo de firma corporativa
        /// </summary>
        [DefaultValue(CorporateSignatureType.None)]
        [EnumDataType(typeof(CorporateSignatureType))]
        public CorporateSignatureType Type { get; set; }

        /// <summary>
        /// Identificador único de firma corporativa
        /// </summary>
        public string CorporateSignatureId { get; set; }

        /// <summary>
        /// Validación de firma corporativa.
        /// Comprueba que, si se va a utilizar, se indica el identificador de firma
        /// </summary>
        /// <param name="signature">Firma corporativa</param>
        /// <returns>Un <see cref="ValidationResult"/> indicando si la validación
        /// se ha completado con éxito o no.</returns>
        public static ValidationResult ValidateCorporateSignature(CorporateSignatureBase signature)
        {
            if ((signature?.Type ?? CorporateSignatureType.None) != CorporateSignatureType.None
                && string.IsNullOrEmpty(signature.CorporateSignatureId))
            {
                return new ValidationResult($"The {nameof(CorporateSignatureId)} field is required.");
            }

            return ValidationResult.Success;
        }
    }
}
