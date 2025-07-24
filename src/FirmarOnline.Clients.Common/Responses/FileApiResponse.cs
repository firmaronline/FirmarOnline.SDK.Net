using System;
using System.IO;
using System.Net.Http;
using System.Net.Mime;
using System.Threading.Tasks;

namespace FirmarOnline.Clients.Common.Responses
{
    /// <summary>
    /// Define la respuesta a una llamada API que recupera un fichero
    /// </summary>
    public class FileApiResponse : ApiResponse<Stream>, IDisposable
    {
        private HttpResponseMessage _response;

        /// <summary>
        /// Tipo MIME
        /// </summary>
        public ContentType ContentType { get; internal set; }

        /// <summary>
        /// Nombre del fichero
        /// </summary>
        public string FileName { get; internal set; }

        /// <summary>
        /// Constructor privado para que las instancias se creen utilizando el método CreateAsync
        /// </summary>
        private FileApiResponse() { }

        /// <summary>
        /// Define la respuesta de una llamada API que devuelve un fichero
        /// </summary>
        /// <param name="response">Respuesta del servidor</param>
        /// <returns>Un <see cref="FileApiResponse"/> con el contenido de la respuesta</returns>
        public static new async Task<FileApiResponse> CreateAsync(HttpResponseMessage response)
        {
            var apiResponse = new FileApiResponse
            {
                _response = response,
                StatusCode = response.StatusCode,
            };
            if (response.Content != null)
            {
                if (response.IsSuccessStatusCode)
                {
                    apiResponse.Value = await response.Content.ReadAsStreamAsync();
                    apiResponse.ContentType = new ContentType(response.Content.Headers.ContentType.ToString());
                    apiResponse.FileName = response.Content.Headers.ContentDisposition?.FileName;
                }
                else
                {
                    apiResponse.Problem = await ApiResponseProblem.FromHttpResponseAsync(response);
                }
            }
            return apiResponse;
        }

        /// <summary>
        /// Libera los recursos utilizados por el <see cref="FileApiResponse"/>
        /// </summary>
        public void Dispose()
        {
            _response?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
