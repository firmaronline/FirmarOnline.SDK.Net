namespace FirmarOnline.Model.Validations
{
    /// <summary>
    /// Clase base para implementación de tipos de datos de validación
    /// para valores de tipo string
    /// </summary>
    public abstract class StringValidationTypeBase : IStringValidationType
    {
        /// <summary>
        /// Comprueba que el valor proporcionado cumple con la validación
        /// </summary>
        /// <param name="value">Valor a comprobar</param>
        /// <param name="strict">Indica si el valor debe estar en un formato
        /// normalizado (true) o si no es necesario (false)</param>
        /// <returns>true si es un valor válido, de lo contrario false</returns>
        public virtual bool IsValid(string value, bool strict)
        {
            var valueToValidate = strict ? value : Normalize(value);
            return IsValid(valueToValidate);
        }

        /// <summary>
        /// Comprueba que el valor proporcionado cumple con la validación
        /// </summary>
        /// <param name="value">Valor a comprobar</param>
        /// <returns>true si es un valor válido, de lo contrario false</returns>
        public abstract bool IsValid(string value);

        /// <summary>
        /// Normaliza el valor proporcionado con las reglas especificadas
        /// por el tipo de validación
        /// </summary>
        /// <param name="value">Valor a normalizar</param>
        /// <returns>Valor normalizado</returns>
        public virtual string Normalize(string value)
        {
            return value;
        }
    }

}
