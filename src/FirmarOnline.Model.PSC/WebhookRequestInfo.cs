using System;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Modelo de solicitud de Webhook.
    /// </summary>
    public class WebhookRequestInfo
    {
        /// <summary>
        /// Fecha y hora de solicitud.
        /// </summary>
        public DateTimeOffset Timestamp { get; set; }

        /// <summary>
        /// Éxito o fallo de la solicitud.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Código de tipo de evento.
        /// </summary>
        public int EventType { get; set; }

        /// <summary>
        /// Mensaje.
        /// </summary>
        public string Message { get; set; } = string.Empty;
    }
}