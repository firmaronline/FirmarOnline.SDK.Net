using System.Net.Http;
using System.Threading.Tasks;
using System;
using FirmarOnline.Clients.Common.Responses;
using System.Net.Http.Headers;
using System.Net;
using System.IO;



#if NET6_0_OR_GREATER
using System.Net.Http.Json;
#else
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
#endif

namespace FirmarOnline.Clients.Common
{
    /// <summary>
    /// Clase base para módulos internos que exponen apis
    /// </summary>
    public abstract partial class ApiClientBase
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="ApiClientBase"/>
        /// </summary>
        /// <param name="httpClientFactory">Factoría para creación de instancias de <see cref="HttpClient"/></param>
        protected ApiClientBase(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(GetType().FullName);
        }

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="ApiClientBase"/>
        /// </summary>
        /// <param name="apiBaseAddress">Url base de la api</param>
        /// <param name="authenticationToken">Token de autenticación o api key válida para la url indicada</param>
        public ApiClientBase(Uri apiBaseAddress, string authenticationToken)
        {
#if NET6_0_OR_GREATER
            ArgumentNullException.ThrowIfNull(apiBaseAddress);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(authenticationToken);
#else
            if (apiBaseAddress is null) { throw new ArgumentNullException(nameof(apiBaseAddress)); }
            if (string.IsNullOrWhiteSpace(authenticationToken))
            {
                throw new ArgumentException($"'{nameof(authenticationToken)}' cannot be null or whitespace.", nameof(authenticationToken));
            }
#endif

            _httpClient = new()
            {
                BaseAddress = apiBaseAddress
            };
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authenticationToken);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Envía una petición PUT a la url especificada
        /// </summary>
        /// <param name="url">La <see cref="Uri"/> a la que se enviará la petición</param>
        /// <returns>Un objeto <see cref="ApiResponse"/> con el estado devuelto.</returns>
        protected virtual async Task<ApiResponse> PutAsync(Uri url)
        {
            return await PutAsync<object>(url, null);
        }

        /// <summary>
        /// Envía una petición PUT a la url especificada
        /// </summary>
        /// <param name="url">La url a la que se enviará la petición</param>
        /// <returns>Un objeto <see cref="ApiResponse"/> con el estado devuelto.</returns>
        protected virtual async Task<ApiResponse> PutAsync(string url)
        {
            return await PutAsync<object>(url, null);
        }

        /// <summary>
        /// Envía una petición PUT a la url especificada
        /// </summary>
        /// <typeparam name="TRequestData">Tipo de datos del contenido del cuerpo de la petición</typeparam>
        /// <param name="url">La <see cref="Uri"/> a la que se enviará la petición</param>
        /// <param name="data">El contenido del cuerpo de la petición</param>
        /// <returns>Un objeto <see cref="ApiResponse"/> con el estado devuelto.</returns>
        protected virtual async Task<ApiResponse> PutAsync<TRequestData>(Uri url, TRequestData data)
        {
            return await HttpPutAsync(data, uri: url);
        }

        /// <summary>
        /// Envía una petición PUT a la url especificada
        /// </summary>
        /// <typeparam name="TRequestData">Tipo de datos del contenido del cuerpo de la petición</typeparam>
        /// <param name="url">La url a la que se enviará la petición</param>
        /// <param name="data">El contenido del cuerpo de la petición</param>
        /// <returns>Un objeto <see cref="ApiResponse"/> con el estado devuelto.</returns>
        protected virtual async Task<ApiResponse> PutAsync<TRequestData>(string url, TRequestData data)
        {
            return await HttpPutAsync(data, url: url);
        }

        private async Task<ApiResponse> HttpPutAsync<TRequestData>(TRequestData data, string url = null, Uri uri = null)
        {
#if NET6_0_OR_GREATER
            using HttpContent content = JsonContent.Create(data, options: ApiDefaults.JsonSerializerOptions);
#else
            using HttpContent content = new StringContent(JsonConvert.SerializeObject(data, ApiDefaults.JsonSerializerOptions),
                        Encoding.UTF8, "application/json");
#endif
            using var response = await (uri == null ? _httpClient.PutAsync(url, content) : _httpClient.PutAsync(uri, content));
            var apiResponse = await ApiResponse.CreateAsync(response);
            return apiResponse;
        }

        /// <summary>
        /// Envía una petición DELETE a la url especificada
        /// </summary>
        /// <param name="url">La <see cref="Uri"/> a la que se enviará la petición</param>
        /// <returns>Un objeto <see cref="ApiResponse"/> con el estado devuelto.</returns>
        protected virtual async Task<ApiResponse> DeleteAsync(Uri url)
        {
            return await HttpDeleteAsync(uri: url);
        }

        /// <summary>
        /// Envía una petición DELETE a la url especificada
        /// </summary>
        /// <param name="url">La url a la que se enviará la petición</param>
        /// <returns>Un objeto <see cref="ApiResponse"/> con el estado devuelto.</returns>
        protected virtual async Task<ApiResponse> DeleteAsync(string url)
        {
            return await HttpDeleteAsync(url: url);
        }

        /// <summary>
        /// Envía una petición DELETE a la url especificada
        /// </summary>
        /// <param name="url">La <see cref="Uri"/> a la que se enviará la petición</param>
        /// <typeparam name="TResponseData">Tipo de datos del valor devuelto</typeparam>
        /// <returns>Un objeto <see cref="ApiResponse"/> con el estado devuelto.</returns>
        protected virtual async Task<ApiResponse<TResponseData>> DeleteAsync<TResponseData>(Uri url)
        {
            return await HttpDeleteAsync<TResponseData>(uri: url);
        }

        /// <summary>
        /// Envía una petición DELETE a la url especificada
        /// </summary>
        /// <param name="url">La url a la que se enviará la petición</param>
        /// <typeparam name="TResponseData">Tipo de datos del valor devuelto</typeparam>
        /// <returns>Un objeto <see cref="ApiResponse"/> con el estado devuelto.</returns>
        protected virtual async Task<ApiResponse<TResponseData>> DeleteAsync<TResponseData>(string url)
        {
            return await HttpDeleteAsync<TResponseData>(url: url);
        }

        private async Task<ApiResponse> HttpDeleteAsync(string url = null, Uri uri = null)
        {
            using var response = await (uri == null ? _httpClient.DeleteAsync(url) : _httpClient.DeleteAsync(uri));
            var apiResponse = await ApiResponse.CreateAsync(response);
            return apiResponse;
        }

        private async Task<ApiResponse<TResponseData>> HttpDeleteAsync<TResponseData>(string url = null, Uri uri = null)
        {
            using var response = await (uri == null ? _httpClient.DeleteAsync(url) : _httpClient.DeleteAsync(uri));
            var apiResponse = await ApiResponse<TResponseData>.CreateAsync(response);
            return apiResponse;
        }

        /// <summary>
        /// Comprueba el estado de la respuesta de la API y lanza una excepción si no es exitosa.
        /// </summary>
        /// <param name="response">Respuesta recibida por una llamada a la API</param>
        protected static void CheckResponseStatus(ApiResponse response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var uri = response.Problem?.RequestUri?.ToString() ?? "unknown";
                var status = response.StatusCode;
                var reason = response.Problem?.ReasonPhrase ?? "No reason provided";
                var content = response.Problem?.Content ?? string.Empty;
                var detail = GetDetailFromContent(content);

                throw response.StatusCode switch
                {
                    HttpStatusCode.Unauthorized => new UnauthorizedAccessException($"Unauthorized request to {uri}."),
                    HttpStatusCode.RequestTimeout => new TimeoutException($"Request to {uri} timed out."),
#if NET6_0_OR_GREATER
                    _ => new HttpRequestException(HttpRequestError.Unknown, message: detail ?? reason, statusCode: status)
                    {
                        Source = uri
                    }
#else
                    _ => new HttpRequestException($"Request error calling {uri}. Status Code: {status}. Reason: {detail ?? reason}. Content: {content}")
#endif
                };
            }
        }

        /// <summary>
        /// Extrae el valor de la propiedad "detail" de un contenido JSON de respuesta de error.
        /// </summary>
        /// <param name="content">Contenido JSON de la respuesta</param>
        /// <returns>El valor de "detail" si existe, o <see langword="null"/> si no se puede extraer.</returns>
        private static string GetDetailFromContent(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                return null;

            try
            {
#if NET6_0_OR_GREATER
                using var doc = System.Text.Json.JsonDocument.Parse(content);
                if (doc.RootElement.TryGetProperty("detail", out var detailElement))
                    return detailElement.GetString();
#else
                var json = JObject.Parse(content);
                return json["detail"]?.Value<string>();
#endif
            }
            catch
            {
                // Si el contenido no es JSON válido, se ignora y se devuelve null
            }

            return null;
        }

        /// <summary>
        /// Convierte un <see cref="Stream"/> de un fichero a una cadena en base 64.
        /// </summary>
        /// <param name="file">Contenido del fichero a convertir</param>
        /// <returns>Cadena en base 64 con el contenido del fichero</returns>
        protected static async Task<string> Stream2Base64Async(Stream file)
        {
            string base64;
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                base64 = Convert.ToBase64String(memoryStream.ToArray());
            }

            return base64;
        }
    }
}
