using FirmarOnline.Clients.Common;
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
        /// <summary>
        /// Inicializa una nueva instancia de <see cref="PSCClient"/>
        /// </summary>
        /// <param name="apiBaseAddress">Url base de la api del PSC</param>
        /// <param name="authenticationToken">Token de autenticación</param>
        public PSCClient(Uri apiBaseAddress, string authenticationToken)
            : base(apiBaseAddress, authenticationToken) { }

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="PSCClient"/>
        /// </summary>
        /// <param name="httpClientFactory">Factoría para crear instancias de <see cref="HttpClient"/></param>
        public PSCClient(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory) { }

        /// <summary>
        /// Url de la API del PSC en el entorno de pruebas
        /// </summary>
        public static readonly Uri PSCSandboxEnvironmentUrl = new("https://restapi.firmar.info/psc/");

        /// <summary>
        /// Url de la API del PSC en el entorno de producción
        /// </summary>
        public static readonly Uri PSCProductionEnvironmentUrl = new("https://restapi.firmar.online/psc/");


        /// <summary>
        /// Recupera el estado actual de un sobre
        /// </summary>
        /// <param name="documentSetId">Identificador único del sobre</param>
        /// <returns>Un objeto <see cref="DocumentSetStatusCode"/> con el estado del sobre</returns>
        public async Task<DocumentSetStatusCode> GetDocumentSetStatusAsync(string documentSetId)
        {
            var result = await GetAsync<DocumentSetStatusCode>($"v40/documentset/status/{documentSetId}");

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
            var result = await GetFileAsync($"v40/documentset/evidences/{documentSetId}");

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
            var url = $"v41/documentset/document/{documentSetId}{(string.IsNullOrEmpty(documentId) ? string.Empty : $"/{documentId}")}";
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
            var result = await PutAsync($"v40/documentset/resend/{documentSetId}");

            CheckResponseStatus(result);
        }

        /// <summary>
        /// Cancela el procesamiento de un sobre
        /// </summary>
        /// <param name="documentSetId">Identificador único del sobre</param>
        public async Task CancelDocumentSetAsync(string documentSetId)
        {
            var result = await PutAsync($"v40/documentset/cancel/{documentSetId}");

            CheckResponseStatus(result);
        }

        /// <summary>
        /// Purga los documentos de un sobre finalizado.
        /// </summary>
        /// <param name="documentSetId">Identificador único del sobre</param>
        public async Task PurgeDocumentSetAsync(string documentSetId)
        {
            var result = await PutAsync($"v40/documentset/purge/{documentSetId}");

            CheckResponseStatus(result);
        }

        /// <summary>
        /// Recupera la URL de acceso al sobre para los creados a través de NewDocumentSetUrl
        /// </summary>
        /// <param name="documentSetId">Identificador único del sobre</param>
        /// <returns>Url del sobre</returns>
        public async Task<string> GetDocumentSetUrlAsync(string documentSetId)
        {
            var result = await GetAsync<string>($"v40/documentset/url/{documentSetId}");

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
            var result = await GetAsync<DocumentSetErrorInfo>($"v40/documentset/error/{documentSetId}");

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
            var result = await GetAsync<ICollection<AuditEvent>>($"v40/documentset/audittrail/{documentSetId}");

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
            var result = await GetFileAsync($"v40/documentset/legalaudittrail/{documentSetId}");

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
            var result = await GetAsync<DocumentSetInfo>($"v40/documentset/{documentSetId}");

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
            var result = await GetAsync<ICollection<DocumentSetInfo>>($"v40/documentset/infobyreference/{documentSetReference}");

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
            var result = await GetFileAsync($"v40/documentset/attachment/{documentSetId}/{attachmentId}");

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
            var result = await PostAsync<object>($"v40/webhook/test{(documentSetId.HasValue ? $"/{documentSetId}" : string.Empty)}", null);

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
            var result = await GetAsync<ICollection<UserDevice>>($"v40/device");

            CheckResponseStatus(result);
            return result.Value;
        }
    }
}