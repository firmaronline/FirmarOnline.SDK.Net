using FirmarOnline.Clients.Common.Responses;
using System;
using System.Threading.Tasks;
using System.Net.Http;

#if NET6_0_OR_GREATER
using System.Net.Http.Json;
#else
using Newtonsoft.Json;
using System.Text;
#endif

namespace FirmarOnline.Clients.Common
{
    public abstract partial class ApiClientBase
    {
        /// <summary>
        /// Envía una petición POST a la url especificada
        /// </summary>
        /// <typeparam name="TRequestData">Tipo de datos del contenido del cuerpo de la petición</typeparam>
        /// <param name="url">La <see cref="Uri"/> a la que se enviará la petición</param>
        /// <param name="data">El contenido del cuerpo de la petición</param>
        /// <returns>Un objeto <see cref="ApiResponse"/> con el estado devuelto.</returns>
        protected virtual async Task<ApiResponse> PostAsync<TRequestData>(Uri url, TRequestData data)
        {
            return await HttpPostAsync(data, uri: url);
        }

        /// <summary>
        /// Envía una petición POST a la url especificada
        /// </summary>
        /// <typeparam name="TRequestData">Tipo de datos del contenido del cuerpo de la petición</typeparam>
        /// <param name="url">La url a la que se enviará la petición</param>
        /// <param name="data">El contenido del cuerpo de la petición</param>
        /// <returns>Un objeto <see cref="ApiResponse"/> con el estado devuelto.</returns>
        protected virtual async Task<ApiResponse> PostAsync<TRequestData>(string url, TRequestData data)
        {
            return await HttpPostAsync(data, url: url);
        }

        /// <summary>
        /// Envía una petición POST a la url especificada
        /// </summary>
        /// <typeparam name="TRequestData">Tipo de datos del contenido del cuerpo de la petición</typeparam>
        /// <typeparam name="TResponseData">Tipo de datos del valor devuelto</typeparam>
        /// <param name="url">La <see cref="Uri"/> a la que se enviará la petición</param>
        /// <param name="data">El contenido del cuerpo de la petición</param>
        /// <returns>Un objeto <see cref="ApiResponse{TResponseData}"/> con el estado y el valor devuelto.</returns>
        protected virtual async Task<ApiResponse<TResponseData>> PostAsync<TRequestData, TResponseData>(Uri url, TRequestData data)
        {
            return await HttpPostAsync<TRequestData, TResponseData>(data, uri: url);
        }

        /// <summary>
        /// Envía una petición POST a la url especificada
        /// </summary>
        /// <typeparam name="TRequestData">Tipo de datos del contenido del cuerpo de la petición</typeparam>
        /// <typeparam name="TResponseData">Tipo de datos del valor devuelto</typeparam>
        /// <param name="url">La url a la que se enviará la petición</param>
        /// <param name="data">El contenido del cuerpo de la petición</param>
        /// <returns>Un objeto <see cref="ApiResponse{TResponseData}"/> con el estado y el valor devuelto.</returns>
        protected virtual async Task<ApiResponse<TResponseData>> PostAsync<TRequestData, TResponseData>(string url, TRequestData data)
        {
            return await HttpPostAsync<TRequestData, TResponseData>(data, url: url);
        }

        /// <summary>
        /// Envía una petición POST a la url especificada recibiendo un archivo como respuesta
        /// </summary>
        /// <typeparam name="TRequestData">Tipo de datos del contenido del cuerpo de la petición</typeparam>
        /// <param name="url">La <see cref="Uri"/> a la que se enviará la petición</param>
        /// <param name="data">El contenido del cuerpo de la petición</param>
        /// <returns>Un objeto <see cref="FileApiResponse"/> con el estado y el archivo devuelto.</returns>
        protected virtual async Task<FileApiResponse> PostAndGetFileAsync<TRequestData>(Uri url, TRequestData data)
        {
            return await HttpPostAndGetFile<TRequestData>(data, uri: url);
        }

        /// <summary>
        /// Envía una petición POST a la url especificada recibiendo un archivo como respuesta
        /// </summary>
        /// <typeparam name="TRequestData">Tipo de datos del contenido del cuerpo de la petición</typeparam>
        /// <param name="url">La url a la que se enviará la petición</param>
        /// <param name="data">El contenido del cuerpo de la petición</param>
        /// <returns>Un objeto <see cref="FileApiResponse"/> con el estado y el archivo devuelto.</returns>
        protected virtual async Task<FileApiResponse> PostAndGetFileAsync<TRequestData>(string url, TRequestData data)
        {
            return await HttpPostAndGetFile<TRequestData>(data, url: url);
        }

        private async Task<ApiResponse<TResponseData>> HttpPostAsync<TRequestData, TResponseData>(TRequestData data, string url = null, Uri uri = null)
        {
#if NET6_0_OR_GREATER
            using HttpContent content = JsonContent.Create(data, options: ApiDefaults.JsonSerializerOptions);
#else
            using HttpContent content = new StringContent(JsonConvert.SerializeObject(data, ApiDefaults.JsonSerializerOptions),
                        Encoding.UTF8, "application/json");
#endif
            using var response = await (uri == null ? _httpClient.PostAsync(url, content) : _httpClient.PostAsync(uri, content));
            var apiResponse = await ApiResponse<TResponseData>.CreateAsync(response);
            return apiResponse;
        }

        private async Task<ApiResponse> HttpPostAsync<TRequestData>(TRequestData data, string url = null, Uri uri = null)
        {
#if NET6_0_OR_GREATER
            using HttpContent content = JsonContent.Create(data, options: ApiDefaults.JsonSerializerOptions);
#else
            using HttpContent content = new StringContent(JsonConvert.SerializeObject(data, ApiDefaults.JsonSerializerOptions),
                        Encoding.UTF8, "application/json");
#endif
            using var response = await (uri == null ? _httpClient.PostAsync(url, content) : _httpClient.PostAsync(uri, content));
            var apiResponse = await ApiResponse.CreateAsync(response);
            return apiResponse;
        }

        private async Task<FileApiResponse> HttpPostAndGetFile<TRequestData>(TRequestData data, string url = null, Uri uri = null)
        {
            // No hacemos dispose del HttpResponseMessage para que el stream devuelto esté disponible
            // Si no habría que hacer una copia del stream y devolver la copia
#if NET6_0_OR_GREATER
            using HttpContent content = JsonContent.Create(data, options: ApiDefaults.JsonSerializerOptions);
#else
            using HttpContent content = new StringContent(JsonConvert.SerializeObject(data, ApiDefaults.JsonSerializerOptions),
                        Encoding.UTF8, "application/json");
#endif
            var response = await (uri == null ? _httpClient.PostAsync(url, content) : _httpClient.PostAsync(uri, content));
            var apiResponse = await FileApiResponse.CreateAsync(response);
            return apiResponse;
        }

    }
}
