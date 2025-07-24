using System.ComponentModel.DataAnnotations;

namespace FirmarOnline.Model
{
    /// <summary>
    /// Datos biométricos de firma
    /// </summary>
    public abstract class BiometricSignatureBase
    {
        /// <summary>
        /// Imagen de la firma en formato jpg y codificada en base64
        /// </summary>
        [Required]
        public string B64JPGImage { get; set; }
        /// <summary>
        /// Clave de cifrado
        /// </summary>
        public string EncryptKey { get; set; }
    }
}
