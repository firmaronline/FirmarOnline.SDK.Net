using Xunit;

namespace FirmarOnline.Model.PSC.Tests.CorporateSignatures
{
    public class CorporateSignatureBaseTests
    {
        [Fact]
        public void Requires_Id_When_Type_Is_Not_None()
        {
            var sig = new CorporateSignature { Type = CorporateSignatureType.Start };
            var results = ValidationHelper.Validate(sig);
            Assert.Contains(results, r => r.ErrorMessage != null && r.ErrorMessage.Contains("CorporateSignatureId"));
        }

        [Fact]
        public void Valid_When_Type_Is_None()
        {
            var sig = new CorporateSignature { Type = CorporateSignatureType.None };
            Assert.True(ValidationHelper.IsValid(sig));
        }

        [Fact]
        public void Valid_When_Type_Not_None_And_Id_Provided()
        {
            var sig = new CorporateSignature { Type = CorporateSignatureType.End, CorporateSignatureId = "CORP-1" };
            Assert.True(ValidationHelper.IsValid(sig));
        }
    }
}
