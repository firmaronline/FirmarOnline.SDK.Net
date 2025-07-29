using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace FirmarOnline.Model
{
    /// <summary>
    /// Información de archivo.
    /// </summary>
    public class FileResult
    {
        private string name;

        /// <summary>
        /// Nombre.
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = SanitizeName(value); }
        }

        /// <summary>
        /// Mime type.
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Contenido.
        /// </summary>
        public Stream Content { get; set; }

        /// <summary>
        /// Sanitización del nombre de un documento. Los caracteres '~', '`' y '.' no dan problemas
        /// a la hora de generar ficheros (probado en Windows), así que se dejan como están.
        /// </summary>
        /// <param name="documentName">Nombre original del documento sin sanitizar.</param>
        /// <returns>Nombre del documento sanitizado.</returns>
        public static string SanitizeName(string documentName)
        {
            if (documentName == null) return "unknown_file_name";

            documentName = RemoveAccents(documentName);

            char replacement = '_';
            char[] charsToReplace = { '<', '>', ':', '"', '/', '\\', '|', '?', '*', '#', '%', '&', '@', '^', '$', '!', ' ', ',', ';', '[', ']', '(', ')' };

            var stringBuilder = new StringBuilder();
            foreach (var c in documentName)
            {
                if (c <= 127 && Array.IndexOf(charsToReplace, c) == -1)
                {
                    stringBuilder.Append(c);
                }
                else
                {
                    stringBuilder.Append(replacement);
                }
            }
            string result = stringBuilder.ToString();

            return result;
        }

        /// <summary>
        /// Reemplazar las vocales con acentos por vocales sin acentos.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string RemoveAccents(string input)
        {
            var normalizedString = input.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (char c in normalizedString)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}