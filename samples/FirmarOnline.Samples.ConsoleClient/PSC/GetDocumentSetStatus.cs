using FirmarOnline.Clients.PSC;
using System.Diagnostics;
using System.Text.Json;

namespace FirmarOnline.Samples.ConsoleClient.PSC
{
    /// <summary>
    /// Consulta el estado actual de un sobre utilizando su identificador.
    /// </summary>
    internal static partial class GetDocumentSetSamples
    {
        /// <summary>
        /// Ejemplo de llamada a la API para obtener El estado actual de un sobre.
        /// </summary>
        public static async Task GetDocumentSetStatusAsync()
        {
            var currentFileName = new StackTrace(true).GetFrame(0)?.GetFileName();
            MenuService.ShowColoredMessage($"Ejecutando código de ejemplo de {Path.GetFileName(currentFileName)}", ConsoleColor.Yellow);

            // Solicitar al usuario un identificador de sobre
            var documentSetId = MenuService.PromptDocumentSetId();

            // Creación del cliente para acceso a la API
            var client = new PSCClient(
                // Url de la API, se utiliza el entorno de producción o sandbox según la configuración
                apiBaseAddress: SampleValues.IsProduction ? PSCClient.PSCProductionEnvironmentUrl : PSCClient.PSCSandboxEnvironmentUrl,
                // Token de autenticación o api key válida para la url indicada
                authenticationToken: SampleValues.AuthenticationToken);

            // Llamada a la API 
            var status = await client.GetDocumentSetStatusAsync(documentSetId);

            MenuService.ShowColoredMessage($"Estado del sobre: {status}.", ConsoleColor.Green);
        }
    }
}
