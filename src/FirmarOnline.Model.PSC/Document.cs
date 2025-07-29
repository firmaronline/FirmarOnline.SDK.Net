using System.ComponentModel.DataAnnotations;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Define un documento de firmar.online Store
    /// </summary>
    public class Document : DocumentContent
    {
        /// <summary>
        /// Descripción
        /// </summary>
        [MaxLength(255)]
        public string Description { get; set; }

        /// <summary>
        /// Identificador del documento
        /// </summary>
        [MaxLength(50)]
        public string Id { get; set; }
    }
}