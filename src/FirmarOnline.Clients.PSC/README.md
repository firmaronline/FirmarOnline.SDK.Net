# FirmarOnline.Clients.PSC

Cliente para acceso a la API pública del PSC (Prestador de Servicios de Confianza).

+ [Incialización del cliente](#inicialización-del-cliente)
+ [Método PostDocumentSetAsync](#método-postdocumentsetasync)
+ [Método PostDocumentSetAndGetUrlAsync](#método-postdocumentsetandgeturlasync)
+ [Método CancelDocumentSetAsync](#método-canceldocumentsetasync)
+ [Método ResendDocumentSetAsync](#método-resenddocumentsetasync)
+ [Método PurgeDocumentSetAsync](#método-purgedocumentsetasync)
+ [Método GetDocumentSetInfoAsync](#método-getdocumentsetinfoasync)
+ [Método GetDocumentSetErrorInfoAsync](#método-getdocumentseterrorinfoasync)
+ [Método GetDocumentAsync](#método-getdocumentasync)
+ [Método GetEvidencesAsync](#método-getevidencesasync)
+ [Método GetAuditTrailAsync](#método-getaudittrailasync)
+ [Método GetHistoryAsync](#método-gethistoryasync)
+ [Método VerifyLegalAuditTrailAsync](#método-verifylegalaudittrailasync)
+ [Método VerifySignedPDFAsync](#método-verifysignedpdfasync)

## Inicialización del cliente

Añadir al proyecto el paquete NuGet `FirmarOnline.Clients.PSC`.

```
dotnet add package FirmarOnline.Clients.PSC
```

La API se puede llamar a través del objeto `PSCClient` o a través de los métodos de extensión definidos para el `HttpClient`.

### Utilizando PSCClient

Para instanciar el objeto `PSCClient` debemos indicarle la url en la que se encuentra expuesta la API del PSC y la clave de autenticación (ya sea la Api-Key del usuario o un token de cliente):

```csharp
var client = new PSCClient(new Uri("https://restapi.firmar.online/PSC/v40/"), "<api_key_o_token_de_usuario>");
```

A partir de ahí podemos acceder a la API a través de los diferentes métodos definidos en el objeto `PSCClient`.

Este es el método recomendado y el más simple para acceder a la API del PSC.

### Utilizando métodos de extensión de HttpClient

Para utilizar los métodos de extensión del `HttpClient` deberemos instanciar un objeto `HttpClient` y añadirle las cabeceras de autenticación correspondeientes.

Ya sea utilizando una Api-Key

```csharp
var httpClient = new HttpClient();
httpClient.DefaultRequestHeaders.Add("Api-Key", "<api_key_de_usuario>");
```

o utilizando un token de cliente

```csharp
var httpClient = new HttpClient();
httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer <token_de_cliente>");
```

Es conveniente establecer también la propiedad `BaseAddress` con la url en la que se encuentra alojada la API.
Esto nos permitirá utilizar rutas relativas en las llamadas a los métodos y no tener que introducir la url completa en cada llamada.

```csharp
httpClient.BaseAddress = new Uri("http://restapi.firmar.online/PSC/v40/");
```

A partir de ahí podemos acceder a la API a través de los métodos de extensión del `HttpClient`.

## Método PostDocumentSetAsync

Crea un nuevo sobre y devuelve un identificador uno del sobre creado.

### Uso

```csharp
var newDocumentSet = new DocumentSet
{
    DocumentSetName = "Contrato comercial",
    SenderName = "Paz Valladares Castro",
    SenderMail = "pazvalladarescastro@foo.com",
    ExpirationDaysTimeout = 30,
    ReminderDays = 5,
    SendMethod = SendMethod.Email,
    Documents = new[]
    {
        new Document
        {
            Id = "DOC-01",
            Name = "Contrato",
            Description = "Contrato comercial",
            B64PDFContent = "JVBERi0xLjc......"
        }
    },
    Recipients = new[] { new Recipient
    {
        Id = "REC-01",
        Name = "Hannah Pina Domínguez",
        Email = "hannahpinadominguez@foo.com",
        CardId = "66666666Q",
        PhoneNumber = "+34655552638",
        ActionType = RecipientActionType.BioSignature,
        Widgets = new[]
        {
            new RecipientAction
            {
                DocumentId = "DOC-01",
                Widget = new FixedWidget
                {
                    Width = 300,
                    Height = 120,
                    Page = 2,
                    X = 240,
                    Y = 80,
                    CustomText = new [] { new TextLine { FontSize = 4, Text = $"Firmado por Hannah Pina"} }
                }
            }
        }
    } }
};

var newDocumentSetId = await client.PostDocumentSetAsync(newDocumentSet);
```

## Método PostDocumentSetAndGetUrlAsync

Crea un nuevo sobre devolviendo, además del identificador único del sobre creado, la url de acceso al visor para la firma.
Únicamente admite sobres con un único documento y destinatario.

### Uso

```csharp
var newDocumentSet = new SimpleDocumentSet
{
    DocumentSetName = "Contrato comercial",
    SenderName = "Paz Valladares Castro",
    SenderMail = "pazvalladarescastro@foo.com",
    ExpirationDaysTimeout = 30,
    Document = new Document
    {
        Id = "DOC-01",
        Name = "Contrato",
        Description = "Contrato comercial",
        B64PDFContent = "JVBERi0xLjc......"
    },
    Recipient = new SingleDocumentRecipient
    {
        Name = "Hannah Pina Dominguez",
        Email = "hannahpinadominguez@foo.com",
        CardId = "66666666Q",
        PhoneNumber = "+34655552638",
        ActionType = RecipientActionType.BioSignature,
        Widget = new FixedWidget
        {
            Width = 300,
            Height = 120,
            Page = 2,
            X = 240,
            Y = 80,
            CustomText = new[] { new TextLine { FontSize = 4, Text = "Firmado por Hannah Pina" } }
        }
    }
};

var documentSetInfo = await client.PostDocumentSetAndGetUrlAsync(newDocumentSet);
```

## Método CancelDocumentSetAsync

Cancela el procesamiento de un sobre.

### Uso

```csharp
await client.CancelDocumentSetAsync(newDocumentSetId);
```

## Método ResendDocumentSetAsync

Provoca el reenvío de la notificación al destinatario actual.

### Uso

```csharp
await client.ResendDocuemntSetAsync(newDocumentSetId);
```

## Método PurgeDocumentSetAsync

Purga los documentos de un sobre finalizado.

### Uso

```csharp
await client.PurgeDocumentSetAsync(DocumentSetId);
```

## Método GetDocumentSetInfoAsync

Devuelve el detalle de la definición y estado actual de procesamiento de un sobre.

### Uso

```chsarp
var documentSetInfo = await client.GetDocumentSetInfoAsync(newDocumentSetId);
```

## Método GetDocumentSetErrorInfoAsync

Si se ha producido un error en el procesamiento de un sobre, este método permite recuperar la información detallada del error.

### Uso

```csharp
var errorInfo = await client.GetDcoumentSetErrorInfoAsync(newDocumentSetId);
```

## Método GetDocumentAsync

Recupera un documento de un sobre una vez finalizado el procesamiento de éste.

### Uso

```csharp
var signedDocument = await client.GetDocumentAsync(newDocumentSetId, "DOC-01");
```

El identificador del documento únicamente es necesario indicarlo cuando el sobre contiene más de un documento.

## Método GetEvidencesAsync

Recupera el documento de evidencias del procesamiento del sobre.

### Uso

```csharp
var evidences = await client.GetEvidencesAsync(newDocumentSetId);
```

## Método GetAuditTrailAsync

Devuelve la traza de eventos generados por el procesamiento del sobre.

### Uso

```csharp
var auditEvents = await client.GetAuditTrailAsync(newDocumentSetId);
```` 

## Método GetHistoryAsync

Recupera un listado de los sobres enviados a firmar.

Permite filtrar por estado del sobre y fecha de envío.
Así como establecer un límite de registros a devolver e indicar un offset para implementar paginación.

### Uso

```csharp
var listOfDcoumentSets = await client.GetHistoryAsync(new DocumentSetFilter
{
    Status = new[] { DocumentSetStatusCode.Created, DocumentSetStatusCode.InProcess },
    FromDateTime = new DateTime(2021, 1, 1),
    ToDateTime = new DateTime(2021, 6, 1),
    Reference = "000001",
    Limit = 20,
    Offset = 40
});
```

## Método VerifyLegalAuditTrailAsync

Comprueba la validez de un certificado de trazabilidad.

### Uso

```csharp
var verifyResult = await client.VerifyLegalAuditTrail(base64FileConent);
```

## Método VerifySignedPDFAsync

Verifica las firmas de un documento PDF.

### Uso

```csharp
var signatures = await client.VerifySignedPDFAsync(signedPDFFileStream);
```
