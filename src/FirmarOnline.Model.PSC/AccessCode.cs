using System.ComponentModel.DataAnnotations;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Código de acceso
    /// </summary>
    public class AccessCode
    {
        /// <summary>
        /// Desafío
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Challenge { get; set; }

        /// <summary>
        /// Formato
        /// </summary>
        [MaxLength(80)]
        public string Format { get; set; }
    }
}