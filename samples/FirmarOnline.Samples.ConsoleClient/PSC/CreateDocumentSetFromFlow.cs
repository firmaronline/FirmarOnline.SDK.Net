using FirmarOnline.Clients.PSC;
using FirmarOnline.Model.PSC;
using FirmarOnline.Model.Widgets;
using System.Diagnostics;

namespace FirmarOnline.Samples.ConsoleClient.PSC
{
    /// <summary>
    /// Creación de un sobre a partir de los valores de un flujo de trabajo (flow) definido en la plataforma FirmarOnline.
    /// </summary>
    /// 
    internal static partial class CreateDocumentSetSamples
    {
        /// <summary>
        /// Ejemplo de llamada a la API para crear un sobre a partir de un flujo predefinido
        /// </summary>
        /// <remarks>Requiere de un identificador de flujo previamente creado en la plataforma FirmarOnline del entorno utilizado</remarks>
        public static async Task CreateDocumentSetFromFlowAsync()
        {
            var currentFileName = new StackTrace(true).GetFrame(0)?.GetFileName();
            MenuService.ShowColoredMessage($"Ejecutando código de ejemplo de {Path.GetFileName(currentFileName)}", ConsoleColor.Yellow);

            // Solicitar al usuario el identificador del flujo a utilizar
            var flowId = MenuService.PromptFlowTokenId();

            // Definición del sobre de ejemplo
            var documentSet = new DocumentSetFlow
            {
                FlowTokenId = flowId, // Identificador del flujo a utilizar para crear el sobre
                DocumentSetName = "Sobre de ejemplo", // Nombre del sobre
                Description = "Sobre de ejemplo para FirmarOnline SDK", // Descripción del sobre
                Reference = "REFERENCE-00001", // Referencia del sobre, puede ser un número de pedido, factura, etc.

                // Documentos a firmar
                Documents = [ 
                    new Document
                    {
                        Id = "DOC-00001", // Identificador del documento
                        Name = "Documento de ejemplo", // Nombre del documento
                        B64PDFContent = SampleValues.GetSampleFileContentInBase64("sample_document.pdf") // Contenido del documento PDF en Base64
                    }
                ],

                // Información de los destinatarios
                Recipients = [
                    new RecipientFlow
                    {
                        Name = "John Sanders", // Nombre del destinatario
                        Email = "john.sanders@foo.com", // Email del destinatario
                        CardId = "12345678X", // Identificador del destinatario (puede ser un número de documento, NIE, etc.), obligatorio si se utiliza autenticación MRZ
                        PhoneNumber = "", // Número de teléfono del destinatario con el prefijo (Ejemplo: +34600112233), obligatorio si se utiliza una autenticación o acción que lo requiera
                        // Debe definirse una definición de caja de firma por cada documento definido en la propiedad Documents
                        Widgets = [ new RecipientAction {
                            DocumentId = "DOC-00001", // Identificador del documento al que se aplica la definición de la caja de firma
                            Widget = new FixedWidget // Definición de la caja de firma (FixedWidget, FloatWidget, FieldWidget)
                            {
                                Page = 1, // Página del documento donde se colocará la caja de firma
                                X = 200, // Posición X de la caja de firma en la página
                                Y = 100, // Posición Y de la caja de firma en la página
                                Width = 200, // Ancho de la caja de firma
                                Height = 100, // Alto de la caja de firma
                                Rotation = RotationType.Degrees_0, // Rotación de la caja de firma (Degrees_0, Degrees_90, Degrees_180, Degrees_270)
                                CustomText = [new() { Text = "Firme aquí" }] // Texto personalizado que se mostrará en la caja de firma
                            }
                        }]
                        // Se debería indicar el AccessCode si se ha indicado el ActionType como AccessCode y DeviceId si el método de envío es Device
                    }
                ]
            };

            // Creación del cliente para acceso a la API
            var client = new PSCClient(
                // Url de la API, se utiliza el entorno de producción o sandbox según la configuración
                apiBaseAddress: SampleValues.IsProduction ? PSCClient.PSCProductionEnvironmentUrl : PSCClient.PSCSandboxEnvironmentUrl,
                // Token de autenticación o api key válida para la url indicada
                authenticationToken: SampleValues.AuthenticationToken);

            // Llamada a la API para enviar el sobre a firmar
            var documentSetId = await client.PostDocumentSetFlowSimpleAsync(documentSet);

            MenuService.ShowColoredMessage($"Identificador del sobre creado: {documentSetId}", ConsoleColor.Green);
        }
    }
}
