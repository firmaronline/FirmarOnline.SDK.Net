using System;
using System.Collections.Generic;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Clase información método obtención detalle error
    /// </summary>
    public class DocumentSetErrorInfo : DocumentSetErrorBase
    {   
        /// <summary>
        /// Identificador del DocumentSet
        /// </summary>
        public string DocumentSetId { get; set; }

        /// <summary>
        /// Fecha del evento
        /// </summary>
        public DateTime? EventDateTime { get; set; }

        /// <summary>
        /// Información adicional
        /// </summary>
        public Dictionary<string, string> Data { get; set; }
    }
}