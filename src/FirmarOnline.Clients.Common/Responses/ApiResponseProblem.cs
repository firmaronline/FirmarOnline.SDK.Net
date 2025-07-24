using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FirmarOnline.Clients.Common.Responses
{
    /// <summary>
    /// Detalles del problema cuando un petición no concluye correctamente
    /// </summary>
    public class ApiResponseProblem
    {
        /// <summary>
        /// Cabeceras HTTP de la respuesta del servidor
        /// </summary>
        public Dictionary<string, IEnumerable<string>> ResponseHeaders { get; internal set; }

        /// <summary>
        /// Contenido del mensaje de respuesta en plano
        /// </summary>
        public string Content { get; internal set; }

        /// <summary>
        /// Motivo del problema reportado por el servidor
        /// </summary>
        public string ReasonPhrase { get; internal set; }

        /// <summary>
        /// <see cref="Uri"/> de la petición
        /// </summary>
        public Uri RequestUri { get; internal set; }

        /// <summary>
        /// <see cref="HttpMethod"/> de la petición
        /// </summary>
        public HttpMethod RequestMethod { get; internal set; }

        /// <summary>
        /// Crea una nueva instancia de <see cref="ApiResponseProblem"/> a partir de la respuesta del servidor
        /// </summary>
        /// <param name="response">El <see cref="HttpResponseMessage"/></param>
        /// <returns>Un <see cref="ApiResponseProblem"/> con la información del problema de la petición</returns>
        public static async Task<ApiResponseProblem> FromHttpResponseAsync(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode) { return null; }

            var responseContent = await response.Content?.ReadAsStringAsync();

            var responseProblem = new ApiResponseProblem
            {
                ResponseHeaders = response.Headers.ToDictionary(header => header.Key, header => header.Value),
                Content = responseContent,
                ReasonPhrase = response.ReasonPhrase,
                RequestUri = response.RequestMessage?.RequestUri != null ? response.RequestMessage.RequestUri : null,
                RequestMethod = response.RequestMessage?.Method != null ? new HttpMethod(response.RequestMessage.Method.Method) : null
            };
            //try
            //{
            //    responseProblem.ProblemDetails = JsonSerializer.Deserialize<ProblemDetails>(responseContent, ApiDefaults.JsonSerializerOptions);
            //}
            //catch (JsonException) { }
            return responseProblem;
        }
    }
}
