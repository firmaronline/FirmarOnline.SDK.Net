using FirmarOnline.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FirmarOnline.Clients.Common.Responses
{
    /// <summary>
    /// Define una respuesta a una llamada API en la que se devuelve un listado
    /// paginado de elementos del tipo <typeparamref name="T"/>>
    /// </summary>
    /// <typeparam name="T">Tipo de datos de los elementos de la página</typeparam>
    public class PagedApiResponse<T> : ApiResponse<ICollection<T>>
    {
        /// <summary>
        /// Define la respuesta de una llamada API que devuelve un listado
        /// paginado de elementos del tipo <typeparamref name="T"/>
        /// </summary>
        /// <param name="response">Respuesta del servidor</param>
        /// <param name="pageSize">Número de elementos devueltos en cada página</param>
        /// <param name="firstElementInPage">Índice del primer elemento de la página</param>
        /// <returns>Un <see cref="PagedApiResponse{T}"/> con el contenido de la respuesta</returns>
        public static async Task<PagedApiResponse<T>> CreateAsync(HttpResponseMessage response, int pageSize = 0, int firstElementInPage = 0)
        {
            var apiResponse = new PagedApiResponse<T> { StatusCode = response.StatusCode };
            if (response.Content != null)
            {
                if (response.IsSuccessStatusCode)
                {
                    apiResponse.Value = await response.Content.ReadFromApiJsonAsync<ICollection<T>>();
                    var resultItems = apiResponse.Value?.Count ?? 0;
                    var totalItems = response.Headers.TryGetValues("X-Total-Count", out var totalCountValues)
                                        ? int.Parse(totalCountValues.First()) : 0;
                    apiResponse.TotalCount = Math.Max(totalItems, resultItems);
                    apiResponse.PageSize = Math.Max(pageSize, resultItems);
                    apiResponse.FirstElementInPage = firstElementInPage;
                }
                else
                {
                    apiResponse.Problem = await ApiResponseProblem.FromHttpResponseAsync(response);
                }
            }
            return apiResponse;
        }

        /// <summary>
        /// Total de elementos del listado
        /// </summary>
        public int TotalCount { get; internal set; }

        /// <summary>
        /// Número de elementos devueltos en cada página
        /// </summary>
        public int PageSize { get; internal set; }

        /// <summary>
        /// Índice del primer elemento de la página
        /// </summary>
        public int FirstElementInPage { get; internal set; }

        /// <summary>
        /// Devuelve un <see cref="PageResult{T}"/> con el contenido de la respuesta
        /// </summary>
        public PageResult<T> PageResult
        {
            get
            {
                return new PageResult<T>(IsSuccessStatusCode ? Value : null, TotalCount, FirstElementInPage, PageSize);
            }
        }
    }
}
