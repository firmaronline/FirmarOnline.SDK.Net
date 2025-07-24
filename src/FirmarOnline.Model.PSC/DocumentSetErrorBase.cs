using System.ComponentModel.DataAnnotations;
using static FirmarOnline.Model.PSC.ErrorData;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Clase base con la información de un error 
    /// </summary>
    public abstract class DocumentSetErrorBase
    {
        /// <summary>
        /// Tipo de error
        /// </summary>
        [Required]
        [EnumDataType(typeof(ErrorType))]
        public ErrorType? ErrorType { get; set; }

        /// <summary>
        /// Información del error
        /// </summary>
        [MaxLength(2000)]
        [Required]
        public string ErrorInfo { get; set; }
    }
}