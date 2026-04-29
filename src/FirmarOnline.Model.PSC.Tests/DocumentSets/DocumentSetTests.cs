using Xunit;

namespace FirmarOnline.Model.PSC.Tests.DocumentSets
{
    public class DocumentSetTests
    {
        private static DocumentSet NewBaseDocSet(int countDocument = 1, int countRecipient = 1)
        {
            return new DocumentSet
            {               
                DocumentSetName = "DocumentSet for testing",
                SenderName = "Test Sender",
                SenderMail = "testsender@foo.com",
                ReminderDays = 10,
                ExpirationDaysTimeout = 30,
                Recipients = SampleValuesHelper.RecipientColletion(countRecipient),
                Documents = SampleValuesHelper.DocumentColletionWithPdf(countDocument)
            };
        }

        #region "DocumentSet"

        [Fact]
        public void Succeeds_When_One_Document_And_Recipient()
        {
            var ds = NewBaseDocSet();

            var results = ValidationHelper.Validate(ds);
            Assert.Empty(results);
        }

        [Fact]
        public void Succeeds_When_Multiple_Document_And_Recipient()
        {
            var ds = NewBaseDocSet(countDocument: 3, countRecipient: 5);

            var results = ValidationHelper.Validate(ds);
            Assert.Empty(results);
        }

        [Fact]
        public void Fails_When_ReminderDays_Mayor_ExpirationDaysTimeout()
        {
            var ds = NewBaseDocSet();
            ds.ReminderDays = 10;
            ds.ExpirationDaysTimeout = 5;

            var results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && (r.ErrorMessage.Contains($"{nameof(ds.ReminderDays)}") || r.ErrorMessage.Contains($"{nameof(ds.ReminderDays)}")));
        }

        [Fact]
        public void Fails_When_Multiple_SMS()
        {
            var ds = NewBaseDocSet();
            ds.SendMethod = SendMethod.SMS;
            ds.Recipients[0].AuthType = RecipientAuthenticationType.Otp;
            ds.Recipients[0].ActionType = RecipientActionType.AcceptanceSignature;

            var results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && (r.ErrorMessage.Contains("Only one SMS or WhatsApp message per recipient is allowed")));

            ds.SendMethod = SendMethod.SMS;
            ds.Recipients[0].AuthType = RecipientAuthenticationType.OtpWhatsApp;
            ds.Recipients[0].ActionType = RecipientActionType.AcceptanceSignature;

            results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && (r.ErrorMessage.Contains("Only one SMS or WhatsApp message per recipient is allowed")));

            ds.SendMethod = SendMethod.SMS;
            ds.Recipients[0].AuthType = RecipientAuthenticationType.None;
            ds.Recipients[0].ActionType = RecipientActionType.OTPSignature;

            results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && (r.ErrorMessage.Contains("Only one SMS or WhatsApp message per recipient is allowed")));

            ds.SendMethod = SendMethod.SMS;
            ds.Recipients[0].AuthType = RecipientAuthenticationType.None;
            ds.Recipients[0].ActionType = RecipientActionType.OTPWhatsAppSignature;

            results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && (r.ErrorMessage.Contains("Only one SMS or WhatsApp message per recipient is allowed")));

            ds.SendMethod = SendMethod.SMS;
            ds.Recipients[0].AuthType = RecipientAuthenticationType.None;
            ds.Recipients[0].ActionType = RecipientActionType.BioOTPSignature;

            results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && (r.ErrorMessage.Contains("Only one SMS or WhatsApp message per recipient is allowed")));

            ds.SendMethod = SendMethod.SMS;
            ds.Recipients[0].AuthType = RecipientAuthenticationType.None;
            ds.Recipients[0].ActionType = RecipientActionType.BioOTPWhatsAppSignature;

            results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && (r.ErrorMessage.Contains("Only one SMS or WhatsApp message per recipient is allowed")));

            ds.SendMethod = SendMethod.Email;
            ds.Recipients[0].AuthType = RecipientAuthenticationType.Otp;
            ds.Recipients[0].ActionType = RecipientActionType.OTPSignature;

            results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && (r.ErrorMessage.Contains("Only one SMS or WhatsApp message per recipient is allowed")));
        }

        [Fact]
        public void Fails_When_RecipientsOrder_Not_SendMethodUrl()
        {
            var ds = NewBaseDocSet(countRecipient: 2);
            ds.SendMethod = SendMethod.None;
            ds.Recipients[0].Order = 1;
            ds.Recipients[1].Order = 1;

            var results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && (r.ErrorMessage.Contains("Cannot indicate the order of the recipients if the send method is not indicated")));
        }

        [Fact]
        public void Fails_When_SendMethod_RecipientsEmail_Validate()
        {
            var ds = NewBaseDocSet(countRecipient: 2);
            ds.SendMethod = SendMethod.Email;
            ds.Recipients[0].Email = null;

            var results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && (r.ErrorMessage.Contains("The Email field is required")));

            ds.SendMethod = SendMethod.Device;
            ds.Recipients[0].Email = null;

            results = ValidationHelper.Validate(ds);
            Assert.Empty(results);
        }

        [Fact]
        public void Fails_When_Parallel_Recipients_With_ActionType60()
        {
            var ds = NewBaseDocSet(countRecipient: 2);
            ds.Recipients[0].Order = 1;
            ds.Recipients[0].ActionType = RecipientActionType.CryptoAPISignature;
            ds.Recipients[1].Order = 1;
            ds.Recipients[1].ActionType = RecipientActionType.CryptoAPISignature;

            var results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && (r.ErrorMessage.Contains("parallel") || r.ErrorMessage.Contains("Action Type 60")));
        }

        #endregion

        #region "Documents"

        [Fact]
        public void Fails_When_DocumentContent_Null()
        {
            var ds = NewBaseDocSet();
            ds.Documents[0].B64PDFContent = null;
#if NET6_0_OR_GREATER
            ds.Documents[0].Form = null;
#endif
            ds.Documents[0].FormId = null;

            var results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && r.ErrorMessage.Contains("Exactly one of the following properties must be provided: B64PDFContent, Form, or FormId"));
        }

        [Fact]
        public void Succeeds_When_Multiple_Pdf()
        {
            var ds = NewBaseDocSet(countDocument: 2);
            
            var results = ValidationHelper.Validate(ds);
            Assert.Empty(results);
        }

        [Fact]
        public void Fails_When_Multiple_WebFormTemplete()
        {
            var ds = NewBaseDocSet(countDocument: 2);
            ds.Documents[0].FormId = "FORM_EMPLATE_1";
            ds.Documents[0].B64PDFContent = null;
            ds.Documents[1].FormId = "FORM_EMPLATE_2";
            ds.Documents[1].B64PDFContent = null;

            var results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && r.ErrorMessage.Contains("Only one WebForm can be defined by FormId"));
        }

        #endregion

        #region "Recpients"

        [Fact]
        public void Fails_When_RecipientOrder_Required_And_NotZero()
        {
            var ds = NewBaseDocSet(countRecipient: 3);
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
            var ds = NewBaseDocSet();
            ds.Recipients[0].PhoneNumber = "+443";

            var results = ValidationHelper.Validate(ds);

            Assert.Contains(results, r => r.ErrorMessage != null && r.ErrorMessage.Contains("minimum length of '5'"));
        }

        [Fact]
        public void Fails_When_Recipient_AccessCode_Data_Required()
        {
            var ds = NewBaseDocSet();
            ds.Recipients[0].AuthType = RecipientAuthenticationType.AccessCode;

            var results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && r.ErrorMessage.Contains("The access code challenge data is required"));
        }

        [Fact]
        public void Succeeds_When_Recipient_AccessCode()
        {
            var ds = NewBaseDocSet();
            ds.Recipients[0].AuthType = RecipientAuthenticationType.AccessCode;
            ds.Recipients[0].AccessCode = new RecipientAccessCode() {
                Challenge = "Informe su DNI:",
                Response = "12345678X"
            };

            var results = ValidationHelper.Validate(ds);
            Assert.Empty(results);
        }

        [Fact]
        public void Succeeds_When_Recipient_AuthType_Mfa_With_Two_AuthSteps()
        {
            var ds = NewBaseDocSet();
            ds.Recipients[0].AuthType = RecipientAuthenticationType.Mfa;
            ds.Recipients[0].AuthSteps =
            [
                new AuthenticationStep
                {
                    Type = RecipientAuthenticationType.AccessCode,
                    AccessCode = new RecipientAccessCode
                    {
                        Challenge = "Informe su DNI:",
                        Response = "12345678X"
                    }
                },
                new AuthenticationStep
                {
                    Type = RecipientAuthenticationType.Otp
                }
            ];

            var results = ValidationHelper.Validate(ds);
            Assert.Empty(results);
        }

        [Fact]
        public void Fails_When_Recipient_AuthType_Mfa_With_Less_Than_Two_AuthSteps()
        {
            var ds = NewBaseDocSet();
            ds.Recipients[0].AuthType = RecipientAuthenticationType.Mfa;

            // AuthSteps null
            ds.Recipients[0].AuthSteps = null;
            var results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && r.ErrorMessage.Contains("MFA authentication requires at least two AuthSteps"));

            // AuthSteps empty
            ds.Recipients[0].AuthSteps = [];
            results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && r.ErrorMessage.Contains("MFA authentication requires at least two AuthSteps"));

            // AuthSteps with only one step
            ds.Recipients[0].AuthSteps =
            [
                new AuthenticationStep
                {
                    Type = RecipientAuthenticationType.Otp
                }
            ];
            results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && r.ErrorMessage.Contains("MFA authentication requires at least two AuthSteps"));
        }

        [Fact]
        public void Fails_When_Recipient_AuthType_Not_Mfa_With_AuthSteps()
        {
            var ds = NewBaseDocSet();
            ds.Recipients[0].AuthType = RecipientAuthenticationType.Basic;
            ds.Recipients[0].AuthSteps =
            [
                new AuthenticationStep
                {
                    Type = RecipientAuthenticationType.Otp
                }
            ];

            var results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && r.ErrorMessage.Contains("AuthSteps can only be set when AuthType is MFA"));

            // También con AuthType AccessCode (con AccessCode válido, para aislar el fallo en AuthSteps)
            ds.Recipients[0].AuthType = RecipientAuthenticationType.AccessCode;
            ds.Recipients[0].AccessCode = new RecipientAccessCode { Challenge = "DNI:" };

            results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && r.ErrorMessage.Contains("AuthSteps can only be set when AuthType is MFA"));
        }

        [Fact]
        public void Fails_When_Recipient_AuthType_Mfa_With_Duplicated_AuthStep_Types()
        {
            var ds = NewBaseDocSet();
            ds.Recipients[0].AuthType = RecipientAuthenticationType.Mfa;
            ds.Recipients[0].AuthSteps =
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

            // También con otro tipo (Otp)
            ds.Recipients[0].AuthSteps =
            [
                new AuthenticationStep { Type = RecipientAuthenticationType.Otp },
                new AuthenticationStep { Type = RecipientAuthenticationType.Otp }
            ];

            results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && r.ErrorMessage.Contains("AuthSteps cannot contain duplicated Type values"));
        }

        #endregion

#if NET6_0_OR_GREATER
        [Fact]
        public void Fails_When_Not_WebForm_And_ActionType60()
        {
            var ds = NewBaseDocSet();
            ds.Documents[0] = SampleValuesHelper.DocumentWithForm;
            ds.Recipients[0].ActionType = RecipientActionType.CryptoAPISignature;

            var results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && (r.ErrorMessage.Contains("A document set cannot contain recipients with Action Type 60 and WebForms")));
        }

        [Fact]
        public void Fails_When_CorporateSignature_Start_With_WebForm()
        {
            var ds = NewBaseDocSet();
            ds.Documents[0] = SampleValuesHelper.DocumentWithFormId();
            ds.CorporateSignature = new CorporateSignature
            {
                Type = CorporateSignatureType.Start,
                CorporateSignatureId = "CORP-1"
            };

            var results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && r.ErrorMessage.Contains("corporate signature") || r.MemberNames != null);
        }

        [Fact]
        public void Succeeds_When_CorporateSignature_End_With_WebForm()
        {
            var ds = NewBaseDocSet();
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
        public void Fails_When_WebForm_With_Multiple_Recipients()
        {
            var ds = NewBaseDocSet(countRecipient: 2);
            ds.Documents[0] = SampleValuesHelper.DocumentWithForm;

            var results = ValidationHelper.Validate(ds);
            Assert.Contains(results, r => r.ErrorMessage != null && r.ErrorMessage.Contains("WebForms can only have one recipient"));
        }

        [Fact]
        public void Succeeds_When_WebForm()
        {
            var ds = NewBaseDocSet();
            ds.Documents[0] = SampleValuesHelper.DocumentWithForm;

            var results = ValidationHelper.Validate(ds);
            Assert.Empty(results);
        }

        [Fact]
        public void Succeeds_When_WebFormTemplete()
        {
            var ds = NewBaseDocSet();
            ds.Documents[0] = SampleValuesHelper.DocumentWithFormTemplate;

            var results = ValidationHelper.Validate(ds);
            Assert.Empty(results);
        }

        [Fact]
        public void Fails_When_Documents_Not_Grouped()
        {
            var ds = NewBaseDocSet(countDocument: 3);
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
#endif

    }
}
