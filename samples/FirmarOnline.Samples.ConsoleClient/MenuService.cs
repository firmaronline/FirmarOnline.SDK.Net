using FirmarOnline.Samples.ConsoleClient.ESign;
using FirmarOnline.Samples.ConsoleClient.PSC;
using FirmarOnline.Samples.ConsoleClient.Verify;

namespace FirmarOnline.Samples.ConsoleClient
{
    /// <summary>
    /// Servicio encargado de generar y gestionar los menús de la aplicación
    /// </summary>
    internal static class MenuService
    {
        /// <summary>
        /// Tecla utilizada para salir de un menú interactivo en consola.
        /// </summary>
        internal static char MENU_EXIT_KEY => '0';
      
        #region Generación de menús y gestión de las opciones seleccioandas

        /// <summary> 
        /// Muestra y gestiona el menú principal de la aplicación de consola, permitiendo al usuario acceder a las 
        /// funcionalidades principales de la API de FirmarOnline. 
        /// </summary>
        /// <returns> Una tarea asincrónica que representa la ejecución del menú principal. </returns>
        internal static async Task RunMainMenuAsync()
        {
            await RunMenuAsync("FIRMAR.ONLINE API - SDK", new()
            {
                ['1'] = ("1. PSC - Creación de sobres", async () =>
                {
                    await RunMenuAsync("API PSC - CREACIÓN DE SOBRES", new()
                    {
                        ['1'] = ("1. Simple (1 documento y 1 destinatario)", CreateDocumentSetSamples.CreateSimpleDocumentSetAsync),
                        ['2'] = ("2. Simple y devuelve URL del visor", CreateDocumentSetSamples.CreateDocumentSetAndGetUrl),
                        ['3'] = ("3. Completo", CreateDocumentSetSamples.CreateDocumentSetAsync),
                        ['4'] = ("4. Flujo simple (1 documento y 1 destinatario)", CreateDocumentSetSamples.CreateDocumentSetFromFlowSimpleAsync),
                        ['5'] = ("5. Flujo simple y devuelve URL del visor", CreateDocumentSetSamples.CreateDocumentSetFromFlowAndGetUrlAsync),
                        ['6'] = ("6. Flujo completo", () => CreateDocumentSetSamples.CreateDocumentSetFromFlowAsync())
                    });
                    return;
                }
                ),
                ['2'] = ("2. PSC - Obtener información", async () =>
                {
                    await RunMenuAsync("API PSC - OBTENER INFORMACIÓN DE SOBRES", new()
                    {
                        ['A'] = ("A. Estado actual", () => GetDocumentSetSamples.GetDocumentSetStatusAsync()),
                        ['B'] = ("B. URL visor", () => GetDocumentSetSamples.GetDocumentSetUrlAsync()),
                        ['C'] = ("C. Detalle", () => GetDocumentSetSamples.GetDocumentSetInfoAsync()),
                        ['D'] = ("D. Errores", () => GetDocumentSetSamples.GetDocumentSetErrorInfoAsync()),
                        ['E'] = ("E. Listado histórico por referencia externa", () => GetDocumentSetSamples.GetDocumentSetsInfoByReferenceAsync()),                        
                        ['F'] = ("F. Listado histórico", () => GetDocumentSetSamples.GetHistoryAsync()),
                        ['G'] = ("G. Listado dispositivos", () => GetDocumentSetSamples.GetDevicesAsync()),
                        ['H'] = ("H. Eventos (AuditTrail)", () => GetDocumentSetSamples.GeDocumentSetAuditTrailAsync()),                        
                        ['I'] = ("I. PDF evidencias", () => GetDocumentSetSamples.GetEvidencesAsync()),
                        ['J'] = ("J. JWT evidencias", () => GetDocumentSetSamples.GetDocumentSetLegalAuditTrailAsync()),                        
                        ['K'] = ("K. Descargar documento", () => GetDocumentSetSamples.GetDocumentAsync()),
                        ['L'] = ("L. Descargar documento seleccionado", () => GetDocumentSetSamples.GetDocumentWhenOnlyOneAsync()),
                        ['M'] = ("M. Descargar adjunto", () => GetDocumentSetSamples.GetAttachmentAsync())
                    });
                    return;
                }
                ),
                ['3'] = ("3. PSC - Acciones contra sobres", async () =>
                {
                    await RunMenuAsync("API PSC - ACCIONES CONTRA SOBRES", new()
                    {
                        ['1'] = ("1. Cancelar", () => PutDocumentSetSamples.CancelDocumentSetAsync()),
                        ['2'] = ("2. Reenviar email", () => PutDocumentSetSamples.ResendDocumentSetAsync()),
                        ['3'] = ("3. Borrar documentos", () => PutDocumentSetSamples.PurgeDocumentSetAsync()),
                        ['4'] = ("4. Test WebHook", () => PutWebHookSamples.TestWebHookAsync())
                    });
                    return;
                }
                ),
                ['4'] = ("4. ESIGN - Servicio de firma avanzada", async () =>
                {
                    await RunMenuAsync("API ESIGN - SERVICIO DE FIRMA AVANZADA", new()
                    {
                        ['1'] = ("1. Firmar PDF", () => ESignSamples.SignPDFAsync()),
                        ['2'] = ("2. Añadir sello de tiempo", () => ESignSamples.TimeStampPdfAsync()),
                        ['3'] = ("3. Generar OTP", () => ESignSamples.GenerateOtpAsync()),
                        ['4'] = ("4. Validar OTP", () => ESignSamples.ValidateOtpAsync())
                    });
                    return;
                }
                ),
                ['5'] = ("5. VERIFY - Servicio de validación", async () =>
                {
                    await RunMenuAsync("API VERIFY - SERVICIO DE VALIDACIÓN", new()
                    {
                        ['1'] = ("1. Verificar trazabilidad", () => VerifySamples.VerifyLegalAuditTrailAsync()),
                        ['2'] = ("2. Verificar firmas PDF", () => VerifySamples.VerifySignedPDFAsync())
                    });
                    return;
                }
                )
            }, firstLevel: true);
        }

        /// <summary>
        /// Lógica compartida para mostrar menús con o sin acciones.
        /// </summary>
        /// <param name="title">Título del menú.</param>
        /// <param name="lines">Opciones visibles a mostrar.</param>
        /// <returns>Tecla pulsada.</returns>
        internal static char ShowMenu(string title, IEnumerable<string> lines)
        {
            Console.Clear();

            // Título decorado
            var titleLines = GenerateMenuTitleBlock(title);
            foreach (var line in titleLines)
                Console.WriteLine("\t" + line);

            // Opciones del menú
            foreach (var line in lines)
                Console.WriteLine($"\t - {line}");

            Console.WriteLine($"\n\t - {MENU_EXIT_KEY}. Salir");

            Console.WriteLine($"\n\t{titleLines[1]}");

            // Captura de opción sin necesidad de pulsar Enter
            Console.Write("\n\t - Seleccione una opción: ");

            return char.ToUpperInvariant(Console.ReadKey(intercept: true).KeyChar);
        }

        /// <summary>
        /// Escribe un mensaje en la consola con el color especificado.
        /// </summary>
        /// <param name="message">Mensaje a mostrar.</param>
        /// <param name="color">Color del texto.</param>
        internal static void ShowColoredMessage(string message, ConsoleColor color)
        {
            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine($"\n\t\t{message}");
            Console.ForegroundColor = originalColor;
        }

        /// <summary>
        /// Solicita al usuario el identificador de un sobre mediante la consola.
        /// </summary>
        /// <param name="showMessage">
        /// Indica si debe mostrarse el mensaje de "Ejecutando llamada a la API..." después de ingresar el valor.
        /// Por defecto es <c>true</c>.
        /// </param>
        /// <returns>El identificador del sobre ingresado por el usuario.</returns>
        internal static string PromptDocumentSetId(bool showMessage = false) =>
            ShowMenuInput(MenuPrompts.DOCUMENTSET_TOKEN_ID, showMessage);

        /// <summary>
        /// Solicita al usuario una referencia externa mediante la consola.
        /// </summary>
        /// <param name="showMessage">
        /// Indica si debe mostrarse el mensaje de "Ejecutando llamada a la API..." después de ingresar el valor.
        /// Por defecto es <c>true</c>.
        /// </param>
        /// <returns>La referencia externa ingresada por el usuario.</returns>
        internal static string PromptDocumentSetReference(bool showMessage = false) =>
            ShowMenuInput(MenuPrompts.DOCUMENTSET_REFERENCE, showMessage);

        /// <summary>
        /// Solicita al usuario el identificador de un documento mediante la consola.
        /// </summary>
        /// <param name="showMessage">
        /// Indica si debe mostrarse el mensaje de "Ejecutando llamada a la API..." después de ingresar el valor.
        /// Por defecto es <c>true</c>.
        /// </param>
        /// <returns>El identificador del documento ingresado por el usuario.</returns>
        internal static string PromptDocumentSetDocumentId(bool showMessage = false) =>
            ShowMenuInput(MenuPrompts.DOCUMENTSET_DOCUMENT, showMessage);

        /// <summary>
        /// Solicita al usuario el identificador de un adjunto mediante la consola.
        /// </summary>
        /// <param name="showMessage">
        /// Indica si debe mostrarse el mensaje de "Ejecutando llamada a la API..." después de ingresar el valor.
        /// Por defecto es <c>true</c>.
        /// </param>
        /// <returns>El identificador del adjunto ingresado por el usuario.</returns>
        internal static string PromptDocumentSetAttachmentId(bool showMessage = false) =>
            ShowMenuInput(MenuPrompts.DOCUMENTSET_ATTACHMENT, showMessage);

        /// <summary>
        /// Solicita al usuario un número de teléfono completo en formato internacional (+prefijo).
        /// </summary>
        /// <param name="showMessage">Si es <c>true</c>, se muestra un mensaje de ayuda antes de pedir el número.</param>
        /// <returns>Una cadena con el número de teléfono introducido por el usuario.</returns>
        internal static string PromptPhoneNumber(bool showMessage = false) =>
            ShowMenuInput(MenuPrompts.PHONE_NUMBER, showMessage);

        /// <summary>
        /// Solicita al usuario que introduzca un código OTP (One-Time Password) para validación.
        /// </summary>
        /// <param name="showMessage">Si es <c>true</c>, se muestra un mensaje de ayuda antes de pedir el código OTP.</param>
        /// <returns>Una cadena con el código OTP introducido por el usuario.</returns>
        internal static string PromptValidateOTP(bool showMessage = false) =>
            ShowMenuInput(MenuPrompts.VALIDATE_OTP_CODE, showMessage);

        /// <summary>
        /// Solicita al usuario el identificador del flujo (FlowTokenId) para la cración de un sobre.
        /// </summary>
        /// <param name="showMessage">Si es <c>true</c>, se muestra un mensaje de ayuda antes de pedir el identificador.</param>
        /// <returns>Una cadena con el FlowTokenId introducido por el usuario.</returns>
        internal static string PromptFlowTokenId(bool showMessage = false) =>
            ShowMenuInput(MenuPrompts.DOCUMENTSET_FLOWTOKENID, showMessage);

        #endregion

        #region Funciones privadas

        /// <summary>
        /// Muestra un menú interactivo en consola, permite al usuario seleccionar una opción
        /// y ejecuta la acción asociada de forma asíncrona.
        /// </summary>
        /// <param name="title">
        /// Título principal del menú que se mostrará en consola.
        /// </param>
        /// <param name="menuActions">
        /// Diccionario de acciones disponibles en el menú. Cada entrada contiene una tecla como opción,
        /// una descripción visible para el usuario y una función asíncrona a ejecutar al seleccionar dicha opción.
        /// </param>
        /// <param name="firstLevel">
        /// Indica si el menú actual es el menú principal. Si es <c>false</c>, se solicita confirmación del usuario
        /// antes de volver al menú superior tras ejecutar una acción.
        /// </param>
        /// <returns>
        /// Una tarea que representa la ejecución asíncrona del flujo de menú hasta que el usuario elija salir.
        /// </returns>
        private static async Task RunMenuAsync(string title, Dictionary<char, (string Description, Func<Task> Action)> menuActions,
            bool firstLevel = false)
        {
            while (true)
            {
                var option = ShowMenu(title, menuActions.Values.Select(v => v.Description));

                if (option == MENU_EXIT_KEY)
                    break;

                if (!menuActions.TryGetValue(option, out var selected))
                    continue;

                Console.WriteLine("");

                await selected.Action();

                if (!firstLevel)
                    ShowWaitForUserConfirmation();
            }
        }

        /// <summary>
        /// Muestra un mensaje en consola solicitando al usuario que presione cualquier tecla para continuar,
        /// y espera la pulsación sin mostrarla en pantalla.
        /// </summary>
        private static void ShowWaitForUserConfirmation()
        {
            Console.WriteLine("\n\t - Presiona cualquier tecla para continuar...");
            Console.ReadKey(intercept: true);
        }

        /// <summary>
        /// Muestra en la consola un mensaje resaltado indicando que se está ejecutando una llamada a la API.
        /// </summary>
        internal static void ShowExecuteApiMessage()
        {
            ShowColoredMessage($"\n\t\tEjecutando llamada a la API ...", ConsoleColor.Yellow);
        }

        /// <summary>
        /// Solicita una entrada al usuario desde la consola, validando que no sea vacía o nula.
        /// </summary>
        /// <param name="prompt">
        /// Texto que se muestra como mensaje para indicar qué valor se debe ingresar.
        /// </param>
        /// <param name="showExecutingMessage">
        /// Indica si debe mostrarse un mensaje de "Ejecutando llamada a la API..." después de recibir la entrada.
        /// Valor por defecto: <c>true</c>.
        /// </param>
        /// <returns>
        /// El valor ingresado por el usuario, garantizado como no nulo ni vacío.
        /// </returns>
        private static string ShowMenuInput(string prompt, bool showExecutingMessage = true)
        {
            string? input;
            do
            {
                Console.Write($"\n\t\t - {prompt}: ");
                input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                    ShowColoredMessage("\n\t\t - El valor informado no puede estar vacío.", ConsoleColor.Red);

            } while (string.IsNullOrWhiteSpace(input));

            if (showExecutingMessage)
                ShowExecuteApiMessage();

            return input;
        }

        /// <summary>
        /// Genera un bloque de título en consola con líneas decorativas y el texto centrado.
        /// </summary>
        /// <param name="title">Texto del encabezado.</param>
        /// <param name="width">Ancho total de la línea (por defecto 50).</param>
        /// <returns>Arreglo de strings representando el bloque de encabezado.</returns>
        private static string[] GenerateMenuTitleBlock(string title, int width = 80)
        {
            if (title.Length + 2 > width)
                width = title.Length + 6; // ajusta si el título es muy largo

            string border = new('=', width);
            string centeredTitle = title.PadLeft((width + title.Length) / 2).PadRight(width);

            return ["", border, centeredTitle, border, ""];
        }

        #endregion
    }


    /// <summary>
    /// Contiene los mensajes de entrada utilizados para solicitar datos al usuario en la consola,
    /// como identificadores de sobres, documentos, adjuntos o referencias externas.
    /// </summary>
    internal static class MenuPrompts
    {
        /// <summary>
        /// Mensaje para solicitar el identificador de un sobre.
        /// </summary>
        internal const string DOCUMENTSET_TOKEN_ID = "Identificador de sobre";

        /// <summary>
        /// Mensaje para solicitar una referencia externa asociada al sobre.
        /// </summary>
        internal const string DOCUMENTSET_REFERENCE = "Referencia externa";

        /// <summary>
        /// Mensaje para solicitar el identificador de un documento dentro del sobre.
        /// </summary>
        internal const string DOCUMENTSET_DOCUMENT = "Identificador de documento";

        /// <summary>
        /// Mensaje para solicitar el identificador de un adjunto asociado al sobre.
        /// </summary>
        internal const string DOCUMENTSET_ATTACHMENT = "Identificador de adjunto";

        /// <summary>
        /// Mensaje para solicitar el nº de telefono.
        /// </summary>
        internal const string PHONE_NUMBER = "Nº teléfono (Ejemplo: +34600123456)";

        /// <summary>
        /// Mensaje para validar un código OTP
        /// </summary>
        internal const string VALIDATE_OTP_CODE = "Nº OTP a validar";

        /// <summary>
        /// Mensaje para solicitar el identificador de un flujo para crear un sobre.
        /// </summary>
        internal const string DOCUMENTSET_FLOWTOKENID = "Identificador de flujo";
    }
}
