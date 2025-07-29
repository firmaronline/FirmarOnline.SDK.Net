namespace FirmarOnline.Model
{
    /// <summary>
    /// Propiedades de la firma.
    /// </summary>
    public class SignatureProperties
    {
        /// <summary>
        /// Autor de la firma.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Motivo.
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// Datos de contacto.
        /// </summary>
        public string Contact { get; set; }

        /// <summary>
        /// Dirección.
        /// </summary>
        public string Location { get; set; }
    }
}