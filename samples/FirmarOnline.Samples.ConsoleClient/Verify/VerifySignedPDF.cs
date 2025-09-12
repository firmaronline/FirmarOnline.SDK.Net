using FirmarOnline.Clients.Verify;
using FirmarOnline.Model.Verify;
using System.Diagnostics;

namespace FirmarOnline.Samples.ConsoleClient.Verify
{
    /// <summary>
    /// Verificación de firmas digitales en un documento PDF.
    /// </summary>
    internal static partial class VerifySamples
    {
        /// <summary>
        /// Ejemplo de llamada a la API para verificar las firmas digitales en un documento PDF codificado en base64.
        /// </summary>
        internal static async Task VerifySignedPDFAsync()
        {
            var currentFileName = new StackTrace(true).GetFrame(0)?.GetFileName();
            MenuService.ShowColoredMessage($"Ejecutando código de ejemplo de {Path.GetFileName(currentFileName)}", ConsoleColor.Yellow);

            // Documento PDF en base 64 a firmardo a verificar
            var signedDocument = SampleValues.GetSampleFileContentInBase64("signed_document.pdf");

            // Creación del cliente para acceso a la API
            var client = new VerifyClient(
                // Url de la API, se utiliza el entorno de producción o sandbox según la configuración
                apiBaseAddress: SampleValues.IsProduction ? VerifyClient.verifyProductionEnvironmentUrl : VerifyClient.verifySandboxEnvironmentUrl,
                // Token de autenticación o api key válida para la url indicada
                authenticationToken: SampleValues.AuthenticationToken);

            // Llamada a la API
            var response = await client.VerifySignedPDFAsync(signedDocument, VerifyMode.DigitalSignature);

            // Guardamos el JSON con la información de las firmas en fichero
            var outputPath = await SampleValues.SaveJsonToFileAsync(response, "Verify_Document_Signatures.json");

            MenuService.ShowColoredMessage($"Verificación completada correctamente, resultado guardado en:\n\n\t\t\t→ {outputPath}", ConsoleColor.Green);
        }
    }
}
