using FirmarOnline.Model.Widgets;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Define un destinatario para un sobre con un único documento
    /// </summary>
    public class SingleDocumentRecipient : RecipientWithSignatureType
    {
        /// <summary>
        /// Definición de la caja de firma
        /// </summary>
        public Widget Widget{ get; set; }
    }
}
