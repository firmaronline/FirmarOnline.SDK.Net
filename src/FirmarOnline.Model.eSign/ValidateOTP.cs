using System.ComponentModel.DataAnnotations;

namespace FirmarOnline.Model.eSign
{
    /// <summary>
    /// Definición de datos para validar un código OTP
    /// </summary>    
    public class ValidateOTP
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
        /// OTP de entrada a validar
        /// </summary>
        [Required]
        public string Otp { get; set; }        
    }
}
