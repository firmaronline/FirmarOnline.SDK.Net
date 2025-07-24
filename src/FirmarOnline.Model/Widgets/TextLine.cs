using System.ComponentModel;

namespace FirmarOnline.Model.Widgets
{
    /// <summary>
    /// Define una línea de texto.
    /// </summary>
    public class TextLine
    {
        /// <summary>
        /// Tamaño de fuente.
        /// </summary>
        [DefaultValue(4)]
        public int FontSize { get; set; } = 4;

        /// <summary>
        /// Texto.
        /// </summary>
        public string Text { get; set; }
    }
}