using FirmarOnline.Types.Validations;

namespace FirmarOnline.Model.Certificates
{
    /// <summary>
    /// Certificado PKCS #12
    /// </summary>
    public class PKCS12Certificate : Certificate
    {
        /// <summary>
        /// Certificado en base 64.
        /// </summary>
        [Base64]
        public string P12Certificate { get; set; }
    }
}