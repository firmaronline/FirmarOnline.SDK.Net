using Xunit;

namespace FirmarOnline.Model.PSC.Tests.Recipients
{
    public class RecipientCollectionTests
    {
        [Fact]
        public void Fails_RecipientCollection_When_Some_Have_Order_And_Others_Not()
        {
            var recipients = SampleValuesHelper.RecipientColletion(2);
            recipients[0].Order = 1;
            recipients[1].Order = null;

            var results = ValidationHelper.Validate(recipients);
            Assert.Contains(results, r => r.ErrorMessage != null && r.ErrorMessage.Contains("order"));
        }

        [Fact]
        public void Succeeds_RecipientCollection_When_All_Have_Order()
        {
            var recipients = SampleValuesHelper.RecipientColletion(2);
            recipients[0].Order = 1;
            recipients[1].Order = 2;
                        
            Assert.True(ValidationHelper.IsValid(recipients));
        }

        [Fact]
        public void Fails_RecipientCollectionFlow_When_Some_Have_Order_And_Others_Not()
        {
            var recipients = SampleValuesHelper.RecipientFlowColletion(2);
            recipients[0].Order = 1;
            recipients[1].Order = null;

            var results = ValidationHelper.Validate(recipients);
            Assert.Contains(results, r => r.ErrorMessage != null && r.ErrorMessage.Contains("order"));
        }

        [Fact]
        public void Succeeds_RecipientCollectionFlow_When_All_Have_Order()
        {
            var recipients = SampleValuesHelper.RecipientFlowColletion(2);
            recipients[0].Order = 1;
            recipients[1].Order = 2;

            Assert.True(ValidationHelper.IsValid(recipients));
        }

        [Fact]
        public void Succeeds_RecipientCollection_When_Not_Parallel_With_ActionType60()
        {
            var recipients = SampleValuesHelper.RecipientColletion(2);
            recipients[0].Order = 1;
            recipients[0].ActionType = RecipientActionType.CryptoAPISignature;
            recipients[1].Order = 2;
            recipients[1].ActionType = RecipientActionType.CryptoAPISignature;

            var results = ValidationHelper.Validate(recipients);
            Assert.True(ValidationHelper.IsValid(recipients));
        }

        [Fact]
        public void Fails_RecipientCollection_When_Parallel_With_ActionType60()
        {
            var recipients = SampleValuesHelper.RecipientColletion(2);
            recipients[0].Order = 1;
            recipients[0].ActionType = RecipientActionType.CryptoAPISignature;
            recipients[1].Order = 1;
            recipients[1].ActionType = RecipientActionType.CryptoAPISignature;

            var results = ValidationHelper.Validate(recipients);
            Assert.Contains(results, r => r.ErrorMessage != null && (r.ErrorMessage.Contains("parallel") || r.ErrorMessage.Contains("Action Type 60")));
        }
    }
}
