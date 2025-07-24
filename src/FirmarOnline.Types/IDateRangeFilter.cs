using System;

namespace FirmarOnline.Types
{
    /// <summary>
    /// Define un filtro con rango de fechas
    /// </summary>
    public interface IDateRangeFilter
    {
        /// <summary>
        /// Fecha/hora de inicio
        /// </summary>
        DateTime? FromDateTime { get; set; }
        /// <summary>
        /// Fecha/hora de fin
        /// </summary>
        DateTime? ToDateTime { get; set; }
    }
}
