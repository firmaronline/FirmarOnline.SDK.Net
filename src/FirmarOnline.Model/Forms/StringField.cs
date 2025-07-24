#if NET6_0_OR_GREATER

using System.ComponentModel.DataAnnotations;

namespace FirmarOnline.Model.Forms
{
    /// <summary>
    /// Campo cuyo valor es un string.
    /// </summary>
    [CustomValidation(typeof(StringField), nameof(ValidateInput),
    ErrorMessage = "The Input item is not valid.")]
    public abstract class StringField : FieldBase<string>
    {
        /// <summary>
        /// Valor.
        /// </summary>
        [MaxLength(255)]
        public override string Value { get; set; }
    }
}

#endif