using FirmarOnline.Model.PSC;
using FirmarOnline.Model.Widgets;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

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
        /// </summary>
        public static readonly SingleDocumentCorporateSignature SingleDocumentCorporateSignature = new()
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
        /// Ejemplo de firma corporativa, habría que reemplazar el CorporateSignatureId por uno válido definido en la plataforma FirmarOnline.
        /// La configuración de firma corporativa debería utilizarse para informar la propiedad CorporateSignature de los sobres a crear.
        /// Esta definición se puede aplicar a sobres con múltiples documentos.
        /// </summary>
        public static readonly CorporateSignature DocumentCorporateSignature = new()
        {
            CorporateSignatureId = "XXXXXXXXXXXXXXXXXXXXXXXXXX", // Identificador de la firma corporativa
            Type = CorporateSignatureType.Start,
            Widgets = [
                // Definición de la caj ade firma para el 1º documento
                new CorporateSignatureAction() {
                    DocumentId = "DOC-00001",
                    Widget = new FixedWidget
                    {
                        Page = 1,
                        X = 200,
                        Y = 250,
                        Width = 200,
                        Height = 100,
                        Rotation = RotationType.Degrees_0,
                        CustomText = [new() { Text = "Firme aquí" }]
                    }
                }
            ]
        };

        /// <summary>
        /// Ejemplo para añadir una lista de destinatarios a los que se enviará una copia firmada de los documentos
        /// </summary>
        public static readonly List<Notification> Notifications =
        [
            new Notification()
            {
                Name = "Arnold Stehr",
                Email = "arnold.stehr@foo.com",
            },
            new Notification()
            {
                Name = "Shaun Rowe",
                Email = "shaun.rowe@foo.com",
            }
        ];

        /// <summary>
        /// Ejemplo para añadir definir una lista con los ficheros a anexar que pedirá el visor de documentos antes de firmar
        /// </summary>
        public static readonly List<RecipientDefinitionAttachment> Attachments =
        [
            new RecipientDefinitionAttachment()
            {                
                Description = "Imagen frontal del DNI", // Descripción que aparecerá en el visor para pedir el fichero a anexar                
                Required = true, // Indica si es fichero anexo es requerido
            },
            new RecipientDefinitionAttachment()
            {
                Description = "Imagen trasera del DNI",
                Required = false,
            }
        ];

        /// <summary>
        /// Ejemplo para configurar la autenticación mediante código de acceso
        /// </summary>
        public static readonly RecipientAccessCode RecipientAccessCode = new()
        {   
            Challenge = "Introduzca su DNI con letra en minúscula", // Desafío 
            Format = "[0-9]{4,9}[a-z]", // (Opcional) Expresión regular con el formato de la respuesta
            Response = "12345678", // Respuesta valida que debera informar el destinatario
        };

        /// <summary>
        /// Ejemplo para configurar la autenticación mediante código de acceso en un flujo
        /// </summary>
        public static readonly AccessCode AccessCode = new()
        {
            Challenge = "Introduzca su DNI con letra en minúscula", // Desafío 
            Format = "[0-9]{4,9}[a-z]", // (Opcional) Expresión regular con el formato de la respuesta
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

        /// <summary>
        /// Recupera el contenido de un archivo de recursos en formato Base64.
        /// </summary>
        /// <param name="fileName">Nombre del archivo de recursos</param>
        /// <returns>El contenido del archivo en formato Base 64</returns>
        /// <exception cref="FileNotFoundException">Si el archivo de recursos indicado no existe</exception>
        internal static string GetSampleFileContent(string fileName)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(fileName);
            var assembly = Assembly.GetExecutingAssembly();
            using Stream? stream = assembly.GetManifestResourceStream($"FirmarOnline.Samples.ConsoleClient.resources.{fileName}")
                ?? throw new FileNotFoundException($"El archivo '{fileName}' no se encuentra en los recursos del ensamblado.");
            using MemoryStream memoryStream = new();
            stream.CopyTo(memoryStream);
            return Encoding.UTF8.GetString(memoryStream.ToArray()).TrimStart('\uFEFF');
        }

        /// <summary>
        /// Guarda un flujo de datos en un archivo físico.
        /// </summary>
        /// <param name="stream">Flujo de datos que será escrito en el archivo.</param>
        /// <param name="fileName">Nombre del fichero.</param>
        /// <returns>Ruta física del fichero generado.</returns>
        internal static async Task<string> SaveStreamToFileAsync(Stream stream, string fileName)
        {
            if (stream is not MemoryStream memoryStream || !memoryStream.CanRead)
                throw new ArgumentException("El flujo proporcionado no es válido o no se puede leer.", nameof(stream));

            var fullPath = BuildOutputPath(fileName);

            string? directory = Path.GetDirectoryName(fullPath);
            if (string.IsNullOrWhiteSpace(directory))
                throw new InvalidOperationException("La ruta del directorio de salida no es válida.");

            Directory.CreateDirectory(directory);
            await File.WriteAllBytesAsync(fullPath, memoryStream.ToArray());

            return fullPath;
        }

        /// <summary>
        /// Serializa un objeto a formato JSON y lo guarda en un archivo.
        /// </summary>
        /// <typeparam name="T">Tipo del objeto a serializar.</typeparam>
        /// <param name="data">Datos que se desean guardar en formato JSON.</param>
        /// <param name="fileName">Nombre del fichero.</param>
        /// <returns>Ruta física del fichero generado.</returns>
        internal static async Task<string> SaveJsonToFileAsync<T>(T data, string fileName)
        {
            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });

            var fullPath = BuildOutputPath(fileName);

            string? directory = Path.GetDirectoryName(fullPath);
            if (string.IsNullOrWhiteSpace(directory))
                throw new InvalidOperationException("La ruta del directorio de salida no es válida.");

            Directory.CreateDirectory(directory);
            await File.WriteAllTextAsync(fullPath, json);

            // Mensaje de éxito en consola con color verde
            return fullPath;
        }

        /// <summary>
        /// Opciones de serialización de un JSON
        /// </summary>
        internal static readonly JsonSerializerOptions JsonOptionsViewConsole = new()
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        /// <summary>
        /// Construye la ruta completa para guardar los archivos generados con los resultados de las llamadas 
        /// a la API combinando la ruta base con el nombre del archivo.
        /// </summary>
        /// <param name="fileName">Nombre del archivo de salida.</param>
        /// <returns>Ruta absoluta del archivo.</returns>
        private static string BuildOutputPath(string fileName)
        {
            return Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\OutPutFiles\", fileName));
        }
    }
}
