using System;

namespace FirmarOnline.Model.Verify
{
    /// <summary>
    /// Información del sello de tiempo de la firma
    /// </summary>
    public class TSPInfo
    {
        /// <summary>
        /// La fecha/hora del sello de tiempo
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// número de serie de la respuesta de sello de tiempo
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// Algoritmo usado para la firma de la respuesta de la TSA
        /// </summary>
        public string HashAlg { get; set; }

        /// <summary>
        /// semilla de respuesta que retorna la TSA
        /// </summary>
        public string ResponseNonce { get; set; }

        /// <summary>
        /// nombre que retorna la TSA
        /// </summary>
        public string TSAName { get; set; }

        /// <summary>
        /// la TSA nos retorna los parámetros de precisión?
        /// </summary>
        public bool AccuracySet { get; set; }

        /// <summary>
        /// precisión en segundos
        /// </summary>
        public int AccuracySec { get; set; }
        /// <summary>
        /// precisión en milisegundos
        /// </summary>
        public int AccuracyMilli { get; set; }
        /// <summary>
        /// precisión en microsegundos
        /// </summary>
        public int AccuracyMicro { get; set; }

        /// <summary>
        /// información del firmador (la TSA)
        /// </summary>
        public CertificateInfo SignerTSA { get; set; }

    }
}