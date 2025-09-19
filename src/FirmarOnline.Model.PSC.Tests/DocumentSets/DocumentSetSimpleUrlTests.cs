using Xunit;

namespace FirmarOnline.Model.PSC.Tests.DocumentSets
{
    public class DocumentSetSimpleUrlTests
    {
        private static SimpleDocumentSet NewBaseDocSet()
        {
            return new SimpleDocumentSet
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
        public void Succeeds_When_AuthType_None_ActionType_BioSignature()
        {
            var ds = NewBaseDocSet();

            var results = ValidationHelper.Validate(ds);
            Assert.Empty(results);
        }

#if NET6_0_OR_GREATER

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
#endif

    }
}
