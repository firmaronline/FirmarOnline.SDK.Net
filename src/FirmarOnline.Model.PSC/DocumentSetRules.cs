using System;
using System.Collections.Generic;
using System.Linq;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Reglas de validación para la composición de un conjunto de documentos (document set)
    /// antes de cursar una operación de firma.
    /// </summary>
    internal static class DocumentSetRules
    {
        /// <summary>
        /// Validación de que si hay formularios no puede haber más de un destinatario.
        /// </summary>
        /// <param name="documents">Enumeración de documentos a validar.</param>
        /// <param name="recipients">Enumeración de destinatarios del envío.</param>
        /// <returns>
        /// <see langword="true"/> si:
        /// <list type="bullet">
        /// <item><description>hay exactamente un destinatario; o</description></item>
        /// <item><description>no existe ningún documento con formulario (WebForm).</description></item>
        /// </list>
        /// En caso contrario, devuelve <see langword="false"/>.
        /// </returns>
        internal static bool CheckDocumentTypeByRecipients(IEnumerable<Document> documents, IEnumerable<RecipientBase> recipients)
        {
#if NET6_0_OR_GREATER
            return recipients.Count() == 1 || documents.All(doc => doc.Form == null && doc.FormId == null);
#else
            return recipients.Count() == 1 || documents.All(doc => doc.FormId == null);
#endif
        }

        /// <summary>
        /// Validación de que si hay un Action Type 60 no puede haber ningún WebForm.
        /// </summary>
        /// <param name="documentsContent">Enumeración con el contenido de los documentos.</param>
        /// <param name="recipients">Enumeración de destinatarios.</param>
        /// <returns>
        /// <see langword="false"/> si hay al menos un destinatario con
        /// <see cref="RecipientActionType.CryptoAPISignature"/> y existe algún WebForm; en caso contrario,
        /// <see langword="true"/>.
        /// </returns>
        internal static bool CheckDocumentTypeByActionType60(IEnumerable<DocumentContent> documentsContent, IEnumerable<RecipientWithSignatureType> recipients)
        {
            if (recipients.Any(r => r.ActionType == RecipientActionType.CryptoAPISignature) &&
#if NET6_0_OR_GREATER
                documentsContent.Any(doc => doc.Form != null || doc.FormId != null))
#else
                documentsContent.Any(doc => doc.FormId != null))
#endif
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Valida que, si hay firma corporativa al inicio, no exista ningún documento WebForm.
        /// </summary>
        /// <param name="corporateSignatureBase">Definición de la firma corporativa.</param>
        /// <param name="documentsContent">Contenidos de los documentos a firmar.</param>
        /// <returns>
        /// <see langword="false"/> si existe WebForm y la firma corporativa está configurada al inicio
        /// (<see cref="CorporateSignatureType.Start"/> o <see cref="CorporateSignatureType.StartAndEnd"/>);
        /// en caso contrario, <see langword="true"/>.
        /// </returns>
        internal static bool CheckDocumentTypeByCorporateSignature(CorporateSignatureBase corporateSignatureBase, IEnumerable<DocumentContent> documentsContent)
        {
            // Validaciones si el contenido del documento es WebForm.
#if NET6_0_OR_GREATER
            if (documentsContent.Any(doc => doc.Form != null || doc.FormId != null))
#else
            if (documentsContent.Any(doc => doc.FormId != null))
#endif
            {
                // Hay que verificar que el documento WebForm no tenga firma corporativa al inicio.
                if (corporateSignatureBase != null &&
                    (corporateSignatureBase.Type == CorporateSignatureType.Start || corporateSignatureBase.Type == CorporateSignatureType.StartAndEnd))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
