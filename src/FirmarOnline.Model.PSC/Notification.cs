using System.ComponentModel.DataAnnotations;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Notificación
    /// </summary>
    public class Notification
    {
        /// <summary>
        /// Nombre
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [EmailAddress]
        [MaxLength(255)]
        [Required]
        public string Email { get; set; }
    }
}