using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Define un sobre de firma remota con un único documento y destinatario
    /// </summary>
    [CustomValidation(typeof(SimpleDocumentSet), nameof(ValidateDocumentTypeByCorporateSignature), ErrorMessage = "The documentSet definition is not valid.")]
    [CustomValidation(typeof(SimpleDocumentSet), nameof(ValidateDocumentTypeByActionType60), ErrorMessage = "The documentSet definition is not valid.")]
    public class SimpleDocumentSet : DocumentSetStandAloneBase
    {
        /// <summary>
        /// Firma corporativa
        /// </summary>
        public SingleDocumentCorporateSignature CorporateSignature { get; set; }

        /// <summary>
        /// Documento
        /// </summary>
        public DocumentContent Document { get; set; }

        /// <summary>
        /// Destinatario del sobre
        /// </summary>
        public SingleDocumentRecipient Recipient { get; set; }

        /// <summary>
        /// Validación de que si hay un Action Type 60 no puede haber ningún WebForm.
        /// </summary>
        public static ValidationResult ValidateDocumentTypeByActionType60(SimpleDocumentSet simpleDocumentSet)
        {
            if (DocumentSetRules.CheckDocumentTypeByActionType60([simpleDocumentSet.Document], [simpleDocumentSet.Recipient]))
                return ValidationResult.Success;
            else
                return new ValidationResult("A document set cannot contain recipients with Action Type 60 and WebForms.", [nameof(simpleDocumentSet.Document)]);
        }

        /// <summary>
        /// Validación de que si hay firma corporativa al inicio no puede haber ningún WebForm.
        /// </summary>
        public static ValidationResult ValidateDocumentTypeByCorporateSignature(SimpleDocumentSet simpleDocumentSet)
        {
            if (DocumentSetRules.CheckDocumentTypeByCorporateSignature(simpleDocumentSet.CorporateSignature, [simpleDocumentSet.Document]))
                return ValidationResult.Success;
            else
                return new ValidationResult("It is not possible set a corporate signature at the beginning if the content of the document is a WebForm.");
        }
    }

    /// <summary>
    /// Define un sobre de firma remota con un único documento y destinatario
    /// especificando el método de envío de la url
    /// </summary>
    public class SimpleDocumentSetWithSendMethod : SimpleDocumentSet
    {
        private SendMethod sendMethod = SendMethod.Email;

        /// <summary>
        /// Método de envío
        /// </summary>
        [EnumDataType(typeof(SendMethod))]
        public SendMethod SendMethod
        {
            get => sendMethod;
            set
            {
                if (value == SendMethod.None)
                {
                    throw new InvalidEnumArgumentException("Se debe especificar un método de envío para las urls.");
                }
                sendMethod = value;
            }
        }
    }
}