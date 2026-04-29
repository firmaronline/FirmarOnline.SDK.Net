using System.ComponentModel.DataAnnotations;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Paso de autenticación.
    /// </summary>
    public class AuthenticationStep
    {
        /// <summary>
        /// Tipo de autenticación de este paso.
        /// </summary>
        [Required]
        [EnumDataType(typeof(RecipientAuthenticationType))]
        public RecipientAuthenticationType Type { get; set; }

        /// <summary>
        /// Código de acceso (requerido si Type = AccessCode).
        /// </summary>
        public RecipientAccessCode AccessCode { get; set; }
    }
}