using FirmarOnline.Model.Widgets;
using System.ComponentModel.DataAnnotations;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Definición de la acción a realizar para un documento
    /// </summary>
    public class RecipientAction
    {
        /// <summary>
        /// Identificador del documento
        /// </summary>
        [MaxLength(50)]
        public string DocumentId { get; set; }

        /// <summary>
        /// Definición de la caja de firma
        /// </summary>
        public Widget Widget { get; set; }
    }
}