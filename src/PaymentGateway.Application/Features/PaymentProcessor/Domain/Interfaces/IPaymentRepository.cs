namespace PaymentGateway.Application.Features.PaymentProcessor.Domain.Interfaces
{
    using Models;
    using PaymentGateway.Application.Core.Domain.Interfaces;

    public interface IPaymentRepository : IRepository<Payment>
    {
    }
}
