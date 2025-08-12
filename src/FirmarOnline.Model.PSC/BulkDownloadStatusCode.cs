using System.Linq;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Códigos de estado de descarga masiva de documentos
    /// </summary>
    public enum BulkDownloadStatusCode
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
        /// Completada
        /// </summary>
        Completed = 300,
        /// <summary>
        /// Se ha producido un error
        /// </summary>
        Error = 800
    }

    /// <summary>
    /// Define métodos de extensión para la enumeración <see cref="BulkDownloadStatusCode"/>
    /// </summary>
    public static class BulkDownloadStatusCodeExtensions
    {
        private static readonly BulkDownloadStatusCode[] _notfinalizedStatusCodes =
            [BulkDownloadStatusCode.Created, BulkDownloadStatusCode.InProcess];

        /// <summary>
        /// Comprueba si el valor del estado indica que la descarga masiva de documentos está pendiente de
        /// ser procesada. No está en estado final.
        /// </summary>
        /// <param name="status">Valor de estado a comprobar</param>
        /// <returns>true si está pendiente de procesar,
        /// en otro caso devuelve false</returns>
        public static bool IsInprocessStatusCode(this BulkDownloadStatusCode status)
        {
            return _notfinalizedStatusCodes.Contains(status);
        }
    }
}
