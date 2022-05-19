// See https://aka.ms/new-console-template for more information
using PaymentGateway.Mock.Helper;

using RestEase;

using WireMock.Admin.Mappings;
using WireMock.Client;

var mockServerUrl = Environment.GetEnvironmentVariable("MockServerUrl") ?? "http://localhost:1080/";

var api = RestClient.For<IWireMockAdminApi>(new Uri(mockServerUrl));

Console.WriteLine(" Reset mappings ");

_ = api.ResetMappingsAsync().GetAwaiter().GetResult();

Console.WriteLine(" Adding Expectations ");

var mappings = new List<MappingModel>()
{
    ExpectationsHelper.AddGetPayment("ACTVPTPL", "0001"),
    ExpectationsHelper.AddPostPayment("ACTVPTPL", "0001"),
};


_ = api.PostMappingsAsync(mappings).GetAwaiter().GetResult();

Console.WriteLine(" Expectations successfully added ");