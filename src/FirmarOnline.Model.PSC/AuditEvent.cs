using System;
using System.Collections.Generic;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Registro de un evento producido en un sobre
    /// </summary>
    public class AuditEvent
    {
        /// <summary>
        /// Fecha/hora del evento
        /// </summary>
        public DateTime EventDateTime { get; set; }
        /// <summary>
        /// Tipo de evento
        /// </summary>
        public AuditEventType EventType { get; set; }
        /// <summary>
        /// Destinatario asociado al evento
        /// </summary>
        public string RecipientId { get; set; }
        /// <summary>
        /// Documento sobre el que se produce el evento
        /// </summary>
        public string DocumentId { get; set; }       
        /// <summary>
        /// Información adicional
        /// </summary>
        public Dictionary<string, string> Data { get; set; }
    }
}
