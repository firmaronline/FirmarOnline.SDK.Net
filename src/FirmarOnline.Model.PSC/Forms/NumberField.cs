#if NET6_0_OR_GREATER

using System.ComponentModel.DataAnnotations;

namespace FirmarOnline.Model.PSC.Forms
{
    /// <summary>
    ///
    /// </summary>
    [CustomValidation(typeof(NumberField), nameof(ValidateInput),
    ErrorMessage = "The Input item is not valid.")]
    public class NumberField : FieldBase<decimal?>
    {
    }
}

#endif