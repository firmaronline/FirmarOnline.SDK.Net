using Xunit;

namespace FirmarOnline.Model.PSC.Tests.DocumentSetFlows
{
    public class DocumentSetFlowSimpleTests
    {
        private static DocumentSetFlow NewBaseDocSet(int countDocument = 1, int countRecipient = 1)
        {
            return new DocumentSetFlow
            {
                DocumentSetName = "Flow",
                FlowTokenId = "FLOWID-001",
                Recipients = SampleValuesHelper.RecipientFlowColletion(countRecipient),
                Documents = SampleValuesHelper.DocumentColletionWithPdf(countDocument)
            };
        }

        #region "DocumentSetFlows"

        [Fact]
        public void Succeeds_When_One_Document_And_Recipient()
        {
            var ds = NewBaseDocSet();

            var results = ValidationHelper.Validate(ds);
            Assert.Empty(results);
        }

        [Fact]
        public void Succeeds_When_Multiple_Document_And_Recipient()
        {
            var ds = NewBaseDocSet(countDocument: 3, countRecipient: 5);

            var results = ValidationHelper.Validate(ds);
            Assert.Empty(results);
        }

        [Fact]
        public void Fails_When_RecipientOrder_Required_And_NotZero()
        {
            var ds = NewBaseDocSet(countRecipient: 3);
            ds.Recipients[0].Order = 1;
            ds.Recipients[1].Order = null;
            ds.Recipients[2].Order = 3;

            var results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && (r.ErrorMessage.Contains("You must indicate the order to all recipients.")));

            ds.Recipients[0].Order = 1;
            ds.Recipients[1].Order = 0;
            ds.Recipients[2].Order = 3;

            results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && (r.ErrorMessage.Contains("You must indicate the order to all recipients.")));
        }

        #endregion

        #region "Documents"

        [Fact]
        public void Succeeds_When_WebFormTemplete()
        {
            var ds = NewBaseDocSet();
            ds.Documents[0] = SampleValuesHelper.DocumentWithFormTemplate;

            var results = ValidationHelper.Validate(ds);
            Assert.Empty(results);
        }

        [Fact]
        public void Fails_When_DocumentContent_Null()
        {
            var ds = NewBaseDocSet();
            ds.Documents[0].B64PDFContent = null;
#if NET6_0_OR_GREATER
            ds.Documents[0].Form = null;
#endif
            ds.Documents[0].FormId = null;

            var results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && r.ErrorMessage.Contains("Exactly one of the following properties must be provided: B64PDFContent, Form, or FormId"));
        }

        #endregion

        #region "Recpients"

        [Fact]
        public void Fails_When_Recipient_Phone_Is_Too_Short()
        {
            var ds = NewBaseDocSet();
            ds.Recipients[0].PhoneNumber = "+443";

            var results = ValidationHelper.Validate(ds);

            Assert.Contains(results, r => r.ErrorMessage != null && r.ErrorMessage.Contains("minimum length of '5'"));
        }

        #endregion

#if NET6_0_OR_GREATER

        [Fact]
        public void Fails_When_Documents_Not_Grouped()
        {
            var ds = NewBaseDocSet(countDocument: 3);
            ds.Documents[0] = SampleValuesHelper.DocumentWithPdf;
            ds.Documents[1] = SampleValuesHelper.DocumentWithForm;
            ds.Documents[2] = SampleValuesHelper.DocumentWithPdf;

            var results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && r.ErrorMessage.Contains("WebForms and PDFs must be groupe"));

            ds.Documents[0] = SampleValuesHelper.DocumentWithPdf;
            ds.Documents[1] = SampleValuesHelper.DocumentWithFormTemplate;
            ds.Documents[2] = SampleValuesHelper.DocumentWithPdf;

            results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && r.ErrorMessage.Contains("WebForms and PDFs must be groupe"));

            ds.Documents[0] = SampleValuesHelper.DocumentWithForm;
            ds.Documents[1] = SampleValuesHelper.DocumentWithPdf;
            ds.Documents[2] = SampleValuesHelper.DocumentWithForm;

            results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && r.ErrorMessage.Contains("WebForms and PDFs must be groupe"));
        }

        [Fact]
        public void Fails_When_WebForm_With_Multiple_Recipients()
        {
            var ds = NewBaseDocSet(countRecipient: 2);
            ds.Documents[0] = SampleValuesHelper.DocumentWithForm;

            var results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && r.ErrorMessage.Contains("WebForms can only have one recipient"));
        }

        [Fact]
        public void Succeeds_When_WebForm()
        {
            var ds = NewBaseDocSet();
            ds.Documents[0] = SampleValuesHelper.DocumentWithForm;

            var results = ValidationHelper.Validate(ds);
            Assert.Empty(results);
        }
#endif

    }
}
