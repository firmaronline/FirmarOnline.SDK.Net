using Xunit;

namespace FirmarOnline.Model.PSC.Tests.DocumentSetFlows
{
    public class DocumentSetFlowUrlTests
    {
        private static DocumentSetFlowUrlWithOverrides NewBaseFlow(int countDocument = 1, int countRecipient = 1)
        {
            return new DocumentSetFlowUrlWithOverrides
            {
                DocumentSetName = "FlowWithOverrides",
                FlowTokenId = "FLOWID-001",
                Recipients = SampleValuesHelper.RecipientFlowColletion(countRecipient),
                Documents = SampleValuesHelper.DocumentColletionWithPdf(countDocument)
            };
        }

        #region "DocumentSetUrl"

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
        public void Fails_When_ReminderDays_Mayor_ExpirationDaysTimeout()
        {
            var ds = NewBaseFlow();
            ds.ReminderDays = 10;
            ds.ExpirationDaysTimeout = 5;

            var results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && (r.ErrorMessage.Contains($"{nameof(ds.ReminderDays)}") || r.ErrorMessage.Contains($"{nameof(ds.ReminderDays)}")));
        }

        [Fact]
        public void Fails_When_Multiple_SMS()
        {
            var ds = NewBaseFlow();
            ds.AuthenticationType = RecipientAuthenticationType.Otp;
            ds.ActionType = RecipientActionType.OTPWhatsAppSignature;

            var results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && (r.ErrorMessage.Contains("Only the sending of an SMS or WhatsApp")));

            ds.AuthenticationType = RecipientAuthenticationType.Otp;
            ds.ActionType = RecipientActionType.OTPSignature;

            ds.AuthenticationType = RecipientAuthenticationType.Otp;
            ds.ActionType = RecipientActionType.OTPWhatsAppSignature;

            ds.AuthenticationType = RecipientAuthenticationType.Otp;
            ds.ActionType = RecipientActionType.BioOTPSignature;

            ds.AuthenticationType = RecipientAuthenticationType.OtpWhatsApp;
            ds.ActionType = RecipientActionType.BioOTPWhatsAppSignature;

            results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && (r.ErrorMessage.Contains("Only the sending of an SMS or WhatsApp")));

            ds.AuthenticationType = RecipientAuthenticationType.OtpWhatsApp;
            ds.ActionType = RecipientActionType.OTPWhatsAppSignature;

            results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && (r.ErrorMessage.Contains("Only the sending of an SMS or WhatsApp")));

            ds.AuthenticationType = RecipientAuthenticationType.OtpWhatsApp;
            ds.ActionType = RecipientActionType.BioOTPSignature;

            results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && (r.ErrorMessage.Contains("Only the sending of an SMS or WhatsApp")));

            ds.AuthenticationType = RecipientAuthenticationType.OtpWhatsApp;
            ds.ActionType = RecipientActionType.BioOTPWhatsAppSignature;
        }

        [Fact]
        public void Fails_When_Parallel_Recipients_With_ActionType60()
        {
            var ds = NewBaseFlow(countRecipient: 2);
            ds.Recipients[0].Order = 1;
            ds.ActionType = RecipientActionType.CryptoAPISignature;
            ds.Recipients[1].Order = 1;
            ds.ActionType = RecipientActionType.CryptoAPISignature;

            var results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && (r.ErrorMessage.Contains("parallel") || r.ErrorMessage.Contains("Action Type 60")));
        }

        [Fact]
        public void Fails_When_CorporateSignature_Start_With_WebForm()
        {
            var ds = NewBaseFlow();
            ds.Documents[0] = SampleValuesHelper.DocumentWithFormId();
            ds.CorporateSignature = new CorporateSignature
            {
                Type = CorporateSignatureType.Start,
                CorporateSignatureId = "CORP-1"
            };

            var results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && r.ErrorMessage.Contains("corporate signature") || r.MemberNames != null);
        }

        #endregion "DocumentSetUrl"

        #region "Documents"

        [Fact]
        public void Fails_When_DocumentContent_Null()
        {
            var ds = NewBaseFlow();
            ds.Documents[0].B64PDFContent = null;
#if NET6_0_OR_GREATER
            ds.Documents[0].Form = null;
#endif
            ds.Documents[0].FormId = null;

            var results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && r.ErrorMessage.Contains("Exactly one of the following properties must be provided: B64PDFContent, Form, or FormId"));
        }

        [Fact]
        public void Succeeds_When_WebFormTemplete()
        {
            var ds = NewBaseFlow();
            ds.Documents[0] = SampleValuesHelper.DocumentWithFormTemplate;

            var results = ValidationHelper.Validate(ds);
            Assert.Empty(results);
        }

        [Fact]
        public void Succeeds_When_Multiple_Pdf()
        {
            var ds = NewBaseFlow(countDocument: 2);

            var results = ValidationHelper.Validate(ds);
            Assert.Empty(results);
        }

        [Fact]
        public void Fails_When_Multiple_WebFormTemplete()
        {
            var ds = NewBaseFlow(countDocument: 2);
            ds.Documents[0].FormId = "FORM_EMPLATE_1";
            ds.Documents[0].B64PDFContent = null;
            ds.Documents[1].FormId = "FORM_EMPLATE_2";
            ds.Documents[1].B64PDFContent = null;

            var results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && r.ErrorMessage.Contains("Only one WebForm can be defined by FormId"));
        }

        #endregion "Documents"

        #region "Recpients"

        [Fact]
        public void Fails_When_RecipientOrder_Required_And_NotZero()
        {
            var ds = NewBaseFlow(countRecipient: 3);
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

        [Fact]
        public void Fails_When_Recipient_Phone_Is_Too_Short()
        {
            var ds = NewBaseFlow();
            ds.Recipients[0].PhoneNumber = "+443";

            var results = ValidationHelper.Validate(ds);

            Assert.Contains(results, r => r.ErrorMessage != null && r.ErrorMessage.Contains("minimum length of '5'"));
        }

        #endregion "Recpients"

        #region "Authentication"

        [Fact]
        public void Fails_When_AuthenticationType_AccessCode_Without_Challenge()
        {
            var ds = NewBaseFlow();
            ds.AuthenticationType = RecipientAuthenticationType.AccessCode;

            var results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && r.ErrorMessage.Contains("The access code challenge data is required"));
        }

        [Fact]
        public void Succeeds_When_AuthenticationType_AccessCode_With_Challenge()
        {
            var ds = NewBaseFlow();
            ds.AuthenticationType = RecipientAuthenticationType.AccessCode;
            ds.AccessCode = new AccessCode { Challenge = "Informe su DNI:" };

            var results = ValidationHelper.Validate(ds);
            Assert.Empty(results);
        }

        [Fact]
        public void Succeeds_When_AuthenticationType_Mfa_With_Two_AuthSteps()
        {
            var ds = NewBaseFlow();
            ds.AuthenticationType = RecipientAuthenticationType.Mfa;
            ds.AuthSteps =
            [
                new AuthenticationStep
                {
                    Type = RecipientAuthenticationType.AccessCode,
                    AccessCode = new RecipientAccessCode { Challenge = "Informe su DNI:" }
                },
                new AuthenticationStep { Type = RecipientAuthenticationType.Otp }
            ];

            var results = ValidationHelper.Validate(ds);
            Assert.Empty(results);
        }

        [Fact]
        public void Fails_When_AuthenticationType_Mfa_With_Less_Than_Two_AuthSteps()
        {
            var ds = NewBaseFlow();
            ds.AuthenticationType = RecipientAuthenticationType.Mfa;

            // AuthSteps null
            ds.AuthSteps = null;
            var results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && r.ErrorMessage.Contains("MFA authentication requires at least two AuthSteps"));

            // AuthSteps empty
            ds.AuthSteps = [];
            results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && r.ErrorMessage.Contains("MFA authentication requires at least two AuthSteps"));

            // AuthSteps with only one step
            ds.AuthSteps =
            [
                new AuthenticationStep { Type = RecipientAuthenticationType.Otp }
            ];
            results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && r.ErrorMessage.Contains("MFA authentication requires at least two AuthSteps"));
        }

        [Fact]
        public void Fails_When_AuthenticationType_Mfa_With_Duplicated_AuthStep_Types()
        {
            var ds = NewBaseFlow();
            ds.AuthenticationType = RecipientAuthenticationType.Mfa;
            ds.AuthSteps =
            [
                new AuthenticationStep
                {
                    Type = RecipientAuthenticationType.AccessCode,
                    AccessCode = new RecipientAccessCode { Challenge = "DNI:" }
                },
                new AuthenticationStep
                {
                    Type = RecipientAuthenticationType.AccessCode,
                    AccessCode = new RecipientAccessCode { Challenge = "Otro:" }
                }
            ];

            var results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && r.ErrorMessage.Contains("AuthSteps cannot contain duplicated Type values"));

            ds.AuthSteps =
            [
                new AuthenticationStep { Type = RecipientAuthenticationType.Otp },
                new AuthenticationStep { Type = RecipientAuthenticationType.Otp }
            ];

            results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && r.ErrorMessage.Contains("AuthSteps cannot contain duplicated Type values"));
        }

        [Fact]
        public void Fails_When_AuthenticationType_Mfa_With_AuthStep_Type_None()
        {
            var ds = NewBaseFlow();
            ds.AuthenticationType = RecipientAuthenticationType.Mfa;
            ds.AuthSteps =
            [
                new AuthenticationStep { Type = RecipientAuthenticationType.None },
                new AuthenticationStep { Type = RecipientAuthenticationType.Otp }
            ];

            var results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && r.ErrorMessage.Contains("AuthSteps cannot contain a step with Type None"));
        }

        [Fact]
        public void Fails_When_AuthenticationType_Mfa_With_AccessCode_Step_Without_Challenge()
        {
            var ds = NewBaseFlow();
            ds.AuthenticationType = RecipientAuthenticationType.Mfa;
            ds.AuthSteps =
            [
                new AuthenticationStep
                {
                    Type = RecipientAuthenticationType.AccessCode,
                    AccessCode = new RecipientAccessCode { Challenge = null }
                },
                new AuthenticationStep { Type = RecipientAuthenticationType.Otp }
            ];

            var results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && r.ErrorMessage.Contains("The access code challenge data is required"));
        }

        [Fact]
        public void Fails_When_AuthenticationType_Not_Mfa_With_AuthSteps()
        {
            var ds = NewBaseFlow();
            ds.AuthenticationType = RecipientAuthenticationType.Basic;
            ds.AuthSteps =
            [
                new AuthenticationStep { Type = RecipientAuthenticationType.Otp }
            ];

            var results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && r.ErrorMessage.Contains("AuthSteps can only be set when AuthenticationType is MFA"));

            // También con AuthenticationType AccessCode (con AccessCode válido para aislar el fallo en AuthSteps)
            ds.AuthenticationType = RecipientAuthenticationType.AccessCode;
            ds.AccessCode = new AccessCode { Challenge = "DNI:" };

            results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && r.ErrorMessage.Contains("AuthSteps can only be set when AuthenticationType is MFA"));
        }

        #endregion "Authentication"

#if NET6_0_OR_GREATER

        [Fact]
        public void Succeeds_When_CorporateSignature_End_With_WebForm()
        {
            var ds = NewBaseFlow();
            ds.Documents[0] = SampleValuesHelper.DocumentWithForm;
            ds.CorporateSignature = new CorporateSignature
            {
                Type = CorporateSignatureType.End,
                CorporateSignatureId = "CORP-1"
            };

            var results = ValidationHelper.Validate(ds);
            Assert.Empty(results);
        }

        [Fact]
        public void Fails_When_Not_WebForm_And_ActionType60()
        {
            var ds = NewBaseFlow();
            ds.Documents[0] = SampleValuesHelper.DocumentWithForm;
            ds.ActionType = RecipientActionType.CryptoAPISignature;

            var results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && (r.ErrorMessage.Contains("A document set cannot contain recipients with Action Type 60 and WebForms")));
        }

        [Fact]
        public void Fails_When_WebForm_With_Multiple_Recipients()
        {
            var ds = NewBaseFlow(countRecipient: 2);
            ds.Documents[0] = SampleValuesHelper.DocumentWithForm;

            var results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && r.ErrorMessage.Contains("WebForms can only have one recipient"));
        }

        [Fact]
        public void Fails_When_Documents_Not_Grouped()
        {
            var ds = NewBaseFlow(countDocument: 3);
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
        public void Succeeds_When_WebForm()
        {
            var ds = NewBaseFlow();
            ds.Documents[0] = SampleValuesHelper.DocumentWithForm;

            var results = ValidationHelper.Validate(ds);
            Assert.Empty(results);
        }

#endif
    }
}