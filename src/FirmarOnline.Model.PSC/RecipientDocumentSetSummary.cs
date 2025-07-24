using FirmarOnline.Api.Enums;
using System.ComponentModel.DataAnnotations;

namespace FirmarOnline.Model.PSC /*OJO*/ // Debería ser privado?
{
    /// <summary>
    /// Resumen de estado de mis documentos
    /// </summary>
    public class RecipientDocumentSetSummary : HistoryItemSummary
    {
        /// <summary>
        /// Nombre del sobre
        /// </summary>
        public string DocumentSetName { get; set; }

        /// <summary>
        /// Descripción del sobre.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Estado del sobre
        /// </summary>
        [EnumDataType(typeof(DocumentSetStatusCode))]
        public DocumentSetStatusCode DocumentSetStatus { get; set; }

        /// <summary>
        /// Remitente del sobre
        /// </summary>
        public string SenderName { get; set; }

        /// <summary>
        /// Estado de recipient
        /// </summary>
        public RecipientStatus RecipientActionStatus { get; set; }

        /// <summary>
        /// Url de acceso al visor
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// Tipo de sobre (PDF/JSON/Multiple).
        /// </summary>
        public DocumentSetType DocumentSetType { get; set; }

        /// <summary>
        /// Identificador de destinatario establecido por el emisor.
        /// </summary>
        public string RecipientId { get; set; }
    }
}