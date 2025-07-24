using System.ComponentModel.DataAnnotations;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Código de acceso
    /// </summary>
    public class RecipientAccessCode
    {
        /// <summary>
        /// Desafío
        /// </summary>
        [MaxLength(255)]
        public string Challenge { get; set; }

        /// <summary>
        /// Respuesta
        /// </summary>
        [MaxLength(50)]
        public string Response { get; set; }

        /// <summary>
        /// Formato
        /// </summary>
        [MaxLength(80)]
        public string Format { get; set; }
    }
}