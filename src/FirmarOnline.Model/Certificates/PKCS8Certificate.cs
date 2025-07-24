using FirmarOnline.Types.Validations;

namespace FirmarOnline.Model.Certificates
{
    /// <summary>
    /// Certificado PKCS #8.
    /// </summary>
    public class PKCS8Certificate : Certificate
    {
        /// <summary>
        /// Parte pública de certificado en base 64.
        /// </summary>
        [Base64]
        public string P8PublicCert { get; set; }

        /// <summary>
        /// Parte privada de certificado en base 64.
        /// </summary>
        [Base64]
        public string P8PrivateKey { get; set; }

        /// <summary>
        /// Bundle CA para cadena de validación en base 64.
        /// </summary>
        [Base64]
        public string P8BundleCA { get; set; }
    }
}