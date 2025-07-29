using FirmarOnline.Clients.Common;
using FirmarOnline.Model;
using FirmarOnline.Model.eSign;
using FirmarOnline.Model.Widgets;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace FirmarOnline.Clients.eSign
{
    /// <summary>
    /// Cliente para acceso a la API pública de Firma Avanzada
    /// </summary>
    public class ESignClient : ApiClientBase
    {
        /// <summary>
        /// Inicializa una nueva instancia de <see cref="ESignClient"/>
        /// </summary>
        /// <param name="apiBaseAddress">Url base de la api del eSign</param>
        /// <param name="authenticationToken">Token de autenticación o api key válida para la url indicada</param>
        public ESignClient(Uri apiBaseAddress, string authenticationToken)
            : base(apiBaseAddress, authenticationToken) { }

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="ESignClient"/>
        /// </summary>
        /// <param name="httpClientFactory">Factoría para crear instancias de <see cref="HttpClient"/></param>
        protected ESignClient(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory) { }

        /// <summary>
        /// Url de la API del eSign en el entorno de pruebas
        /// </summary>
        public static readonly Uri eSignSandboxEnvironmentUrl = new("https://restapi.firmar.info/esign");

        /// <summary>
        /// Url de la API del eSign en el entorno de producción
        /// </summary>
        public static readonly Uri eSignProductionEnvironmentUrl = new("https://restapi.firmar.online/esign");

        /// <summary>
        /// Firma un documento con certificado electrónico
        /// </summary>
        /// <param name="pdfFile">Contenido del fichero PDF</param>
        /// <param name="widget">Caja de firma</param>
        /// /// <param name="options">Opciones de firma</param>
        /// <returns>El documento firmado</returns>
        public async Task<Stream> SignPDFAsync(Stream pdfFile, Widget widget = null, SignatureOptions options = null)
        {
#if NET6_0_OR_GREATER
            ArgumentNullException.ThrowIfNull(pdfFile);
#else
            if (pdfFile == null)
            {
                throw new ArgumentNullException(nameof(pdfFile));
            }
#endif
            string base64 = await Stream2Base64Async(pdfFile);
            return await SignPDFAsync(base64, widget, options);
        }

        /// <summary>
        /// Firma un documento con certificado electrónico
        /// </summary>
        /// <param name="b64PDFContent">Contenido del fichero PDF en base 64</param>
        /// <param name="widget">Caja de firma</param>
        /// <param name="options">Opciones de firma</param>
        /// <returns>El documento firmado</returns>
        public async Task<Stream> SignPDFAsync(string b64PDFContent, Widget widget = null, SignatureOptions options = null)
        {
#if NET6_0_OR_GREATER
            ArgumentNullException.ThrowIfNullOrWhiteSpace(b64PDFContent);
#else
            if (string.IsNullOrWhiteSpace(b64PDFContent))
            {
                throw new ArgumentException("PDF content cannot be null or empty.", nameof(b64PDFContent));
            }
#endif
            var result = await PostAndGetFileAsync("v40/", new Signature
            {
                B64PDFContent = b64PDFContent,
                Widget = widget,
                Options = options
            });

            CheckResponseStatus(result);
            return result.Value;
        }

        /// <summary>
        /// Aplica un sello de tiempo a un documento firmado
        /// </summary>
        /// <param name="pdfFile">Contenido del fichero PDF</param>
        /// <returns>El documento PDF con el sello de tiempo</returns>
        public async Task<Stream> TimeStampAsync(Stream pdfFile)
        {
            var base64 = await Stream2Base64Async(pdfFile);
            return await TimeStampAsync(base64);
        }

        /// <summary>
        /// Aplica un sello de tiempo a un documento firmado
        /// </summary>
        /// <param name="b64PDFContent">Contenido del fichero PDF en base 64</param>
        /// <returns>El documento PDF con el sello de tiempo</returns>
        public async Task<Stream> TimeStampAsync(string b64PDFContent)
        {
            var result = await PostAndGetFileAsync("v40/timestamp", new Timestamp
            {
                B64PDFContent = b64PDFContent
            });

            CheckResponseStatus(result);
            return result.Value;
        }

        /// <summary>
        /// Genera y envía un nuevo OTP (One-Time Password).
        /// </summary>
        /// <param name="generateOTP">Datos para la geneción del OTP.</param>
        public async Task GenerateOtpAsync(GenerateOTP generateOTP)
        {
            var result = await PostAsync("v40/GenerateOTP", generateOTP);
            CheckResponseStatus(result);
        }

        /// <summary>
        /// Validación de un código OTP (One-Time Password).
        /// </summary>
        /// <param name="validateOTP">Datos para la validación del OTP.</param>
        public async Task ValidateOtpAsync(ValidateOTP validateOTP)
        {
            var result = await PostAsync("v40/ValidateOTP", validateOTP);
            CheckResponseStatus(result);
        }
    }
}