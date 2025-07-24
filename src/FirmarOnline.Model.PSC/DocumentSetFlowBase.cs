using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Clase base para la definición de flujos
    /// </summary>
    public abstract class DocumentSetFlowBase : DocumentSetBase
    {
        /// <summary>
        /// Identificador de token de flujo para cliente
        /// </summary>
        [Required]
        [MaxLength(80)]
        public string FlowTokenId { get; set; }

        /// <summary>
        /// Documentos
        /// </summary>
        public IEnumerable<Document> Documents { get; set; }

        /// <summary>
        /// Destinatarios
        /// </summary>
        public IEnumerable<RecipientFlow> Recipients { get; set; }
    }
}