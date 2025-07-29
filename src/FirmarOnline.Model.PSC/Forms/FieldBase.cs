#if NET6_0_OR_GREATER

using System.ComponentModel.DataAnnotations;

namespace FirmarOnline.Model.PSC.Forms
{
    /// <summary>
    /// Campo.
    /// </summary>

    public abstract class FieldBase<T> : InputItemBase
    {
        /// <summary>
        /// Valor. Es de tipo genérico y se especifica en clases heredadas.
        /// </summary>
        public virtual T Value { get; set; }

        /// <summary>
        /// Patrón de introducción del valor.
        /// </summary>
        public string Pattern { get; set; }

        /// <summary>
        /// Validación de Input.
        /// El Id es requerido para los Item de Field.
        /// </summary>
        /// <param name="fieldBase">Campo a validar.</param>
        /// <returns></returns>
        public static ValidationResult ValidateInput(FieldBase<T> fieldBase)
        {
            if (string.IsNullOrWhiteSpace(fieldBase.Id))
            {
                return new ValidationResult("Input id is required");
            }

            return ValidationResult.Success;
        }
    }
}
#endif