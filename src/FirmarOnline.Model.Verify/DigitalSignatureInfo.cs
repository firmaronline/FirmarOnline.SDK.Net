using System;

namespace FirmarOnline.Model.Verify
{
    /// <summary>
    /// Información de verificación de una firma digital
    /// </summary>
    public class DigitalSignatureInfo
    {
        /// <summary>
        /// firma válida o no
        /// </summary>
        public bool Valid { get; set; }

        /// <summary>
        /// es la última firma? (para Resumen)
        /// </summary>
        public bool IsLastSignature { get; set; }

        /// <summary>
        /// firmador válido? (para Resumen)
        /// </summary>
        public bool SignerValid { get; set; }

        /// <summary>
        /// tiene sello de tiempo seguro?
        /// </summary>
        public bool HasTimestamp { get; set; }
        /// <summary>
        /// tiene información de revocación?
        /// </summary>
        public bool HasRevocationInfo { get; set; }

        /// <summary>
        /// forma de la firma
        /// </summary>
        public DigitalSignatureProfile SignatureForm { get; set; }
        /// <summary>
        /// forma de la firma PAdES
        /// </summary>
        public PadesSignatureForm SignatureFormPAdES { get; set; }


        /// <summary>
        /// índice real de la firma
        /// </summary>
        public int EffectiveIndex { get; set; }

        /// <summary>
        /// nombre de la firma
        /// </summary>
        public string SignatureName { get; set; }

        /// <summary>
        /// timestamp local al que se ha producido la firma (en UTC)
        /// </summary>
        public DateTime LocalSigningTime { get; set; }

        /// <summary>
        /// si está disponible, ID de producto que produce la firma
        /// </summary>
        public string ProductID { get; set; }


        /// <summary>
        /// Autor de la firma
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// Razón de la firma
        /// </summary>
        public string Reason { get; set; }
        /// <summary>
        /// Localización del firmador
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// Información de contacto del firmador
        /// </summary>
        public string ContactInfo { get; set; }

        /// <summary>
        /// información del firmador
        /// </summary>
        public CertificateInfo Signer { get; set; }

        /// <summary>
        /// Certificados además del firmador que contiene la firma
        /// </summary>
        public CertificateInfo[] IncludedCertificates { get; set; }

        /// <summary>
        /// información del sello de hora
        /// </summary>
        public TspInfo[] TSPInfo { get; set; }

        /// <summary>
        /// información de respuesta de revocación OCSP
        /// </summary>
        public OcspInfo[] OCSPInfo { get; set; }

        /// <summary>
        /// información de respuesta de revocación OCSP
        /// </summary>
        public CrlInfo[] CRLInfo { get; set; }

        /// <summary>
        /// Otra información de la firma digital, propiedades del widget, etc...
        /// </summary>
        public OtherSignatureInfo OtherSignatureInfo { get; set; }

    }
}