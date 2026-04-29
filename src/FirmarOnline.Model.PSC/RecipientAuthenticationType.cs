using System.ComponentModel.DataAnnotations;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Tipo de autenticación a realizar por el destinatario de un documento
    /// </summary>
    public enum RecipientAuthenticationType
    {
        /// <summary>
        /// Sin autenticación
        /// </summary>
        [Display(Name = "Sin Autenticación")]
        None = 0,

        /// <summary>
        /// Autenticación básica (pantalla previa)
        /// </summary>
        [Display(Name = "Básica")]
        Basic = 10,

        /// <summary>
        /// Autenticación mediante código de acceso
        /// </summary>
        [Display(Name = "Código Acceso")]
        AccessCode = 20,

        /// <summary>
        /// Autenticación mediante Mrz y Ocr
        /// </summary>
        [Display(Name = "MrzOcr")]
        MrzOcr = 30,

        /// <summary>
        /// Autenticación mediante Mrz
        /// </summary>
        [Display(Name = "Mrz")]
        Mrz = 31,

        /// <summary>
        /// Autenticación mediante OTP
        /// </summary>
        [Display(Name = "OTP")]
        Otp = 40,

        /// <summary>
        /// Autenticación mediante OTP por Whatsapp
        /// </summary>
        [Display(Name = "OTP WhatsApp")]
        OtpWhatsApp = 41,

        /// <summary>
        /// Autenticación multifactor
        /// </summary>
        [Display(Name = "MFA")]
        Mfa = 100
    }

    /// <summary>
    /// Métodos de extensión para <see cref="RecipientAuthenticationType"/>
    /// </summary>
    public static class RecipientAuthenticationTypeExtensions
    {
        /// <summary>
        /// Indica si el <see cref="RecipientAuthenticationType"/> requiere verificación del teléfono.
        /// </summary>
        /// <param name="authenticationType"><see cref="RecipientAuthenticationType"/> a comprobar.</param>
        /// <returns>True si utiliza verificación del teléfono, en otro caso devuelve False.</returns>
        public static bool RequiresPhoneVerification(this RecipientAuthenticationType authenticationType)
        {
            return authenticationType == RecipientAuthenticationType.Otp || authenticationType == RecipientAuthenticationType.OtpWhatsApp;
        }
    }
}