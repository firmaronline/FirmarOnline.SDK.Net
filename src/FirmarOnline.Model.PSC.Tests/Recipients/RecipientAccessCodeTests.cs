using Xunit;

namespace FirmarOnline.Model.PSC.Tests.Recipients
{
    public class RecipientAccessCodeTests
    {
        [Fact]
        public void Succeeds_When_Response_Matches_Format()
        {
            var accessCode = new RecipientAccessCode
            {
                Challenge = "Informe su DNI:",
                Response = "12345678",
                Format = "^[0-9]{8}$"
            };

            var results = ValidationHelper.Validate(accessCode);
            Assert.True(ValidationHelper.IsValid(results));
        }

        [Fact]
        public void Fails_When_Response_Does_Not_Match_Format()
        {
            var accessCode = new RecipientAccessCode
            {
                Challenge = "Informe su DNI:",
                Response = "ABC",
                Format = "^[0-9]{8}$"
            };

            var results = ValidationHelper.Validate(accessCode);
            Assert.Contains(results, r => r.ErrorMessage != null && r.ErrorMessage.Contains("The Response does not match the specified Format"));
        }

        [Fact]
        public void Succeeds_When_Format_Is_Null()
        {
            var accessCode = new RecipientAccessCode
            {
                Challenge = "Informe su DNI:",
                Response = "cualquier cosa"
            };

            var results = ValidationHelper.Validate(accessCode);
            Assert.True(ValidationHelper.IsValid(results));
        }

        [Fact]
        public void Succeeds_When_Response_Is_Null()
        {
            var accessCode = new RecipientAccessCode
            {
                Challenge = "Informe su DNI:",
                Format = "^[0-9]{8}$"
            };

            var results = ValidationHelper.Validate(accessCode);
            Assert.True(ValidationHelper.IsValid(results));
        }

        [Fact]
        public void Fails_When_Format_Is_Not_Valid_Regex()
        {
            var accessCode = new RecipientAccessCode
            {
                Challenge = "Informe su DNI:",
                Response = "12345678",
                Format = "[invalid("
            };

            var results = ValidationHelper.Validate(accessCode);
            Assert.Contains(results, r => r.ErrorMessage != null && r.ErrorMessage.Contains("The Format is not a valid regular expression"));
        }
    }
}