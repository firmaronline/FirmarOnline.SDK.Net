using FirmarOnline.Clients.PSC;
using System.Diagnostics;

namespace FirmarOnline.Samples.ConsoleClient.PSC
{
    /// <summary>
    /// Llamada al WebHook definido a nivel de empresa en la plataforma FirmarOnline.
    /// </summary>
    internal static partial class PutWebHookSamples
    {
        /// <summary>
        /// Ejemplo de llamada a la API para enviar un evento de sobre al WebHook configurado a nivel de empresa.
        /// </summary>
        /// <remarks>Requiere tener configurada desde plataforma de FirmarOnline en la página "Integración API" la URL del 
        /// WebHook donde enviar los eventos.</remarks>
        public static async Task TestWebHookAsync()
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
            await client.TestWebHookAsync();

            MenuService.ShowColoredMessage($"Llamada al WebHook ejecutada correctamente.", ConsoleColor.Green);
        }
    }
}
