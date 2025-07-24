using System.ComponentModel.DataAnnotations;
#if NET6_0_OR_GREATER
using System.Text.Json.Serialization;
#else
using Newtonsoft.Json;
#endif

namespace FirmarOnline.Model.Widgets
{
    /// <summary>
    /// Caja de firma con posicionamiento definido por un campo del PDF
    /// </summary>
    public class FieldWidget : Widget
    {
        /// <summary>
        /// Devuelve el tipo de posicionamiento de la caja de firma
        /// </summary>
        [JsonIgnore]
        public override PositionType Type => PositionType.Field;

        /// <summary>
        /// Nombre del campo en el que se ubicará la caja de firma
        /// </summary>
        [MaxLength(50)]
        public string FieldName { get; set; }
    }
}