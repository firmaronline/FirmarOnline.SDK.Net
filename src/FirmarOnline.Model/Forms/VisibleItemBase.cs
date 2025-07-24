#if NET6_0_OR_GREATER

namespace FirmarOnline.Model.Forms
{
    /// <summary>
    /// Elementos visibles del formulario.
    /// </summary>
    public abstract class VisibleItemBase : ItemBase
    {
        /// <summary>
        /// Visible (true por defecto).
        /// </summary>
        public bool Visible { get; set; } = true;
    }
}

#endif