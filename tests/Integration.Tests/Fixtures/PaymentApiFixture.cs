using System.Diagnostics.CodeAnalysis;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Integration.Tests.Fixtures
{
    [ExcludeFromCodeCoverage]
    public class PaymentApiFixture : WebApplicationFactory<Program>
    {
        private readonly string _environment;

        public PaymentApiFixture(string environment = "Development")
        {
            _environment = environment;
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.UseEnvironment(_environment);

            builder.ConfigureServices(services =>
            {
                
            });

            return base.CreateHost(builder);
        }

    }
}
