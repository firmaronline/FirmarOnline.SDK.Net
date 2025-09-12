# ✍️ FirmarOnline.Clients.ESign

Cliente oficial en .NET para acceder a la API pública de [**eSign (Servicio de Firma Avanzada)**](https://restapi.firmar.info/index.html?urls.primaryName=Servicio+de+Firma+Avanzada).  

Permite **firmar documentos PDF con certificado**, añadir **sellos de tiempo (TSA)** y gestionar **OTP (One Time Password)** para la autenticación de firmantes.

---

## 🧭 Índice
1. [⚙️ Instalación e inicialización](#️-instalación-e-inicialización)  
2. [🛠️ Compatibilidad](#️-compatibilidad)  
3. [📡 Métodos disponibles](#-métodos-disponibles)  
   - ✒️ [Firma de documentos](#️-firma-de-documentos)  
   - 🔐 [Gestión de OTP](#-gestión-de-otp)  
4. [🖥️ Ejemplos en aplicación de consola](#️-ejemplos-en-aplicación-de-consola)  

---

## ⚙️ Instalación e inicialización

Instalar el paquete NuGet:

```bash
dotnet add package FirmarOnline.Clients.ESign
```

Crear instancia del cliente en **sandbox** o **producción**:

```csharp
// Sandbox
var client = new ESignClient(ESignClient.ESignSandboxEnvironmentUrl, "<api_key_o_token>");

// Producción
var client = new ESignClient(ESignClient.ESignProductionEnvironmentUrl, "<api_key_o_token>");
```

---

## 🛠️ Compatibilidad

Este cliente se compila para dos frameworks:

- 🟢 **.NET 8.0** → versión moderna y recomendada  
- 🟦 **.NET Standard 2.0** → compatibilidad con proyectos existentes  

---

## 📡 Métodos disponibles

### ✒️ Firma de documentos

- **Firmar PDF con certificado**  
  Método: `SignPdfAsync`  
  Permite firmar electrónicamente un documento PDF con un certificado válido.  

- **Añadir sello de tiempo (TSA)**  
  Método: `TimeStampAsync`  
  Aplica un sello de tiempo a un documento PDF para acreditar su existencia en un momento determinado.  

---

### 🔐 Gestión de OTP

- **Generar OTP**  
  Método: `GenerateOtpAsync`  
  Genera un código de un solo uso que se envía al firmante para autenticación.  

- **Validar OTP**  
  Método: `ValidateOtpAsync`  
  Valida el código OTP introducido por el firmante antes de continuar con el proceso de firma.  

---

## 🖥️ Ejemplos en aplicación de consola

La solución incluye un cliente de consola en **.NET 8** con ejemplos listos para ejecutar:  

📂 Ruta:  
```plaintext
\FirmarOnline.SDK\samples\FirmarOnline.Samples.ConsoleClient\FirmarOnline.Samples.ConsoleClient.sln
```

En este proyecto se encuentran implementados los escenarios de **ESign**:  

- ✒️ `ESign/SignPdf.cs` → Ejemplo de **firma PDF con certificado**  
- 🕒 `ESign/TimeStampPdf.cs` → Ejemplo de **añadir sello de tiempo (TSA)**  
- 🔑 `ESign/GenerateOtp.cs` → Ejemplo de **generar OTP**  
- ✅ `ESign/ValidateOtp.cs` → Ejemplo de **validar OTP**  

👉 Consulta también el [README del Console Client](../../samples/FirmarOnline.Samples.ConsoleClient/README.md) para más información sobre la ejecución.
