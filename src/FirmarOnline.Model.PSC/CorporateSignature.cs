using System.Collections.Generic;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Firma corporativa 
    /// </summary>
    public class CorporateSignature : CorporateSignatureBase
    {
        /// <summary>
        /// Definición de las cajas de firma en cada uno de los documentos
        /// </summary>
        public IEnumerable<CorporateSignatureAction> Widgets { get; set; }
    }
}
