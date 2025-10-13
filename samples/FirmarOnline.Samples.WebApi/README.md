# FirmarOnline Samples Web API

Esta aplicación ASP.NET Core permite enviar documentos para firma biométrica a un destinatario y obtener el estado del sobre a través de una API REST. Demuestra cómo integrar el cliente PSC de FirmarOnline usando inyección de dependencias.

## Configuración

La aplicación está preconfigurada para usar el entorno **Sandbox** de FirmarOnline con un token de ejemplo. El token de autenticación está definido directamente en el código (`Program.cs`) como en la aplicación de consola:

```csharp
const string authenticationToken = "e199ba90-3c50-4715-9be8-dee52f9a87c7";
const bool isProduction = false; // Sandbox
```

Para usar tu propio token o cambiar al entorno de producción, modifica estas constantes en `Program.cs`.

## Ejecución

### Desde Visual Studio
1. Abre el proyecto en Visual Studio 2022
2. Establece `FirmarOnline.Samples.WebApi` como proyecto de inicio
3. Presiona F5 para ejecutar

### Desde línea de comandos
```bash
cd samples/FirmarOnline.Samples.WebApi
dotnet run
```

La aplicación se ejecutará en:
- HTTPS: `https://localhost:7002`
- HTTP: `http://localhost:5002`

## Endpoints disponibles

### 1. Enviar documento para firma biométrica
**POST** `/api/documentset/create-example`

Envía un documento PDF a un destinatario para firma biométrica usando `PostDocumentSetSimpleAsync`. No requiere parámetros, utiliza datos predefinidos.

**Ejemplo con curl:**
```bash
curl -X POST https://localhost:7002/api/documentset/create-example
```

**Respuesta exitosa:**
```json
{
  "documentSetId": "550e8400-e29b-41d4-a716-446655440000",
  "message": "Documento enviado para firma biométrica exitosamente"
}
```

### 2. Obtener estado de un sobre
**GET** `/api/documentset/status/{documentSetId}`

Obtiene el estado actual de un sobre por su ID.

**Ejemplo con curl:**
```bash
curl https://localhost:7002/api/documentset/status/550e8400-e29b-41d4-a716-446655440000
```

**Respuesta exitosa:**
```json
{
  "documentSetId": "550e8400-e29b-41d4-a716-446655440000",
  "status": "Pending",
  "message": "Estado obtenido exitosamente"
}
```

## Implementación

La aplicación registra el cliente PSC en el contenedor de dependencias con configuración directa:

```csharp
// Registro del cliente PSC
builder.Services.AddScoped<PSCClient>(serviceProvider =>
{
    // Configuración directa como en la aplicación de consola
    const string authenticationToken = "e199ba90-3c50-4715-9be8-dee52f9a87c7";
    const bool isProduction = false;
    
    var apiUrl = isProduction 
        ? PSCClient.PSCProductionEnvironmentUrl 
        : PSCClient.PSCSandboxEnvironmentUrl;
    
    return new PSCClient(apiUrl, authenticationToken);
});
```

El controlador recibe el cliente a través de inyección de dependencias:

```csharp
[ApiController]
[Route("api/[controller]")]
public class DocumentSetController : ControllerBase
{
    private readonly PSCClient _pscClient;

    public DocumentSetController(PSCClient pscClient)
    {
        _pscClient = pscClient;
    }

    [HttpPost("create-example")]
    public async Task<object> CreateExampleDocumentSet()
    {
        // Enviar documento para firma biométrica usando PostDocumentSetSimpleAsync
        var documentSetId = await _pscClient.PostDocumentSetSimpleAsync(documentSet);
        return new { DocumentSetId = documentSetId, Message = "Documento enviado para firma biométrica exitosamente" };
    }

    [HttpGet("status/{documentSetId}")]
    public async Task<object> GetDocumentSetStatus(string documentSetId)
    {
        // Obtener estado del sobre
        var status = await _pscClient.GetDocumentSetStatusAsync(documentSetId);
        return new { DocumentSetId = documentSetId, Status = status, Message = "Estado obtenido exitosamente" };
    }
}
```

## Flujo de trabajo típico

1. **Enviar documento para firma**: 
   ```bash
   curl -X POST https://localhost:7002/api/documentset/create-example
   ```

2. **Copiar el `documentSetId` de la respuesta**

3. **Consultar el estado del sobre**:
   ```bash
   curl https://localhost:7002/api/documentset/status/TU_DOCUMENT_SET_ID
   ```

## Estructura del proyecto

```
FirmarOnline.Samples.WebApi/
├── Controllers/
│   └── DocumentSetController.cs    # Controlador con endpoints
├── resources/
│   └── sample_document.pdf         # Documento PDF de ejemplo
├── Properties/
│   └── launchSettings.json         # Configuración de lanzamiento
├── appsettings.json                # Configuración de logging
└── Program.cs                      # Configuración de la aplicación
```