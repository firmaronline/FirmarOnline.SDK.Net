using FirmarOnline.Model.PSC;
using FirmarOnline.Model.Widgets;
using System.Reflection;

namespace FirmarOnline.Samples.ConsoleClient
{
    /// <summary>
    /// Establece los valores a utilizar en las llamadas de ejemplo
    /// </summary>
    internal static class SampleValues
    {
        /// <summary>
        /// Establece si se utiliza el entorno de producción o el de pruebas (sandbox).
        /// </summary>
        public static readonly bool IsProduction = false;

        /// <summary>
        /// Representa la clave de autenticación que se utilizará para autenticar las peticiones.
        /// Debe ser una clave apropiada para el entorno seleccionado.
        /// Puede ser un token de autenticación proporcionado por Edatalia para el entorno o una API Key de usuario.
        /// </summary>
        /// <remarks>
        /// En este ejemplo se estabece en una propiedad estática para facilitar su uso en el código de ejemplo.
        /// Las claves de autenticación no deben exponerse en código fuente público ni compartirse de forma insegura.
        /// </remarks>
        public static readonly string AuthenticationToken = "e199ba90-3c50-4715-9be8-dee52f9a87c7";

        // Valores avanzados para la creación de sobres

        /// <summary>
        /// Ejemplo de firma corporativa, habría que reemplazar el CorporateSignatureId por uno válido definido en la plataforma FirmarOnline.
        /// La configuración de firma corporativa debería utilizarse para informar la propiedad CorporateSignature de los sobres a crear.
        /// Esta definición se puede aplicar a sobres de un único documento (ya que tiene una única definición de caja de firma)
        /// Para sobres con múltiples documentos debería utilizarse la clase <see cref="CorporateSignature"/>
        /// </summary>
        public static readonly SingleDocumentCorporateSignature? SingleDocumentCorporateSignature = new()
        {
            CorporateSignatureId = "XXXXXXXXXXXXXXXXXXXXXXXXXX", // Identificador de la firma corporativa
            Type = CorporateSignatureType.Start,
            Widget = new FixedWidget // Definición de la caja de firma para la firma corporativa
            {
                Page = 1,
                X = 200,
                Y = 250,
                Width = 200,
                Height = 100,
                Rotation = RotationType.Degrees_0,
                CustomText = [new() { Text = "Firme aquí" }]
            }
        };

        /// <summary>
        /// Recupera el contenido de un archivo de recursos en formato Base64.
        /// </summary>
        /// <param name="fileName">Nombre del archivo de recursos</param>
        /// <returns>El contenido del archivo en formato Base 64</returns>
        /// <exception cref="FileNotFoundException">Si el archivo de recursos indicado no existe</exception>
        internal static string GetSampleFileContentInBase64(string fileName)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(fileName);
            var assembly = Assembly.GetExecutingAssembly();
            using Stream? stream = assembly.GetManifestResourceStream($"FirmarOnline.Samples.ConsoleClient.resources.{fileName}")
                ?? throw new FileNotFoundException($"El archivo '{fileName}' no se encuentra en los recursos del ensamblado.");
            using MemoryStream memoryStream = new();
            stream.CopyTo(memoryStream);
            return Convert.ToBase64String(memoryStream.ToArray());
        }
    }
}
