# 🔍 FirmarOnline.Clients.Verify

Cliente oficial en .NET para acceder a la API pública de [**Verify (Servicio de Validación)**](https://restapi.firmar.info/index.html?urls.primaryName=Servicio+de+Validaci%C3%B3n).  

Permite **validar firmas electrónicas en documentos PDF** y comprobar la **validez de certificados de trazabilidad (LegalAuditTrail)**.

---

## 🧭 Índice
1. [⚙️ Instalación e inicialización](#️-instalación-e-inicialización)  
2. [🛠️ Compatibilidad](#️-compatibilidad)  
3. [📡 Métodos disponibles](#-métodos-disponibles)  
   - 🔏 [Verificación de firmas](#-verificación-de-firmas)  
   - 📜 [Verificación de trazabilidad](#-verificación-de-trazabilidad)  
4. [🖥️ Ejemplos en aplicación de consola](#️-ejemplos-en-aplicación-de-consola)  

---

## ⚙️ Instalación e inicialización

Instalar el paquete NuGet:

```bash
dotnet add package FirmarOnline.Clients.Verify
```

Crear instancia del cliente en **sandbox** o **producción**:

```csharp
// Sandbox
var client = new VerifyClient(VerifyClient.VerifySandboxEnvironmentUrl, "<api_key_o_token>");

// Producción
var client = new VerifyClient(VerifyClient.VerifyProductionEnvironmentUrl, "<api_key_o_token>");
```

---

## 🛠️ Compatibilidad

Este cliente se compila para dos frameworks:

- 🟢 **.NET 8.0** → versión moderna y recomendada  
- 🟦 **.NET Standard 2.0** → compatibilidad con proyectos existentes  

---

## 📡 Métodos disponibles

### 🔏 Verificación de firmas

- **Verificar firmas en un documento PDF**  
  Método: `VerifySignedPDFAsync`  
  Permite analizar un documento PDF firmado electrónicamente y obtener el detalle de cada firma incluida.  

---

### 📜 Verificación de trazabilidad

- **Verificar certificado de trazabilidad (LegalAuditTrail)**  
  Método: `VerifyLegalAuditTrailAsync`  
  Comprueba la validez del **LegalAuditTrail** emitido en un proceso de firma.  

---

## 🖥️ Ejemplos en aplicación de consola

La solución incluye un cliente de consola en **.NET 8** con ejemplos listos para ejecutar:  

📂 Ruta:  
```plaintext
\FirmarOnline.SDK\samples\FirmarOnline.Samples.ConsoleClient\FirmarOnline.Samples.ConsoleClient.sln
```

En este proyecto se encuentran implementados los escenarios de **Verify**:  

- 🔏 `Verify/VerifySignedPDF.cs` → Ejemplo de **verificar firmas en un PDF**  
- 📜 `Verify/VerifyLegalAuditTrail.cs` → Ejemplo de **verificar un LegalAuditTrail**  

👉 Consulta también el [README del Console Client](../../samples/FirmarOnline.Samples.ConsoleClient/README.md) para más información sobre la ejecución.
