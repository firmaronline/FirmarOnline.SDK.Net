using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Información del sobre
    /// </summary>
    public class DocumentSetInfo
    {
        /// <summary>
        /// Remitente del sobre
        /// </summary>
        [MaxLength(255)]
        public string SenderName { get; set; }

        /// <summary>
        /// Mail del remitente
        /// </summary>
        [EmailAddress]
        [MaxLength(255)]
        public string SenderMail { get; set; }

        /// <summary>
        /// Nombre del sobre
        /// </summary>
        [MaxLength(255)]
        public string DocumentSetName { get; set; }

        /// <summary>
        /// Campo referencia del sobre
        /// </summary>
        [MaxLength(64)]
        public string DocumentSetReference { get; set; }

        /// <summary>
        /// Identificador del sobre
        /// </summary>
        public string DocumentSetId { get; set; }

        /// <summary>
        /// Fecha de creación del sobre
        /// </summary>
        public DateTime? CreationDate { get; set; }

        /// <summary>
        /// Fecha de caducidad del sobre
        /// </summary>
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// Fecha estimada de purgado de documentos
        /// </summary>
        public DateTime? EstimatedPurgationDate { get; set; }

        /// <summary>
        /// Indica si los documentos del sobre han sido purgados
        /// </summary>
        public bool Purgated { get; set; }

        /// <summary>
        ///  Fecha en la que se borran físicamente los archivos
        /// </summary>
        public DateTime? PurgationDate { get; set; }

        /// <summary>
        /// Sobre descargado
        /// </summary>
        public bool Downloaded { get; set; }

        /// <summary>
        /// Tipo de envio (None, email,sms)
        /// </summary>
        public SendMethod SendMethod { get; set; }

        /// <summary>
        /// Número de días tras los que se enviará un recordatorio
        /// </summary>
        [Range(0, 180)]
        public int ReminderDays { get; set; }

        /// <summary>
        /// Dias a los que caduca el sobre
        /// </summary>
        public int ExpireDays { get; set; }

        /// <summary>
        /// Lista de fechas en las que se envía recordatorio
        /// </summary>
        public IEnumerable<DateTime?> ListDatesReminderDays { get; set; }

        /// <summary>
        /// Estado del Sobre
        /// </summary>
        public DocumentSetStatusCode Status { get; set; }

        /// <summary>
        /// Instante actualización estado del sobre
        /// </summary>
        public DateTime? StatusTime { get; set; }

        /// <summary>
        ///  Documentos
        /// </summary>
        public IEnumerable<DocumentInfo> Documents { get; set; }

        /// <summary>
        /// Interesados
        /// </summary>
        public IEnumerable<RecipientInfo> Recipients { get; set; }

        /// <summary>
        /// Token identificador de equipo.
        /// </summary>
        public string TeamId { get; set; }

        /// <summary>
        /// Nombre de equipo.
        /// </summary>
        public string TeamName { get; set; }

        /// <summary>
        /// Token identificador de flujo.
        /// </summary>
        public string FlowTokenId { get; set; }

        /// <summary>
        /// Nombre de flujo.
        /// </summary>
        public string FlowName { get; set; }

        /// <summary>
        /// Habilitar LTV (validación a largo plazo) para firma
        /// </summary>
        public bool Ltv { get; set; }
    }
}