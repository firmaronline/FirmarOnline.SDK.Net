using System;
using System.ComponentModel.DataAnnotations;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Opciones de filtrado para consultas de sobres
    /// </summary>
    [CustomValidation(typeof(DocumentSetQueryFilter), nameof(ValidateDocumentSetFilter),
        ErrorMessage = "The query is too expensive. Set a date range no greater than 90 days.")]
    public class DocumentSetQueryFilter : DateRangeFilterBase
    {
        /// <summary>
        /// Códigos de estado a mostrar
        /// </summary>
        public DocumentSetStatusCode[] Status { get; set; }

        /// <summary>
        /// Referencia externa de sobre para cliente
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// Nombre del sobre
        /// </summary>
        public string DocumentSetName { get; set; }

        /// <summary>
        /// Nombre del destinatario
        /// </summary>
        public string RecipientName { get; set; }

        /// <summary>
        /// Email del destinatario
        /// </summary>
        public string RecipientEmail { get; set; }

        /// <summary>
        /// Número de teléfono del destinatario
        /// </summary>
        public string RecipientPhoneNumber { get; set; }

        /// <summary>
        /// Identificación (DNI, NIF,...) del destinatario
        /// </summary>
        public string RecipientCardId { get; set; }

        /// <summary>
        /// Nombre de documento
        /// </summary>
        public string DocumentName { get; set; }

        /// <summary>
        /// Equipos a mostrar
        /// </summary>
        public string[] Teams { get; set; }

        /// <summary>
        /// Método de envío a destinatarios
        /// </summary>
        public SendMethod[] SendMethod { get; set; }

        /// <summary>
        /// Tipos de acciones a realizar por los destinatarios de sobre.
        /// </summary>
        public RecipientActionType[] RecipientActionTypes { get; set; }

        /// <summary>
        /// Identificador de usuario.
        /// </summary>
        public bool OnlyCurrentUser { get; set; } = false;

        /// <summary>
        /// Validación del filtro de sobres.
        /// Comprueba que la consulta a realizar no sea excesivamente costosa.
        /// Si realiza filtros por campos no indexados se debe acotar a un rango de fechas no
        /// superior a 90 días.
        /// </summary>
        /// <param name="documentSetFilter">Filtro de sobres</param>
        /// <returns><see cref="ValidationResult"/> con el resultado de la validación</returns>
        public static ValidationResult ValidateDocumentSetFilter(DocumentSetQueryFilter documentSetFilter)
        {
            // Si el rango de fechas es mayor de 90 días no se puede buscar por campos no indexados
            if (documentSetFilter.FromDateTime is null
                || ((documentSetFilter.ToDateTime ?? DateTime.UtcNow) - documentSetFilter.FromDateTime.Value).TotalDays > 90)
            {
                var errorMessage = "The query is too expensive. To search by {0} set a date range no greater than 90 days.";
                if (!string.IsNullOrEmpty(documentSetFilter.DocumentSetName))
                {
                    return new ValidationResult(string.Format(errorMessage, "documentset name"), [nameof(DocumentSetName)]);
                }
                if (!string.IsNullOrEmpty(documentSetFilter.RecipientName))
                {
                    return new ValidationResult(string.Format(errorMessage, "recipient name"), [nameof(RecipientName)]);
                }
                if (!string.IsNullOrEmpty(documentSetFilter.RecipientEmail))
                {
                    return new ValidationResult(string.Format(errorMessage, "recipient email"), [nameof(RecipientEmail)]);
                }
                if (!string.IsNullOrEmpty(documentSetFilter.RecipientPhoneNumber))
                {
                    return new ValidationResult(string.Format(errorMessage, "recipient phone number"), [nameof(RecipientPhoneNumber)]);
                }
                if (!string.IsNullOrEmpty(documentSetFilter.RecipientCardId))
                {
                    return new ValidationResult(string.Format(errorMessage, "recipient card id"), [nameof(RecipientCardId)]);
                }
                if (!string.IsNullOrEmpty(documentSetFilter.DocumentName))
                {
                    return new ValidationResult(string.Format(errorMessage, "document name"), [nameof(DocumentName)]);
                }
            }

            return ValidationResult.Success;
        }
    }
}