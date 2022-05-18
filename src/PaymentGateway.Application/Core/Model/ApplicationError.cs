namespace PaymentGateway.Application.Core.Model
{
    using System;

    public class ApplicationError: Exception
    {
        public int Code { get; set; }

        public string DeveloperMessage { get; set; } = string.Empty;
    }
}
