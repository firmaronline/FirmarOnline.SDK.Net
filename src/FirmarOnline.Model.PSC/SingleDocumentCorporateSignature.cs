using FirmarOnline.Model.Widgets;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Firma corporativa de un sobre con un único documento
    /// </summary>
    public class SingleDocumentCorporateSignature : CorporateSignatureBase
    {
        /// <summary>
        /// Definición de la caja de firma del documento
        /// </summary>
        public Widget Widget { get; set; }
    }
}
