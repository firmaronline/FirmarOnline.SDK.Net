using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Define un sobre de firma remota con un único documento y destinatario
    /// </summary>
    [CustomValidation(typeof(SimpleDocumentSet), nameof(ValidateSimpleDocumentSet), ErrorMessage = "The simple documentSet definition is not valid.")]
    [CustomValidation(typeof(SimpleDocumentSet), nameof(ValidateDocumentTypeByActionType60), ErrorMessage = "The documentSet definition is not valid.")]
    public class SimpleDocumentSet : DocumentSetStandAloneBase
    {
        /// <summary>
        /// Firma corporativa
        /// </summary>
        public CorporateSignatureSimple CorporateSignature { get; set; }

        /// <summary>
        /// Documento
        /// </summary>
        public DocumentContent Document { get; set; }

        /// <summary>
        /// Destinatario del sobre
        /// </summary>
        public SingleDocumentRecipient Recipient { get; set; }

        /// <summary>
        /// Validación de la definición del sobre simple.
        /// </summary>
        /// <param name="simpleDocumentSet">Definición del sobre simple.</param>
        /// <returns></returns>
        public static ValidationResult ValidateSimpleDocumentSet(SimpleDocumentSet simpleDocumentSet)
        {
            // Validaciones si el contenido del documento es WebForm.
            if (simpleDocumentSet.Document.Form != null || simpleDocumentSet.Document.FormId != null)
            {
                // Hay que verificar que el documento WebForm no tenga firma corporativa al inicio.
                if (simpleDocumentSet.CorporateSignature != null &&
                    (simpleDocumentSet.CorporateSignature.Type == CorporateSignatureType.Start || simpleDocumentSet.CorporateSignature.Type == CorporateSignatureType.StartAndEnd))
                {
                    return new ValidationResult("It is not possible set a corporate signature at the beginning if the content of the document is a WebForm.");
                }

                // Hay que verificar que el documento WebForm no se firme con certificado.
                if (simpleDocumentSet.Recipient.ActionType == RecipientActionType.CryptoAPISignature)
                {
                    return new ValidationResult("It is not possible to set sign with customer certificate as the recipient action type if the content of the document is a WebFrom.");
                }
            }
            return ValidationResult.Success;
        }

        /// <summary>
        /// Validación de que si hay un Action Type 60 no puede haber ningún WebForm.
        /// </summary>
        public static ValidationResult ValidateDocumentTypeByActionType60(SimpleDocumentSet simpleDocumentSet)
        {
            if (CheckDocumentTypeByActionType60(new List<DocumentContent> { simpleDocumentSet.Document }, new List<RecipientWithSignatureType> { simpleDocumentSet.Recipient }))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("A document set cannot contain recipients with Action Type 60 and WebForms.", new string[] { nameof(simpleDocumentSet.Document) });
            }
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