# 📚 FirmarOnline .NET SDK

Este repositorio contiene el **código fuente de los clientes oficiales en .NET** y los **modelos de datos** para interactuar con la API pública de [**FirmarOnline**](https://restapi.firmar.info/index.html).  

📂 Solución principal (clientes y modelos de datos):  
```plaintext
\FirmarOnline.SDK\src\FirmarOnline.SDK.sln
```

A partir de este código se generan los paquetes NuGet oficiales:

- 📦 **FirmarOnline.Clients.PSC** → Cliente para la API del **PSC (Prestador de Servicios de Confianza)**  
- ✍️ **FirmarOnline.Clients.ESign** → Cliente para la API del **Servicio de Firma Avanzada** (OTP, firma de PDF, sello de tiempo)  
- 🔍 **FirmarOnline.Clients.Verify** → Cliente para la API del **Servicio de Validación** (firmas y trazabilidad)  

Los clientes se compilan para dos frameworks:

- 🟢 **.NET 8.0** → versión moderna y recomendada  
- 🟦 **.NET Standard 2.0** → compatibilidad con proyectos existentes  

De esta forma, el SDK puede integrarse tanto en proyectos actuales como en soluciones que aún dependan de .NET Standard.

---

El repositorio incluye además **aplicaciones de ejemplo en .NET 8** que actúan como guía práctica para aprender a utilizar los paquetes NuGet y realizar llamadas reales a las APIs de FirmarOnline.  

📂 Soluciones de ejemplo:  
```plaintext
\FirmarOnline.SDK\samples\FirmarOnline.Samples.ConsoleClient\FirmarOnline.Samples.ConsoleClient.sln
\FirmarOnline.SDK\samples\FirmarOnline.Samples.WebApi\FirmarOnline.Samples.WebApi.sln
```

---

## 📑 Índice
1. [🚀 Quickstart](#-quickstart)  
2. [📦 Paquetes disponibles](#-paquetes-disponibles)  
3. [🖥️ Ejemplos de uso](#️-ejemplos-de-uso)  

---

## 🚀 Quickstart


1. ✅ Requisitos previos  
   - ⚙️ **.NET 8 SDK** instalado  
   - 💻 **Visual Studio 2022** (recomendado **v17.10+**) con carga de trabajo *.NET 8* o **Visual Studio Code** con **.NET 8 SDK** y extensión **C# Dev Kit**  
   - 🔑 **Token de autenticación** proporcionado por **Edatalia** o **API Key** creada en **FirmarOnline** ([**Sandbox**](https://app.firmar.info/signatures/remote/settings) o [**Producción**](https://app.firmar.online/signatures/remote/settings))  

2. Crear proyecto de consola:  
   ```bash
   dotnet new console -n Demo && cd Demo
   ```

3. Instalar un cliente (ejemplo PSC):  
   ```bash
   dotnet add package FirmarOnline.Clients.PSC
   ```

4. Código mínimo (ejemplo con **PSC** para el entorno de Sandox)  
   En este ejemplo se crea y envía un sobre para firma digital.  
   Es necesario indicar un **documento PDF codificado en Base64** que será el que el destinatario reciba para firmar.  

   ```csharp
   // Inicializar cliente PSC en entorno sandbox
   var client = new PSCClient(PSCClient.PSCSandboxEnvironmentUrl, "<api_key_o_token>");

   // Definir el sobre de firma
   var documentSet = new SimpleDocumentSetWithSendMethod {
       SenderName = "FirmarOnline SDK",          // Nombre del emisor
       SenderMail = "noreply.sdk@firmar.online", // Correo del emisor
       DocumentSetName = "Sobre de ejemplo",     // Título del sobre
       SendMethod = SendMethod.Email,            // Método de envío (Email, SMS, etc.)
       ExpirationDaysTimeout = 10,               // Días de validez del sobre

       // Documento PDF a firmar (contenido en Base64)
       Document = new Document {
           Name = "Documento de ejemplo.pdf",
           B64PDFContent = "BASE64_DEL_PDF_AQUI"
       },

       // Información del destinatario
       Recipient = new SingleDocumentRecipient {
           Name = "John Sanders",
           Email = "john.sanders@foo.com",
           AuthType = RecipientAuthenticationType.None,    // Tipo de autenticación (None, OTP, etc.)
           ActionType = RecipientActionType.BioSignature   // Acción requerida (firma biométrica en este caso)
       }
   };

   // Enviar el sobre a FirmarOnline
   var documentSetId = await client.PostDocumentSetSimpleAsync(documentSet);
   Console.WriteLine($"Sobre enviado con ID: {documentSetId}");
   ```

---

## 📦 Paquetes disponibles

Cada cliente dispone de su propia documentación con ejemplos de uso y listado de métodos soportados:

- 📦 **PSC – Prestador de Servicios de Confianza**  
  [Ver documentación →](src/FirmarOnline.Clients.PSC/README.md)  
  Permite la **gestión completa de sobres de firma**: creación, consulta de estado, descargas de documentos, obtención de evidencias y operaciones sobre sobres.  

- ✍️ **ESign – Firma avanzada**  
  [Ver documentación →](src/FirmarOnline.Clients.eSign/README.md)  
  Ofrece **firma avanzada de documentos PDF**, aplicación de **sellos de tiempo (TSA)** y autenticación de firmantes mediante **OTP**.  

- 🔍 **Verify – Validación**  
  [Ver documentación →](src/FirmarOnline.Clients.Verify/README.md)  
  Permite la **verificación de firmas electrónicas en documentos PDF** y la validación de certificados de **trazabilidad (LegalAuditTrail)**.  

---

## 🖥️ Ejemplos de uso

El repositorio incluye **aplicaciones de ejemplo en .NET 8** dentro de la carpeta `samples` que sirven como **guía práctica** para aprender a utilizar los paquetes NuGet del SDK y realizar llamadas reales a las APIs de FirmarOnline.

### 🖥️ Console Client

**Aplicación de consola** con menú interactivo para probar las APIs de **PSC**, **ESign** y **Verify**.

📂 Proyecto:  
```plaintext
\FirmarOnline.SDK\samples\FirmarOnline.Samples.ConsoleClient\FirmarOnline.Samples.ConsoleClient.sln
```

La aplicación contiene **ejemplos listos para ejecutar** que muestran cómo realizar llamadas reales a la API utilizando los clientes NuGet del SDK.

👉 Para más detalles consulta el [README del Console Client](samples/FirmarOnline.Samples.ConsoleClient/README.md).

### 🌐 Web API

**Aplicación ASP.NET Core** que demuestra cómo integrar el cliente PSC usando inyección de dependencias y exponer funcionalidades a través de una API REST.

📂 Proyecto:  
```plaintext
\FirmarOnline.SDK\samples\FirmarOnline.Samples.WebApi\FirmarOnline.Samples.WebApi.sln
```

La aplicación incluye endpoints para **crear sobres de ejemplo** y **consultar el estado** de sobres existentes, mostrando cómo usar `PostDocumentSetSimpleAsync` y `GetDocumentSetStatusAsync` en un entorno web.

👉 Para más detalles consulta el [README de la Web API](samples/FirmarOnline.Samples.WebApi/README.md).

---