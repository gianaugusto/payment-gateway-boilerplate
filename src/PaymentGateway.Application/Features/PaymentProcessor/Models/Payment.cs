﻿namespace PaymentGateway.Application.Features.PaymentProcessor.Models
{
    using System;

    public class Payment
    {
        public Payment() { }
        
        public Guid PaymentId { get; set; }
       
        public Guid MerchantId { get; set; }

        public Guid SourceId { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.UtcNow;

        public string PaymentType { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public string Reference { get; set; }

        public string Description { get; set; }

        public string SuccessUrl { get; set; } = string.Empty;

        public string FailureUrl { get; set; } = string.Empty;

        public Guid CustomerId { get; set; }
    }
}
