namespace FirmarOnline.Model.Verify
{
    /// <summary>
    /// Colección de firmas biométricas de un documento
    /// </summary>
    public class BioSignatureInfoCollection
    {
        /// <summary>
        /// Firmas biométricas
        /// </summary>
        public BioSignatureInfo[] Signatures { get; set; }
    }
}