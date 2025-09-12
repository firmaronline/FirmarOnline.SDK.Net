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
        public DigitalSignatureCollection DigitalSignatures { get; set; }
        /// <summary>
        /// Firmas Biométricas
        /// </summary>
        public BioSignatureCollection BioSignatures { get; set; }
    }
}
