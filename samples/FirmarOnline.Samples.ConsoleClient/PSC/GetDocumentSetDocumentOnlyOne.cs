using FirmarOnline.Clients.PSC;
using System.Diagnostics;

namespace FirmarOnline.Samples.ConsoleClient.PSC
{
    /// <summary>
    /// Descarga la última versión del documento solicitado de un sobre.
    /// </summary>
    internal static partial class GetDocumentSetSamples
    {
        /// <summary>
        /// Ejemplo de llamada a la API para obtener la última versión del documento solicitado de un sobre.
        /// </summary>
        /// <remarks>Requiere de un identificador de sobre y de documento.</remarks>
        public static async Task GetDocumentWhenOnlyOneAsync()
        {
            var currentFileName = new StackTrace(true).GetFrame(0)?.GetFileName();
            MenuService.ShowColoredMessage($"Ejecutando código de ejemplo de {Path.GetFileName(currentFileName)}", ConsoleColor.Yellow);

            // Solicitar un identificador de sobre
            var documentSetId = MenuService.PromptDocumentSetId();

            // Solicitar un identificador del documento
            string documentId = MenuService.PromptDocumentSetDocumentId();

            // Creación del cliente para acceso a la API
            var client = new PSCClient(
                // Url de la API, se utiliza el entorno de producción o sandbox según la configuración
                apiBaseAddress: SampleValues.IsProduction ? PSCClient.PSCProductionEnvironmentUrl : PSCClient.PSCSandboxEnvironmentUrl,
                // Token de autenticación o api key válida para la url indicada
                authenticationToken: SampleValues.AuthenticationToken);

            // Llamada a la API 
            var document = await client.GetDocumentAsync(documentSetId, documentId);

            // Guardamos el documento descargado
            var outputPath = await SampleValues.SaveStreamToFileAsync(document.Content, $"PSC_{documentSetId}_Document_{documentId}.pdf");

            MenuService.ShowColoredMessage($"Documento descargado correctamente:\n\n\t\t\t→ {outputPath}", ConsoleColor.Green);
        }
    }
}
