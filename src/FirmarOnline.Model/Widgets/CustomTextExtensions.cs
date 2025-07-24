using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace FirmarOnline.Model.Widgets
{
    /// <summary>
    /// Métodos de extensión para el texto personalizado de las cajas de firma.
    /// </summary>
    public static class CustomTextExtensions
    {
        /// <summary>
        /// Devuelve una cadena en base64 que contine el texto personalizado serializado en XML.
        /// </summary>
        /// <param name="customText">Texto personalizado.</param>
        /// <returns>Cadena en base64 con el XML.</returns>
        public static string ToXml(this IEnumerable<TextLine> customText)
        {
            var doc = new XmlDocument();
            var xmlDeclaration = doc.CreateXmlDeclaration("1.0", "ISO-8859-1", null);
            doc.AppendChild(xmlDeclaration);
            var rootElement = doc.CreateElement("Lines");
            foreach (var line in customText ?? Enumerable.Empty<TextLine>())
            {
                var xmlLine = doc.CreateElement("Line");
                var size = doc.CreateAttribute("Size");
                size.Value = line.FontSize.ToString();
                xmlLine.Attributes.Append(size);
                xmlLine.InnerText = line.Text;
                rootElement.AppendChild(xmlLine);
            }
            doc.AppendChild(rootElement);
            return doc.OuterXml;
        }
    }
}