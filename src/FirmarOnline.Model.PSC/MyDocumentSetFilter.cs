using FirmarOnline.Api.Enums;
using System.ComponentModel.DataAnnotations;

namespace FirmarOnline.Model.PSC /*OJO*/ // Debería ser privado?
{
    /// <summary>
    /// Opciones de filtrado para listado de mis documentos
    /// </summary>
    public class MyDocumentSetFilter : HistoryFilter
    {
        /// <summary>
        /// Número máximo de elementos a devolver
        /// </summary>
        [Range(1, 1000)]
        public new int Limit { get; set; } = 50;

        /// <summary>
        /// Códigos de estado de sobre a mostrar
        /// </summary>
        public DocumentSetStatusCode[] DocumentSetStatus { get; set; }

        /// <summary>
        /// Referencia externa de sobre para cliente
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// Identificador único de sobre
        /// </summary>
        public string DocumentSetId { get; set; }

        /// <summary>
        /// Códigos de estado de recipient a mostrar.
        /// </summary>
        public RecipientStatus[] RecipientStatus { get; set; }

        /// <summary>
        /// Tipo de acción a realizar por el destinatario
        /// </summary>
        public RecipientActionType[] RecipientActionType { get; set; }
    }
}