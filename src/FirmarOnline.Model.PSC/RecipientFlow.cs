using System.Collections.Generic;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Define un destinatario de un sobre de firma remota
    /// </summary>
    public class RecipientFlow : RecipientBase
    {
        /// <summary>
        /// Identificador del destinatario
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Posición del destinatario en el flujo de firma
        /// </summary>
        public int? Order { get; set; }

        /// <summary>
        /// Definición de las cajas de firma en cada uno de los documentos
        /// </summary>
        public IEnumerable<RecipientAction> Widgets { get; set; }
    }
}