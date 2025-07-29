using System;
using System.Collections.Generic;

namespace FirmarOnline.Model.WebHook
{
    /// <summary>
    /// Define el formato en el que se envían las notificaciones de eventos a través de WebHooks.
    /// </summary>
    public class WebHookInfo
    {
        /// <summary>
        /// Tipo de Evento
        /// </summary>
        public EventCode EventType { get; set; }
        /// <summary>
        /// Momento del evento
        /// </summary>
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Datos extra según el tipo de evento
        /// </summary>
        public Dictionary<string, string> Data { get; set; }
    }
}
