using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Código de acceso
    /// </summary>
    [CustomValidation(typeof(AccessCode), nameof(ValidateFormat),
        ErrorMessage = "The Format is not a valid regular expression.")]
    public class AccessCode
    {
        /// <summary>
        /// Desafío
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Challenge { get; set; }

        /// <summary>
        /// Formato
        /// </summary>
        [MaxLength(80)]
        public string Format { get; set; }

        /// <summary>
        /// Valida que, si se especifica un <see cref="Format"/>, este sea una expresión regular válida.
        /// </summary>
        /// <param name="accessCode">Código de acceso a validar.</param>
        /// <returns>Un <see cref="ValidationResult"/> con el resultado de la validación.</returns>
        public static ValidationResult ValidateFormat(AccessCode accessCode)
        {
            // Sin instancia o sin formato no hay nada que validar.
            if (accessCode is null || string.IsNullOrEmpty(accessCode.Format))
            {
                return ValidationResult.Success;
            }

            try
            {
                _ = Regex.Match(string.Empty, accessCode.Format);
            }
            catch (System.ArgumentException)
            {
                // El Format no es una expresión regular válida.
                return new ValidationResult("The Format is not a valid regular expression.", [nameof(Format)]);
            }

            return ValidationResult.Success;
        }
    }
}