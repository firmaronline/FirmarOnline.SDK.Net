using FirmarOnline.Clients.PSC;
using System.Diagnostics;

namespace FirmarOnline.Samples.ConsoleClient.PSC
{
    /// <summary>
    /// 
    /// </summary>
    internal static partial class GetDocumentSetSamples
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pscClient"></param>
        /// <param name="documentSetId"></param>
        /// <returns></returns>
        public static async Task GetDocumentSetUrlAsync()
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
            var docViewerUrl = await client.GetDocumentSetUrlAsync(documentSetId);

            MenuService.ShowColoredMessage($"Url del visor: {docViewerUrl}.", ConsoleColor.Green);
        }
    }
}
