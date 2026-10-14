using Xunit;

namespace FirmarOnline.Model.PSC.Tests.Templates
{
    public class TemplateTests
    {
        [Fact]
        public void Succeeds_When_OtpSms_Is_Null()
        {
            var template = new Template();

            Assert.True(ValidationHelper.IsValid(template));
        }

        [Fact]
        public void Succeeds_When_OtpSms_Contains_Otp_Tag_And_Is_Within_Length()
        {
            var template = new Template
            {
                OtpSms = "Tu código es {otp}"
            };

            Assert.True(ValidationHelper.IsValid(template));
        }

        [Fact]
        public void Fails_When_OtpSms_Does_Not_Contain_Otp_Tag()
        {
            var template = new Template
            {
                OtpSms = "Tu código de verificación"
            };

            var results = ValidationHelper.Validate(template);
            Assert.Contains(results, r => r.ErrorMessage != null && r.ErrorMessage.Contains("OTP SMS text must contain {otp} tag template"));
        }

        [Fact]
        public void Fails_When_OtpSms_Exceeds_Max_Length()
        {
            var template = new Template
            {
                OtpSms = "{otp}" + new string('a', 400)
            };

            var results = ValidationHelper.Validate(template);
            Assert.Contains(results, r => r.ErrorMessage != null && r.ErrorMessage.Contains("OTP SMS text length must be <= 400"));
        }
    }
}
