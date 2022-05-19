namespace Integration.Tests
{
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    using AutoFixture;

    using Integration.Tests.Fixtures;

    using PaymentGateway.Application.Features.PaymentProcessor.Commands;

    using Xunit;

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
            using var response = await this.client.GetAsync("api/v1/merchants/0001/payments");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task PostAsync_ValidPayment_ReturnsValid()
        {
            // Arrange & Act
            var processPayment = new Fixture().Create<ProcessPayment>();
            using var response = await this.client.PostAsJsonAsync("api/v1/merchants/E659D58B-9EF6-4E78-8EB0-71E7AE88A701/payments", processPayment);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
    }
}
