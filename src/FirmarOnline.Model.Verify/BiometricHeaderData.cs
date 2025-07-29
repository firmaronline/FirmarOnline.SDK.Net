namespace FirmarOnline.Model.Verify
{
    /// <summary>
    /// Datos opcionales incluidos en la cabecera de una firma biométrica.
    /// </summary>
    public enum BiometricHeaderData
    {
        /// <summary>
        /// No se incluye información adicional en la cabecera.
        /// </summary>
        None,

        /// <summary>
        /// Información del software utilizado para capturar o procesar la firma.
        /// </summary>
        Software,

        /// <summary>
        /// Información del hardware empleado (por ejemplo, tableta digitalizadora, sensor).
        /// </summary>
        Hardware,

        /// <summary>
        /// Datos de ubicación GPS capturados durante la firma.
        /// </summary>
        GPS,

        /// <summary>
        /// Variables o mensajes mostrados al usuario durante el proceso de firma.
        /// </summary>
        Vars
    }
}