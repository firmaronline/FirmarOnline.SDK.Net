using System.ComponentModel.DataAnnotations;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Certificado de trazabilidad
    /// </summary>
    public class LegalAuditTrail
    {
        /// <summary>
        /// Contenido de certificado de trazabilidad
        /// </summary> 
        [Required]
        public string LegalAuditTrailContent { get; set; }
    }
}
