using System.ComponentModel;

namespace FirmarOnline.Model
{
    /// <summary>
    /// Opciones de firma
    /// </summary>
    public class SignatureOptions
    {
        /// <summary>
        /// Inicializa una nueva instancia de <see cref="SignatureOptions"/>
        /// </summary>
        public SignatureOptions(){
            IncludeTimestamp = false;
            IncludeOCSP = false;
            ValidateCertificate = true;
            PdfCertifiedSignature = false;
        }

        /// <summary>
        /// Incluir sello de tiempo
        /// </summary>
        [DefaultValue(false)]
        public bool IncludeTimestamp { get; set; }

        /// <summary>
        /// Incluir validación de certificado (OCSP)
        /// </summary>
        [DefaultValue(false)]
        public bool IncludeOCSP { get; set; }

        /// <summary>
        /// Validar certificado expirado
        /// </summary>
        [DefaultValue(true)]
        public bool ValidateCertificate { get; set; }

        /// <summary>
        /// Usar firma de tipo certificación MDP (no admite firmas posteriores)
        /// </summary>
        [DefaultValue(false)]
        public bool PdfCertifiedSignature { get; set; }
    }
}
