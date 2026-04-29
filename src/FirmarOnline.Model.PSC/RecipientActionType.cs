using System.ComponentModel.DataAnnotations;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Tipo de acción a realizar por el destinatario de un documento
    /// </summary>
    public enum RecipientActionType
    {
        /// <summary>
        /// Notificación fehaciente
        /// </summary>
        [Display(Name = "Notificación Fehaciente")]
        CertifiedNotification = 10,

        /// <summary>
        /// Aceptación
        /// </summary>
        [Display(Name = "Aceptación")]
        Acceptance = 20,

        /// <summary>
        /// Aceptación con firma de certificado
        /// </summary>
        [Display(Name = "Aceptación con Firma")]
        AcceptanceSignature = 21,

        /// <summary>
        /// Firma biométrica
        /// </summary>
        [Display(Name = "Firma Biométrica")]
        BioSignature = 30,

        /// <summary>
        /// Firma OTP
        /// </summary>
        [Display(Name = "Firma con OTP")]
        OTPSignature = 40,

        /// <summary>
        /// Firma OTP WhatsApp
        /// </summary>
        [Display(Name = "Firma con OTP WhatsApp")]
        OTPWhatsAppSignature = 41,

        /// <summary>
        /// Firma biométrica y OTP
        /// </summary>
        [Display(Name = "Firma Biométrica con OTP")]
        BioOTPSignature = 50,

        /// <summary>
        /// Firma biométrica y OTP WhatsApp
        /// </summary>
        [Display(Name = "Firma Biométrica con OTP WhatsApp")]
        BioOTPWhatsAppSignature = 51,

        /// <summary>
        /// Firma con certificado de cliente
        /// </summary>
        [Display(Name = "Firma con Certificado de cliente")]
        CryptoAPISignature = 60,

        /// <summary>
        /// Firma con certificado PKCS #8
        /// </summary>
        [Display(Name = "Firma con Certificado PKCS #8")]
        PKCS8Signature = 62
    }

    /// <summary>
    /// Métodos de extensión para <see cref="RecipientActionType"/>
    /// </summary>
    public static class RecipientActionTypeExtensions
    {
        /// <summary>
        /// Indica si el <see cref="RecipientActionType"/> requiere verificación de teléfono.
        /// </summary>
        /// <param name="actionType"><see cref="RecipientActionType"/> a comprobar.</param>
        /// <returns>True si utiliza verificación del teléfono, en otro caso devuelve False.</returns>
        public static bool RequiresPhoneVerification(this RecipientActionType actionType)
        {
            return actionType == RecipientActionType.OTPSignature ||
                actionType == RecipientActionType.OTPWhatsAppSignature ||
                actionType == RecipientActionType.BioOTPSignature ||
                actionType == RecipientActionType.BioOTPWhatsAppSignature;
        }
    }
}