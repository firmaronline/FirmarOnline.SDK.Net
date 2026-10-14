using Xunit;

namespace FirmarOnline.Model.PSC.Tests.Recipients
{
    /// <summary>
    /// Tests de <see cref="RecipientAuthenticationTypeExtensions.FindForbiddenMfaCombination"/>.
    /// </summary>
    public class ForbiddenMfaCombinationTests
    {
        [Fact]
        public void ForbiddenMfaCombinations_Contains_Expected_Pairs()
        {
            Assert.Equal(4, RecipientAuthenticationTypeExtensions.ForbiddenMfaCombinations.Count);
            Assert.Contains((RecipientAuthenticationType.MrzOcr, RecipientAuthenticationType.Mrz), RecipientAuthenticationTypeExtensions.ForbiddenMfaCombinations);
            Assert.Contains((RecipientAuthenticationType.LivenessAndCardId, RecipientAuthenticationType.MrzOcr), RecipientAuthenticationTypeExtensions.ForbiddenMfaCombinations);
            Assert.Contains((RecipientAuthenticationType.LivenessAndCardId, RecipientAuthenticationType.Mrz), RecipientAuthenticationTypeExtensions.ForbiddenMfaCombinations);
            Assert.Contains((RecipientAuthenticationType.Liveness, RecipientAuthenticationType.LivenessAndCardId), RecipientAuthenticationTypeExtensions.ForbiddenMfaCombinations);
        }

        [Theory]
        [InlineData(RecipientAuthenticationType.MrzOcr, RecipientAuthenticationType.Mrz)]
        [InlineData(RecipientAuthenticationType.Mrz, RecipientAuthenticationType.MrzOcr)]
        [InlineData(RecipientAuthenticationType.LivenessAndCardId, RecipientAuthenticationType.MrzOcr)]
        [InlineData(RecipientAuthenticationType.MrzOcr, RecipientAuthenticationType.LivenessAndCardId)]
        [InlineData(RecipientAuthenticationType.LivenessAndCardId, RecipientAuthenticationType.Mrz)]
        [InlineData(RecipientAuthenticationType.Mrz, RecipientAuthenticationType.LivenessAndCardId)]
        [InlineData(RecipientAuthenticationType.Liveness, RecipientAuthenticationType.LivenessAndCardId)]
        [InlineData(RecipientAuthenticationType.LivenessAndCardId, RecipientAuthenticationType.Liveness)]
        public void FindForbiddenMfaCombination_Returns_Combination_Regardless_Of_Order(
            RecipientAuthenticationType first, RecipientAuthenticationType second)
        {
            var result = new[] { RecipientAuthenticationType.AccessCode, first, second }.FindForbiddenMfaCombination();

            Assert.True(result.HasValue);
            Assert.Contains(first, new[] { result.Value.First, result.Value.Second });
            Assert.Contains(second, new[] { result.Value.First, result.Value.Second });
        }

        [Theory]
        [InlineData(RecipientAuthenticationType.AccessCode, RecipientAuthenticationType.MrzOcr)]
        [InlineData(RecipientAuthenticationType.Liveness, RecipientAuthenticationType.MrzOcr)]
        [InlineData(RecipientAuthenticationType.Liveness, RecipientAuthenticationType.Mrz)]
        [InlineData(RecipientAuthenticationType.LivenessAndCardId, RecipientAuthenticationType.Otp)]
        [InlineData(RecipientAuthenticationType.LivenessAndCardId, RecipientAuthenticationType.OtpEmail)]
        public void FindForbiddenMfaCombination_Returns_Null_When_Combination_Is_Allowed(
            RecipientAuthenticationType first, RecipientAuthenticationType second)
        {
            Assert.Null(new[] { first, second }.FindForbiddenMfaCombination());
        }

        [Fact]
        public void FindForbiddenMfaCombination_Returns_Null_When_Null_Or_Empty()
        {
            Assert.Null(((RecipientAuthenticationType[])null).FindForbiddenMfaCombination());
            Assert.Null(new RecipientAuthenticationType[0].FindForbiddenMfaCombination());
        }
    }
}
