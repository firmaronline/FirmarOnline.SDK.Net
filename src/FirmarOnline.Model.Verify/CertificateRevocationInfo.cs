using System;

namespace FirmarOnline.Model.Verify
{
    /// <summary>
    /// información de los certificados contenida
    /// </summary>
    public class CertificateRevocationInfo
    {
        /// <summary>
        /// nombre común del certificado
        /// </summary>
        public string CommonName { get; set; }
        /// <summary>
        /// consigue la huella digital del certificado en SHA1
        /// </summary>
        public string ThumbprintSHA1 { get; set; }
        /// <summary>
        /// consigue la huella digital del certificado en SHA256
        /// </summary>
        public string ThumbprintSHA256 { get; set; }

        /// <summary>
        /// estado de revocación del certificado
        /// </summary>
        public RevocationStatus RevocationStatus { get; set; }

        /// <summary>
        /// timestamp de cuándo ha sido revocado el certificado
        /// </summary>
        public DateTime RevocationTime { get; set; }

        /// <summary>
        /// timestamp de cuándo se ha producido ésta respuesta de revocación
        /// </summary>
        public DateTime ThisUpdate { get; set; }
        /// <summary>
        /// timestamp de cuándo debiera refrescarse ésta respuesta de revocación
        /// </summary>
        public DateTime NextUpdate { get; set; }
    }
}