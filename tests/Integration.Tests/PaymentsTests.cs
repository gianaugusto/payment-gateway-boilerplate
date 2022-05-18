
using System.Threading.Tasks;
using System;

using Integration.Tests.Fixtures;

using Xunit;
using System.Net.Http;
using System.Net;

namespace Integration.Tests
{
    [Collection("PaymentApiIntegrationTests")]
    public class PaymentsTests
    {
        private readonly HttpClient client;

        public PaymentsTests(PaymentApiFixture paymentApiFixture)
        {
            this.client = paymentApiFixture.CreateClient();
        }

        [Fact]
        public async Task GetAsync_GetAllPayments_ReturnsValid()
        {
            // Arrange & Act
            using var response = await this.client.GetAsync("/hellofunc");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
