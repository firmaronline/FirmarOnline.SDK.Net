namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Enumeración tipo de error
    /// </summary>
    public enum ErrorType
    {
        /// <summary>
        /// No disponible
        /// </summary>
        NA,

        /// <summary>
        /// Error interno del sistema
        /// </summary>
        InternalError,

        /// <summary>
        /// Error en el módulo de envío de emails
        /// </summary>
        SendMail,

        /// <summary>
        /// Error en el módulo de envío de SMS
        /// </summary>
        SendSMS,

        /// <summary>
        /// Error en el módulo de envío de WhatsApp
        /// </summary>
        SendWhatsApp,

        /// <summary>
        /// Error en la validación de los OTP
        /// </summary>
        ValidationOTP,

        /// <summary>
        /// Error en la validación de los OTP por WhatsApp
        /// </summary>
        ValidationWhatsApp,

        /// <summary>
        /// Error modulo interno encriptación
        /// </summary>
        InternalEncryptError,

        /// <summary>
        /// Error en validación de código de acceso
        /// </summary>
        ValidationAccessCode,

        /// <summary>
        /// Error en validación de MRZ
        /// </summary>
        ValidationMRZ,

        /// <summary>
        /// Archivo PDF no válido
        /// </summary>
        InvalidPDFFile,

        /// <summary>
        /// Error enviado por un dispositivo
        /// </summary>
        DeviceError
    }
}
