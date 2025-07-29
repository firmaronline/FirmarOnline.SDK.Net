namespace FirmarOnline.Model
{
    /// <summary>
    /// Opciones de paginación
    /// </summary>
    public interface IPageFilter
    {
        /// <summary>
        /// Número máximo de elementos a devolver
        /// </summary>
        int Limit { get; set; }
        /// <summary>
        /// Desplazamiento, número de elementos a saltarse
        /// </summary>
        int Offset { get; set; }
    }
}
