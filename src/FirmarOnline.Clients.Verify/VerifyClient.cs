using FirmarOnline.Clients.Common;
using FirmarOnline.Model.Certificates;
using FirmarOnline.Model.Verify;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace FirmarOnline.Clients.Verify
{
    /// <summary>
    /// Cliente para acceso a la API pública de verificación de firma electrónica
    /// </summary>
    public class VerifyClient : ApiClientBase
    {
        /// <summary>
        /// Inicializa una nueva instancia de <see cref="VerifyClient"/>
        /// </summary>
        /// <param name="apiBaseAddress">Url base de la api de verificación de firma</param>
        /// <param name="authenticationToken">Token de autenticación</param>
        public VerifyClient(Uri apiBaseAddress, string authenticationToken)
            : base(apiBaseAddress, authenticationToken) { }

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="VerifyClient"/>
        /// </summary>
        /// <param name="httpClientFactory">Factoría para crear instancias de <see cref="HttpClient"/></param>
        public VerifyClient(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory) { }

        /// <summary>
        /// Url de la API de verificación de documentos firmados en el entorno de pruebas
        /// </summary>
        public static readonly Uri verifySandboxEnvironmentUrl = new("https://restapi.firmar.info/verify/");

        /// <summary>
        /// Url de la API de verificación de documentos firmados en el entorno de producción
        /// </summary>
        public static readonly Uri verifyProductionEnvironmentUrl = new("https://restapi.firmar.online/verify/");

        /// <summary>
        /// Verifica que un certificado de trazabilidad es válido
        /// </summary>
        /// <param name="legalAuditTrailContent">Contenido del certificado de trazabilidad</param>
        /// <returns>Verificación de la firma con extracción de la información del firmante y las trazas</returns>
        public async Task<VerifyLegalAuditTrail> VerifyLegalAuditTrailAsync(string legalAuditTrailContent)
        {
            var result = await PostAsync<LegalAuditTrail, VerifyLegalAuditTrail>(
                "v40/legalAuditTrail",
                new LegalAuditTrail { LegalAuditTrailContent = legalAuditTrailContent });

            CheckResponseStatus(result);
            return result.Value;
        }

        /// <summary>
        /// Verifica las firmas de un documento PDF
        /// </summary>
        /// <param name="pdfFile">Contenido del fichero PDF firmado</param>
        /// <param name="mode">Modo de verificación de firma</param>
        /// <param name="certificate">Certificado para descifrar los datos biométricos</param>
        /// <returns>Información de verificación de las firmas del documento</returns>
        public async Task<DocumentSignatureCollection> VerifySignedPDFAsync(Stream pdfFile, VerifyMode mode, PKCS12Certificate certificate = null)
        {
            var base64 = await Stream2Base64Async(pdfFile);
            return await VerifySignedPDFAsync(base64, mode, certificate);
        }

        /// <summary>
        /// Verifica las firmas de un documento PDF
        /// </summary>
        /// <param name="b64PDFContent">Contenido del fichero PDF firmado en base 64</param>
        /// <param name="mode">Modo de verificación de firma</param>
        /// <param name="certificate">Certificado para descifrar los datos biométricos</param>
        /// <returns>Información de verificación de las firmas del documento</returns>
        public async Task<DocumentSignatureCollection> VerifySignedPDFAsync(string b64PDFContent, VerifyMode mode, PKCS12Certificate certificate = null)
        {
            var result = await PostAsync<VerifyPdfDocument, DocumentSignatureCollection>(
                "v40/pdf",
                new VerifyPdfDocument
                {
                    B64PDFContent = b64PDFContent,
                    Mode = mode,
                    Certificate = certificate
                });

            CheckResponseStatus(result);
            return result.Value;
        }
    }
}
