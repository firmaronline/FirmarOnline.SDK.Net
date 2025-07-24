using System;
using System.ComponentModel.DataAnnotations;

namespace FirmarOnline.Types.Validations
{
    /// <summary>
    /// Especifica que un valor de campo de datos es un número de teléfono soportado
    /// por la aplicación
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class SupportedPhoneAttribute : DataTypeAttribute
    {
        /// <summary>
        /// Inicializa una nueva instancia de <see cref="SupportedPhoneAttribute"/>
        /// </summary>
        /// <param name="strict">true para indicar que el valor debe estar en formato
        /// normalizado, de lo contrario false</param>
        public SupportedPhoneAttribute(bool strict = false) : base(DataType.PhoneNumber)
        {
            ErrorMessage = "The {0} field is not a supported phone number.";
            StrictMode = strict;
        }

        /// <summary>
        /// Indica si por defecto se aplica el modo estricto (exige que el valor
        /// esté en formato normalizado), o no
        /// </summary>
        protected bool StrictMode { get; set; }

        /// <summary>
        /// Comprueba que el campo contiene un número de teléfono soportado
        /// por la aplicación
        /// </summary>
        /// <param name="value">El valor a comprobar</param>
        /// <returns>true si es un valor válido</returns>
        public override bool IsValid(object value)
        {
            return IsValid(value, StrictMode);
        }

        /// <summary>
        /// Comprueba que el campo contiene un número de teléfono soportado
        /// por la aplicación
        /// </summary>
        /// <param name="value">El valor a comprobar</param>
        /// <param name="strict">Indica si el valor debe estar en formato normalizado (true),
        /// o si puede ser un valor desnormalizado (false)</param>
        /// <returns>ture si es un valor válido</returns>
        public virtual bool IsValid(object value, bool strict)
        {
            if (value == null)
            {
                return true;
            }

            if (!(value is string valueAsString))
            {
                return false;
            }

            if (string.IsNullOrEmpty(valueAsString))
            {
                return true;
            }

            var valueToValidate = strict ? valueAsString
                : StringValidator<SupportedPhoneValidationType>
                            .Normalize(valueAsString);

            return StringValidator<SupportedPhoneValidationType>
                            .IsValid(valueToValidate);

        }

    }
}
