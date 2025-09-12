using FirmarOnline.Clients.PSC;
using FirmarOnline.Model.PSC;
using FirmarOnline.Model.Widgets;
using System.Diagnostics;

namespace FirmarOnline.Samples.ConsoleClient.PSC
{
    /// <summary>
    /// Creación de un sobre simple con un único destinatario y un único documento.
    /// </summary>
    internal static partial class CreateDocumentSetSamples
    {
        /// <summary>
        /// Ejemplo de llamada a la API para crear un sobre con la definición completa que permite incluir varios documentos y destinatarios.
        /// </summary>
        public static async Task CreateDocumentSetAsync()
        {
            var currentFileName = new StackTrace(true).GetFrame(0)?.GetFileName();
            MenuService.ShowColoredMessage($"Ejecutando código de ejemplo de {Path.GetFileName(currentFileName)}", ConsoleColor.Yellow);

            // Definición del sobre de ejemplo
            var documentSet = new DocumentSet
            {
                DocumentSetName = "Sobre de ejemplo", // Nombre del sobre
                Description = "Sobre de ejemplo para FirmarOnline SDK", // Descripción del sobre
                Reference = "REFERENCE-00001", // Referencia del sobre, puede ser un número de pedido, factura, etc.
                SendMethod = SendMethod.Email, // Método de envío del sobre (Email, SMS, etc.)
                ExpirationDaysTimeout = 10, // Días de validez del sobre, después de los cuales se considerará expirado
                //ReminderDays = 1, // Número de días tras los que se enviará un recordatorio
                //Ltv = false, // Indica si la firma debe ser LTV (Long Term Validation)
                //TeamId = "", // Equipo al que va a pertenecer en nuevo sobre, si no se indica, será el equipo por defecto del usuario o empresa
                
                // Emisor
                SenderName = "FirmarOnline SDK", // Nombre del emisor del sobre
                SenderMail = "noreply.sdk@firmar.online", // Email del emisor del sobre

                // Documentos a firmar
                Documents = [ new Document
                {
                    Id = "DOC-00001", // Identificador del documento
                    Name = "Documento de ejemplo", // Nombre del documento
                    B64PDFContent = SampleValues.GetSampleFileContentInBase64("sample_document.pdf") // Contenido del documento PDF en Base64
                }],

                // Información de los destinatarios
                Recipients = [ new Recipient
                {
                    //Id = "REC-00001", // Identificador único del destinatario, si no se informa se generará automáticamente un identificador
                    //Order = 1, // Posición del destinatario en el flujo de firma, si se repite la posición se podrá firmar en paralelo
                    Name = "John Sanders", // Nombre del destinatario
                    Email = "javier.almodovar@edatalia.com", // Email del destinatario
                    CardId = "12345678X", // Identificador del destinatario (puede ser un número de documento, NIE, etc.), obligatorio si se utiliza autenticación MRZ
                    PhoneNumber = "", // Número de teléfono del destinatario con el prefijo (Ejemplo: +34600112233), obligatorio si se utiliza una autenticación o acción que lo requiera
                    AuthType = RecipientAuthenticationType.None, // Tipo de autenticación del destinatario (None, Basic, RecipientAccessCode, etc.)
                    ActionType = RecipientActionType.BioSignature, // Tipo de acción del destinatario (CertifiedNotification, Acceptance, AcceptanceSignature, etc.)
                    //DeviceId = null, // Si el método de envío es Device, indicaremos el dispositivo al que enviaremos el documento a firmar
                    // Debe definirse una definición de caja de firma por cada documento definido en la propiedad Documents
                    Widgets = [ new RecipientAction {
                        DocumentId = "DOC-00001", // Identificador del documento al que se aplica la definición de la caja de firma
                        Widget = new FixedWidget // Definición de la caja de firma (FixedWidget, FloatWidget, FieldWidget)
                        {
                            Page = 1, // Página del documento donde se colocará la caja de firma
                            X = 350, // Posición X de la caja de firma en la página
                            Y = 100, // Posición Y de la caja de firma en la página
                            Width = 200, // Ancho de la caja de firma
                            Height = 100, // Alto de la caja de firma
                            Rotation = RotationType.Degrees_0, // Rotación de la caja de firma (Degrees_0, Degrees_90, Degrees_180, Degrees_270)
                            CustomText = [new() { Text = "Firme aquí" }] // Texto personalizado que se mostrará en la caja de firma
                        }
                    }],

                    // Ver en SampleValues.cs cómo configurar la autenticación por código de acceso
                    // Descomentando la siguiente línea se configurará el RecipientAccessCode si se ha indicado el ActionType como RecipientAccessCode
                    //RecipientAccessCode = SampleValues.RecipientAccessCode,
                    
                    // Ver en SampleValues.cs cómo configurar una lista de ficheros a anexar
                    // Descomentando la siguiente línea se añadirá una lista que indicará los ficheros a anexar antes de firmar desde el visor de documentos
                    //Attachments = SampleValues.Attachments
                }],

                // Ver en SampleValues.cs cómo configurar una firma corporativa
                // Descomentando la siguiente línea tomará la configuración de firma corporativa de SampleValues.cs
                //CorporateSignature = SampleValues.DocumentCorporateSignature,

                // Ver en SampleValues.cs cómo configurar los detinatarios de notificación
                // Descomentando la siguiente línea se añadiran los detinatarios a los que se les enviara una copia del documento firmado
                //Notifications = SampleValues.Notifications
            };

            // Creación del cliente para acceso a la API
            var client = new PSCClient(
                // Url de la API, se utiliza el entorno de producción o sandbox según la configuración
                apiBaseAddress: SampleValues.IsProduction ? PSCClient.PSCProductionEnvironmentUrl : PSCClient.PSCSandboxEnvironmentUrl,
                // Token de autenticación o api key válida para la url indicada
                authenticationToken: SampleValues.AuthenticationToken);

            // Llamada a la API para enviar el sobre a firmar
            var documentSetId = await client.PostDocumentSetAsync(documentSet);

            MenuService.ShowColoredMessage($"Identificador del sobre creado: {documentSetId}", ConsoleColor.Green);
        }
    }
}
