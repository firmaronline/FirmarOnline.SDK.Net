namespace FirmarOnline.Model.Verify
{
    /// <summary>
    /// Especifica el perfil o nivel de firma digital aplicado a un documento PDF,
    /// basado en el estándar PAdES(PDF Advanced Electronic Signatures).
    /// </summary>
    public enum DigitalSignatureProfile
    {
        /// <summary>
        /// PAdES estándar
        /// </summary>
        PAdES_Standard,
        /// <summary>
        /// PAdES avanzada
        /// </summary>
        PAdES_Advanced,
        /// <summary>
        /// PAdES archivo
        /// </summary>
        PAdES_Archive,
        /// <summary>
        /// Básica
        /// </summary>
        Basic
    }
}