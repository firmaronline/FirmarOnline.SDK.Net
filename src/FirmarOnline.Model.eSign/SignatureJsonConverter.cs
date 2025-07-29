#if NET6_0_OR_GREATER

using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FirmarOnline.Model.eSign
{
    /// <summary>
    /// Conversor de JSON para <see cref="Signature"/>.
    /// Permite convertir a <see cref="Signature"/> o <see cref="BioSignature"/>
    /// dependiendo de si la información incluye información biométrica o no
    /// </summary>
    public class SignatureJsonConverter : JsonConverter<Signature>
    {
        /// <summary>
        /// Convierte el JSON a <see cref="Signature"/> o <see cref="BioSignature"/>
        /// dependiendo de si el JSON incluye información biométrica o no
        /// </summary>
        /// <param name="reader">El <see cref="Utf8JsonReader"/> del que leer</param>
        /// <param name="typeToConvert">El <see cref="Type"/> que se está convirtiendo</param>
        /// <param name="options">Las <see cref="JsonSerializerOptions"/> a utilizar</param>
        /// <returns>El valor convertido</returns>
        public override Signature Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using JsonDocument doc = JsonDocument.ParseValue(ref reader);
            var root = doc.RootElement;

            var caseOptions = options.PropertyNameCaseInsensitive ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
            bool hasBiometricInfo = root.EnumerateObject().Any(p => string.Equals(p.Name, nameof(BioSignature.B64PublicBioData), caseOptions))
                                    && root.EnumerateObject().Any(p => string.Equals(p.Name, nameof(BioSignature.B64PrivateBioData), caseOptions));

            string json = root.GetRawText();

            // Para que no entre en bucle eliminamos el convertidor de las opciones de serialización
            var optionsWithoutConverter = new JsonSerializerOptions(options);
            optionsWithoutConverter.Converters.Remove(optionsWithoutConverter.Converters.First(c => c is SignatureJsonConverter));

            return hasBiometricInfo
                ? JsonSerializer.Deserialize<BioSignature>(json, optionsWithoutConverter)
                : JsonSerializer.Deserialize<Signature>(json, optionsWithoutConverter);
        }

        /// <summary>
        /// Convierte los datos de firma a JSON
        /// </summary>
        /// <param name="writer">El <see cref="Utf8JsonWriter"/> al que escribir</param>
        /// <param name="value">El valor a convertir</param>
        /// <param name="options">El <see cref="JsonSerializerOptions"/> a utilizar</param>
        public override void Write(Utf8JsonWriter writer, Signature value, JsonSerializerOptions options)
        {
            if (value is BioSignature webSignature)
            {
                JsonSerializer.Serialize(writer, webSignature, options);
            }
            else
            {
                JsonSerializer.Serialize(writer, value, options);
            }
        }
    }
}

#else

using System;
using FirmarOnline.Model.eSign;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

/// <summary>
/// Conversor de JSON para <see cref="Signature"/>.
/// Permite convertir a <see cref="Signature"/> o <see cref="BioSignature"/>
/// dependiendo de si la información incluye información biométrica o no
/// </summary>
public class SignatureJsonConverter : JsonConverter
{
    /// <summary>
    /// Comprueba que el objeto proporcionado sea del tipo <see cref="Signature"/>
    /// </summary>
    /// <param name="objectType">Tipo del objeto</param>
    /// <returns>True si es un tipo <see cref="Signature"/>, si no false</returns>
    public override bool CanConvert(Type objectType)
    {
        return typeof(Signature).IsAssignableFrom(objectType);
    }


    /// <summary>
    /// Lee el JSON devolviendo un <see cref="BioSignature"/> o un <see cref="Signature"/> en 
    /// función de si la información del JSON contiene datos biométricos
    /// </summary>
    /// <param name="reader">El <see cref="JsonReader"/> del que leer</param>
    /// <param name="objectType">Tipo del objeto</param>
    /// <param name="existingValue">El valor existente del objeto que se está leyendo</param>
    /// <param name="serializer">El serializador</param>
    /// <returns>Un <see cref="BioSignature"/> o un <see cref="Signature"/> en función de si
    /// el JSON contiene datos biométricos</returns>
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        JObject jObject = JObject.Load(reader);

        bool hasBiometricInfo = jObject.ContainsKey(nameof(BioSignature.B64PublicBioData)) &&
                                jObject.ContainsKey(nameof(BioSignature.B64PrivateBioData));

        JsonSerializer noConverterSerializer = new();
        foreach (var conv in serializer.Converters)
        {
            if (conv is not SignatureJsonConverter)
                noConverterSerializer.Converters.Add(conv);
        }

        return hasBiometricInfo
            ? jObject.ToObject<BioSignature>(noConverterSerializer)
            : jObject.ToObject<Signature>(noConverterSerializer);
    }

    /// <summary>
    /// Genera la representación JSON del objeto <see cref="Signature"/>
    /// </summary>
    /// <param name="writer">El <see cref="JsonWriter"/> en el que escribir</param>
    /// <param name="value">El objeto a serializar</param>
    /// <param name="serializer">El serializador</param>
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value is BioSignature webSignature)
        {
            serializer.Serialize(writer, webSignature);
        }
        else
        {
            serializer.Serialize(writer, value);
        }
    }
}

#endif