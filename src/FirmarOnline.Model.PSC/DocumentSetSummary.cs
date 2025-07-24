using System.ComponentModel.DataAnnotations;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Resumen de estado de sobre enviado
    /// </summary>
    public class DocumentSetSummary : HistoryItemSummary
    {
        /// <summary>
        /// Nombre del sobre
        /// </summary>
        [MaxLength(255)]
        public string DocumentSetName { get; set; }

        /// <summary>
        /// Estado del sobre
        /// </summary>
        [EnumDataType(typeof(DocumentSetStatusCode))]
        public DocumentSetStatusCode Status { get; set; }

        /// <summary>
        /// Sobre descargado
        /// </summary>
        public bool Downloaded { get; set; }
    }
}