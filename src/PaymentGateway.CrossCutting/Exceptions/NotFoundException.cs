﻿namespace PaymentGateway.CrossCutting.Exceptions
{
    using System;

    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        { }
    }
}
