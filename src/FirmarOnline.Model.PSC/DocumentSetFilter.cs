using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Opciones de filtrado para listados de sobres
    /// </summary>
    [CustomValidation(typeof(DocumentSetQueryFilter), nameof(ValidateDocumentSetFilter),
        ErrorMessage = "The query is too expensive. Set a date range no greater than 90 days.")]
    public class DocumentSetFilter : DocumentSetQueryFilter, IPageFilter
    {
        /// <summary>
        /// Número máximo de elementos a devolver
        /// </summary>
        [DefaultValue(50)]
        [Range(1, 100)]
        public int Limit { get; set; } = 50;

        /// <summary>
        /// Desplazamiento, número de elementos a saltarse
        /// </summary>
        public int Offset { get; set; }
    }
}