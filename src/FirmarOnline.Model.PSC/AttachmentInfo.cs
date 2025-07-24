using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Información de anexo
    /// </summary>
    public class AttachmentInfo
    {
        /// <summary>
        /// Descripción de anexo
        /// </summary>
        [MaxLength(255)]
        public string Description { get; set; }

        /// <summary>
        /// Anexo requerido
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        /// Lista de ficheros
        /// </summary>
        public IEnumerable<AttachmentFileInfo> Files { get; set; }
    }
}