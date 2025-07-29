namespace FirmarOnline.Model.Verify
{
    /// <summary>
    /// Estado de revocación del certificado
    /// </summary>
    public enum RevocationStatus
    {
        /// <summary>
        /// Normal -no revocado-
        /// </summary>
        Good,
        /// <summary>
        /// Revocado!
        /// </summary>
        Revoked,
        /// <summary>
        /// Desconocido éste certificado
        /// </summary>
        Unknown,
        /// <summary>
        /// No encontrada infórmación para éste certificado
        /// </summary>
        NotFound
    }
}