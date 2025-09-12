using FirmarOnline.Clients.PSC;
using System.Diagnostics;
using System.Text.Json;

namespace FirmarOnline.Samples.ConsoleClient.PSC
{
    /// <summary>
    /// Busca el/los sobres que coinciden con la referencia externa, devuelve el detalle de la definición y estado actual de procesamiento
    /// </summary>
    internal static partial class GetDocumentSetSamples
    {
        /// <summary>
        /// Ejemplo de llamada a la API para obtener el detalle de los sobres que tengan la referencia externa informada.
        /// </summary>
        /// <remarks>Requiere de sobres que tengan informado la referencia a partir de la cual se buscara.</remarks>
        public static async Task GetDocumentSetsInfoByReferenceAsync()
        {
            var currentFileName = new StackTrace(true).GetFrame(0)?.GetFileName();
            MenuService.ShowColoredMessage($"Ejecutando código de ejemplo de {Path.GetFileName(currentFileName)}", ConsoleColor.Yellow);

            // Solicitar una referencia externa
            string reference = MenuService.PromptDocumentSetReference();

            // Creación del cliente para acceso a la API
            var client = new PSCClient(
                // Url de la API, se utiliza el entorno de producción o sandbox según la configuración
                apiBaseAddress: SampleValues.IsProduction ? PSCClient.PSCProductionEnvironmentUrl : PSCClient.PSCSandboxEnvironmentUrl,
                // Token de autenticación o api key válida para la url indicada
                authenticationToken: SampleValues.AuthenticationToken);

            // Llamada a la API 
            var result = await client.GetDocumentSetsInfoByReferenceAsync(reference);

            MenuService.ShowColoredMessage($"\n{JsonSerializer.Serialize(result, SampleValues.JsonOptionsViewConsole)}", ConsoleColor.Green);
        }
    }
}
