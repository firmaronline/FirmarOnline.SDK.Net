using FirmarOnline.Clients.PSC;
using System.Diagnostics;

namespace FirmarOnline.Samples.ConsoleClient.PSC
{
    /// <summary>
    /// Descarga el archivo JWT con las trazas legales del sobre.
    /// </summary>
    internal static partial class GetDocumentSetSamples
    {
        /// <summary>
        /// Ejemplo de llamada a la API para obtener el fichero JSON firmado que contiene las trazas legales del procesamiento de un sobre.
        /// </summary>
        /// <remarks>Requiere de un identificador de sobre.</remarks>
        public static async Task GetDocumentSetLegalAuditTrailAsync()
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
            var legalAuditTrail = await client.GetLegalAuditTrailAsync(documentSetId);

            // Guardamos el JWT con las trazas legales del sobre en fichero
            var outputPath = await SampleValues.SaveStreamToFileAsync(legalAuditTrail, $"PSC_{documentSetId}_LegalAuditTrail.pdf");

            MenuService.ShowColoredMessage($"Fichero de evidencias firmado obtenido correctamente:\n\n\t\t\t→ {outputPath}", ConsoleColor.Green);
        }
    }
}
