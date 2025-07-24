namespace FirmarOnline.Types.Validations
{
    /// <summary>
    /// Valida si una cadena base64 es una imagen JPG válida
    /// </summary>
    public class Base64JPGValidationType : StringValidationTypeBase
    {
        /// <summary>
        /// Implementación comprobación
        /// </summary>
        /// <param name="value">JPG en base 64</param>
        /// <returns>true es jpg, false no</returns>
        public override bool IsValid(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < 5)
            {
                return false;
            }
            string data = value.Substring(0, 5);
            return data.ToUpper() switch
            {
                "/9J/4" => true,//jpg
                _ => false,//other types
            };
        }
    }
}
