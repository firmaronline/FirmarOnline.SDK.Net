using System;

namespace FirmarOnline.Model.Validations
{
    /// <summary>
    /// Tipo para validar cadenas en base64
    /// </summary>
    public class Base64ValidationType : StringValidationTypeBase
    {
        /// <summary>
        /// Comprueba si es base64 válido
        /// </summary>
        /// <param name="value">El valor a comprobar</param>
        /// <returns>true si el valor es válido, de lo contrario false</returns>
        public override bool IsValid(string value)
        {
            try
            {
                _ = Convert.FromBase64String(value);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}