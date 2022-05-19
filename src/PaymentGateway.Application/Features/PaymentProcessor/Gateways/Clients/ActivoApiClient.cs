namespace PaymentGateway.Application.Features.PaymentProcessor.Gateways.Clients
{
    using System;
    using System.Net;
    using System.Net.Http.Json;

    using CrossCutting.Exceptions;

    using MediatR;

    using Newtonsoft.Json;

    using PaymentGateway.Application.Features.PaymentProcessor.Gateways.Interfaces;

    using PaymentProcessor.Models.PaymentIssuer;

    using Serilog;

    /// <summary>
    /// Client for request to Activo API
    /// </summary>
    public class ActivoApiClient : IBankApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly HttpStatusCode[] _avoidStatusCode = { HttpStatusCode.BadGateway, HttpStatusCode.BadRequest, HttpStatusCode.InternalServerError };




        public ActivoApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient = httpClient;
        }

        public async Task<Response> ProcessPaymentAsync(Request request, CancellationToken cancellationToken)
        {
            var response = await this._httpClient.PostAsJsonAsync("/ACTVPTPL/payments", request, cancellationToken);

            if (_avoidStatusCode.Contains(response.StatusCode))
            {
                var message = await response.Content.ReadAsStringAsync(cancellationToken);
                Log.Error($"{nameof(ActivoApiClient)}.ProcessPaymentAsync: failed to call ACTVPTPL Api", () => new { response.StatusCode, Message = message });

                throw new HttpResponseException(response.StatusCode, message);
            }

            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            var result = JsonConvert.DeserializeObject<Response>(content);

            return result;
        }

        public async Task<Response> GetPaymentByIdAsync(string id, CancellationToken cancellationToken)
        {
            var response = await this._httpClient.GetAsync($"/ACTVPTPL/payments/{id}", cancellationToken);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                var message = await response.Content.ReadAsStringAsync(cancellationToken);
                Log.Error($"{nameof(ActivoApiClient)}.GetPaymentByIdAsync: failed to call ACTVPTPL Api", () => new { response.StatusCode, Message = message });

                throw new HttpResponseException(response.StatusCode, message);
            }

            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            var result = JsonConvert.DeserializeObject<Response>(content);

            return result;
        }
    }
}
