#if NET6_0_OR_GREATER

using System;
using System.ComponentModel.DataAnnotations;

namespace FirmarOnline.Model.PSC.Forms
{
    /// <summary>
    /// Fecha.
    /// </summary>
    [CustomValidation(typeof(DateField), nameof(ValidateInput),
    ErrorMessage = "The Input item is not valid.")]
    public class DateField : FieldBase<DateTime?>
    {
        /// <summary>
        /// Indica si tiene la fecha actual (para fields de tipo date).
        /// </summary>
        public bool Today { get; set; }
    }
}
#endif