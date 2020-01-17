using NFluent;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
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
        /*
        [Fact]
        public async Task MockTimeout()
        {
            var server =new TestServer();
            server.SimulateTimeout();

            using var client = new HttpClient(server);

            var result = await client.GetAsync("https://httpbin.org/get");

            Check.That(result.StatusCode).IsEqualTo(HttpStatusCode.OK);
        }*/

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
