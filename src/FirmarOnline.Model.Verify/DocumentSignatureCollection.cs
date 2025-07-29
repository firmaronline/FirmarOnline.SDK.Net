namespace FirmarOnline.Model.Verify
{
    /// <summary>
    /// Colección de firmas de un documento
    /// </summary>
    public class DocumentSignatureCollection
    {
        /// <summary>
        /// Firmas Digitales
        /// </summary>
        public DigitalSignatureInfoCollection DigitalSignatures { get; set; }
        /// <summary>
        /// Firmas Biométricas
        /// </summary>
        public BioSignatureInfoCollection BioSignatures { get; set; }
    }
}
