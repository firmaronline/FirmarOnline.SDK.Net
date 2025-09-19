using Xunit;

namespace FirmarOnline.Model.PSC.Tests.Recipients
{
    public class RecipientWithSignatureTypeTests
    {
        [Fact]
        public void Succeeds_RecipientWithSignatureType_When_AccessCode()
        {
            var r = new Recipient
            {
                Name = "A",
                Email = "a@b.com",
                AuthType = RecipientAuthenticationType.AccessCode,
                ActionType = RecipientActionType.OTPSignature, 
                AccessCode = new RecipientAccessCode { Challenge = "Informe su DNI" }
            };
            var results = ValidationHelper.Validate(r);
            Assert.True(ValidationHelper.IsValid(results));
        }

        [Fact]
        public void Fails_RecipientWithSignatureTyp_When_AccessCode_Not_Challenge()
        {
            var r = new Recipient
            {
                Name = "A", Email = "a@b.com",
                AuthType = RecipientAuthenticationType.AccessCode,
                ActionType = RecipientActionType.OTPSignature, // any valid action type (not certified notification)
                AccessCode = new RecipientAccessCode { Challenge = null }
            };
            var results = ValidationHelper.Validate(r);
            Assert.Contains(results, x => x.ErrorMessage != null && x.ErrorMessage.Contains("access code challenge"));
        }

        [Fact]
        public void Succeeds_RecipientWithSignatureType_When_CertifiedNotification()
        {
            var r = new Recipient
            {
                Name = "A",
                Email = "a@b.com",
                AuthType = RecipientAuthenticationType.None,
                ActionType = RecipientActionType.CertifiedNotification,
            };
            var results = ValidationHelper.Validate(r);
            Assert.True(ValidationHelper.IsValid(results));
        }

        [Fact]
        public void Fails_RecipientWithSignatureType_When_CertifiedNotification_And_Attachments()
        {
            var r = new Recipient
            {
                Name = "A", Email = "a@b.com",
                AuthType = RecipientAuthenticationType.None,
                ActionType = RecipientActionType.CertifiedNotification,
                Attachments = new List<RecipientDefinitionAttachment>
                {
                    new RecipientDefinitionAttachment { Description = "Doc", Required = true }
                }
            };
            var results = ValidationHelper.Validate(r);
            Assert.Contains(results, x => x.ErrorMessage != null && x.ErrorMessage.Contains("Attachments"));
        }
    }
}
