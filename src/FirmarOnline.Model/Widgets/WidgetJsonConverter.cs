#if NET6_0_OR_GREATER

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FirmarOnline.Model.Widgets
{
    /// <summary>
    /// Convierte un <see cref="Widget"/> en JSON o desde JSON.
    /// </summary>
    public partial class WidgetJsonConverter : JsonConverter<Widget>
    {
        /// <summary>
        /// Lee y convierte el JSON en un <see cref="Widget"/>.
        /// Dependiendo de los valores especificados en las propiedades devolverá un <see cref="FieldWidget"/>,
        /// un <see cref="FloatWidget"/>, un <see cref="FixedWidget"/> o un <see cref="Widget"/>.
        /// </summary>
        /// <param name="reader">Lector.</param>
        /// <param name="typeToConvert">Tipo que se va a convertir.</param>
        /// <param name="options">Objeto que especifica las opciones de serialización que se van a utilizar.</param>
        /// <returns>El valor convertido.</returns>
        public override Widget Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (typeToConvert == typeof(Widget))
            {
                var doc = JsonDocument.ParseValue(ref reader);

                foreach (var property in doc.RootElement.EnumerateObject())
                {
                    // Determinar el tipo de posicionamiento.
                    if (property.Name.ToString().Equals("fieldName", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrWhiteSpace(property.Value.ToString()))
                    {
                        return JsonSerializer.Deserialize<FieldWidget>(doc.RootElement.GetRawText(), options);
                    }
                    else if (property.Name.ToString().Equals("text", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrWhiteSpace(property.Value.ToString()))
                    {
                        return JsonSerializer.Deserialize<FloatWidget>(doc.RootElement.GetRawText(), options);
                    }
                    else if (property.Name.ToString().Equals("page", StringComparison.OrdinalIgnoreCase)
                        && int.TryParse(property.Value.ToString(), out int pageNumber) && pageNumber >= 0)
                    {
                        return JsonSerializer.Deserialize<FixedWidget>(doc.RootElement.GetRawText(), options);
                    }
                }

                return null;
            }

            throw new NotSupportedException();
        }

        /// <summary>
        /// Escribe el <see cref="Widget"/> especificado como JSON.
        /// </summary>
        /// <param name="writer">El sistema de escritura en el que se va a escribir.</param>
        /// <param name="value">Valor que se va a convertir en JSON.</param>
        /// <param name="options">Objeto que especifica las opciones de serialización que se van a utilizar.</param>
        public override void Write(Utf8JsonWriter writer, Widget value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }
    }
}

#else

using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FirmarOnline.Model.Widgets
{
    /// <summary>
    /// Lee y convierte el JSON en un <see cref="Widget"/>.
    /// Dependiendo de los valores especificados en las propiedades devolverá un <see cref="FieldWidget"/>,
    /// un <see cref="FloatWidget"/>, un <see cref="FixedWidget"/> o un <see cref="Widget"/>.
    /// </summary>
    public class WidgetJsonConverter : JsonConverter
    {
        /// <summary>
        /// Comprueba que el objeto proporcionado sea del tipo <see cref="Widget"/>
        /// </summary>
        /// <param name="objectType">Tipo del objeto</param>
        /// <returns>True si es un tipo <see cref="Widget"/>, si no false</returns>
        public override bool CanConvert(Type objectType)
        {
            return typeof(Widget).IsAssignableFrom(objectType);
        }

        /// <summary>
        /// Lee y convierte el JSON en un <see cref="Widget"/>.
        /// Dependiendo de los valores especificados en las propiedades devolverá un <see cref="FieldWidget"/>,
        /// un <see cref="FloatWidget"/>, un <see cref="FixedWidget"/> o un <see cref="Widget"/>.
        /// </summary>
        /// <param name="reader">El <see cref="JsonReader"/> del que leer</param>
        /// <param name="objectType">Tipo del objeto</param>
        /// <param name="existingValue">El valor existente del objeto que se está leyendo</param>
        /// <param name="serializer">El serializador</param>
        /// <returns>El valor convertido</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);

            JsonSerializer noConverterSerializer = new JsonSerializer();
            foreach (var conv in serializer.Converters)
            {
                if (!(conv is WidgetJsonConverter))
                    noConverterSerializer.Converters.Add(conv);
            }

            // Identificar tipo a deserializar según propiedades presentes
            var fieldName = jObject.Value<string>("fieldName");
            if (!string.IsNullOrWhiteSpace(fieldName))
            {
                return jObject.ToObject<FieldWidget>(noConverterSerializer);
            }

            var text = jObject.Value<string>("text");
            if (!string.IsNullOrWhiteSpace(text))
            {
                return jObject.ToObject<FloatWidget>(noConverterSerializer);
            }

            var pageToken = jObject["page"];
            if (pageToken != null && pageToken.Type == JTokenType.Integer && (int)pageToken >= 0)
            {
                return jObject.ToObject<FixedWidget>(noConverterSerializer);
            }

            return null;
        }

        /// <summary>
        /// Escribe el <see cref="Widget"/> especificado como JSON.
        /// </summary>
        /// <param name="writer">El <see cref="JsonWriter"/> en el que escribir</param>
        /// <param name="value">El objeto a serializar</param>
        /// <param name="serializer">El serializador</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value, value.GetType());
        }
    }
}

#endif