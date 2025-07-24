using System.ComponentModel.DataAnnotations;
#if NET6_0_OR_GREATER
using System.Text.Json.Serialization;
#else
using Newtonsoft.Json;
#endif

namespace FirmarOnline.Model.Widgets
{
    /// <summary>
    /// Caja de firma con posicionamiento relativo a un texto determinado.
    /// </summary>
    public class FloatWidget : Widget
    {
        /// <summary>
        /// Devuelve el tipo de posicionamiento de la caja de firma.
        /// </summary>
        [JsonIgnore]
        public override PositionType Type => PositionType.Float;

        /// <summary>
        /// Ancho.
        /// </summary>
        [Range(50, int.MaxValue, ErrorMessage = "El ancho de la caja debe ser mayor de {1}.")]
        public int Width { get; set; }

        /// <summary>
        /// Alto.
        /// </summary>
        [Range(50, int.MaxValue, ErrorMessage = "La altura de la caja debe ser mayor de {1}.")]
        public int Height { get; set; }

        /// <summary>
        /// Texto a buscar.
        /// </summary>
        [MaxLength(1024)]
        public string Text { get; set; }

        /// <summary>
        /// Desplazamiento horizontal a partir del texto.
        /// </summary>
        public int GapX { get; set; }

        /// <summary>
        /// Desplazamiento vertical a partir del texto.
        /// </summary>
        public int GapY { get; set; }
    }
}