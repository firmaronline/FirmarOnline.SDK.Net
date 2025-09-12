using FirmarOnline.Clients.PSC;
using System.Diagnostics;
using System.Text.Json;

namespace FirmarOnline.Samples.ConsoleClient.PSC
{
    /// <summary>
    /// Listado de dispositivos de un usuario
    /// </summary>
    internal static partial class GetDocumentSetSamples
    {
        /// <summary>
        /// Ejemplo de llamada a la API para obtener los dispositivos dados de alta para un usuario
        /// </summary>
        public static async Task GetDevicesAsync()
        {
            var currentFileName = new StackTrace(true).GetFrame(0)?.GetFileName();
            MenuService.ShowColoredMessage($"Ejecutando código de ejemplo de {Path.GetFileName(currentFileName)}", ConsoleColor.Yellow);

            // Creación del cliente para acceso a la API
            var client = new PSCClient(
                // Url de la API, se utiliza el entorno de producción o sandbox según la configuración
                apiBaseAddress: SampleValues.IsProduction ? PSCClient.PSCProductionEnvironmentUrl : PSCClient.PSCSandboxEnvironmentUrl,
                // Token de autenticación o api key válida para la url indicada
                authenticationToken: SampleValues.AuthenticationToken);

            // Llamada a la API 
            var result = await client.GetDeviceAsync();

            MenuService.ShowColoredMessage($"\n{JsonSerializer.Serialize(result, SampleValues.JsonOptionsViewConsole)}", ConsoleColor.Green);
        }
    }
}
