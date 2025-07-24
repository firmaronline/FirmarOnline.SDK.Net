using FirmarOnline.Types.Widgets;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Firma corporativa de un sobre simple
    /// </summary>
    public class CorporateSignatureSimple : CorporateSignatureBase
    {
        /// <summary>
        /// Definición de la caja de firma del documento
        /// </summary>
        public Widget Widget { get; set; }
    }
}
