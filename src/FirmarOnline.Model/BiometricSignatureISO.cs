using System.ComponentModel.DataAnnotations;

namespace FirmarOnline.Model
{
    /// <summary>
    /// Datos biómetricos de firma en formato ISO (ISO/IEC 19794-7)
    /// </summary>
    public class BiometricSignatureISO: BiometricSignatureBase
    {
        /// <summary>
        /// Cabecera
        /// </summary>
        [Required]
        public string Header { get; set; }
        /// <summary>
        /// Cuerpo
        /// </summary>
        [Required]
        public string EncryptedBody { get; set; }
    }
}
