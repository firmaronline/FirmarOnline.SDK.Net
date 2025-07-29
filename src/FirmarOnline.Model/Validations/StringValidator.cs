namespace FirmarOnline.Model.Validations
{
    /// <summary>
    /// Validador para valores de cadena.
    /// </summary>
    public static class StringValidator
    {
        /// <summary>
        /// Comprueba que el valor proporcionado cumple con la validación
        /// </summary>
        /// <typeparam name="TValidation">Tipo de validación</typeparam>
        /// <param name="value">Valor a comprobar</param>
        /// <param name="strict">Indica si el valor debe estar en un formato
        /// normalizado (true) o si no es necesario</param>
        /// <returns>true si es un valor válido, de lo contrario false</returns>
        public static bool IsValid<TValidation>(string value, bool strict = false)
                where TValidation : IStringValidationType, new()
        {
            var validationType = new TValidation();
            return validationType.IsValid(value, strict);
        }

        /// <summary>
        /// Normaliza el valor proporcionado con las reglas especificadas
        /// por el tipo de validación
        /// </summary>
        /// <param name="value">Valor a normalizar</param>
        /// <returns>Valor normalizado</returns>
        public static string Normalize<TValidation>(string value)
            where TValidation : IStringValidationType, new()
        {
            var validationType = new TValidation();
            return validationType.Normalize(value);
        }
    }

    /// <summary>
    /// Validador para valores de cadena.
    /// </summary>
    /// <typeparam name="TValidation">Tipo de validación</typeparam>
    public static class StringValidator<TValidation>
                    where TValidation : IStringValidationType, new()
    {
        /// <summary>
        /// Comprueba que el valor proporcionado cumple con la validación
        /// </summary>
        /// <param name="value">Valor a comprobar</param>
        /// <param name="strict">Indica si el valor debe estar en un formato
        /// normalizado (true) o si no es necesario</param>
        /// <returns>true si es un valor válido, de lo contrario false</returns>
        public static bool IsValid(string value, bool strict = false)
        {
            var validationType = new TValidation();
            return validationType.IsValid(value, strict);
        }

        /// <summary>
        /// Normaliza el valor proporcionado con las reglas especificadas
        /// por el tipo de validación
        /// </summary>
        /// <param name="value">Valor a normalizar</param>
        /// <returns>Valor normalizado</returns>
        public static string Normalize(string value)
        {
            var validationType = new TValidation();
            return validationType.Normalize(value);
        }
    }
}
