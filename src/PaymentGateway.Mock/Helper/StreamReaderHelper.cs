namespace PaymentGateway.Mock.Helper
{
    using System.IO;
    using System.Text;

    public static class StreamReaderHelper
    {
        public static string ExtractJson(string filePath)
        {
            using var reader = new StreamReader(filePath, Encoding.UTF8);
            return reader.ReadToEnd();
        }
    }
}
