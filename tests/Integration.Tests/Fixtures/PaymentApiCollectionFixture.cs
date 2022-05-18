
using Xunit;

namespace Integration.Tests.Fixtures
{

    [CollectionDefinition("PaymentApiIntegrationTests")]
    public class PaymentApiCollectionFixture : ICollectionFixture<PaymentApiFixture>
    {
    }
}
