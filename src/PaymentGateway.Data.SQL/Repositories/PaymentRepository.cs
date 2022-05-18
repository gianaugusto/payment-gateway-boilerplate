namespace PaymentGateway.Data.SQL.Repositories
{
    using Application.Features.PaymentProcessor.Domain.Interfaces;
    using Application.Features.PaymentProcessor.Domain.Models;
    using Microsoft.EntityFrameworkCore;

    public class PaymentRepository : 
        Repository<Payment>,
        IPaymentRepository
    {
        public PaymentRepository(DbContext context) : base(context)
        {
        }
    }
}
