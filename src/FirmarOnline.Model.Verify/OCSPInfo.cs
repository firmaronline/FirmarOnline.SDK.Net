using System;
using System.Collections.Generic;

namespace FirmarOnline.Model.Verify
{
    /// <summary>
    /// Información de revocación de tipo OCSP
    /// </summary>
    public class OcspInfo
    {
        /// <summary>
        /// nombre del OCSP que nos ha llegado de respuesta
        /// </summary>
        public List<string> ResponderName { get; set; }

        /// <summary>
        /// Timestamp al que se ha producido la respuesta OCSP
        /// </summary>
        public DateTime ProducedAt { get; set; }

        /// <summary>
        /// validación de respuesta OCSP
        /// </summary>
        public bool ResponseValid { get; set; }

        /// <summary>
        /// algoritmo de firma de la respuesta OCSP
        /// </summary>
        public string SigAlg { get; set; }

        /// <summary>
        /// información del firmador (la TSA)
        /// </summary>
        public CertificateInfo SignerOCSP { get; set; }

        /// <summary>
        /// información de los certificados contenida
        /// </summary>
        public CertificateRevocationInfo[] RevocationInfo { get; set; }

    }
}