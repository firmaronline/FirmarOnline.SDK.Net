#if NET6_0_OR_GREATER

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FirmarOnline.Model.Certificates
{
    /// <summary>
    /// Convierte un <see cref="Certificate"/> en JSON o desde JSON.
    /// </summary>
    public partial class CertificateJsonConverter : JsonConverter<Certificate>
    {
        /// <summary>
        /// Lee y convierte el JSON en un <see cref="Certificate"/>
        /// dependiendo de los valores especificados en las propiedades devolverá un <see cref="PKCS12Certificate"/>,
        /// o un <see cref="PKCS8Certificate"/>.
        /// </summary>
        /// <param name="reader">Lector.</param>
        /// <param name="typeToConvert">Tipo que se va a convertir.</param>
        /// <param name="options">Objeto que especifica las opciones de serialización que se van a utilizar.</param>
        /// <returns>El valor convertido.</returns>
        public override Certificate Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (typeToConvert == typeof(Certificate))
            {
                var doc = JsonDocument.ParseValue(ref reader);

                // Determinar el tipo de posicionamiento
                if (doc.RootElement.TryGetProperty("p12Certificate", out JsonElement p12Certificate)
                        && !string.IsNullOrWhiteSpace(p12Certificate.GetString()))
                {
                    return JsonSerializer.Deserialize<PKCS12Certificate>(doc.RootElement.GetRawText(), options);
                }
                if (doc.RootElement.TryGetProperty("p8PublicCert", out JsonElement p8PublicCert)
                    && !string.IsNullOrWhiteSpace(p8PublicCert.GetString()) ||
                    doc.RootElement.TryGetProperty("p8PrivateKey", out JsonElement p8PrivateKey)
                    && !string.IsNullOrWhiteSpace(p8PrivateKey.GetString()))
                {
                    return JsonSerializer.Deserialize<PKCS8Certificate>(doc.RootElement.GetRawText(), options);
                }

                return null;
            }

            throw new NotSupportedException();
        }

        /// <summary>
        /// Escribe el <see cref="Certificate"/> especificado como JSON.
        /// </summary>
        /// <param name="writer">El sistema de escritura en el que se va a escribir.</param>
        /// <param name="value">Valor que se va a convertir en JSON.</param>
        /// <param name="options">Objeto que especifica las opciones de serialización que se van a utilizar.</param>
        public override void Write(Utf8JsonWriter writer, Certificate value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }
    }
}

#else

using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FirmarOnline.Model.Certificates
{
    /// <summary>
    /// Convierte un <see cref="Certificate"/> en JSON o desde JSON.
    /// </summary>
    public class CertificateJsonConverter : JsonConverter
    {
        /// <summary>
        /// Comprueba que el objeto proporcionado sea del tipo <see cref="Certificate"/>
        /// </summary>
        /// <param name="objectType">Tipo del objeto</param>
        /// <returns>True si es un tipo <see cref="Certificate"/>, si no false</returns>
        public override bool CanConvert(Type objectType)
        {
            return typeof(Certificate).IsAssignableFrom(objectType);
        }

        /// <summary>
        /// Lee y convierte el JSON en un <see cref="Certificate"/>
        /// dependiendo de los valores especificados en las propiedades devolverá un <see cref="PKCS12Certificate"/>,
        /// o un <see cref="PKCS8Certificate"/>.
        /// </summary>
        /// <param name="reader">El <see cref="JsonReader"/> del que leer</param>
        /// <param name="objectType">Tipo del objeto</param>
        /// <param name="existingValue">El valor existente del objeto que se está leyendo</param>
        /// <param name="serializer">El serializador</param>
        /// <returns>El valor convertido</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);

            // Extraer propiedades relevantes
            var p12Certificate = jObject.Value<string>("p12Certificate");
            var p8PublicCert = jObject.Value<string>("p8PublicCert");
            var p8PrivateKey = jObject.Value<string>("p8PrivateKey");

            JsonSerializer noConverterSerializer = new();
            foreach (var conv in serializer.Converters)
            {
                if (conv is not CertificateJsonConverter)
                    noConverterSerializer.Converters.Add(conv);
            }

            if (!string.IsNullOrWhiteSpace(p12Certificate))
            {
                return jObject.ToObject<PKCS12Certificate>(noConverterSerializer);
            }

            if (!string.IsNullOrWhiteSpace(p8PublicCert) || !string.IsNullOrWhiteSpace(p8PrivateKey))
            {
                return jObject.ToObject<PKCS8Certificate>(noConverterSerializer);
            }

            return null;
        }

        /// <summary>
        /// Escribe el <see cref="Certificate"/> especificado como JSON.
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