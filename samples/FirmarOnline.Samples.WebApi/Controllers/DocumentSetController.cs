using FirmarOnline.Clients.PSC;
using FirmarOnline.Model.PSC;
using FirmarOnline.Model.Widgets;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace FirmarOnline.Samples.WebApi.Controllers
{
    /// <summary>
    /// Controlador para la gestión de sobres de documentos
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentSetController : ControllerBase
    {
        private readonly PSCClient _pscClient;

        public DocumentSetController(PSCClient pscClient)
        {
            _pscClient = pscClient;
        }

        /// <summary>
        /// Envía un documento para firma biométrica con datos predefinidos usando PostDocumentSetSimpleAsync
        /// </summary>
        /// <returns>ID del sobre creado</returns>
        [HttpPost("create-example")]
        public async Task<object> CreateExampleDocumentSet()
        {
            // Crear el sobre usando PostDocumentSetSimpleAsync
            var documentSet = new SimpleDocumentSetWithSendMethod
            {
                DocumentSetName = "Sobre de ejemplo desde API",
                Description = "Sobre de ejemplo creado desde la API Web de FirmarOnline SDK",
                Reference = "API-EXAMPLE-001",
                SendMethod = SendMethod.Email,
                ExpirationDaysTimeout = 15,
                
                // Información del emisor
                SenderName = "FirmarOnline SDK API",
                SenderMail = "noreply.api@firmar.online",

                // Documento a firmar (usando documento de ejemplo)
                Document = new Document
                {
                    Name = "Documento de ejemplo API.pdf",
                    B64PDFContent = GetSampleDocumentBase64()
                },

                // Información del destinatario
                Recipient = new SingleDocumentRecipient
                {
                    Name = "Jane Doe",
                    Email = "jane.doe@example.com",
                    CardId = "87654321Y",
                    AuthType = RecipientAuthenticationType.None,
                    ActionType = RecipientActionType.BioSignature,
                    Widget = new FixedWidget
                    {
                        Page = 1,
                        X = 350,
                        Y = 100,
                        Width = 200,
                        Height = 100
                    }
                }
            };

            // Llamada al cliente PSC para crear el sobre
            var documentSetId = await _pscClient.PostDocumentSetSimpleAsync(documentSet);

            return new { 
                DocumentSetId = documentSetId,
                Message = "Documento enviado para firma biométrica exitosamente"
            };
        }

        /// <summary>
        /// Obtiene el estado de un sobre por su ID
        /// </summary>
        /// <param name="documentSetId">ID del sobre</param>
        /// <returns>Estado del sobre</returns>
        [HttpGet("status/{documentSetId}")]
        public async Task<object> GetDocumentSetStatus(string documentSetId)
        {
            // Llamada al cliente PSC para obtener el estado del sobre
            var status = await _pscClient.GetDocumentSetStatusAsync(documentSetId);

            return new { 
                DocumentSetId = documentSetId,
                Status = status,
                Message = "Estado obtenido exitosamente"
            };
        }

        /// <summary>
        /// Obtiene el contenido del documento de ejemplo en Base64
        /// </summary>
        /// <returns>Contenido del documento en Base64</returns>
        private static string GetSampleDocumentBase64()
        {
            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream("FirmarOnline.Samples.WebApi.resources.sample_document.pdf")!;
            using var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            return Convert.ToBase64String(memoryStream.ToArray());
        }
    }
}