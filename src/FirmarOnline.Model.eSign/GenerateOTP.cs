using FirmarOnline.Model.Validations;
using System.ComponentModel.DataAnnotations;

namespace FirmarOnline.Model.eSign
{
    /// <summary>
    /// Datos para la generación y envío de un nuevo OTP
    /// </summary>    
    [CustomValidation(typeof(GenerateOtp), nameof(ValidateGenerateOTP), ErrorMessage = "The generate OTP definition is not valid.")]
    public class GenerateOtp
    {
        /// <summary>
        /// Código hash SHA256
        /// </summary>
        public string Hash { get; set; }

        /// <summary>
        /// Código de sesión
        /// </summary>
        [Required]
        public string SessionCode { get; set; }

        /// <summary>
        /// Código de usuario
        /// </summary>
        [Required]
        public string UserId { get; set; }

        /// <summary>
        /// Número de teléfono al que se le enviará el OTP.
        /// </summary>
        [Required]
        [SupportedPhone]
        [MaxLength(50)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Texto del mensaje SMS, debe contener el tag ##OTP## que representa el código OTP generado.
        /// </summary>
        [Required]
        [MaxLength(130)]
        public string Text { get; set; }

        /// <summary>
        /// Emisor del SMS
        /// </summary>
        [MaxLength(11)]
        public string From { get; set; } = "firmaonline";

        /// <summary>
        /// Validaciones del modelo de datos
        /// </summary>
        /// <param name="generateOTP">Datos para la generación y envío de un nuevo OTP</param>
        /// <returns></returns>
        public static ValidationResult ValidateGenerateOTP(GenerateOtp generateOTP)
        {
            var isValidPhoneNumber = StringValidator<SupportedPhoneValidationType>.IsValid(generateOTP.PhoneNumber, true) 
                                && !generateOTP.PhoneNumber.Trim().Contains(" "); 

            if (!isValidPhoneNumber)
            {
                return new ValidationResult("Invalid phone number validation.", [nameof(generateOTP.PhoneNumber)]);
            }

            if (!generateOTP.Text.Contains("##OTP##"))
            {
                return new ValidationResult("The SMS message does not contain the tag ##OTP##.", [nameof(generateOTP.Text)]);
            }

            return ValidationResult.Success;
        }
    }
}
