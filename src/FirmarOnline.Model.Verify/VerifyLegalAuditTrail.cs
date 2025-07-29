namespace FirmarOnline.Model.Verify
{
    /// <summary>
    /// Resumen de verificación de certificado de trazabilidad
    /// </summary>
    public class VerifyLegalAuditTrail
    {
        /// <summary>
        /// Firma válida
        /// </summary>
        public bool Integrity { get; set; }
        /// <summary>
        /// Información del firmante
        /// </summary>
        public string SignerInfo { get; set; }
        /// <summary>
        /// AuditTrail
        /// </summary>
        public string AuditTrail { get; set; }
    }
}
