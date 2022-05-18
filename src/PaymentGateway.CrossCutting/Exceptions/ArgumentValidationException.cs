namespace PaymentGateway.CrossCutting.Exceptions
{
    using System;

    [Serializable]
    public class ArgumentValidationException : Exception
    {
        public const string GenericMessage = "ArgumentValidationException was thrown.";

        public ArgumentValidationException()
            : base(GenericMessage)
        {
        }

        public ArgumentValidationException(string message)
            : base(message)
        {
        }

        public ArgumentValidationException(string message, Exception inner)
            : base(message, inner)
        {
        }

    }
}
