using Xunit;

namespace FirmarOnline.Model.PSC.Tests.DocumentSetFlows
{
    public class DocumentSetFlowTests
    {
        private static DocumentSetFlowWithOverrides NewBaseFlow(int countDocument = 1, int countRecipient = 1)
        {
            return new DocumentSetFlowWithOverrides
            {
                DocumentSetName = "FlowWithOverrides",
                FlowTokenId = "FLOWID-001",
                Recipients = SampleValuesHelper.RecipientFlowColletion(countRecipient),
                Documents = SampleValuesHelper.DocumentColletionWithPdf(countDocument)
            };
        }

        [Fact]
        public void Succeeds_When_One_Document_And_Recipient()
        {
            var ds = NewBaseFlow();

            var results = ValidationHelper.Validate(ds);
            Assert.Empty(results);
        }

        [Fact]
        public void Succeeds_When_Multiple_Documents_And_Recipients()
        {
            var ds = NewBaseFlow(countDocument: 2, countRecipient: 3);

            var results = ValidationHelper.Validate(ds);
            Assert.Empty(results);
        }

        [Fact]
        public void Fails_When_SendMethod_Url_And_Recipient_Order()
        {
            var ds = NewBaseFlow(countRecipient: 3);
            ds.SendMethod = SendMethod.None;
            ds.Recipients[0].Order = 1;
            ds.Recipients[1].Order = 2;
            ds.Recipients[2].Order = 3;

            var results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && (r.ErrorMessage.Contains("order of the recipients") || r.ErrorMessage.Contains("send method")));
        }

        [Fact]
        public void Fails_When_Multiple_SMS()
        {
            var ds = NewBaseFlow();
            ds.SendMethod = SendMethod.SMS;
            ds.AuthenticationType = RecipientAuthenticationType.Otp;
            ds.ActionType = RecipientActionType.AcceptanceSignature;

            var results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && (r.ErrorMessage.Contains("Only the sending of an SMS or WhatsApp")));

            ds.SendMethod = SendMethod.SMS;
            ds.AuthenticationType = RecipientAuthenticationType.OtpWhatsApp;
            ds.ActionType = RecipientActionType.AcceptanceSignature;

            results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && (r.ErrorMessage.Contains("Only the sending of an SMS or WhatsApp")));

            ds.SendMethod = SendMethod.SMS;
            ds.AuthenticationType = RecipientAuthenticationType.None;
            ds.ActionType = RecipientActionType.OTPSignature;

            results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && (r.ErrorMessage.Contains("Only the sending of an SMS or WhatsApp")));

            ds.SendMethod = SendMethod.SMS;
            ds.AuthenticationType = RecipientAuthenticationType.None;
            ds.ActionType = RecipientActionType.OTPWhatsAppSignature;

            results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && (r.ErrorMessage.Contains("Only the sending of an SMS or WhatsApp")));

            ds.SendMethod = SendMethod.SMS;
            ds.AuthenticationType = RecipientAuthenticationType.None;
            ds.ActionType = RecipientActionType.BioOTPSignature;

            results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && (r.ErrorMessage.Contains("Only the sending of an SMS or WhatsApp")));

            ds.SendMethod = SendMethod.SMS;
            ds.AuthenticationType = RecipientAuthenticationType.None;
            ds.ActionType = RecipientActionType.BioOTPWhatsAppSignature;

            results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && (r.ErrorMessage.Contains("Only the sending of an SMS or WhatsApp")));

            ds.SendMethod = SendMethod.Email;
            ds.AuthenticationType = RecipientAuthenticationType.Otp;
            ds.ActionType = RecipientActionType.OTPSignature;

            results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && (r.ErrorMessage.Contains("Only the sending of an SMS or WhatsApp")));
        }

        [Fact]
        public void Succeeds__When_SendMethod_Device_Without_Email()
        {
            var ds = NewBaseFlow();
            ds.SendMethod = SendMethod.Device;
            ds.Recipients[0].Email = null;

            var results = ValidationHelper.Validate(ds);
            Assert.Empty(results);
        }

        [Fact]
        public void Fails__When_SendMethod_Not_Device_Without_Email()
        {
            var ds = NewBaseFlow();
            ds.SendMethod = SendMethod.SMS;
            ds.Recipients[0].Email = null;

            var results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && (r.ErrorMessage.Contains("The Email field is required")));
        }

        [Fact]
        public void Fails_When_ReminderDays_Mayor_ExpirationDaysTimeout()
        {
            var ds = NewBaseFlow();
            ds.ReminderDays = 10;
            ds.ExpirationDaysTimeout = 5;

            var results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && (r.ErrorMessage.Contains($"{nameof(ds.ReminderDays)}") || r.ErrorMessage.Contains($"{nameof(ds.ReminderDays)}")));
        }
    }
}
