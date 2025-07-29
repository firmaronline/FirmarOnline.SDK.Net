using System;
#if NET6_0_OR_GREATER
using System.Text.Json;
#else
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
#endif

namespace FirmarOnline.Clients.Common
{
    /// <summary>
    /// Valores por defecto utilizados por las APIs de Edatalia
    /// </summary>
    public static class ApiDefaults
    {
#if NET6_0_OR_GREATER
        private static readonly JsonSerializerOptions _defaultJsonSerializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        };

        /// <summary>
        /// Especifica la directiva usada para convertir nombres de propiedades a json
        /// </summary>
        public static JsonNamingPolicy JsonNamingPolicy => _defaultJsonSerializerOptions.PropertyNamingPolicy;

        /// <summary>
        /// Indica si los nombres de propiedades json usan una comparación sin distinción entre mayúsculas y minúsculas
        /// </summary>
        public static bool JsonCaseInsensitive => _defaultJsonSerializerOptions.PropertyNameCaseInsensitive;

        /// <summary>
        /// Opciones de serialización a/desde JSON por defecto
        /// </summary>
        public static JsonSerializerOptions JsonSerializerOptions => _defaultJsonSerializerOptions;
#else
        private static readonly JsonSerializerSettings _defaultJsonSerializerOptions = new()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        /// <summary>
        /// Opciones de serialización a/desde JSON por defecto
        /// </summary>
        public static JsonSerializerSettings JsonSerializerOptions => _defaultJsonSerializerOptions;

#endif
    }
}