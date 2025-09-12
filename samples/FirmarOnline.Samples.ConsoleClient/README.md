# 📋 FirmarOnline API – SDK de ejemplos (Console Client)

Cliente de consola desarrollado en **.NET 8** para invocar los servicios **eSign**, **PSC** y **Verify** de la API de [**FirmarOnline**](https://restapi.firmar.info/index.html), mediante un menú interactivo.

Incluye ejemplos listos para ejecutar utilizando los paquetes **NuGet**, que proporcionan clientes para acceder a los distintos métodos de las APIs de **FirmarOnline**:

- 📦 **FirmarOnline.Clients.PSC** → Cliente para la API del **PSC (Prestador de Servicios de Confianza)**.  
- ✍️ **FirmarOnline.Clients.ESign** → Cliente para la API de **firma avanzada** (OTP, firma de PDF, sello de tiempo).  
- 🔍 **FirmarOnline.Clients.Verify** → Cliente para la API de **validación** (firmas y trazabilidad).  

---

## 🧭 Índice
1. [✅ Requisitos](#-requisitos)  
2. [📥 Descargar y arrancar aplicación](#-descargar-código-y-arrancar-aplicación)  
   - [1. Descargar código](#1---descargar-código)  
   - [2. Abrir y configurar solución](#2---abrir-y-configurar-solución)  
   - [3. Ejecutar aplicación](#3---ejecución-de-la-aplicación-de-consola)  
3. [🔁 Llamadas de ejemplo por área](#-llamadas-de-ejemplo-por-área)  
   - [📦 PSC – Sobres](#psc---prestador-de-servicios-de-confianza-sobres)  
   - [✍️ ESIGN – Firma avanzada](#4-esign---servicio-de-firma-avanzada)  
   - [🔍 VERIFY – Validación](#5-verify---servicio-de-validación)  

---

## ✅ Requisitos
- ⚙️ **.NET 8 SDK** instalado.
- 💻 **Visual Studio 2022** (recomendado **v17.10+**) con carga de trabajo *.NET 8*, **o** **Visual Studio Code** con **.NET 8 SDK** y extensión **C# Dev Kit**.
- 🔑 **Token de autenticación** proporcionado por **Edatalia** o **API Key** creada en **FirmarOnline** ([**Sandbox**](https://app.firmar.info/signatures/remote/settings) o [**Producción**](https://app.firmar.online/signatures/remote/settings))  

---

## 📥 Descargar código y arrancar aplicación

### 1 - Descargar código

- 📥 Descargar o clonar el repositorio desde [GitHub][source].  

- 📂 **Estructura de carpetas del proyecto (aplicación de consola):**

```
   FirmarOnline.Samples.ConsoleClient
    ├── Program.cs                 # Punto de entrada de la app de consola
    ├── MenuService.cs             # Lógica y navegación de menús
    ├── SampleValues.cs            # Config y utilidades comunes para ejemplos
    ├── ESign/                     # Ejemplos de API de firma avanzada
    │   ├── GenerateOtp.cs         # Enviar OTP (RFC 6238 TOTP)
    │   ├── ValidateOtp.cs         # Validar OTP
    │   ├── SignPdf.cs             # Firmar PDF con certificado
    │   └── TimeStampPdf.cs        # Añadir sello de tiempo (TSA) a PDF
    ├── PSC/                       # Ejemplos de API para gestionar sobres
    │   ├── CreateDocumentSet*.cs  # Crear sobres (simple, flow, con URL)
    │   ├── GetDocumentSet*.cs     # Obtener info (URL, eventos, evidencias, docs, anexos)
    │   ├── PutDocumentSet*.cs     # Acciones (cancelar, purge, resend)
    │   └── PutWebHookTest.cs      # Probar WebHook de la empresa
    ├── Verify/                    # Ejemplos de API de validación
    │   ├── VerifySignedPDF.cs     # Verificar firmas digitales en PDF
    │   └── VerifyLegalAuditTrail.cs # Verificar certificado de trazabilidad
    └── resources/                 # Documentos de ejemplo (recursos embebidos)
        ├── sample_document.pdf    # PDF base para pruebas
        ├── signed_document.pdf    # PDF firmado de ejemplo
        └── widget.jpg             # Imagen de fondo de la caja de firma
        └── legal_audit_trail.jws  # Fichero JWS con la definción y evidencias de un sobre
```
> Los archivos de `resources/` se leen como recursos embebidos (ver `GetSampleFileContent(...)`).

### 2 - Abrir y configurar solución

- 📂 Abrir el proyecto de consola con los ejemplos en **Visual Studio** o **Visual Studio Code**:

```plaintext
    \FirmarOnline.SDK\samples\FirmarOnline.Samples.ConsoleClient\FirmarOnline.Samples.ConsoleClient.sln
```

- 🔄 Restaurar los paquetes NuGet necesarios para los clientes de FirmarOnline:

```bash
dotnet restore
```
- 📦 **Paquetes incluidos:**

>- 📦 FirmarOnline.Clients.PSC
>- ✍️ FirmarOnline.Clients.ESign
>- 🔍 FirmarOnline.Clients.Verify

- ⚙️ **Opciones de configuración** (en `SampleValues.cs`):  
>- 🌐 `IsProduction`: Activa el **entorno de producción** (`true`) o el **sandbox** (`false`). Afecta a las URLs base de los clientes (`PSC`, `ESign`, `Verify`). Valor por defecto en los ejemplos: `false` (sandbox).  
>- 🔑 `Token de autenticación / API Key`: clave de autenticación del entorno seleccionado (producción/sandbox) que se utilizará para autenticar las peticiones, puede ser un token de autenticación proporcionado por Edatalia o una API Key genearada desde la aplicación de [FirmarOnline][web-settings-sandbox].

### 3 - Ejecución de la aplicación de consola
Al arrancar, aparece el **menú interactivo** en la consola, se muestra un menu que agrupa las disitntas llamadas a la APIs:

- `1` → 📦 PSC – Creación de sobres  
- `2` → 📦 PSC – Información de sobres  
- `3` → 📦 PSC – Acciones contra sobres  
- `4` → ✍️ ESIGN – Firma avanzada  
- `5` → 🔍 VERIFY – Validación  

Usa las teclas indicadas en pantalla para navegar. La tecla `0` sale del menú actual.

---
## 🔁 Llamadas de ejemplo por área

### 📦 PSC - Prestador de Servicios de Confianza (Sobres)
Llamadas a la API del [PSC][api-doc-psc] agrupadas por:

#### 1. PSC – Creación de sobres

| Opción de menú | Descripción breve | Fichero |
|---|---|---|
| 1. Simple (1 documento y 1 destinatario) | Crea un sobre básico con un único documento y destinatario. | `PSC/CreateDocumentSetSimple.cs` |
| 2. Simple y devuelve URL del visor | Crea el sobre básico y devuelve la URL del visor. | `PSC/CreateDocumentSetAndGetUrl.cs` |
| 3. Completo | Crea el sobre con varios destinatarios y documentos para firmar. | `PSC/CreateDocumentSetAndGetUrl.cs` |
| 4. Flujo simple (1 documento y 1 destinatario) | Crea el sobre a partir de un *flujo* con un documento y destinatario. | `PSC/CreateDocumentSetFromFlowSimple.cs` |
| 5. Flujo simple y devuelve URL del visor | Crea el sobre a partir de un *flujo* con un documento y destinatario, devuelve la URL del visor. | `PSC/CreateDocumentSet.cs` |
| 6. Flujo completo | Crea el sobre a partir de un *flujo* con varios destintario y documentos a firmar. | `PSC/CreateDocumentSetFromFlowAndGetUrl.cs` |

📌 **Opciones adicionales en la creación de sobres**

Los ejemplos incluidos permiten crear sobres con una definición básica.  
En el código encontrarás comentadas otras opciones que se pueden habilitar al generar un nuevo sobre:

- ✍️ **Firma corporativa**: posibilidad de añadir una firma corporativa al inicio o al final.  
- 📎 **Adjuntos**: requerir (o no) que el destinatario suba documentación adicional antes de firmar.  
- 📧 **Destinatarios de notificación**: enviar automáticamente los documentos firmados a otros destinatarios por email.  
- 🗂️ **Equipo**: especificar el equipo al que pertenece el documento.  
- 🔄 **Firma en paralelo**: si se indica el mismo número en el campo `Order`, varios destinatarios podrán firmar en paralelo al activarse dentro del flujo.  
- 🔑 **Autenticación por código de acceso**: configurar una pregunta de seguridad, el formato de la respuesta y la respuesta esperada.  

---

#### 2. PSC – Obtener información de sobres

| Opción de menú | Descripción breve | Fichero |
|---|---|---|
| A. Estado actual | Obtiene el estado actual de un sobre. | `PSC/GetDocumentSetStatus.cs` |
| B. URL visor | Obtiene la URL del visor del sobre. | `PSC/GetDocumentSetUrl.cs` |
| C. Detalle | Obtiene el detalle de la definción de un sobre. | `PSC/GetDocumentSetInfo.cs` |
| D. Errores | Recupera información de posible error en el sobre. | `PSC/GetDocumentSetErrorInfo.cs` |
| E. Listado histórico por referencia externa | Devuelve un listado de los sobres coincidentes con el identificador externo. | `PSC/GetDocumentSetInfoByReference.cs` |
| F. Listado histórico | Consulta de histórico de sobres enviados. | `PSC/GetDocumentSetHistory.cs` |
| G. Listado dispositivos | Lista dispositivos por compañia. | `PSC/GetDocumentSetDevices.cs` |
| H. Eventos (AuditTrail) |Recupera los eventos generados por el procesamiento del sobre. | `PSC/GetDocumentSetAuditTrail.cs` |
| I. PDF evidencias | Descarga el PDF de evidencias del sobre. | `PSC/GetDocumentSetEvidences.cs` |
| J. JWT evidencias | Recupera un JSON firmado (información del sobre + evidencias) en formato JWT. | `PSC/GetDocumentSetLegalAuditTrail.cs` |
| K. Descargar documento | Descarga documento del sobre. | `PSC/GetDocumentSetDocument.cs` |
| L. Descargar documento seleccionado | Descarga un documento concreto del sobre. | `PSC/GetDocumentSetDocumentOnlyOne.cs` |
| M. Anexos | Descarga anexos subidos por el destinatario. | `PSC/GetDocumentSetAttachment.cs` |

Los resultados de ciertas operaciones se guardan en la carpeta **OutPutFiles** de la solución con la siguiente nomenclatura:

- `PSC_{documentSetId}_Document.pdf` → Documento único descargado de un sobre.  
- `PSC_{documentSetId}_Document_{documentId}.pdf` → Documento específico descargado por ID.  
- `PSC_{documentSetId}_Evidences.pdf` → PDF de evidencias del sobre.  
- `PSC_{documentSetId}_LegalAuditTrail.pdf` → Certificado de trazabilidad (AuditTrail).  
- `PSC_{documentSetId}_AuditEvents.json` → Historial de eventos del sobre.  
- `PSC_{documentSetId}_Attachment_{attachmentId}` → Anexo subido por un destinatario.  
---

#### 3. PSC – Acciones contra sobres
| Opción de menú | Descripción breve | Fichero |
|---|---|---|
| 1. Cancelar | Cancela el procesamiento del sobre. | `PSC/PutDocumentSetCancel.cs` |
| 2. Reenviar email | Reenvía el email al destinatario actual. | `PSC/PutDocumentSetResend.cs` |
| 3. Borrar documentos | Borra manualmente los documentos de un sobre finalizado. | `PSC/PutDocumentSetPurge.cs` |
| 4. Test WebHook | Llama al WebHook configurado en la empresa. | `PSC/PutWebHookTest.cs` |

---

### ✍️ 4 ESIGN - Servicio de firma avanzada
Llamadas a la API para la firma avanzada de documentos [eSign][api-doc-esign]

| Opción de menú | Descripción breve | Fichero |
|---|---|---|
| 1. Firmar PDF | Firma un documento PDF con certificado electrónico. | `ESign/SignPdf.cs` |
| 2. Añadir sello de tiempo | Añade sello de tiempo (TSA) a un PDF. | `ESign/TimeStampPdf.cs` |
| 3. Generar OTP | Envía OTP por SMS (RFC 6238 TOTP). | `ESign/GenerateOtp.cs` |
| 4. Validar OTP | Valida un OTP recibido. | `ESign/ValidateOtp.cs` |

El resultado de las opciones de **Firmar PDF** y **Añadir sello de tiempo** se guarda en la carpeta **OutPutFiles** de la solución:

- `eSign_Document_Signed.pdf` → Documento firmado con certificado electrónico.  
- `eSign_Document_TimeStamp.pdf` → Documento con sello de tiempo (TSA).  

---

### 🔍 5 VERIFY - Servicio de validación
Llamadas a la API para verificación documentos [Verify][api-doc-verify]

| Opción de menú | Descripción breve | Fichero |
|---|---|---|
| 1. Verificar trazabilidad | Verifica certificados de trazabilidad (AuditTrail). | `Verify/VerifyLegalAuditTrail.cs` |
| 2. Verificar firmas PDF | Analiza firmas digitales en PDF y devuelve detalle. | `Verify/VerifySignedPDF.cs` |

El resultado de las operaciones de verificación se guarda en la carpeta **OutPutFiles** de la solución:

- `Verify_Document_Signatures.json` → Resultado del análisis de firmas digitales en PDF.  
- `Verify_LegalAuditTrail.json` → Resultado de la verificación de certificados de trazabilidad (AuditTrail).  

<!-- LINKS -->
[api-docs]: https://restapi.firmar.info/index.html
[api-doc-psc]: https://restapi.firmar.info/index.html
[api-doc-esign]: https://restapi.firmar.info/index.html?urls.primaryName=Servicio+de+Firma+Avanzada
[api-doc-verify]: https://restapi.firmar.info/index.html?urls.primaryName=Servicio+de+Validaci%C3%B3n

[web-settings-sandbox]: https://app.firmar.info/signatures/remote/settings

[source]: hhttps://github.com/Asier-Villanueva/FirmarOnline.SDK
