namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Información del documento
    /// </summary>
    public class DocumentInfo
    {
        /// <summary>
        ///  Identificador interno del cliente del documento
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Nombre del documento
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Hash del documento
        /// </summary>
        public string Hash { get; set; }
    }
}