using FirmarOnline.Clients.eSign;
using FirmarOnline.Model;
using FirmarOnline.Model.Widgets;
using System.Diagnostics;

namespace FirmarOnline.Samples.ConsoleClient.ESign
{
    /// <summary>
    /// Firma de documentos con certificado electrónico.
    /// </summary>
    internal static partial class ESignSamples
    {
        /// <summary>
        /// Ejemplo de llamada a la API para firmar un documento PDF (en base64) con una firma visual configurada mediante un widget.
        /// </summary>
        internal static async Task SignPDFAsync()
        {
            var currentFileName = new StackTrace(true).GetFrame(0)?.GetFileName();
            MenuService.ShowColoredMessage($"Ejecutando código de ejemplo de {Path.GetFileName(currentFileName)}", ConsoleColor.Yellow);

            // Definición de caja de firma a añadir en el documento
            var widget = new FixedWidget // Definición de la caja de firma (FixedWidget, FloatWidget, FieldWidget)
            {
                Page = 1, // Página del documento donde se colocará la caja de firma
                X = 350, // Posición X de la caja de firma en la página
                Y = 100, // Posición Y de la caja de firma en la página
                Width = 150, // Ancho de la caja de firma
                Height = 80, // Alto de la caja de firma
                Rotation = RotationType.Degrees_0, // Rotación de la caja de firma (Degrees_0, Degrees_90, Degrees_180, Degrees_270)
                B64Image = SampleValues.GetSampleFileContentInBase64("widget.jpg"), // Imagen de fondo de la caja de firma en base64 (JPG)
                CustomText = [new() { Text = "Firme aquí" }] // Texto personalizado que se mostrará en la caja de firma
            };

            // Documento PDF en base 64 a firmar
            var b64PDFContent = SampleValues.GetSampleFileContentInBase64("sample_document.pdf");

            // Creación del cliente para acceso a la API
            var client = new ESignClient(
                // Url de la API, se utiliza el entorno de producción o sandbox según la configuración
                apiBaseAddress: SampleValues.IsProduction ? ESignClient.eSignProductionEnvironmentUrl : ESignClient.eSignSandboxEnvironmentUrl,
                // Token de autenticación o api key válida para la url indicada
                authenticationToken: SampleValues.AuthenticationToken);

            // Llamada a la API
            using var stream = await client.SignPDFAsync(b64PDFContent, widget, new SignatureOptions());

            // Guardamos el documento firmado en fichero
            var outputPath = await SampleValues.SaveStreamToFileAsync(stream, "eSign_Document_Signed.pdf");

            MenuService.ShowColoredMessage($"Firma completada correctamente, documento guardado en:\n\n\t\t\t→ {outputPath}", ConsoleColor.Green);
        }
    }
}
