using NFluent;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace httpclient.testserver
{
    public class HttpClientTests
    {
        [Fact]
        public async Task MockGetRequest()
        {
            var server = new TestServer();

            using var client = new HttpClient(server);

            var result = await client.GetAsync("https://httbin.org/get");

            Check.That(result.StatusCode).IsEqualTo(HttpStatusCode.OK);
        }

        [Fact]
        public async Task MockCustomGetRequest()
        {
            var server = new TestServer();
            server.RespondWith(HttpStatusCode.NotFound);

            using var client = new HttpClient(server);

            var result = await client.GetAsync("https://httbin.org/get");
            
            Check.That(result.StatusCode).IsEqualTo(HttpStatusCode.NotFound);
        }
        
        [Fact]
        public async Task MockTimeout()
        {
            var server =new TestServer();
            server.SimulateTimeout();

            using var client = new HttpClient(server);

            await client.GetAsync("https://httpbin.org/get");
        }

        [Fact]
        public async Task RealTimeout()
        {
            using var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(10);

            await client.GetAsync("https://httpbin.org/drip?duration=10&numbytes=10&code=200&delay=10");
        }

        [Fact]
        public async Task AssertRequest()
        {
            var server = new TestServer();

            using var client = new HttpClient(server);

            var result = await client.GetAsync("https://httpbin.org/get");

            Check.That(server.Requests).HasMadeCalls();
        }

        [Fact]
        public async Task AssertRequestWithMethod()
        {
            var server = new TestServer();

            using var client = new HttpClient(server);

            var result = await client.GetAsync("https://httpbin.org/get");

            Assert.Throws<HttpRequestAssertException>(() => server.HasRequests().WithMethod(HttpMethod.Get).WithVersion("1.0"));
        }
    }
}
