using Edatalia.Net.ApiClients;
using Edatalia.Net.ApiClients.Response;
using Edatalia.Types;
using FirmarOnline.Model;
using FirmarOnline.Model.PSC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace FirmarOnline.Clients.PSC
{
    /// <summary>
    /// Cliente para acceso a la API pública del PSC (Prestador de Servicios de Confianza)
    /// </summary>
    public partial class PSCClient : ApiClientBase
    {
        private readonly Uri _verifyApiBaseAddress;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="PSCClient"/>
        /// </summary>
        /// <param name="httpClientFactory"></param>
        public PSCClient(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory)
        {
            // TODO: Los métodos de verificación deberíamos moverlos a otro cliente, no son específicos del PSC
            // Url base para la api de verify. Reemplazamos el segmento "psc" por "verify"
            // Creamos un HttpClient únicamente para obtener la url base del psc
            var httpClient = httpClientFactory.CreateClient(GetType().FullName);
            var builder = new UriBuilder(httpClient.BaseAddress);
            var segments = builder.Path.Split('/');
            for (int i = 0; i < segments.Length; i++)
            {
                if (segments[i] == "psc")
                {
                    segments.SetValue("verify", i);
                }
            }

            builder.Path = string.Join("/", segments);
            _verifyApiBaseAddress = builder.Uri;
        }

        /// <summary>
        /// Recupera el estado actual de un sobre
        /// </summary>
        /// <param name="documentSetId">Identificador único del sobre</param>
        /// <returns>Un objeto <see cref="DocumentSetStatusCode"/> con el estado del sobre</returns>
        public async Task<DocumentSetStatusCode> GetDocumentSetStatusAsync(string documentSetId)
        {
            var result = await GetAsync<DocumentSetStatusCode>($"documentset/status/{documentSetId}");

            CheckResponseStatus(result);
            return result.Value;
        }

        /// <summary>
        /// Recupera el documento de evidencias del procesamiento del sobre
        /// </summary>
        /// <param name="documentSetId">Identificador del sobre</param>
        /// <returns>El documento de evidencias</returns>
        public async Task<Stream> GetEvidencesAsync(string documentSetId)
        {
            var result = await GetFileAsync($"documentset/evidences/{documentSetId}");

            CheckResponseStatus(result);
            return result.Value;
        }

        /// <summary>
        /// Recupera un documento de un sobre una vez finalizado el procesamiento de éste
        /// </summary>
        /// <param name="documentSetId">Identificador del sobre</param>
        /// <param name="documentId">Identificador del documento.
        /// No es necesario indicarlo si el sobre tiene un único documento.</param>
        /// <returns>El contenido del documento</returns>
        public async Task<FileResult> GetDocumentAsync(string documentSetId, string documentId = null)
        {
            var url = $"documentset/document/{documentSetId}{(string.IsNullOrEmpty(documentId) ? string.Empty : $"/{documentId}")}";
            var result = await GetFileAsync(url);

            CheckResponseStatus(result);

            return new FileResult
            {
                Name = result.FileName,
                ContentType = result.ContentType.ToString(),
                Content = result.Value
            };
        }

        /// <summary>
        /// Provoca el reenvío del email de aviso al destinatario actual
        /// </summary>
        /// <param name="documentSetId">Identificador único del sobre</param>
        public async Task ResendDocumentSetAsync(string documentSetId)
        {
            var result = await PutAsync($"documentset/resend/{documentSetId}");

            CheckResponseStatus(result);
        }

        /// <summary>
        /// Cancela el procesamiento de un sobre
        /// </summary>
        /// <param name="documentSetId">Identificador único del sobre</param>
        public async Task CancelDocumentSetAsync(string documentSetId)
        {
            var result = await PutAsync($"documentset/cancel/{documentSetId}");

            CheckResponseStatus(result);
        }

        /// <summary>
        /// Purga los documentos de un sobre finalizado.
        /// </summary>
        /// <param name="documentSetId">Identificador único del sobre</param>
        public async Task PurgeDocumentSetAsync(string documentSetId)
        {
            var result = await PutAsync($"documentset/purge/{documentSetId}");

            CheckResponseStatus(result);
        }

        /// <summary>
        /// Recupera la URL de acceso al sobre para los creados a través de NewDocumentSetUrl
        /// </summary>
        /// <param name="documentSetId">Identificador único del sobre</param>
        /// <returns>Url del sobre</returns>
        public async Task<string> GetDocumentSetUrlAsync(string documentSetId)
        {
            var result = await GetAsync<string>($"documentset/url/{documentSetId}");

            CheckResponseStatus(result);
            return result.Value;
        }

        /// <summary>
        /// Devuelve la información del error producido en el procesamiento del sobre
        /// </summary>
        /// <param name="documentSetId">Identificador único del sobre</param>
        /// <returns>Objeto <see cref="DocumentSetErrorInfo"/> con el detalle del error</returns>
        public async Task<DocumentSetErrorInfo> GetDocumentSetErrorInfoAsync(string documentSetId)
        {
            var result = await GetAsync<DocumentSetErrorInfo>($"documentset/error/{documentSetId}");

            CheckResponseStatus(result);
            return result.Value;
        }

        /// <summary>
        /// Devuelve la traza de eventos generados por el procesamiento del sobre
        /// </summary>
        /// <param name="documentSetId">Identificador único del sobre</param>
        /// <returns>Colección de objetos <see cref="AuditEvent"/> con el detalle de los eventos</returns>
        public async Task<ICollection<AuditEvent>> GetAuditTrailAsync(string documentSetId)
        {
            var result = await GetAsync<ICollection<AuditEvent>>($"documentset/audittrail/{documentSetId}");

            CheckResponseStatus(result);
            return result.Value;
        }

        /// <summary>
        /// Obtiene el stream con el json firmado
        /// </summary>
        /// <param name="documentSetId">Identificador único del sobre</param>
        /// <returns>Stream con el json firmado (información del sobre + evidencias)</returns>
        public async Task<Stream> GetLegalAuditTrailAsync(string documentSetId)
        {
            var result = await GetFileAsync($"documentset/legalaudittrail/{documentSetId}");

            CheckResponseStatus(result);
            return result.Value;
        }

        /// <summary>
        /// Devuelve el detalle de la definición y estado actual de procesamiento de un sobre
        /// </summary>
        /// <param name="documentSetId">Identificador único del sobre</param>
        /// <returns>Un objeto <see cref="DocumentSetInfo"/> con la información del sobre</returns>
        public async Task<DocumentSetInfo> GetDocumentSetInfoAsync(string documentSetId)
        {
            var result = await GetAsync<DocumentSetInfo>($"documentset/{documentSetId}");

            CheckResponseStatus(result);
            return result.Value;
        }

        /// <summary>
        /// Busca el/los documentsets que coinciden con la referencia externa
        /// </summary>
        /// <param name="documentSetReference">Referencia externa de sobre</param>
        /// <returns>Información del sobre</returns>
        public async Task<ICollection<DocumentSetInfo>> GetDocumentSetsInfoByReferenceAsync(string documentSetReference)
        {
            var result = await GetAsync<ICollection<DocumentSetInfo>>($"documentset/infobyreference/{documentSetReference}");

            CheckResponseStatus(result);
            return result.Value;
        }

        /// <summary>
        /// Recupera un anexo de un sobre
        /// </summary>
        /// <param name="documentSetId">Identificador del sobre</param>
        /// <param name="attachmentId">Identificador del anexo</param>
        /// <returns>Información y contenido del anexo</returns>
        public async Task<FileResult> GetAttachmentAsync(string documentSetId, string attachmentId)
        {
            var result = await GetFileAsync($"documentset/attachment/{documentSetId}/{attachmentId}");

            CheckResponseStatus(result);
            return new FileResult
            {
                Name = result.FileName,
                ContentType = result.ContentType.ToString(),
                Content = result.Value
            };
        }

        /// <summary>
        /// Envía una notificación de prueba al WebHook si está configurado
        /// </summary>
        /// <param name="documentSetId">Identificador único de sobre</param>
        public async Task<bool> TestWebHookAsync(int? documentSetId = null)
        {
            var result = await PostAsync<object>($"webhook/test{(documentSetId.HasValue ? $"/{documentSetId}" : string.Empty)}", null);

            CheckResponseStatus(result);
            return true;
        }

        /// <summary>
        /// Devuelve los dispositivos disponibles.
        /// </summary>
        /// <returns>
        /// Una colección de objetos <see cref="UserDevice"/> que representan los dispositivos disponibles.
        /// </returns>
        public async Task<ICollection<UserDevice>> GetDeviceAsync()
        {
            var result = await GetAsync<ICollection<UserDevice>>($"device");

            CheckResponseStatus(result);
            return result.Value;
        }

        private static void CheckResponseStatus(ApiResponse response)
        {
            if (!response.IsSuccessStatusCode)
            {
                FirmarOnlineRequestException exception;
                if (response.Problem?.ProblemDetails != null)
                {
                    var problemDetails = new ProblemDetails
                    {
                        Detail = response.Problem.ProblemDetails.Detail,
                        Extensions = response.Problem.ProblemDetails.Extensions,
                        Instance = response.Problem.ProblemDetails.Instance,
                        Status = (int)response.Problem.ProblemDetails.Status,
                        Title = response.Problem.ProblemDetails.Title,
                        Type = response.Problem.ProblemDetails.Type
                    };
                    exception = new FirmarOnlineRequestException(problemDetails, response.Problem.ReasonPhrase, response.Problem.RequestUri, response.Problem.RequestMethod);
                }
                else
                {
                    exception = new FirmarOnlineRequestException(response.Problem?.ReasonPhrase, response.Problem?.RequestUri, response.Problem?.RequestMethod);
                }
                exception.Content = response.Problem?.Content;
                throw exception;
            }
        }

        private static async Task<string> Stream2Base64Async(Stream pdfFile)
        {
            string base64;
            using (var memoryStream = new MemoryStream())
            {
                await pdfFile.CopyToAsync(memoryStream);
                base64 = Convert.ToBase64String(memoryStream.ToArray());
            }

            return base64;
        }
    }
}