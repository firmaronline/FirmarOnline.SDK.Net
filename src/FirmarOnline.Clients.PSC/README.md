# 📦 FirmarOnline.Clients.PSC

Cliente oficial en .NET para acceder a la API pública de [**PSC (Prestador de Servicios de Confianza)**](https://restapi.firmar.info/index.html?urls.primaryName=Servicio+PSC).  

Permite **crear, consultar y gestionar sobres de firma**, así como recuperar **evidencias, adjuntos y ejecutar operaciones de control**.

---

## 🧭 Índice
1. [⚙️ Instalación e inicialización](#️-instalación-e-inicialización)  
2. [🛠️ Compatibilidad](#️-compatibilidad)  
3. [📡 Métodos disponibles](#-métodos-disponibles)  
   - ✍️ [Creación de sobres](#️-creación-de-sobres)  
   - ℹ️ [Consulta de información](#ℹ️-consulta-de-información)  
   - ⚡ [Operaciones sobre sobres](#⚡-operaciones-sobre-sobres)  
4. [🖥️ Ejemplos en aplicación de consola](#️-ejemplos-en-aplicación-de-consola)  

---

## ⚙️ Instalación e inicialización

Instalar el paquete NuGet:

```bash
dotnet add package FirmarOnline.Clients.PSC
```

Crear instancia del cliente en **sandbox** o **producción**:

```csharp
// Sandbox
var client = new PSCClient(PSCClient.PSCSandboxEnvironmentUrl, "<api_key_o_token>");

// Producción
var client = new PSCClient(PSCClient.PSCProductionEnvironmentUrl, "<api_key_o_token>");
```

---

## 🛠️ Compatibilidad

Este cliente se compila para dos frameworks:

- 🟢 **.NET 8.0** → versión moderna y recomendada  
- 🟦 **.NET Standard 2.0** → compatibilidad con proyectos existentes  

---

## 📡 Métodos disponibles

### ✍️ Creación de sobres

- **Crear sobre simple (1 documento y 1 destinatario)**  
  Método: `PostDocumentSetSimpleAsync`  
  Crea un sobre básico con un único documento y un solo destinatario.  

- **Crear sobre simple y obtener URL del visor**  
  Método: `PostDocumentSetAndGetUrlAsync`  
  Genera un sobre simple y devuelve directamente la URL del visor para que el destinatario pueda acceder al documento.  

- **Crear sobre con múltiples documentos y destinatarios**  
  Método: `PostDocumentSetAsync`  
  Permite crear un sobre más complejo con varios documentos y múltiples firmantes.  

- **Crear sobre desde flujo simple (1 documento y 1 destinatario)**  
  Método: `PostDocumentSetFlowSimpleAsync`  
  Inicializa un sobre a partir de una definición de flujo preconfigurada con un único documento y destinatario.  

- **Crear sobre desde flujo y obtener URL del visor**  
  Método: `PostDocumentSetFlowAndGetUrlAsync`  
  Genera un sobre basado en un flujo definido y devuelve la URL del visor para la firma.  

- **Crear sobre desde flujo completo**  
  Método: `PostDocumentSetFlowAsync`  
  Permite la creación de sobres avanzados a partir de un flujo completo, con múltiples documentos, destinatarios y reglas.  

### ℹ️ Consulta de información

- **Obtener estado actual del sobre** → `GetDocumentSetStatusAsync`  
  Devuelve el estado en el que se encuentra un sobre (pendiente, firmado, cancelado, etc.).  

- **Obtener URL del visor** → `GetDocumentSetUrlAsync`  
  Recupera la URL actual del visor de un sobre para el destinatario.  

- **Obtener detalle de un sobre** → `GetDocumentSetInfoAsync`  
  Devuelve información completa del sobre: documentos, destinatarios, acciones y configuración.  

- **Recuperar errores de procesamiento** → `GetDocumentSetErrorInfoAsync`  
  Obtiene los errores registrados en el proceso de creación o gestión del sobre.  

- **Listado por referencia externa** → `GetDocumentSetsInfoByReferenceAsync`  
  Recupera información de sobres asociados a una referencia externa definida por el integrador.  

- **Histórico de sobres** → `GetHistoryAsync`  
  Devuelve el histórico de sobres creados, incluyendo estados y fechas.  

- **Dispositivos registrados** → `GetDevicesAsync`  
  Obtiene los dispositivos vinculados a los firmantes de un sobre.  

- **Eventos (AuditTrail)** → `GetAuditTrailAsync`  
  Devuelve el registro de eventos (audit trail) asociados al proceso de firma del sobre.  

- **PDF de evidencias** → `GetEvidencesAsync`  
  Permite descargar un PDF con las evidencias generadas durante el proceso de firma.  

- **LegalAuditTrail (JWT)** → `GetLegalAuditTrailAsync`  
  Devuelve el LegalAuditTrail en formato JWT para su validación jurídica.  

- **Descargar documento principal** → `GetDocumentAsync`  
  Descarga el documento principal firmado del sobre.  

- **Descargar documento específico** → `GetDocumentAsync`  
  Descarga un documento concreto del sobre mediante su identificador.  

- **Descargar adjunto** → `GetAttachmentAsync`  
  Recupera un archivo adjunto asociado al sobre.  

### ⚡ Operaciones sobre sobres

- **Cancelar sobre** → `CancelDocumentSetAsync`  
  Cancela un sobre en curso, evitando que los firmantes puedan continuar el proceso.  

- **Reenviar email al destinatario** → `ResendDocumentSetAsync`  
  Reenvía la notificación por correo electrónico a los destinatarios del sobre.  

- **Borrar documentos de un sobre finalizado** → `PurgeDocumentSetAsync`  
  Elimina los documentos asociados a un sobre ya cerrado para liberar espacio o cumplir políticas de retención.  

- **Enviar evento de prueba a WebHook** → `TestWebHookAsync`  
  Envía un evento de prueba al endpoint configurado como WebHook para validar la integración.  

---

## 🖥️ Ejemplos en aplicación de consola

La solución incluye un cliente de consola en **.NET 8** con ejemplos listos para ejecutar:  

📂 Ruta:  
```plaintext
\FirmarOnline.SDK\samples\FirmarOnline.Samples.ConsoleClient\FirmarOnline.Samples.ConsoleClient.sln
```

En este proyecto se encuentran implementados los escenarios de **PSC** con su clase correspondiente:

- ✍️ **Creación**
  - `PostDocumentSetSimpleAsync` → `PSC/CreateDocumentSetSimple.cs`
  - `PostDocumentSetAndGetUrlAsync` → `PSC/CreateDocumentSetAndGetUrl.cs`
  - `PostDocumentSetAsync` → `PSC/CreateDocumentSet.cs`
  - `PostDocumentSetFlowSimpleAsync` → `PSC/CreateDocumentSetFromFlowSimple.cs`
  - `PostDocumentSetFlowAndGetUrlAsync` → `PSC/CreateDocumentSetFromFlowAndGetUrl.cs`
  - `PostDocumentSetFlowAsync` → `PSC/CreateDocumentSetFromFlow.cs`

- ℹ️ **Información**
  - `GetDocumentSetStatusAsync` → `PSC/GetDocumentSetStatus.cs`
  - `GetDocumentSetUrlAsync` → `PSC/GetDocumentSetUrl.cs`
  - `GetDocumentSetInfoAsync` → `PSC/GetDocumentSetInfo.cs`
  - `GetDocumentSetErrorInfoAsync` → `PSC/GetDocumentSetErrorInfo.cs`
  - `GetDocumentSetsInfoByReferenceAsync` → `PSC/GetDocumentSetInfoByReference.cs`
  - `GetHistoryAsync` → `PSC/GetDocumentSetHistory.cs`
  - `GetDevicesAsync` → `PSC/GetDocumentSetDevices.cs`
  - `GetAuditTrailAsync` → `PSC/GetDocumentSetAuditTrail.cs`
  - `GetEvidencesAsync` → `PSC/GetDocumentSetEvidences.cs`
  - `GetLegalAuditTrailAsync` → `PSC/GetDocumentSetLegalAuditTrail.cs`
  - `GetDocumentAsync` → `PSC/GetDocumentSetDocument.cs`
  - `GetDocumentAsync` (documento específico) → `PSC/GetDocumentSetDocumentOnlyOne.cs`
  - `GetAttachmentAsync` → `PSC/GetDocumentSetAttachment.cs`

- ⚡ **Operaciones**
  - `CancelDocumentSetAsync` → `PSC/PutDocumentSetCancel.cs`
  - `ResendDocumentSetAsync` → `PSC/PutDocumentSetResend.cs`
  - `PurgeDocumentSetAsync` → `PSC/PutDocumentSetPurge.cs`
  - `TestWebHookAsync` → `PSC/PutWebHookTest.cs`

👉 Consulta también el [README del Console Client](../../samples/FirmarOnline.Samples.ConsoleClient/README.md) para más información sobre la ejecución.
