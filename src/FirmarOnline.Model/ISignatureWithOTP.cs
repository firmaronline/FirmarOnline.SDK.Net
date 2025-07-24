namespace FirmarOnline.Model
{
    /// <summary>
    /// Define las propiedades que debe incluir una firma con OTP
    /// </summary>
    public interface ISignatureWithOTP
    {
        /// <summary>
        /// Identificador de petición para relacionar el documento con la
        /// solucitud del PIN OTP
        /// </summary>
        string RequestId { get; set; }
        /// <summary>
        /// PIN OTP
        /// </summary>
        string OTPCode { get; set; }
    }
}