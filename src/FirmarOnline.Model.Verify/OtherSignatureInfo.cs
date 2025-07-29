namespace FirmarOnline.Model.Verify
{
    /// <summary>
    /// Información extra de la firma Digital
    /// </summary>
    public class OtherSignatureInfo
    {
        /// <summary>
        /// Define el tipo de firma según su función dentro del documento PDF.
        /// </summary>
        public PdfSignatureContext SignatureType { get; set; }

        /// <summary>
        /// Algoritmo usado para la firma
        /// </summary>
        public string HashAlg { get; set; }

        /// <summary>
        /// permitido rellenar formularios sin alterar la firma
        /// </summary>
        public bool AllowedFillInForms { get; set; }
        /// <summary>
        /// permitido rellenar comentarios sin alterar la firma
        /// </summary>
        public bool AllowedComment { get; set; }

        /// <summary>
        /// widget visible o no
        /// </summary>
        public bool WidgetVisible { get; set; }
        /// <summary>
        /// X del Widget
        /// </summary>
        public int WidgetX { get; set; }
        /// <summary>
        /// Y del Widget
        /// </summary>
        public int WidgetY { get; set; }
        /// <summary>
        /// Ancho del Widget
        /// </summary>
        public int WidgetWidth { get; set; }
        /// <summary>
        /// Alto del Widget
        /// </summary>
        public int WidgetHeight { get; set; }
        /// <summary>
        /// Página del Widget
        /// </summary>
        public int WidgetPage { get; set; }
        /// <summary>
        /// mostrado o no el widget en todas las páginas
        /// </summary>
        public bool WidgetShowOnAllPages { get; set; }

        /// <summary>
        /// algoritmo de cifrado de flujos
        /// </summary>
        public string StreamEncryptionAlgorithm { get; set; }
        /// <summary>
        /// longitud de llave cifrado de flujos
        /// </summary>
        public int StreamEncryptionKeyBits { get; set; }
        /// <summary>
        /// algoritmo de cifrado de cadenas
        /// </summary>
        public string StringEncryptionAlgorithm { get; set; }
        /// <summary>
        /// longitud de llave cifrado de cadenas
        /// </summary>
        public int StringEncryptionKeyBits { get; set; }

    }
}