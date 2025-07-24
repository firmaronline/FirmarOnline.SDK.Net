#if NET6_0_OR_GREATER

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FirmarOnline.Model.Widgets
{
    /// <summary>
    /// Convierte un <see cref="IEnumerable{TextLine}"/> en JSON o desde JSON.
    /// </summary>
    public class CustomTextJsonConverter : JsonConverter<IEnumerable<TextLine>>
    {
        /// <summary>
        /// Lee y convierte el JSON en un <see cref="IEnumerable{TextLine}"/>.
        /// Si el valor es un string creamos una colección de una única línea con el texto.
        /// </summary>
        /// <param name="reader">Lector.</param>
        /// <param name="typeToConvert">Tipo que se va a convertir.</param>
        /// <param name="options">Objeto que especifica las opciones de serialización que se va a utilizar.</param>
        /// <returns>El valor convertido.</returns>
        public override IEnumerable<TextLine> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var doc = JsonDocument.ParseValue(ref reader);
            if (doc.RootElement.ValueKind == JsonValueKind.String)
            {
                return [ new() { Text = doc.RootElement.GetString() } ];
            }

            return JsonSerializer.Deserialize<TextLine[]>(doc.RootElement.GetRawText(), options);
        }

        /// <summary>
        /// Escribe el <see cref="IEnumerable{TextLine}"/> especificado como JSON.
        /// </summary>
        /// <param name="writer">El sistema de escritura en el que se va a escribir.</param>
        /// <param name="value">Valor que se va a convertir a JSON.</param>
        /// <param name="options">Objeto que especifia las opciones de serialización que se va a utilizar.</param>
        public override void Write(Utf8JsonWriter writer, IEnumerable<TextLine> value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }
    }
}
#else

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FirmarOnline.Model.Widgets
{
    /// <summary>
    /// Convierte un <see cref="IEnumerable{TextLine}"/> en JSON o desde JSON.
    /// </summary>
    public class CustomTextJsonConverter : JsonConverter
    {
        /// <summary>
        /// Comprueba que el tipo a convertir es un <see cref="IEnumerable{TextLine}"/>
        /// </summary>
        /// <param name="objectType">Tipo del objeto</param>
        /// <returns>True si es un tipo <see cref="IEnumerable{TextLine}"/>, si no false</returns>
        public override bool CanConvert(Type objectType)
        {
            return typeof(IEnumerable<TextLine>).IsAssignableFrom(objectType);
        }

        /// <summary>
        /// Lee y convierte el JSON en un <see cref="IEnumerable{TextLine}"/>.
        /// Si el valor es un string creamos una colección de una única línea con el texto.
        /// </summary>
        /// <param name="reader">El <see cref="JsonReader"/> del que leer</param>
        /// <param name="objectType">Tipo del objeto</param>
        /// <param name="existingValue">El valor existente del objeto que se está leyendo</param>
        /// <param name="serializer">El serializador</param>
        /// <returns>El valor convertido</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);

            JsonSerializer noConverterSerializer = new JsonSerializer();
            foreach (var conv in serializer.Converters)
            {
                if (!(conv is CustomTextJsonConverter))
                    noConverterSerializer.Converters.Add(conv);
            }

            if (token.Type == JTokenType.String)
            {
                return new List<TextLine> { new TextLine { Text = token.ToString() } };
            }

            return token.ToObject<List<TextLine>>(noConverterSerializer);
        }

        /// <summary>
        /// Escribe el <see cref="IEnumerable{TextLine}"/> especificado como JSON.
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