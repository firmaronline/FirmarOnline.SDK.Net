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
        /// <summary>
        /// Url de la API del PSC en el entorno de pruebas
        /// </summary>
        public static readonly Uri PSCSandboxEnvironmentUrl = new("https://restapi.firmar.info/psc/v40");

        /// <summary>
        /// Url de la API del PSC en el entorno de producción
        /// </summary>
        public static readonly Uri PSCProductionEnvironmentUrl = new("https://restapi.firmar.online/psc/v40");

        /// <summary>
        /// Url de la API del eSign en el entorno de pruebas
        /// </summary>
        public static readonly Uri eSignSandboxEnvironmentUrl = new("https://restapi.firmar.info/esign/v40");

        /// <summary>
        /// Url de la API del eSign en el entorno de producción
        /// </summary>
        public static readonly Uri eSignProductionEnvironmentUrl = new("https://restapi.firmar.online/esign/v40");

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