using FirmarOnline.Clients.PSC;
using System.Diagnostics;

namespace FirmarOnline.Samples.ConsoleClient.PSC
{
    /// <summary>
    /// Obtención de documentos adjuntos de un sobre.
    /// </summary>
    internal static partial class GetDocumentSetSamples
    {
        /// <summary>
        /// Ejemplo de llamada a la API para obtener un fichero adjunto de un sobre finalizado.
        /// </summary>
        /// <remarks>Requiere de un de sobre con ficheros adjuntados.</remarks>
        public static async Task GetAttachmentAsync()
        {
            var currentFileName = new StackTrace(true).GetFrame(0)?.GetFileName();
            MenuService.ShowColoredMessage($"Ejecutando código de ejemplo de {Path.GetFileName(currentFileName)}", ConsoleColor.Yellow);

            // Solicitar un identificador de sobre
            var documentSetId = MenuService.PromptDocumentSetId();

            // Solicitar un identificador del fichero adjunto
            string attachmentId = MenuService.PromptDocumentSetAttachmentId();

            // Creación del cliente para acceso a la API
            var client = new PSCClient(
                // Url de la API, se utiliza el entorno de producción o sandbox según la configuración
                apiBaseAddress: SampleValues.IsProduction ? PSCClient.PSCProductionEnvironmentUrl : PSCClient.PSCSandboxEnvironmentUrl,
                // Token de autenticación o api key válida para la url indicada
                authenticationToken: SampleValues.AuthenticationToken);

            // Llamada a la API 
            var document = await client.GetAttachmentAsync(documentSetId, attachmentId);

            // Guardamos el documento adjunto sin extensión
            var outputPath = await SampleValues.SaveStreamToFileAsync(document.Content, $"PSC_{documentSetId}_Attachment_{attachmentId}");

            MenuService.ShowColoredMessage($"Adjunto descargado correctamente:\n\n\t\t\t→ {outputPath}", ConsoleColor.Green);
        }
    }
}
