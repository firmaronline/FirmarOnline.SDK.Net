using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Define un sobre de documentos para enviar a firma remota (sin flujo).
    /// </summary>
    public abstract class DocumentSetStandAloneBase : DocumentSetBase
    {
        /// <summary>
        /// Remitente del sobre
        /// </summary>
        [MaxLength(255)]
        public string SenderName { get; set; }

        /// <summary>
        /// Mail del remitente
        /// </summary>
        [EmailAddress]
        [Required]
        [MaxLength(255)]
        public string SenderMail { get; set; }

        /// <summary>
        /// Número de días de validez del sobre
        /// </summary>
        [Range(1, 180)]
        public int ExpirationDaysTimeout { get; set; }

        /// <summary>
        /// Habilitar LTV (validación a largo plazo) para firma
        /// </summary>
        [DefaultValue(false)]
        public bool Ltv { get; set; }
    }
}