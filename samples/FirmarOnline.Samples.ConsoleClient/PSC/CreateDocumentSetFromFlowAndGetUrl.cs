using FirmarOnline.Clients.PSC;
using FirmarOnline.Model.PSC;
using FirmarOnline.Model.Widgets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirmarOnline.Samples.ConsoleClient.PSC
{
    /// <summary>
    /// Creación de un sobre a partir de los valores de un flujo de trabajo (flow) definido en la plataforma FirmarOnline.
    /// </summary>
    /// 
    internal static partial class CreateDocumentSetSamples
    {
        /// <summary>
        /// Ejemplo de llamada a la API para crear un sobre a partir de un flujo predefinido recuperando la url para acceder al visor de firma.
        /// </summary>
        /// <remarks>Requiere de un identificador de flujo previamente creado en la plataforma FirmarOnline del entorno utilizado</remarks>
        public static async Task CreateDocumentSetFromFlowAndGetUrlAsync()
        {
            var currentFileName = new StackTrace(true).GetFrame(0)?.GetFileName();
            MenuService.ShowColoredMessage($"Ejecutando código de ejemplo de {Path.GetFileName(currentFileName)}", ConsoleColor.Yellow);

            // Solicitar al usuario el identificador del flujo a utilizar
            var flowId = MenuService.PromptFlowTokenId();

            // Definición del sobre de ejemplo
            var documentSet = new DocumentSetFlowUrlWithOverrides
            {
                FlowTokenId = flowId, // Identificador del flujo a utilizar para crear el sobre
                DocumentSetName = "Sobre de ejemplo", // Nombre del sobre
                Description = "Sobre de ejemplo para FirmarOnline SDK", // Descripción del sobre
                Reference = "REFERENCE-00001", // Referencia del sobre, puede ser un número de pedido, factura, etc.
                TeamId = "", // Equipo al que va a pertenecer en nuevo sobre, si no se indica, será el equipo por defecto del usuario o empresa

                // Campos que se pueden sobreescribir del flujo
                SenderName = "FirmarOnline SDK", // Nombre del emisor del sobre
                SenderMail = "noreply.sdk@firmar.online", // Email del emisor del sobre
                Language = Model.LanguageCode.es_ES, // Idioma de flujo
                ExpirationDaysTimeout = 10, // Días de validez del sobre, después de los cuales se considerará expirado
                ReminderDays = 1, // Número de días tras los que se enviará un recordatorio
                SendDocToRecipient = false, // Envío a los destinatarios de los documentos del proceso al final del flujo
                SendDocToSender = true, // Envío al remitente de los documentos y el certificado de trazabilidad del proceso al final del flujo
                Ltv = false, // Indica si la firma debe ser LTV (Long Term Validation)
                CorporateSignature = SampleValues.DocumentCorporateSignature, // Ejemplo de firma corporativa
                Notifications = SampleValues.Notifications, // Ejemplo para configurar destinatarios a los que se enviará una copia firmada de los documentos
                AuthenticationType = RecipientAuthenticationType.None, // Tipo de autenticación del destinatario (None, Basic, RecipientAccessCode, etc.)
                ActionType = RecipientActionType.BioSignature, // // Tipo de acción del destinatario (CertifiedNotification, Acceptance, AcceptanceSignature, etc.)
                AccessCode = SampleValues.AccessCode, // Si la autenticación es por código de acceso, configuramos la pregunta y opcinalmente el formato de la respuesta

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
                        Id = "REC-00001", // Identificador único del destinatario, si no se informa se generará automáticamente un identificador
                        Name = "John Sanders", // Nombre del destinatario
                        Email = "john.sanders@foo.com", // Email del destinatario
                        CardId = "12345678X", // Identificador del destinatario (puede ser un número de documento, NIE, etc.), obligatorio si se utiliza autenticación MRZ
                        PhoneNumber = "", // Número de teléfono del destinatario con el prefijo (Ejemplo: +34600112233), obligatorio si se utiliza una autenticación o acción que lo requiera
                        DeviceId = null, // Si el método de envío es Device, indicaremos el dispositivo al que enviaremos el documento a firmar
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
                        }],
                        // Ver en SampleValues.cs cómo configurar la autenticación por código de acceso
                        // Descomentando la siguiente línea se configurará el RecipientAccessCode si se ha indicado el ActionType como RecipientAccessCode
                        AccessCode = SampleValues.RecipientAccessCode,                    
                        // Ver en SampleValues.cs cómo configurar una lista de ficheros a anexar
                        // Descomentando la siguiente línea se añadirá una lista que indicará los ficheros a anexar antes de firmar desde el visor de documentos
                        Attachments = SampleValues.Attachments                        
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
            var createdDocumentSet = await client.PostDocumentSetFlowAndGetUrlAsync(documentSet);

            MenuService.ShowColoredMessage($"Identificador del sobre creado: {createdDocumentSet.Id}", ConsoleColor.Green);
            MenuService.ShowColoredMessage($"Url del sobre creado: {createdDocumentSet.Url}", ConsoleColor.Green);
        }
    }
}
