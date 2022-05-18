namespace PaymentGateway.Application.Features.PaymentProcessor.Models.PaymentIssuer
{
    public class Response
    {
        public Response() { }

        public Response(string id, string status, Source source) : this()
        {
            Id = id;
            Status = status;
            Source = source;
        }

        public string Id { get; set; }

        public string Status { get; set; }

        public Source Source { get; set; }

    }
}
