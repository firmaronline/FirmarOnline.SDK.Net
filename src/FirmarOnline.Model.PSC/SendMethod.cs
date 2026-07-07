using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Método de envío.
    /// </summary>
    public enum SendMethod
    {
        /// <summary>
        /// Ninguno
        /// </summary>
        [Display(Name = "Ninguno")]
        None = 0,

        /// <summary>
        /// Email
        /// </summary>
        [Display(Name = "Email")]
        Email = 1,

        /// <summary>
        /// SMS
        /// </summary>
        [Display(Name = "SMS")]
        SMS = 2,

        /// <summary>
        /// WhatsApp
        /// </summary>
        [Display(Name = "WhatsApp")]
        WhatsApp = 3,

        /// <summary>
        /// Dispositivo.
        /// </summary>
        [Display(Name = "Device")]
        Device = 4
    }

    /// <summary>
    /// Métodos de extensión para <see cref="SendMethod"/>
    /// </summary>
    public static class SendMethodExtensions
    {
        /// <summary>
        /// Lista con todos los métodos de envío que no son a dispositivo
        /// </summary>
        private static readonly SendMethod[] _sendMethodsToCheck = [SendMethod.None, SendMethod.SMS, SendMethod.Email, SendMethod.WhatsApp];

        /// <summary>
        /// Indica si el <see cref="SendMethod"/> requiere verificación del teléfono.
        /// </summary>
        /// <param name="sendMethod"><see cref="SendMethod"/> a comprobar.</param>
        /// <returns>True si utiliza verificación del teléfono, en otro caso devuelve False</returns>
        public static bool RequiresPhoneVerification(this SendMethod sendMethod)
        {
            return sendMethod == SendMethod.SMS || sendMethod == SendMethod.WhatsApp;
        }

        /// <summary>
        /// Indica si el <see cref="SendMethod"/> utiliza envío por email.
        /// </summary>
        /// <param name="sendMethod"><see cref="SendMethod"/> a comprobar.</param>
        /// <returns>True si utiliza envío por email, en otro caso devuelve False.</returns>
        public static bool RequiresEmailDelivery(this SendMethod sendMethod)
        {
            return sendMethod == SendMethod.Email;
        }

        /// <summary>
        /// Indica si la lista de <see cref="SendMethod"/> contiene todos los metodos de
        /// envío que no son a dispositivo.
        /// </summary>
        /// <param name="sendMethod"><see cref="SendMethod"/> a comprobar.</param>
        /// <returns>True se indican todos los métodos de envío que no son a dispotivo,
        /// en otro caso devuelve False</returns>
        public static bool IsAllNotDevice(this SendMethod[] sendMethod)
        {
            return _sendMethodsToCheck.All(method => sendMethod.Contains(method));
        }

        /// <summary>
        /// Obtiene la lista de <see cref="SendMethod"/> que no son a dispositivo
        /// </summary>
        public static SendMethod[] GetAllNotDevice()
        {
            return _sendMethodsToCheck;
        }
    }
}