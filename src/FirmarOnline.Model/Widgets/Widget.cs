using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FirmarOnline.Types.Validations;

#if NET6_0_OR_GREATER
using System.Text.Json.Serialization;
#else
using Newtonsoft.Json;
#endif

namespace FirmarOnline.Model.Widgets
{
    /// <summary>
    /// Define la caja de firma.
    /// </summary>
    [JsonConverter(typeof(WidgetJsonConverter))]
    public abstract class Widget
    {
        /// <summary>
        /// Devuelve el tipo de posicionamiento de la caja de firma.
        /// </summary>
        [JsonIgnore]
        public virtual PositionType Type => PositionType.Manual;

        /// <summary>
        /// Imagen de fondo de la caja de firma en base64 (JPG).
        /// </summary>
        [Base64JPG]
        public string B64Image { get; set; }

        /// <summary>
        /// Texto personalizado a mostrar junto a la firma.
        /// </summary>
        [JsonConverter(typeof(CustomTextJsonConverter))]
        public IEnumerable<TextLine> CustomText { get; set; }

        /// <summary>
        /// Rotación de la caja de firma.
        /// </summary>
        [EnumDataType(typeof(RotationType))]
        public RotationType? Rotation { get; set; }

        /// <summary>
        /// Comprueba si una caja de firma es invisible.
        /// </summary>
        /// <returns>True si es invisible, sino false</returns>
        [JsonIgnore]
        public bool IsInvisible
        {
            get
            {
                if (this is FixedWidget fixedWidget)
                {
                    return fixedWidget.Width == 0 && fixedWidget.Height == 0;
                }
                if (this is FloatWidget floatWidget)
                {
                    return floatWidget.Width == 0 && floatWidget.Height == 0;
                }
                return false;
            }
        }
    }

    /// <summary>
    /// Tipo de posicionamiento de la caja de firma.
    /// </summary>
    public enum PositionType
    {
        /// <summary>
        /// Posicionamiento en un campo predefinido en el documento.
        /// </summary>
        Field,

        /// <summary>
        /// Posicionamiento fijo.
        /// </summary>
        Fixed,

        /// <summary>
        /// Posicionamiento relativo a un texto.
        /// </summary>
        Float,

        /// <summary>
        /// Posicionamiento manual.
        /// </summary>
        Manual
    }

    /// <summary>
    /// Rotation de la caja de firma.
    /// </summary>
    public enum RotationType
    {
        /// <summary>
        /// 0 grados.
        /// </summary>
        Degrees_0,

        /// <summary>
        /// 90 grados.
        /// </summary>
        Degrees_90,

        /// <summary>
        /// 180 grados.
        /// </summary>
        Degrees_180,

        /// <summary>
        /// 270 grados.
        /// </summary>
        Degrees_270
    }
}