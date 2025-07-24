using System;
using System.ComponentModel.DataAnnotations;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Información de fichero de anexo
    /// </summary>
    public class AttachmentFileInfo
    {
        /// <summary>
        /// Token de anexo
        /// </summary>
        [MaxLength(80)]
        public string TokenId { get; set; }

        /// <summary>
        /// Fecha de creación de registro
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Nombre de fichero anexo
        /// </summary>
        [MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Tamaño en bytes del fichero anexo
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Bandera para marcar el anexo como purgado
        /// </summary>
        public bool Purgated { get; set; }

        /// <summary>
        /// Momento del purgado de anexo
        /// </summary>
        public DateTime? PurgeDate { get; set; }
    }
}