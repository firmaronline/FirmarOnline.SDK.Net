namespace FirmarOnline.Model
{
    /// <summary>
    /// Define los datos de acceso a un proveedor de servicios.
    /// </summary>
    public class ExternalProvider
    {
        /// <summary>
        /// Url de acceso al servicio.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Usuario.
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Contraseña en base 64.
        /// </summary>
        public string Password { get; set; }
    }
}
