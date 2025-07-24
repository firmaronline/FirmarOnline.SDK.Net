using ESSNET.Model.Verify;
using FirmarOnline.Model;
using FirmarOnline.Model.PSC;
using FirmarOnline.Types;
using FirmarOnline.Types.Certificates;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FirmarOnline.Clients.PSC
{
    public partial class PSCClient
    {
        /// <summary>
        /// Verifica que un certificado de trazabilidad es válido
        /// </summary>
        /// <param name="legalAuditTrailContent">Contenido del certificado de trazabilidad</param>
        /// <returns>Verificación de la firma con extracción de la información del firmante y las trazas</returns>
        public async Task<VerifyLegalAuditTrail> VerifyLegalAuditTrailAsync(string legalAuditTrailContent)
        {
            var result = await PostAsync<LegalAuditTrail, VerifyLegalAuditTrail>(
                new Uri(_verifyApiBaseAddress, "legalAuditTrail"),
                new LegalAuditTrail { LegalAuditTrailContent = legalAuditTrailContent });

            CheckResponseStatus(result);
            return result.Value;
        }

        /// <summary>
        /// Verifica las firmas de un documento PDF
        /// </summary>
        /// <param name="pdfFile">Contenido del fichero PDF firmado</param>
        /// <param name="mode">Modo de verificación de firma</param>
        /// <param name="certificate">Certificado para descifrar los datos biométricos</param>
        /// <returns>Información de verificación de las firmas del documento</returns>
        public async Task<Signatures> VerifySignedPDFAsync(Stream pdfFile, VerifyMode mode, PKCS12Certificate certificate = null)
        {
            var base64 = await Stream2Base64Async(pdfFile);
            return await VerifySignedPDFAsync(base64, mode, certificate);
        }

        /// <summary>
        /// Verifica las firmas de un documento PDF
        /// </summary>
        /// <param name="b64PDFContent">Contenido del fichero PDF firmado en base 64</param>
        /// <param name="mode">Modo de verificación de firma</param>
        /// <param name="certificate">Certificado para descifrar los datos biométricos</param>
        /// <returns>Información de verificación de las firmas del documento</returns>
        public async Task<Signatures> VerifySignedPDFAsync(string b64PDFContent, VerifyMode mode, PKCS12Certificate certificate = null)
        {
            var result = await PostAsync<VerifyPDFDocument, Signatures>(
                new Uri(_verifyApiBaseAddress, "pdf"),
                new VerifyPDFDocument
                {
                    B64PDFContent = b64PDFContent,
                    Mode = mode,
                    Certificate = certificate
                });

            CheckResponseStatus(result);
            return result.Value;
        }
    }
}
