using System;
using System.Collections.Generic;

namespace FirmarOnline.Model.Verify
{
    /// <summary>
    /// clase para información de certificados
    /// </summary>
    public class CertificateInfo
    {
        /// <summary>
        /// versión del certificado
        /// </summary>
        public byte Version { get; set; }

        /// <summary>
        /// nombre común del certificado
        /// </summary>
        public string CommonName { get; set; }

        /// <summary>
        /// Certificado válido?
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// número de serie del certificado
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// algoritmo de firma del certificado
        /// </summary>
        public string PublicKeyAlg { get; set; }
        /// <summary>
        /// algoritmo de firma del certificado
        /// </summary>
        public string SigAlg { get; set; }

        /// <summary>
        /// Información del emisor
        /// </summary>
        public List<string> Issuer { get; set; }

        /// <summary>
        /// Inicio validez certificado
        /// </summary>
        public DateTime ValidFrom { get; set; }
        /// <summary>
        /// Fin validez certificado
        /// </summary>
        public DateTime ValidTo { get; set; }

        /// <summary>
        /// Información de la entidad final
        /// </summary>
        public List<string> Subject { get; set; }

        /// <summary>
        /// tamaño de la llave pública
        /// </summary>
        public int PublicKeySize { get; set; }

        /// <summary>
        /// consigue la huella digital del certificado en SHA1
        /// </summary>
        public string ThumbprintSHA1 { get; set; }
        /// <summary>
        /// consigue la huella digital del certificado en SHA256
        /// </summary>
        public string ThumbprintSHA256 { get; set; }

        /// <summary>
        /// es el uso de llave crítico? hay que hacer caso
        /// </summary>
        public bool KeyUsageCritical { get; set; }

        /// <summary>
        /// firma de CRL's
        /// </summary>
        public bool KeyUsageCRLSign { get; set; }
        /// <summary>
        /// cifrado de datos
        /// </summary>
        public bool KeyUsageDataEncipherment { get; set; }
        /// <summary>
        /// descifrado
        /// </summary>
        public bool KeyUsageDecipherOnly { get; set; }
        /// <summary>
        /// cifrado
        /// </summary>
        public bool KeyUsageEncipherOnly { get; set; }
        /// <summary>
        /// firma digital
        /// </summary>
        public bool KeyUsageDigitalSignature { get; set; }
        /// <summary>
        /// no repudio del firmante
        /// </summary>
        public bool KeyUsageNonRepudiation { get; set; }
        /// <summary>
        /// intercambio de llaves
        /// </summary>
        public bool KeyUsageKeyAgreement { get; set; }
        /// <summary>
        /// firma de llaves de certificados
        /// </summary>
        public bool KeyUsageKeyCertSign { get; set; }
        /// <summary>
        /// Cifrado de llaves
        /// </summary>
        public bool KeyUsageKeyEncipherment { get; set; }
    }
}