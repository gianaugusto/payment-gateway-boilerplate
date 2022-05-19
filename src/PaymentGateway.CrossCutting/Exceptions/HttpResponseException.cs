namespace PaymentGateway.CrossCutting.Exceptions
{
    using System;
    using System.Net;

    public class HttpResponseException : Exception
    {
        private readonly HttpStatusCode _code;
        private readonly string _content;

        public HttpResponseException(HttpStatusCode code, string content)
        {
            this._code = code;
            this._content = content;
        }

        public override string Message => $"Status: {_code}, Content: {_content}";
    }
}
