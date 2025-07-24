using System.Net.Http;
using System.Net;
using System.Threading.Tasks;

namespace FirmarOnline.Clients.Common.Responses
{
    /// <summary>
    /// Define la respuesta a una llamada API sin contenido
    /// </summary>
    public class ApiResponse
    {
        /// <summary>
        /// <see cref="HttpStatusCode"/> de la respuesta
        /// </summary>
        public HttpStatusCode StatusCode { get; internal set; }

        /// <summary>
        /// Detalles del problema en caso de no recibir un código de estado correcto
        /// </summary>
        public ApiResponseProblem Problem { get; internal set; }

        /// <summary>
        /// Indica si la respuesta a la llamada ha sido correcta
        /// </summary>
        public bool IsSuccessStatusCode => (int)StatusCode >= 200 && (int)StatusCode <= 299;

        /// <summary>
        /// Crea una respuesta de llamada API a partir de un código de estado HTTP
        /// </summary>
        /// <param name="response">Respuesta del servidor</param>
        /// <returns>Un <see cref="ApiResponse"/> con el estado indicado</returns>
        public static async Task<ApiResponse> CreateAsync(HttpResponseMessage response)
        {
            var apiResponse = new ApiResponse { StatusCode = response.StatusCode };
            if (!response.IsSuccessStatusCode)
            {
                apiResponse.Problem = await ApiResponseProblem.FromHttpResponseAsync(response);
            }
            return apiResponse;
        }
    }

    /// <summary>
    /// Define una respuesta a una llamada API en la que se devuelve
    /// contenido de tipo <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T">Tipo del contenido de la respuesta</typeparam>
    public class ApiResponse<T> : ApiResponse
    {
        /// <summary>
        /// Valor de tipo <typeparamref name="T"/> devuelto por el servidor
        /// </summary>
        public T Value { get; internal set; }

        /// <summary>
        /// Define la respuesta de una llamada API que devuelve contenido
        /// del tipo <typeparamref name="T"/>
        /// </summary>
        /// <param name="response">Respuesta del servidor</param>
        /// <returns>Un <see cref="ApiResponse{T}"/> con el contenido de la respuesta</returns>
        public static new async Task<ApiResponse<T>> CreateAsync(HttpResponseMessage response)
        {
            var apiResponse = new ApiResponse<T>
            {
                StatusCode = response.StatusCode,
            };
            if (response.Content != null)
            {
                if (response.IsSuccessStatusCode)
                {
                    apiResponse.Value = await response.Content.ReadFromApiJsonAsync<T>();
                }
                else
                {
                    apiResponse.Problem = await ApiResponseProblem.FromHttpResponseAsync(response);
                }
            }
            return apiResponse;
        }
    }
}
