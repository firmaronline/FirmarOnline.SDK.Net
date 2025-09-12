using FirmarOnline.Clients.eSign;
using FirmarOnline.Model.eSign;
using System.Diagnostics;

namespace FirmarOnline.Samples.ConsoleClient.ESign
{
    /// <summary>
    /// Envía un OTP por SMS, según el estandar RFC 6238 (TOTP)
    /// </summary>
    internal static partial class ESignSamples
    {
        /// <summary>
        /// Ejemplo de llamada a la API para enviar un código OTP (One-Time Password) al firmante asociado a un hash y sesión.
        /// </summary>
        internal static async Task GenerateOtpAsync()
        {
            var currentFileName = new StackTrace(true).GetFrame(0)?.GetFileName();
            MenuService.ShowColoredMessage($"Ejecutando código de ejemplo de {Path.GetFileName(currentFileName)}", ConsoleColor.Yellow);

            // Solicitar el nº de teléfono al que enviar el código OTP
            var phoneNumber = MenuService.PromptPhoneNumber();

            var generateOtpData = new GenerateOtp
            {
                Hash = "b221d9dbb083a7f33428d7c2a3c3198ae925614d70210e28716ccaa7cd4ddb79", // Código hash SHA256 del pdf para el cálculo de hashes del OTP
                SessionCode = "String_123456", // Código de sesión para el cálculo de hashes del OTP
                UserId = "String_430", // Identificador de usuario para el cálculo de hashes del OTP
                PhoneNumber = phoneNumber,  // N.º de teléfono al que se enviará el SMS con el código OTP
                From = "Firmaonline", // Emisor del SMS
                Text = "Hola! Introduzca FO-##OTP## para firmar." // Texto del SMS, usar ##OTP## para poner el OTP
            };

            // Creación del cliente para acceso a la API
            var client = new ESignClient(
                // Url de la API, se utiliza el entorno de producción o sandbox según la configuración
                apiBaseAddress: SampleValues.IsProduction ? ESignClient.eSignProductionEnvironmentUrl : ESignClient.eSignSandboxEnvironmentUrl,
                // Token de autenticación o api key válida para la url indicada
                authenticationToken: SampleValues.AuthenticationToken);

            // Llamada a la API
            await client.GenerateOtpAsync(generateOtpData);

            MenuService.ShowColoredMessage($"Código OTP generado correctamente.", ConsoleColor.Green);
        }
    }
}
