namespace PaymentGateway.Mock.Helper
{
    using System.Diagnostics;
    using System.Net;

    using WireMock.Admin.Mappings;

    public class ExpectationsHelper
    {
        public static string ExtractJson(string expectationPath)
        {
            return StreamReaderHelper.ExtractJson(expectationPath);
        }

        public static MappingModel AddGetPayment(string issuer, string paymentId)
        {
            return new MappingModel
                {
                    Request = new RequestModel
                    {
                        Path = $"/{issuer}/payments/{paymentId}",
                        Methods = new[] { "GET" },
                    },
                    Response = new ResponseModel
                    {
                        Body = ExtractJson($@"Expectations/Issuer/{issuer}/Payments/{paymentId}_ReturnOkStatus.json"),
                        Headers = new Dictionary<string, object> { { "Content-Type", "application/json" } },
                        StatusCode = HttpStatusCode.OK,
                    },
                };
        }
        public static MappingModel AddPostPayment(string issuer, string paymentId)
        {
            return new MappingModel
                {
                    Request = new RequestModel
                    {
                        Path = $"/{issuer}/payments",
                        Methods = new[] { "POST" },
                    },
                    Response = new ResponseModel
                    {
                        Body = ExtractJson($@"Expectations/Issuer/{issuer}/Payments/{paymentId}_ReturnOkStatus.json"),
                        Headers = new Dictionary<string, object> { { "Content-Type", "application/json" } },
                        StatusCode = HttpStatusCode.Created,
                    },
                };
        }
    }
}
