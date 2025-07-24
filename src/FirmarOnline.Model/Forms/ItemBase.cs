#if NET6_0_OR_GREATER

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FirmarOnline.Model.Forms
{
    /// <summary>
    /// Elementos del formulario.
    /// </summary>
    [JsonConverter(typeof(ItemJsonConverter))]
    public abstract class ItemBase
    {
        /// <summary>
        /// Identificador.
        /// </summary>
        [MaxLength(50)]
        public string Id { get; set; }

        /// <summary>
        /// Tipo de elemento.
        /// </summary>
        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ItemType Type { get; set; }

        /// <summary>
        /// Estilos Css.
        /// </summary>
        public Css Css { get; set; }
    }

    /// <summary>
    /// Tipos de campos
    /// </summary>
    public enum ItemType
    {
        /// <summary>
        /// Imagen
        /// </summary>
        image,

        /// <summary>
        /// Salto de página
        /// </summary>
        pageBreak,
        /// <summary>
        /// 
        /// </summary>
        barBreak,
        /// <summary>
        /// Salto de línea
        /// </summary>
        lineBreak,

        /// <summary>
        /// Texto de cabecera
        /// </summary>
        headerText,
        /// <summary>
        /// Parrafo
        /// </summary>
        paragraphText,
        /// <summary>
        /// Texto
        /// </summary>
        continuousText,

        /// <summary>
        /// Campo de tipo texto
        /// </summary>
        textField,
        /// <summary>
        /// Campo de tipo email
        /// </summary>
        emailField,
        /// <summary>
        /// Campo de tipo selección
        /// </summary>
        radioButton,
        /// <summary>
        /// Campo de tipo numérico
        /// </summary>
        numberField,
        /// <summary>
        /// Campo de tipo fecha
        /// </summary>
        dateField,
        /// <summary>
        /// Campo de lista desplegable
        /// </summary>
        dropDownField,

        /// <summary>
        /// Tabla
        /// </summary>
        table
    }
}
#endif