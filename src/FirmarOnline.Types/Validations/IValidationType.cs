namespace FirmarOnline.Types.Validations
{
    /// <summary>
    /// Tipo de validación
    /// </summary>
    /// <typeparam name="TValue">Tipo de datos del valor a validar</typeparam>
    public interface IValidationType<TValue>
    {
        /// <summary>
        /// Comprueba que el valor proporcionado cumple con la validación
        /// </summary>
        /// <param name="value">Valor a comprobar</param>
        /// <returns>true si es un valor válido, de lo contrario false</returns>
        bool IsValid(TValue value);
    }
}
