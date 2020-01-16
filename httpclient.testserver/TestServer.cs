using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace httpclient.testserver
{
    public class TestServer : HttpMessageHandler
    {
        private HttpResponseMessage response;
        internal void RespondWith(HttpResponseMessage responseMessage)
        {
            response = responseMessage;
        }

        public TestServer RespondWith(HttpStatusCode statusCode)
        {
            RespondWith(new HttpResponseMessage
            {
                StatusCode = statusCode
            });

            return this;
        }

        public TestServer SimulateTimeout()
        {
            RespondWith(new TimeoutHttpResponseMessage());
            return this;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var result = new TaskCompletionSource<HttpResponseMessage>();
            if(response is TimeoutHttpResponseMessage)
            {
                result.SetCanceled();
            }
            else{
                result.SetResult(response);
            }
            return result.Task;
        }
    }

    internal class TimeoutHttpResponseMessage : HttpResponseMessage
    {
    }
}
