namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Enumeración tipo de error.
    /// </summary>
    public enum ErrorType
    {
        /// <summary>
        /// No disponible
        /// </summary>
        NA = 0,

        /// <summary>
        /// Error interno del sistema
        /// </summary>
        InternalError = 1,

        /// <summary>
        /// Error en el módulo de envío de emails
        /// </summary>
        SendMail = 2,

        /// <summary>
        /// Error en el módulo de envío de SMS
        /// </summary>
        SendSMS = 3,

        /// <summary>
        /// Error en el módulo de envío de WhatsApp
        /// </summary>
        SendWhatsApp = 4,

        /// <summary>
        /// Error en la validación de los OTP
        /// </summary>
        ValidationOTP = 5,

        /// <summary>
        /// Error en la validación de los OTP por WhatsApp
        /// </summary>
        ValidationWhatsApp = 6,

        /// <summary>
        /// Error modulo interno encriptación
        /// </summary>
        InternalEncryptError = 7,

        /// <summary>
        /// Error en validación de código de acceso
        /// </summary>
        ValidationAccessCode = 8,

        /// <summary>
        /// Error en validación de MRZ
        /// </summary>
        ValidationMRZ = 9,

        /// <summary>
        /// Archivo PDF no válido
        /// </summary>
        InvalidPDFFile = 10,

        /// <summary>
        /// Error enviado por un dispositivo
        /// </summary>
        DeviceError = 11,

        /// <summary>
        /// Error en la validación de los OTP por Email.
        /// </summary>
        ValidationOTPEmail = 12,

        /// <summary>
        /// Error en la validación de Liveness.
        /// </summary>
        ValidationLiveness = 13
    }
}