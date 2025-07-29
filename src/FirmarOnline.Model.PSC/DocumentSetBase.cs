using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Clase base para la definición de sobres de documentos
    /// para el módulo de firma remota.
    /// </summary>
    public abstract class DocumentSetBase
    {
        /// <summary>
        /// Nombre del sobre.
        /// </summary>
        [MaxLength(255)]
        public string DocumentSetName { get; set; }

        /// <summary>
        /// Descripción del sobre.
        /// </summary>
        [MaxLength(255)]
        public string Description { get; set; }

        /// <summary>
        /// Referencia externa de sobre para cliente.
        /// </summary>
        [MaxLength(64)]
        public string Reference { get; set; }

        /// <summary>
        /// Identificador único de equipo.
        /// </summary>
        [MaxLength(80)]
        public string TeamId { get; set; }

        /// <summary>
        /// Validación de que los documentos estén agrupados. Todos los PDFs juntos y todos los Forms juntos.
        /// </summary>
        /// <param name="documents">Enumeración de documentos.</param>
        /// <returns>True si los documentos están agrupados por tipo. False en caso contrario.</returns>
        internal static bool CheckDocumentsSortedByType(IEnumerable<Document> documents)
        {
            if (!documents.Any()) return true;

#if NET6_0_OR_GREATER
            var documentTypes = documents.Select(d => d.Form != null || d.FormId != null ? 0 : 1).ToList();
#else
            var documentTypes = documents.Select(d => d.FormId != null ? 0 : 1).ToList();
#endif

            // Orden ascendente
            if (documentTypes.FirstOrDefault() == 0)
                return documentTypes.Zip(documentTypes.Skip(1), (curr, next) => curr <= next).All(x => x);
            else
                return documentTypes.Zip(documentTypes.Skip(1), (curr, next) => curr >= next).All(x => x);
        }

        /// <summary>
        /// Validación de que si hay formularios no puede haber más de un destinatario.
        /// </summary>
        /// <param name="documents">Enumeración de documentos.</param>
        /// <param name="recipients">Número de destinatarios.</param>
        /// <returns>False si hay más de un destinatario y uno o más formularios. True en caso contrario.</returns>
        internal static bool CheckDocumentTypeByRecipients(IEnumerable<Document> documents, IEnumerable<RecipientBase> recipients)
        {
#if NET6_0_OR_GREATER
            return recipients.Count() == 1 || documents.All(doc => doc.Form == null && doc.FormId == null);
#else
            return recipients.Count() == 1 || documents.All(doc => doc.FormId == null);
#endif
        }

        /// <summary>
        /// Validación de que no puede haber más de un formulario definido mediante un identificador de formulario.
        /// </summary>
        /// <param name="documents">Enumeración de documentos.</param>
        /// <returns>False si hay más de un formulario definido por plantilla. True en caso contrario.</returns>
        internal static bool CheckOnlyOneFormId(IEnumerable<Document> documents)
        {
            if (documents.Count(d => d.FormId != null) > 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Validación de que si hay un Action Type 60 no puede haber ningún WebForm.
        /// </summary>
        /// <param name="documentsContent">Enumeración de contenido de documentos.</param>
        /// <param name="recipients">Número de destinatarios.</param>
        /// <returns></returns>
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
    }
}