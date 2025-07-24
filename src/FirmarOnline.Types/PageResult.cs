using System.Collections.Generic;

namespace FirmarOnline.Types
{
    /// <summary>
    /// Define los elementos correspondientes a una página
    /// concreta, resultado de paginar una colección
    /// </summary>
    /// <typeparam name="T">Tipo de elementos de la colección</typeparam>
    public class PageResult<T>
    {
        /// <summary>
        /// Inicializa una nueva instancia de <see cref="PageResult{T}"/> con una lista de elementos.
        /// Se entiende que es la lista completa de elementos que van en una única página de resultado.
        /// </summary>
        /// <param name="items">Elementos de la página</param>
        public PageResult(ICollection<T> items) :
            this(items, items.Count)
        { }

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="PageResult{T}"/>
        /// </summary>
        /// <param name="items">Elementos de la página</param>
        /// <param name="totalCount">Total de elementos de la colección</param>
        /// <param name="firstElementInPage">Índice del primer elemento de la página</param>
        /// <param name="pageSize">Tamaño de página</param>
        public PageResult(ICollection<T> items, int totalCount,
                    int firstElementInPage = -1, int pageSize = -1)
        {
            Items = items;
            TotalCount = totalCount;
            FirstElementInPage = firstElementInPage;
            PageSize = pageSize;
        }

        /// <summary>
        /// Lista de elementos de la página
        /// </summary>
        public IEnumerable<T> Items { get; }
        /// <summary>
        /// Número total de elementos de la colección
        /// </summary>
        public int TotalCount { get; }

        /// <summary>
        /// Devuelve el índice del primer elemento de la página si se conoce,
        /// si no se conoce devuelve -1
        /// </summary>
        public int FirstElementInPage { get; }
        /// <summary>
        /// Devuelve el tamaño de página si se ha establecido,
        /// si no devuelve -1
        /// </summary>
        public int PageSize { get; }

    }
}
