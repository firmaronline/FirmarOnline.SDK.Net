using FirmarOnline.Clients.eSign;
using System.Diagnostics;

namespace FirmarOnline.Samples.ConsoleClient.ESign
{
    /// <summary>
    /// Añade un sello de tiempo a un documento
    /// </summary>
    internal static partial class ESignSamples
    {
        /// <summary>
        /// Ejemplo de llamada a la API para aplicar un sello de tiempo a un documento PDF (en base64).
        /// </summary>
        internal static async Task TimeStampPdfAsync()
        {
            var currentFileName = new StackTrace(true).GetFrame(0)?.GetFileName();
            MenuService.ShowColoredMessage($"Ejecutando código de ejemplo de {Path.GetFileName(currentFileName)}", ConsoleColor.Yellow);

            // Documento al que se le añadirá un sello de tiempo
            var signedDocument = SampleValues.GetSampleFileContentInBase64("sample_document.pdf");

            // Creación del cliente para acceso a la API
            var client = new ESignClient(
                // Url de la API, se utiliza el entorno de producción o sandbox según la configuración
                apiBaseAddress: SampleValues.IsProduction ? ESignClient.eSignProductionEnvironmentUrl : ESignClient.eSignSandboxEnvironmentUrl,
                // Token de autenticación o api key válida para la url indicada
                authenticationToken: SampleValues.AuthenticationToken);

            // Llamada a la API
            using var stream = await client.TimeStampAsync(signedDocument);

            // Guardamos el documento con el sello del tiempo en fichero
            var outputPath = await SampleValues.SaveStreamToFileAsync(stream, "eSign_Document_TimeStamp.pdf");

            MenuService.ShowColoredMessage($"Sello de tiempo aplicado correctamente, documento guardado en:\n\n\t\t\t→ {outputPath}", ConsoleColor.Green);
        }
    }
}
