using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NFluent;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace netcoretests
{
    public class HttpClientFactoryTests
    {
        [Fact]
        public async Task HttpClientHandlerCreationTests()
        {
            IHostBuilder builder = new HostBuilder().ConfigureServices(services =>
            {
                services.AddHttpClient();
                services.AddHttpClient("badssl", client =>
                {
                    client.BaseAddress = new System.Uri("https://revoked.badssl.com");
                }).ConfigureHttpMessageHandlerBuilder(builder =>
                {
                    builder.AdditionalHandlers.Add(new LoggingHandler());
                }).ConfigurePrimaryHttpMessageHandler(() =>
                {
                    return new HttpClientHandler()
                    {
                        ServerCertificateCustomValidationCallback = (request, certificate, chain, sslErrors) => false
                    };
                });

                services.AddHttpClient("not-so-badssl", client =>
                {
                    client.BaseAddress = new System.Uri("https://revoked.badssl.com");
                    client.DefaultRequestHeaders.Add("X-ByPassRevoked", "true");
                }).ConfigureHttpMessageHandlerBuilder(builder =>
                {
                    builder.AdditionalHandlers.Add(new LoggingHandler());
                }).ConfigurePrimaryHttpMessageHandler(() =>
                {
                    return new HttpClientHandler()
                    {
                        ServerCertificateCustomValidationCallback = (request, certificate, chain, sslErrors) => false
                    };
                });
            });
            IHost host = builder.Build();

            using IServiceScope serviceScope = host.Services.CreateScope();

            IHttpClientFactory clientFactory = serviceScope.ServiceProvider.GetRequiredService<IHttpClientFactory>();

            HttpClient client = clientFactory.CreateClient("badssl");

            Check.ThatAsyncCode(() => client.GetAsync("/")).Throws<HttpRequestException>();

            client = clientFactory.CreateClient("not-so-badssl");

            var result = await client.GetAsync("/");

            Check.That(result.StatusCode).IsEqualTo(HttpStatusCode.OK);

            client = clientFactory.CreateClient("non-existent");
            await client.GetAsync("https://revoked.badssl.com");
        }

        private class LoggingHandler : DelegatingHandler
        {
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                if (request.Headers.Contains("X-ByPassRevoked"))
                {
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                HttpResponseMessage result = await base.SendAsync(request, cancellationToken);
                return result;
            }
        }
    }
}
