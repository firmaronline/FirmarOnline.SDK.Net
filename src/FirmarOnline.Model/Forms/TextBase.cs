#if NET6_0_OR_GREATER

namespace FirmarOnline.Model.Forms
{
    /// <summary>
    /// Etiquetas de Texto.
    /// </summary>
    public abstract class TextBase : VisibleItemBase
    {
        /// <summary>
        /// Texto de la etiqueta (para etiquetas e inputs).
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Etiqueta cargada a partir de otro campo.
        /// </summary>
        public string GetValuesOf { get; set; }
    }

    /// <summary>
    /// Texto de cabecera.
    /// </summary>
    public class HeaderText : TextBase
    { }

    /// <summary>
    /// Texto de párrafo.
    /// </summary>
    public class ParagraphText : TextBase
    { }

    /// <summary>
    /// Texto contunuo (span).
    /// </summary>
    public class ContinuousText : TextBase
    { }
}

#endif