using FirmarOnline.Clients.PSC;
using System.Diagnostics;

namespace FirmarOnline.Samples.ConsoleClient.PSC
{
    /// <summary>
    /// Borrado manual de documentos de un sobre finalizado.
    /// </summary>
    internal static partial class PutDocumentSetSamples
    {
        /// <summary>
        /// Ejemplo de llamada a la API para borrar los documentos firmados de un sobre finalizado.
        /// </summary>
        /// <remarks>Requiere de un identificador de sobre finalizado.</remarks>
        public static async Task PurgeDocumentSetAsync()
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
            await client.PurgeDocumentSetAsync(documentSetId);

            MenuService.ShowColoredMessage($"Documentos borrados correctamente.", ConsoleColor.Green);
        }
    }
}
