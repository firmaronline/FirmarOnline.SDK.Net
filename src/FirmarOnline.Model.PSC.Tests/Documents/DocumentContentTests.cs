using FirmarOnline.Model.Validations;
using Xunit;

namespace FirmarOnline.Model.PSC.Tests.Documents
{
    public class DocumentContentTests
    {

        [Fact]
        public void Succeeds_When_Only_FormId_Is_Provided()
        {
            var doc = new DocumentContent { Name = "Doc", FormId = "WF-001" };
            Assert.True(ValidationHelper.IsValid(doc));
        }

        [Fact]
        public void Succeeds_When_Only_B64PDFContent_Is_Provided()
        {
            var doc = new DocumentContent { Name = "Doc", B64PDFContent = "JVBERi0xLjMKJSVFT0Y=" };
            Assert.True(ValidationHelper.IsValid(doc));
        }


        [Fact]
        public void Fails_When_No_Content_Field_Is_Provided()
        {
            var doc = new DocumentContent { Name = "Doc" };
            var results = ValidationHelper.Validate(doc);
            Assert.Contains(results, r => r.ErrorMessage != null && r.ErrorMessage.Contains("Exactly one of the following properties must be provided"));
        }

        [Fact]
        public void Fails_When_More_Than_One_Content_Field_Is_Provided()
        {
            var doc = new DocumentContent { Name = "Doc", FormId = "WF-001", B64PDFContent = "JVBERi0xLjMKJSVFT0Y=" };
            var results = ValidationHelper.Validate(doc);
            Assert.Contains(results, r => r.ErrorMessage != null && r.ErrorMessage.Contains("Exactly one of the following properties"));
        }

        [Fact]
        public void Fails_When_B64PDFContent_Is_Not_Correct_Format()
        {
            var doc = new DocumentContent { Name = "Doc", B64PDFContent = "XXXXXXXX=" };
            var results = ValidationHelper.Validate(doc);
            Assert.Contains(results, r => r.ErrorMessage != null && r.ErrorMessage.Contains("El documento debe ser un fichero PDF válido"));
        }

        [Fact]
        public void Fails_WhenBase64PDFAttribute_WhenIsEmpty()
        {
            // Given
            var base64PDFContent = "";
            var attribute = new Base64PDFAttribute();

            // When
            var result = attribute.IsValid(base64PDFContent);

            // Then
            Assert.False(result);
        }

#if NET6_0_OR_GREATER

        [Fact]
        public void Succeeds_When_Only_Form_Is_Provided()
        {
            var form = SampleValuesHelper.DocumentForm;
            var doc = new DocumentContent { Name = "Doc", Form = form };
            Assert.True(ValidationHelper.IsValid(doc));
        }

#endif

    }
}
