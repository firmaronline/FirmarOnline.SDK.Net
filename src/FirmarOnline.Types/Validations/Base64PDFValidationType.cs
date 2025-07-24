using System;
using System.Text;

namespace FirmarOnline.Types.Validations
{
    /// <summary>
    /// Tipo para validar archivos PDF en base64
    /// </summary>
    public class Base64PDFValidationType : StringValidationTypeBase
    {
        /// <summary>
        /// Comprueba si el contenido comienza con la cabecera de PDF %PDF-
        /// </summary>
        /// <param name="value">El valor a comprobar</param>
        /// <returns>true si el valor es válido, de lo contrario false</returns>
        public override bool IsValid(string value)
        {
            var pdfHeader = Encoding.ASCII.GetBytes("%PDF-");

            try
            {
                var content = Convert.FromBase64String(value);

                if (content.Length < pdfHeader.Length)
                {
                    return false;
                }

                for (int i = 0; i < pdfHeader.Length; i++)
                {
                    if (content[i] != pdfHeader[i])
                    {
                        return false;
                    }
                }
            }
            catch (FormatException)
            {
                return false;
            }

            return true;
        }
    }
}
