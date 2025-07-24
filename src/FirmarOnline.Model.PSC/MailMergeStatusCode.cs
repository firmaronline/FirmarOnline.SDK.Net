namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Códigos de estado para combinación de correspondencia
    /// </summary>
    public enum MailMergeStatusCode
    {
        /// <summary>
        /// Creada
        /// </summary>
        Created = 100,

        /// <summary>
        /// En proceso
        /// </summary>
        InProcess = 200,

        /// <summary>
        /// Completada (sin errores)
        /// </summary>
        Completed = 300,

        /// <summary>
        /// Completada (con errores)
        /// </summary>
        CompletedWithErrors = 301,
    }
}
