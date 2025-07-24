#if NET6_0_OR_GREATER

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FirmarOnline.Model.Forms
{
    /// <summary>
    /// Campo de tipo selección mediante Radio Button.
    /// </summary>
    public class RadioButton : StringField
    {
        /// <summary>
        /// Opciones de selección.
        /// </summary>
        public List<Option> Options { get; set; }

        /// <summary>
        /// Acciones en caso de selección.
        /// </summary>
        public List<Action> Actions { get; set; }

        /// <summary>
        /// Mostrar las opciones una encima de otra.
        /// </summary>
        public bool VerticalOptions { get; set; }
    }

    /// <summary>
    /// Opciones de Radio Button.
    /// </summary>
    public class Option
    {
        /// <summary>
        /// Valor de la opción.
        /// </summary>
        [MaxLength(255)]
        public string Value { get; set; }

        /// <summary>
        /// Texto de la opción.
        /// </summary>
        [MaxLength(255)]
        public string Text { get; set; }

        /// <summary>
        /// Valor por defecto de la opción.
        /// </summary>
        public string Checked { get; set; }
    }

    /// <summary>
    /// Opción de combo.
    /// </summary>
    public class Action
    {
        /// <summary>
        /// Lista de identificadores de elementos.
        /// </summary>
        [MaxLength(50)]
        [JsonConverter(typeof(RelatedIdJsonConverter))]
        public IEnumerable<string> RelatedId { get; set; }

        /// <summary>
        /// Valor del elemento.
        /// </summary>
        [MaxLength(255)]
        public string Value { get; set; }

        /// <summary>
        /// Visible.
        /// </summary>
        public bool Visible { get; set; }

        /// <summary>
        /// Requerido.
        /// </summary>
        public bool Required { get; set; }
    }

    /// <summary>
    /// Convierte un string con el campo "RelatedId" en una lista con un valor.
    /// </summary>
    public class RelatedIdJsonConverter : JsonConverter<IEnumerable<string>>
    {
        /// <summary>
        /// Convierte un string con el campo "RelatedId" en una lista con un valor.
        /// </summary>
        /// <param name="reader">Lector.</param>
        /// <param name="typeToConvert">Tipo que se va a convertir.</param>
        /// <param name="options">Objeto que especifica las opciones de serialización que se van a utilizar.</param>
        /// <returns>Objeto convertido.</returns>
        /// <exception cref="JsonException"></exception>
        public override IEnumerable<string> Read(ref Utf8JsonReader reader, System.Type typeToConvert, JsonSerializerOptions options)
        {
            using JsonDocument doc = JsonDocument.ParseValue(ref reader);

            if (doc.RootElement.ValueKind == JsonValueKind.String)
            {
                return [doc.Deserialize<string>()];
            }
            else if (doc.RootElement.ValueKind == JsonValueKind.Array)
            {
                return doc.Deserialize<IEnumerable<string>>();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Escribe el "RelatedId" especificado como un array de un string.
        /// </summary>
        /// <param name="writer">El sistema de escritura en el que se va a escribir.</param>
        /// <param name="value">Valor que se va a convertir en JSON.</param>
        /// <param name="options">Objeto que especifica las opciones de serialización que se van a utilizar.</param>
        /// <exception cref="JsonException"></exception>
        public override void Write(Utf8JsonWriter writer, IEnumerable<string> value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }
    }
}
#endif