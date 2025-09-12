using FirmarOnline.Clients.eSign;
using FirmarOnline.Model.eSign;
using System.Diagnostics;

namespace FirmarOnline.Samples.ConsoleClient.ESign
{
    /// <summary>
    /// Valida un OTP, según el estandar RFC 6238 (TOTP).
    /// </summary>
    internal static partial class ESignSamples
    {
        /// <summary>
        /// Ejemplo de llamada a la API para validar un código OTP previamente recibido por el firmante, relacionado con el documento a firmar.
        /// </summary>
        internal static async Task ValidateOtpAsync()
        {
            var currentFileName = new StackTrace(true).GetFrame(0)?.GetFileName();
            MenuService.ShowColoredMessage($"Ejecutando código de ejemplo de {Path.GetFileName(currentFileName)}", ConsoleColor.Yellow);

            // Solicitar al usuario el OTP a validar
            var validateOTP = MenuService.PromptValidateOTP();

            var validateOTPData = new ValidateOtp
            {
                Hash = "b221d9dbb083a7f33428d7c2a3c3198ae925614d70210e28716ccaa7cd4ddb79", // Código hash SHA256 usado en la generación
                SessionCode = "String_123456", // Código de sesión usado en la generación
                UserId = "String_430", // Identificador de usuario usado en la generación
                Otp = validateOTP // Código OTP a validar
            };

            // Creación del cliente para acceso a la API
            var client = new ESignClient(
                // Url de la API, se utiliza el entorno de producción o sandbox según la configuración
                apiBaseAddress: SampleValues.IsProduction ? ESignClient.eSignProductionEnvironmentUrl : ESignClient.eSignSandboxEnvironmentUrl,
                // Token de autenticación o api key válida para la url indicada
                authenticationToken: SampleValues.AuthenticationToken);

            // Llamada a la API
            await client.ValidateOtpAsync(validateOTPData);

            MenuService.ShowColoredMessage($"Código OTP validado correctamente.", ConsoleColor.Green);
        }
    }
}
