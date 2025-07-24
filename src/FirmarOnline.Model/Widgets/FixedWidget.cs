using System.ComponentModel.DataAnnotations;
#if NET6_0_OR_GREATER
using System.Text.Json.Serialization;
#else
using Newtonsoft.Json;
#endif

namespace FirmarOnline.Model.Widgets
{
    /// <summary>
    /// Caja de firma con posicionamiento fijo.
    /// </summary>
    public class FixedWidget : Widget
    {
        /// <summary>
        /// Devuelve el tipo de posicionamiento de la caja de firma.
        /// </summary>
        [JsonIgnore]
        public override PositionType Type => PositionType.Fixed;

        /// <summary>
        /// Ancho.
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "El ancho de la caja no puede ser un número negativo.")]
        public int Width { get; set; }

        /// <summary>
        /// Alto.
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "La altura de la caja no puede ser un número negativo.")]
        public int Height { get; set; }

        /// <summary>
        /// Número de página. Índice base 1. Si se establece un número de página mayor que el total
        /// de páginas del documento, se utilizará la última página del documento.
        /// </summary>
        [CustomValidation(typeof(FixedWidget), nameof(ValidatePage))]
        public int Page { get; set; }

        /// <summary>
        /// Desplazamiento horizontal desde la esquina inferior izquierda.
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "El desplazamiento horizontal no puede ser un número negativo.")]
        public int X { get; set; }

        /// <summary>
        /// Desplazamiento vertical desde la esquina inferior izquierda.
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "El desplazamiento vertical no puede ser un número negativo.")]
        public int Y { get; set; }

        /// <summary>
        /// Valida que el número de página sea 0 o mayor.
        /// </summary>
        /// <param name="page">Número de página.</param>
        /// <returns>Success si el número de página es 0 o mayor. Error de validación si el número es negativo.</returns>
        public static ValidationResult ValidatePage(int page)
        {
            if (page < 0)
            {
                return new ValidationResult("El número de página debe ser mayor o igual a 0.");
            }
            return ValidationResult.Success;
        }
    }
}