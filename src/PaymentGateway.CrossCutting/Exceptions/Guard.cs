namespace PaymentGateway.CrossCutting.Exceptions
{
    using System;

    public static class Guard
    {
        public static void Against<TException>(bool assertion)
            where TException : Exception
                => Against<TException>(assertion, string.Empty);

        public static void Against<TException>(bool assertion, string message)
            where TException : Exception
        {
            if (!assertion)
            {
                return;
            }

            ThrowException<TException>(new[]
            {
                typeof(string),
                typeof(Exception),
                typeof(ArgumentValidationException),
                typeof(NotFoundException),
                typeof(BadRequestException)
            },
                message,
                new Exception(message));
        }
        

        public static void Against<TException>(bool assertion, Exception ex)
            where TException : Exception
        {
            if (!assertion)
            {
                return;
            }

            ThrowException<TException>(new[] { typeof(Exception) }, ex);
        }

        public static void Against<TException>(bool assertion, string message, Exception ex)
            where TException : Exception
        {
            if (!assertion)
            {
                return;
            }

            ThrowException<TException>(new[] { typeof(string), typeof(Exception) }, message, ex);
        }

        public static void Against<TException>(bool assertion, string message, Func<object> customData)
            where TException : NotFoundException
        {
            if (!assertion)
            {
                return;
            }

            ThrowException<TException>(new[] { typeof(string), typeof(object) }, message, customData());
        }

        private static void ThrowException<TException>(Type[] types, params object[] parameters)
            where TException : Exception
        {
            var constructorInfo = typeof(TException).GetConstructor(types);

            if (constructorInfo == null)
            {
                throw new Exception("Guard could not throw exception: " + typeof(TException));
            }

            throw (TException)constructorInfo.Invoke(parameters);
        }
    }
}
