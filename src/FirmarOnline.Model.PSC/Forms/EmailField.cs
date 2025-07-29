#if NET6_0_OR_GREATER

using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace FirmarOnline.Model.PSC.Forms
{
    /// <summary>
    /// Campo de tipo Email.
    /// </summary>
    [CustomValidation(typeof(EmailField), nameof(ValidateEmail),
        ErrorMessage = "The Email Address is not valid.")]
    public class EmailField : StringField
    {
        /// <summary>
        /// Validación de contenido de Email.
        /// </summary>
        /// <param name="email">Email a validar.</param>
        /// <returns></returns>
        public static ValidationResult ValidateEmail(EmailField email)
        {
            if (!string.IsNullOrWhiteSpace(email.Value.ToString()))
            {
                string emailPattern = @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$";

                if (!Regex.IsMatch(email.Value.ToString(), emailPattern))
                {
                    return new ValidationResult("Email value is not valid");
                }
            }
            return ValidationResult.Success;
        }
    }
}
#endif