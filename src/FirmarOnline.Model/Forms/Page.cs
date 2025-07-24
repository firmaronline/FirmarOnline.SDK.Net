#if NET6_0_OR_GREATER

using System.Collections.Generic;

namespace FirmarOnline.Model.Forms
{
    /// <summary>
    /// Página del formulario.
    /// </summary>
    public class Page
    {
        /// <summary>
        /// Lista de Elementos por página.
        /// </summary>
        public List<ItemBase> Items { get; set; }
    }
}

#endif