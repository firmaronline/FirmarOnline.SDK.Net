using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Define un anexo
    /// </summary>
    public class RecipientDefinitionAttachment
    {
        /// <summary>
        /// Descripción de anexo
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Description { get; set; }

        /// <summary>
        /// Anexo requerido
        /// </summary>
        [DefaultValue(false)]
        public bool Required { get; set; }
    }
}