using FirmarOnline.Clients.Verify;
using System.Diagnostics;

namespace FirmarOnline.Samples.ConsoleClient.Verify
{
    /// <summary>
    /// Verificación de certificados de trazabilidad
    /// </summary>
    internal static partial class VerifySamples
    {
        /// <summary>
        /// Ejemplo de llamada a la API para verificar la validez de un certificado de trazabilidad (legal audit trail).
        /// </summary>
        /// <returns></returns>
        internal static async Task VerifyLegalAuditTrailAsync()
        {
            var currentFileName = new StackTrace(true).GetFrame(0)?.GetFileName();
            MenuService.ShowColoredMessage($"Ejecutando código de ejemplo de {Path.GetFileName(currentFileName)}", ConsoleColor.Yellow);

            // JWT con las evidencias de un sobre a verificar
            var legalAuditTrailContent = SampleValues.GetSampleFileContent("legal_audit_trail.jws");

            // Creación del cliente para acceso a la API
            var client = new VerifyClient(
                // Url de la API, se utiliza el entorno de producción o sandbox según la configuración
                apiBaseAddress: SampleValues.IsProduction ? VerifyClient.verifyProductionEnvironmentUrl : VerifyClient.verifySandboxEnvironmentUrl,
                // Token de autenticación o api key válida para la url indicada
                authenticationToken: SampleValues.AuthenticationToken);

            // Llamada a la API
            var response = await client.VerifyLegalAuditTrailAsync(legalAuditTrailContent);

            // Guardamos el JSON con el resumen de verificación de certificado de trazabilidad
            var outputPath = await SampleValues.SaveJsonToFileAsync(response, "Verify_LegalAuditTrail.json");

            MenuService.ShowColoredMessage($"Verificación completada correctamente, resultado guardado en:\n\n\t\t\t→ {outputPath}", ConsoleColor.Green);
        }
    }
}
