using FirmarOnline.Clients.PSC;
using FirmarOnline.Model.PSC;
using System.Diagnostics;
using System.Text.Json;

namespace FirmarOnline.Samples.ConsoleClient.PSC
{
    /// <summary>
    /// Consulta de sobres enviados a firmar que cumplan los filtros definos.
    /// </summary>
    internal static partial class GetDocumentSetSamples
    {
        /// <summary>
        /// Ejemplo de llamada a la API para obtener una lista de sobres enviados a firmar que cumplan los criterios definidos.
        /// </summary>
        /// <remarks>Este ejemplo devuelve los 2 últimos sobres creados en el último mes, en estado 'En progreso' o 'Completado'
        /// enviados por correo o sms.</remarks>
        /// <returns>Listado de documentos enviados</returns>
        public static async Task GetHistoryAsync()
        {
            var currentFileName = new StackTrace(true).GetFrame(0)?.GetFileName();
            MenuService.ShowColoredMessage($"Ejecutando código de ejemplo de {Path.GetFileName(currentFileName)}", ConsoleColor.Yellow);

            var dateTimeNow = DateTime.Now;

            // Filtros a aplicar
            DocumentSetFilter documentSetFilter = new()
            {   
                Limit = 2, // Número máximo de elementos a devolver
                Offset = 0, // Desplazamiento, número de elementos a saltarse

                // Información del sobre
                FromDateTime = new DateTime(dateTimeNow.Year, dateTimeNow.AddMonths(-1).Month, dateTimeNow.Day), // Fecha creación del sobre desde
                ToDateTime = new DateTime(dateTimeNow.Year, dateTimeNow.Month, dateTimeNow.Day), // Fecha creación del sobre hasta
                Status = [DocumentSetStatusCode.InProcess, DocumentSetStatusCode.Completed], // Códigos de estado de los sobres a recuperar
                SendMethod = [SendMethod.Email, SendMethod.SMS], // Métodos de envió
                //DocumentSetName = "Sobre Demo", // Nombre del sobre
                //Reference = "REF-001", // Referencia externa de sobre para cliente
                //Teams = ["23FA59E5BFAB83750FC096B71393167D", "66558D3FCDE543628AD47059411AEF11"], // Lista de equipos equipos
                
                // Información del destinatario
                //RecipientName = "Primer interviniente", // Nombre del destinatario
                //RecipientEmail = "firmaronline@edatalia.com", // Email del destinatario
                //RecipientCardId = "1234567X", // Identificador del destinatario (puede ser un número de documento, NIE, etc.)
                //RecipientPhoneNumber = "+34600000000", // Número de teléfono del destinatario con el prefijo (Ejemplo: +34600112233)
                //RecipientActionTypes = [RecipientActionType.BioOTPSignature, RecipientActionType.AcceptanceSignature], // Tipos de acciónes del destinatario

                // Información del documento                
                //DocumentName = "DocumentoFirma.pdf" // Nombre del documento
            };

            // Creación del cliente para acceso a la API
            var client = new PSCClient(
                // Url de la API, se utiliza el entorno de producción o sandbox según la configuración
                apiBaseAddress: SampleValues.IsProduction ? PSCClient.PSCProductionEnvironmentUrl : PSCClient.PSCSandboxEnvironmentUrl,
                // Token de autenticación o api key válida para la url indicada
                authenticationToken: SampleValues.AuthenticationToken);

            // Llamada a la API 
            var result = await client.GetHistoryAsync(documentSetFilter);

            MenuService.ShowColoredMessage($"\n{JsonSerializer.Serialize(result, SampleValues.JsonOptionsViewConsole)}", ConsoleColor.Green);
        }
    }
}
