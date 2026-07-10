using Xunit;

namespace FirmarOnline.Model.PSC.Tests.Recipients
{
    public class AccessCodeTests
    {
        [Fact]
        public void Succeeds_When_Format_Is_Valid_Regex()
        {
            var accessCode = new AccessCode
            {
                Challenge = "Informe su DNI:",
                Format = "^[0-9]{8}$"
            };

            var results = ValidationHelper.Validate(accessCode);
            Assert.True(ValidationHelper.IsValid(results));
        }

        [Fact]
        public void Succeeds_When_Format_Is_Null()
        {
            var accessCode = new AccessCode
            {
                Challenge = "Informe su DNI:"
            };

            var results = ValidationHelper.Validate(accessCode);
            Assert.True(ValidationHelper.IsValid(results));
        }

        [Fact]
        public void Fails_When_Format_Is_Not_Valid_Regex()
        {
            var accessCode = new AccessCode
            {
                Challenge = "Informe su DNI:",
                Format = "[invalid("
            };

            var results = ValidationHelper.Validate(accessCode);
            Assert.Contains(results, r => r.ErrorMessage != null && r.ErrorMessage.Contains("The Format is not a valid regular expression"));
        }
    }
}
