namespace PaymentGateway.CrossCutting
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class Response<TResult>
    {
        private readonly IList<string> _messages = new List<string>();

        public IEnumerable<string> Errors { get; }

        public TResult Result { get; }

        public Response(TResult result) 
        {
            Result = result;
            Errors = new ReadOnlyCollection<string>(_messages);
        }

        public void AddError(string message)
        {
            _messages.Add(message);
        }
    }
}