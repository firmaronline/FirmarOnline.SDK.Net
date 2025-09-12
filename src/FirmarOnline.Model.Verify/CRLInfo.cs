namespace FirmarOnline.Model.Verify
{
    /// <summary>
    /// Información de revocación de tipo OCSP
    /// </summary>
    public class CrlInfo
    {
        /// <summary>
        /// información de los certificados contenida
        /// </summary>
        public CertificateRevocationInfo[] RevocationInfo { get; set; }
    }
}