using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FirmarOnline.Model
{
    /// <summary>
    /// Opciones de filtrado para históricos
    /// </summary>
    public class HistoryFilter : DateRangeFilterBase, IPageFilter
    {
        /// <summary>
        /// Número máximo de elementos a devolver
        /// </summary>
        [DefaultValue(50)]
        [Range(1, 100)]
        public int Limit { get; set; } = 50;

        /// <summary>
        /// Desplazamiento, número de elementos a saltarse
        /// </summary>
        public int Offset { get; set; }
    }

    /// <summary>
    /// Opciones de filtrado por frechas
    /// </summary>
    public abstract class DateRangeFilterBase : IDateRangeFilter
    {
        /// <summary>
        /// Fecha/hora de inicio
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime? FromDateTime { get; set; }

        /// <summary>
        /// Fecha/hora de fin
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime? ToDateTime { get; set; }
    }

}
