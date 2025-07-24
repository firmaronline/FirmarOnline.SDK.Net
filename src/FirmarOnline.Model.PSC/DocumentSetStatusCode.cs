using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Códigos de estado de sobre
    /// </summary>
    public enum DocumentSetStatusCode
    {
        /// <summary>
        /// Indeterminado
        /// </summary>
        [Display(Name = "Indeterminado")]
        None = 0,
        /// <summary>
        /// Creado
        /// </summary>
        [Display(Name = "Creado")]
        Created = 100,
        /// <summary>
        /// En proceso
        /// </summary>
        [Display(Name = "En proceso")]
        InProcess = 200,
        /// <summary>
        /// Completado
        /// </summary>
        [Display(Name = "Completado")]
        Completed = 300,
        /// <summary>
        /// Cancelado
        /// </summary>
        [Display(Name = "Cancelado")]
        Canceled = 400,
        /// <summary>
        /// Rechazado
        /// </summary>
        [Display(Name = "Rechazado")]
        Rejected = 500,
        /// <summary>
        /// Eliminado
        /// </summary>
        [Display(Name = "Eliminado")]
        Deleted = 600,
        /// <summary>
        /// Caducado
        /// </summary>
        [Display(Name = "Caducado")]
        Expired = 700,
        /// <summary>
        /// Se ha producido un error
        /// </summary>
        [Display(Name = "Se ha producido un error")]
        Error = 800
    }

    /// <summary>
    /// Define métodos de extensión para la enumeración <see cref="DocumentSetStatusCode"/>
    /// </summary>
    public static class DocumentSetStatusCodeExtensions
    {
        private static readonly DocumentSetStatusCode[] _completedStatusCodes =
                new[] { DocumentSetStatusCode.Completed, DocumentSetStatusCode.Rejected };

        private static readonly DocumentSetStatusCode[] _notfinalizedStatusCodes =
            new[] { DocumentSetStatusCode.None, DocumentSetStatusCode.Created, DocumentSetStatusCode.InProcess };

        private static readonly DocumentSetStatusCode[] _finalizedStatusCodes =
            new[] { DocumentSetStatusCode.Completed, DocumentSetStatusCode.Canceled, DocumentSetStatusCode.Rejected, DocumentSetStatusCode.Deleted, DocumentSetStatusCode.Expired, DocumentSetStatusCode.Error };

        /// <summary>
        /// Comprueba si el valor del estado indica que el procesamiento del sobre
        /// se ha completado
        /// </summary>
        /// <param name="status">Valor de estado a comprobar</param>
        /// <returns>true si el procesamiento del sobre se ha completado,
        /// en otro caso devuelve false</returns>
        public static bool IsCompletedStatusCode(this DocumentSetStatusCode status)
        {
            return _completedStatusCodes.Contains(status);
        }

        /// <summary>
        /// Comprueba si el valor del estado indica que el sobre está pendiente de
        /// ser procesado. No está en estado final.
        /// </summary>
        /// <param name="status">Valor de estado a comprobar</param>
        /// <returns>true si está pendiente de procesar,
        /// en otro caso devuelve false</returns>
        public static bool IsInprocessStatusCode(this DocumentSetStatusCode status)
        {
            return _notfinalizedStatusCodes.Contains(status);
        }

        /// <summary>
        /// Comprueba si el valor del estado indica que el procesamiento del sobre está en un estado final
        /// </summary>
        /// <param name="status">Valor de estado a comprobar</param>
        /// <returns>true si el procesamiento del sobre ha finalizado,
        /// en otro caso devuelve false</returns>
        public static bool IsFinalizedStatusCode(this DocumentSetStatusCode status)
        {
            return _finalizedStatusCodes.Contains(status);
        }

    }
}
