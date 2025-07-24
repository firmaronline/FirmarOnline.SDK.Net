using FirmarOnline.Clients.Common.Responses;
using FirmarOnline.Types;
using System;
using System.Threading.Tasks;

namespace FirmarOnline.Clients.Common
{
    public abstract partial class ApiClientBase
    {
        /// <summary>
        /// Envía una petición GET a la url especificada
        /// </summary>
        /// <typeparam name="TResponseData">Tipo del valor devuelto</typeparam>
        /// <param name="url">La <see cref="Uri"/> a la que se enviará la petición.</param>
        /// <returns>Un objeto <see cref="ApiResponse{T}"/> con el estado y el valor devuelto.</returns>
        protected virtual async Task<ApiResponse<TResponseData>> GetAsync<TResponseData>(Uri url)
        {
            return await HttpGetAsync<TResponseData>(uri: url);
        }

        /// <summary>
        /// Envía una petición GET a la url especificada
        /// </summary>
        /// <typeparam name="TResponseData">Tipo del valor devuelto</typeparam>
        /// <param name="url">La url a la que se enviará la petición.</param>
        /// <returns>Un objeto <see cref="ApiResponse{T}"/> con el estado y el valor devuelto.</returns>
        protected virtual async Task<ApiResponse<TResponseData>> GetAsync<TResponseData>(string url)
        {
            return await HttpGetAsync<TResponseData>(url: url);
        }

        /// <summary>
        /// Envía una petición GET a la url especificada para recuperar una
        /// página de elementos de un listado paginado
        /// </summary>
        /// <typeparam name="TResponseData">El tipo de los elementos listados</typeparam>
        /// <param name="url">La <see cref="Uri"/> a la que se enviará la petición</param>
        /// <param name="pageFilter">(Opcional) Valores para paginación. Si no se indica se entiende que el
        /// resultado contiene todos los elementos del listado.</param>
        /// <returns>Un objeto <see cref="PagedApiResponse{T}"/> con los elementos de la página.</returns>
        protected virtual async Task<PagedApiResponse<TResponseData>> GetPageAsync<TResponseData>(Uri url, IPageFilter pageFilter = null)
        {
            return await HttpGetPageAsync<TResponseData>(uri: url, pageFilter: pageFilter);
        }

        /// <summary>
        /// Envía una petición GET a la url especificada para recuperar una
        /// página de elementos de un listado paginado
        /// </summary>
        /// <typeparam name="TResponseData">El tipo de los elementos listados</typeparam>
        /// <param name="url">La url a las que se enviará la petición</param>
        /// <param name="pageFilter">(Opcional) Valores para paginación. Si no se indica se entiende que el
        /// resultado contiene todos los elementos del listado.</param>
        /// <returns>Un objeto <see cref="PagedApiResponse{T}"/> con los elementos de la página.</returns>
        protected virtual async Task<PagedApiResponse<TResponseData>> GetPageAsync<TResponseData>(string url, IPageFilter pageFilter = null)
        {
            return await HttpGetPageAsync<TResponseData>(url: url, pageFilter: pageFilter);
        }

        /// <summary>
        /// Envía una petición GET a la url especificada para recuperar un fichero
        /// </summary>
        /// <param name="url">La <see cref="Uri"/> a la que se enviará la petición</param>
        /// <returns>Un objeto <see cref="FileApiResponse"/> con el fichero a descargar</returns>
        protected virtual async Task<FileApiResponse> GetFileAsync(Uri url)
        {
            return await HttpGetFileAsync(uri: url);
        }

        /// <summary>
        /// Envía una petición GET a la url especificada para recuperar un fichero
        /// </summary>
        /// <param name="url">La url a la que se enviará la petición</param>
        /// <returns>Un objeto <see cref="FileApiResponse"/> con el fichero a descargar</returns>
        protected virtual async Task<FileApiResponse> GetFileAsync(string url)
        {
            return await HttpGetFileAsync(url: url);
        }

        private async Task<ApiResponse<TResponseData>> HttpGetAsync<TResponseData>(string url = null, Uri uri = null)
        {
            using var response = await (uri == null ? _httpClient.GetAsync(url) : _httpClient.GetAsync(uri));
            return await ApiResponse<TResponseData>.CreateAsync(response);
        }

        private async Task<PagedApiResponse<TResponseData>> HttpGetPageAsync<TResponseData>(string url = null, Uri uri = null, IPageFilter pageFilter = null)
        {
            using var response = await (uri == null ? _httpClient.GetAsync(url) : _httpClient.GetAsync(uri));
            return await PagedApiResponse<TResponseData>.CreateAsync(response, pageSize: pageFilter?.Limit ?? 0, firstElementInPage: (pageFilter?.Offset ?? 0) + 1);
        }

        private async Task<FileApiResponse> HttpGetFileAsync(string url = null, Uri uri = null)
        {
            // No hacemos dispose del HttpResponseMessage para que el stream devuelto esté disponible
            // Si no habría que hacer una copia del stream y devolver la copia
            var response = await (uri == null ? _httpClient.GetAsync(url) : _httpClient.GetAsync(uri));
            var apiResponse = await FileApiResponse.CreateAsync(response);
            return apiResponse;
        }

    }
}
