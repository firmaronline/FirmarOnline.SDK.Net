using Xunit;

namespace FirmarOnline.Model.PSC.Tests.Recipients
{
    public class RecipientBaseTests
    {
        [Fact]
        public void Fails_RecipientBase_When_Phone_Is_Too_Short()
        {
            var r = new RecipientFlow { Name = "A", Email = "a@b.com", PhoneNumber = "+443" };
            var results = ValidationHelper.Validate(r);
            Assert.Contains(results, r => r.ErrorMessage != null && r.ErrorMessage.Contains("minimum length of '5'"));
        }

        [Fact]
        public void Valid_RecipientBase_When_No_Phone()
        {
            var r = new RecipientFlow { Name = "A", Email = "a@b.com" };
            Assert.True(ValidationHelper.IsValid(r));
        }

        [Fact]
        public void Valid_RecipientBase_When_Phone_Length_Is_At_Least_Five()
        {
            var r = new RecipientFlow { Name = "A", Email = "a@b.com", PhoneNumber = "+34620908089" };
            Assert.True(ValidationHelper.IsValid(r));
        }
    }
}
