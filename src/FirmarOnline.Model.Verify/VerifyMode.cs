namespace FirmarOnline.Model.Verify
{
    /// <summary>
    /// Modos de verificación de firma.
    /// </summary>
    public enum VerifyMode
    {
        /// <summary>
        /// Firma electrónica.
        /// </summary>
        DigitalSignature = 0,

        /// <summary>
        /// Firma biométrica.
        /// </summary>
        BioSignature = 1,

        /// <summary>
        /// Firma electrónica y biométrica.
        /// </summary>
        All = 10
    }
}
