using System.ComponentModel.DataAnnotations;
using Xunit;

namespace FirmarOnline.Model.PSC.Tests.Documents
{
    public class DocumentColletionTests
    {
        [Fact]
        public void Fails_When_More_Than_One_FormId_Is_Present()
        {
            var docs = new DocumentColletion
            {
                new Document { Name = "A", FormId = "WF-1" },
                new Document { Name = "B", FormId = "WF-2" }
            };

            var result = DocumentColletion.ValidateOnlyOneFormId(docs);
            Assert.NotEqual(ValidationResult.Success, result);
        }

        [Fact]
        public void Fails_When_Forms_And_Pdfs_Are_Not_Grouped()
        {
            var docs = new DocumentColletion
            {
                new Document { Name = "A", FormId = "WF-1" },
                new Document { Name = "B", B64PDFContent = "PDF" },
                new Document { Name = "C", FormId = "WF-2" }
            };

            var result = DocumentColletion.ValidateDocumentsSortedByType(docs);
            Assert.NotEqual(ValidationResult.Success, result);
        }

        [Fact]
        public void Succeeds_When_Forms_Then_Pdfs_Are_Grouped()
        {
            var docs = new DocumentColletion
            {
                new Document { Name = "A", FormId = "WF-1" },
                new Document { Name = "B", FormId = "WF-2" },
                new Document { Name = "C", B64PDFContent = "PDF" },
            };

            var result = DocumentColletion.ValidateDocumentsSortedByType(docs);
            Assert.Equal(ValidationResult.Success, result);
        }

        [Fact]
        public void Succeeds_When_Pdfs_Then_Forms_Are_Grouped()
        {
            var docs = new DocumentColletion
            {
                new Document { Name = "A", B64PDFContent = "PDF" },
                new Document { Name = "B", B64PDFContent = "PDF" },
                new Document { Name = "C", FormId = "WF-1" },
            };

            var result = DocumentColletion.ValidateDocumentsSortedByType(docs);
            Assert.Equal(ValidationResult.Success, result);
        }
    }
}
