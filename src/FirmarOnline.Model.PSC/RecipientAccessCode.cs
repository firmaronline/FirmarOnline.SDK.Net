using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Código de acceso
    /// </summary>
    [CustomValidation(typeof(RecipientAccessCode), nameof(ValidateResponseFormat),
        ErrorMessage = "The Response does not match the specified Format.")]
    public class RecipientAccessCode
    {
        /// <summary>
        /// Desafío
        /// </summary>
        [MaxLength(255)]
        public string Challenge { get; set; }

        /// <summary>
        /// Respuesta
        /// </summary>
        [MaxLength(50)]
        public string Response { get; set; }

        /// <summary>
        /// Formato
        /// </summary>
        [MaxLength(80)]
        public string Format { get; set; }

        /// <summary>
        /// Valida que, si se especifica un <see cref="Format"/> (expresión regular),
        /// el valor de <see cref="Response"/> cumpla dicho formato.
        /// </summary>
        /// <param name="recipientAccessCode">Código de acceso a validar.</param>
        /// <returns>Un <see cref="ValidationResult"/> con el resultado de la validación.</returns>
        public static ValidationResult ValidateResponseFormat(RecipientAccessCode recipientAccessCode)
        {
            // Sin instancia, sin formato o sin respuesta no hay nada que validar.
            if (recipientAccessCode is null
                || string.IsNullOrEmpty(recipientAccessCode.Format)
                || string.IsNullOrEmpty(recipientAccessCode.Response))
            {
                return ValidationResult.Success;
            }

            try
            {
                if (!Regex.IsMatch(recipientAccessCode.Response, recipientAccessCode.Format))
                {
                    return new ValidationResult("The Response does not match the specified Format.", [nameof(Response)]);
                }
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