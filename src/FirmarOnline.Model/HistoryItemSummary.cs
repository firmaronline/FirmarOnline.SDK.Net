using System;
using System.ComponentModel.DataAnnotations;

namespace FirmarOnline.Model
{
    /// <summary>
    /// Datos básicos de un elemento de un histórico
    /// </summary>
    public abstract class HistoryItemSummary
    {
        /// <summary>
        /// Identificador único
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Fecha/hora de envío
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime ItemDateTime { get; set; }
    }
}
