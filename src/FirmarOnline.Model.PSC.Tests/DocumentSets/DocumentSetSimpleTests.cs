
using System.ComponentModel;
using Xunit;

namespace FirmarOnline.Model.PSC.Tests.DocumentSets
{
    public class DocumentSetSimpleTests
    {
        private static SimpleDocumentSetWithSendMethod NewBaseDocSet()
        {
            return new SimpleDocumentSetWithSendMethod
            {
                DocumentSetName = "DocumentSet fortesting",
                SenderName = "Test Sender",
                SenderMail = "testsender@foo.com",
                ExpirationDaysTimeout = 10,
                Recipient = SampleValuesHelper.SingleDocumentRecipient,
                Document = SampleValuesHelper.DocumentWithPdf
            };
        }

        [Fact]
        public void Succeeds_Defaults_To_Email_SendMethod()
        {
            var simple = new SimpleDocumentSetWithSendMethod
            {
                DocumentSetName = "Sobre",
                Document = SampleValuesHelper.DocumentWithPdf,
                Recipient = SampleValuesHelper.SingleDocumentRecipient
            };

            Assert.Equal(SendMethod.Email, simple.SendMethod);
        }

        [Fact]
        public void Succeeds_When_AuthType_None_ActionType_BioSignature()
        {
            var ds = NewBaseDocSet();

            var results = ValidationHelper.Validate(ds);
            Assert.Empty(results);
        }

        [Fact]
        public void Succeeds_When_WebFormTemplete()
        {
            var ds = NewBaseDocSet();
            ds.Document = SampleValuesHelper.DocumentWithFormTemplate;

            var results = ValidationHelper.Validate(ds);
            Assert.Empty(results);
        }

        [Fact]
        public void Succeeds_When_ActionType60_With_Pdf()
        {
            var ds = NewBaseDocSet();
            ds.Recipient.ActionType = RecipientActionType.CryptoAPISignature;

            var results = ValidationHelper.Validate(ds);
            Assert.Empty(results);
        }


        [Fact]
        public void Fails_When_DocumentContent_Null()
        {
            var ds = NewBaseDocSet();
            ds.Document.B64PDFContent = null;
#if NET6_0_OR_GREATER
            ds.Document.Form = null;
#endif
            ds.Document.FormId = null;

            var results = ValidationHelper.Validate(ds);

            Assert.Contains(results, r => r.ErrorMessage != null && r.ErrorMessage.Contains("Exactly one of the following properties must be provided: B64PDFContent, Form, or FormId"));
        }

        [Fact]
        public void Fails_When_Phone_Is_Too_Short()
        {
            var ds = NewBaseDocSet();
            ds.Recipient.PhoneNumber = "+443";

            var results = ValidationHelper.Validate(ds);

            Assert.Contains(results, r => r.ErrorMessage != null && r.ErrorMessage.Contains("minimum length of '5'"));
        }

        [Fact]
        public void Fails_When_Recipient_AccessCode_Data_Required()
        {
            var ds = NewBaseDocSet();
            ds.Recipient.AuthType = RecipientAuthenticationType.AccessCode;

            var results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && r.ErrorMessage.Contains("The access code challenge data is required"));
        }

        [Fact]
        public void Succeeds_When_Recipient_AccessCode()
        {
            var ds = NewBaseDocSet();
            ds.Recipient.AuthType = RecipientAuthenticationType.AccessCode;
            ds.Recipient.AccessCode = new RecipientAccessCode()
            {
                Challenge = "Informe su DNI:",
                Response = "12345678X"
            };

            var results = ValidationHelper.Validate(ds);
            Assert.Empty(results);
        }

#if NET6_0_OR_GREATER

        [Fact]
        public void Succeeds_When_CorporateSignature_End_With_WebForm()
        {
            var ds = NewBaseDocSet();
            ds.Document = SampleValuesHelper.DocumentWithForm;
            ds.CorporateSignature = new SingleDocumentCorporateSignature
            {
                Type = CorporateSignatureType.End,
                CorporateSignatureId = "CORP-1"
            };

            var results = ValidationHelper.Validate(ds);
            Assert.Empty(results);
        }

        [Fact]
        public void Succeeds_When_WebForm()
        {
            var ds = NewBaseDocSet();
            ds.Document = SampleValuesHelper.DocumentWithForm;

            var results = ValidationHelper.Validate(ds);
            Assert.Empty(results);
        }

        [Fact]
        public void Fails_When_CorporateSignature_Start_With_WebForm()
        {
            var ds = NewBaseDocSet();
            ds.Document = SampleValuesHelper.DocumentWithForm;
            ds.CorporateSignature = new SingleDocumentCorporateSignature
            {
                Type = CorporateSignatureType.Start,
                CorporateSignatureId = "CORP-1"
            };

            var results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && (r.ErrorMessage.Contains("It is not possible set a corporate signature at the beginning if the content of the document is a WebForm")));
        }

        [Fact]
        public void Fails_When_ActionType60_With_WebForm()
        {
            var ds = NewBaseDocSet();
            ds.Document = SampleValuesHelper.DocumentWithForm;
            ds.Recipient.ActionType = RecipientActionType.CryptoAPISignature;

            var results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null);
        }

        [Fact]
        public void Fails_Throws_When_SendMethod_No_Set()
        {
            var simple = new SimpleDocumentSetWithSendMethod
            {
                DocumentSetName = "Sobre",
                Document = SampleValuesHelper.DocumentWithForm,
                Recipient = SampleValuesHelper.SingleDocumentRecipient
            };

            Assert.Throws<InvalidEnumArgumentException>(() => simple.SendMethod = SendMethod.None);
        }

#endif
    }
}
