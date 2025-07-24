using System.ComponentModel.DataAnnotations;

namespace FirmarOnline.Model
{
    /// <summary>
    /// Datos biométricos de firma en formato plano
    /// </summary>
    public class BiometricSignatureRaw: BiometricSignatureBase
    {
        /// <summary>
        /// Datos biométricos codificados en base64
        /// </summary>
        /// <remarks>
        /// Si los datos están encriptados debe especificarse la clave de cifrado
        /// </remarks>
        [Required]
        public string B64Content { get; set; }

    }
}
