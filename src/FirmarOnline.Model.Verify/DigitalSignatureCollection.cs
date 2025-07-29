namespace FirmarOnline.Model.Verify
{

    /// <summary>
    /// Colección de firmas digitales de un documento
    /// </summary>
    public class DigitalSignatureInfoCollection
    {
        /// <summary>
        /// PDF cifrado o no
        /// </summary>
        public bool Encrypted { get; set; }

        /// <summary>
        /// Firmas digitales
        /// </summary>
        public DigitalSignatureInfo[] Signatures { get; set; }
    }
}