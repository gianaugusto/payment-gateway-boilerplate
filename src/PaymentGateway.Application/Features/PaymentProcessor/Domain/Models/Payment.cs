namespace PaymentGateway.Application.Features.PaymentProcessor.Domain.Models
{
    using PaymentGateway.CrossCutting.Enums;

    using System;

    public class Payment
    {
        public Payment() { }

        public Payment(Guid paymentId,
            DateTime createDate,
            string paymentType,
            decimal amount,
            string currency,
            string reference,
            string description,
            Guid customerId,
            Merchant merchant,
            Source source,
            PaymentStatusEnum status) : this()
        {
            PaymentId = paymentId;
            CreateDate = createDate;
            PaymentType = paymentType;
            Amount = amount;
            Currency = currency;
            Reference = reference;
            Description = description;
            CustomerId = customerId;
            Merchant = merchant;
            Source = source;
            Status = status;
        }

        public static readonly string TableName = nameof(Payment);

        public Guid PaymentId { get; set; }

        public Guid MerchantId { get; set; }

        public Guid SourceId { get; set; }

        public DateTime CreateDate { get; set; }

        public string PaymentType { get; set; }
        
        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public string Reference { get; set; }

        public string Description { get; set; }

        public Guid CustomerId { get; set; }

        public Merchant Merchant { get; set; }

        public Source Source { get; set; }

        public PaymentStatusEnum Status { get; set; }

    }
}
