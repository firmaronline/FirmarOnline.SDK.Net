using FirmarOnline.Clients.PSC;
using System.Diagnostics;

namespace FirmarOnline.Samples.ConsoleClient.PSC
{
    /// <summary>
    /// Obtención de las trazas de eventos generados por el procesamiento del sobre.
    /// </summary>
    internal static partial class GetDocumentSetSamples
    {
        /// <summary>
        /// Ejemplo de llamada a la API para obtener las trazas de eventos generados por el procesamiento del sobre.
        /// </summary>
        /// <remarks>Requiere de un identificador de sobre.</remarks>
        public static async Task GeDocumentSetAuditTrailAsync()
        {
            var currentFileName = new StackTrace(true).GetFrame(0)?.GetFileName();
            MenuService.ShowColoredMessage($"Ejecutando código de ejemplo de {Path.GetFileName(currentFileName)}", ConsoleColor.Yellow);

            // Solicitar un identificador de sobre
            var documentSetId = MenuService.PromptDocumentSetId();

            // Creación del cliente para acceso a la API
            var client = new PSCClient(
                // Url de la API, se utiliza el entorno de producción o sandbox según la configuración
                apiBaseAddress: SampleValues.IsProduction ? PSCClient.PSCProductionEnvironmentUrl : PSCClient.PSCSandboxEnvironmentUrl,
                // Token de autenticación o api key válida para la url indicada
                authenticationToken: SampleValues.AuthenticationToken);

            // Llamada a la API 
            var result = await client.GetAuditTrailAsync(documentSetId);

            // Guardamos el JSON con los eventos generados por el procesamiento del sobre
            var outputPath = await SampleValues.SaveJsonToFileAsync(result, $"PSC_{documentSetId}_AuditEvents.json");

            MenuService.ShowColoredMessage($"Fichero de eventos generado correctamente:\n\n\t\t\t→ {outputPath}", ConsoleColor.Green);
        }
    }
}
