namespace FirmarOnline.Model.Validations
{
    /// <summary>
    /// Clase base para tipos de validación de valores de cadena
    /// </summary>
    public interface IStringValidationType : IValidationType<string>
    {

        /// <summary>
        /// Comprueba que el valor proporcionado cumple con la validación
        /// </summary>
        /// <param name="value">Valor a comprobar</param>
        /// <param name="strict">Indica si el valor debe estar en un formato
        /// normalizado (true) o si no es necesario (false)</param>
        /// <returns>true si es un valor válido, de lo contrario false</returns>
        bool IsValid(string value, bool strict);

        /// <summary>
        /// Normaliza el valor proporcionado con las reglas especificadas
        /// por el tipo de validación
        /// </summary>
        /// <param name="value">Valor a normalizar</param>
        /// <returns>Valor normalizado</returns>
        string Normalize(string value);
    }
}
