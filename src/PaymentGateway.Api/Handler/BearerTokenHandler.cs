namespace PaymentGateway.Api.Handler
{
    using IdentityModel.Client;

    public class BearerTokenHandler : DelegatingHandler
    {
        public BearerTokenHandler()
        {
            
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            
            var accessToken = ""; //get from cache

            request.SetBearerToken(accessToken);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
