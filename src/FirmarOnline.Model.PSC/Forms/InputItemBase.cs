#if NET6_0_OR_GREATER

namespace FirmarOnline.Model.PSC.Forms
{
    /// <summary>
    /// Elementos de introducción de datos del formulario.
    /// </summary>
    public abstract class InputItemBase : VisibleItemBase
    {
        /// <summary>
        /// Texto de la etiqueta.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Indica si es editable (true por defecto).
        /// </summary>
        public bool Editable { get; set; } = true;

        /// <summary>
        /// Requerido (false por defecto).
        /// </summary>
        public bool Required { get; set; } = false;
    }
}
#endif