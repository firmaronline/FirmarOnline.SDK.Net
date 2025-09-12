using FirmarOnline.Clients.PSC;
using System.Diagnostics;
using System.Text.Json;

namespace FirmarOnline.Samples.ConsoleClient.PSC
{
    /// <summary>
    /// Devuelve el detalle de la definición y estado actual de procesamiento de un sobre
    /// </summary>
    internal static partial class GetDocumentSetSamples
    {
        /// <summary>
        /// Ejemplo de llamada a la API para obtener el detalle de la definición y estado actual de procesamiento de un sobre.
        /// </summary>
        /// <remarks>Requiere de un identificador de sobre.</remarks>
        public static async Task GetDocumentSetInfoAsync()
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
            var result = await client.GetDocumentSetInfoAsync(documentSetId);

            MenuService.ShowColoredMessage($"\n{JsonSerializer.Serialize(result, SampleValues.JsonOptionsViewConsole)}", ConsoleColor.Green);
        }
    }
}
