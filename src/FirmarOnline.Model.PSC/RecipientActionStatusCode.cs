namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Status de un recipientAction
    /// </summary>
    public enum RecipientActionStatusCode
    {
        /// <summary>
        /// N.A.
        /// </summary>
        None = 0,

        /// <summary>
        /// Creado
        /// </summary>
        Created = 1,

        /// <summary>
        /// Pendiente
        /// </summary>
        Pending = 2,

        /// <summary>
        /// Firmando (evento en visor)
        /// </summary>
        Signing = 3,

        /// <summary>
        /// Aceptado / Aceptado con firma
        /// </summary>
        Accepted = 4,

        /// <summary>
        /// Rechazado
        /// </summary>
        Rejected = 5,

        /// <summary>
        /// Notificación Fehaciente entregada.
        /// </summary>
        CertifiedNotificationDelivered = 6,

        /// <summary>
        /// Firmado
        /// </summary>
        Signed = 7,

        /// <summary>
        ///  cancelado
        /// </summary>
        Canceled = 8
    }
}