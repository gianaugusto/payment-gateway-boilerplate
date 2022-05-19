namespace PaymentGateway.Application.Tests.Features.PaymentProcessor.Handlers
{
    using AutoFixture;

    using AutoMapper;

    using Moq;

    using PaymentGateway.Application.Features.PaymentProcessor.Commands;
    using PaymentGateway.Application.Features.PaymentProcessor.Domain.Interfaces;
    using PaymentGateway.Application.Features.PaymentProcessor.Domain.Models;
    using PaymentGateway.Application.Features.PaymentProcessor.Gateways.Interfaces;
    using PaymentGateway.Application.Features.PaymentProcessor.Handlers;
    using PaymentGateway.Application.Features.PaymentProcessor.Models.PaymentIssuer;

    public class HandlerTests
    {
        private readonly Mock<IPaymentRepository> _paymentRepositoryMock;

        private readonly Mock<IMapper> _mapperMock;

        private readonly Mock<IBankApiClient> _bankApiClientMock;

        private Handler target { get; }


        public HandlerTests()
        {
            this._paymentRepositoryMock = new Mock<IPaymentRepository>();
            this._mapperMock = new Mock<IMapper>();
            this._bankApiClientMock = new Mock<IBankApiClient>();
            this.target = new Handler(
                this._paymentRepositoryMock.Object,
                this._mapperMock.Object,
                this._bankApiClientMock.Object);
        }

        [Fact]
        public async void HandleProcessPayment_ValidRequest_ReturnOk()
        {
            // Arrange
            var cancellationToken = new CancellationToken();
            var processPayment = new Fixture().Create<ProcessPayment>();
            var model = new Fixture().Create<PaymentGateway.Application.Features.PaymentProcessor.Models.Payment>();
            var payment = new Fixture().Build<Payment>().Without(x => x.Merchant).Create();
            var response = new Fixture().Create<Response>();

            this._paymentRepositoryMock.Setup(x => x.InsertAsync(It.IsAny<Payment>(), cancellationToken));
            this._bankApiClientMock.Setup(x => x.ProcessPaymentAsync(It.IsAny<Request>(), cancellationToken)).ReturnsAsync(response);
            this._mapperMock.Setup(x => x.Map<Payment>(processPayment)).Returns(payment);
            this._mapperMock.Setup(x => x.Map(It.IsAny<Response>(), It.IsAny<Payment>())).Returns(payment);
            this._mapperMock.Setup(x => x.Map<PaymentGateway.Application.Features.PaymentProcessor.Models.Payment>(payment)).Returns(model);
            
            // Act
             var act = await this.target.Handle(processPayment, cancellationToken).ConfigureAwait(false);

            // Assert
            Assert.NotNull(act);
            Assert.NotNull(act.Result);

            this._paymentRepositoryMock.VerifyAll();
            this._mapperMock.VerifyAll();
            this._bankApiClientMock.VerifyAll();
        }
    }
}
