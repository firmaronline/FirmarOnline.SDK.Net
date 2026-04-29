namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Información de un paso de autenticación.
    /// </summary>
    public class AuthenticationStepInfo
    {
        /// <summary>
        /// Tipo de autenticación.
        /// </summary>
        public RecipientAuthenticationType Type { get; set; }

        /// <summary>
        /// Desafío de autenticación.
        /// </summary>
        public string Challenge { get; set; }

        /// <summary>
        /// Respuesta de autenticación.
        /// </summary>
        public string Response { get; set; }

        /// <summary>
        /// Formato de autenticación.
        /// </summary>
        public string Format { get; set; }
    }
}