using NFluent;
using NFluent.Extensibility;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace httpclient.testserver
{
    public class TestServer : HttpMessageHandler
    {
        private HttpResponseMessage response = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK
        };
        private ConcurrentQueue<HttpRequestMessage> calls = new ConcurrentQueue<HttpRequestMessage>();
        public IEnumerable<HttpRequestMessage> Requests => calls;

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
            calls.Enqueue(request);

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

        public RequestsAsserter HasRequests()
        {
            return new RequestsAsserter(Requests);
        }
    }

    internal static class HttpRequestMessageExtensions
    {
        public static bool HasMethod(this HttpRequestMessage request, string verb)
        {
            return request.HasMethod(new HttpMethod(verb));
        }

        public static bool HasMethod(this HttpRequestMessage request, HttpMethod httpMethod)
        {
            return request.Method == httpMethod;
        }

        public static bool HasVersion(this HttpRequestMessage request, string version)
        {
            return request.Version == new Version(version);
        }
    }

    internal static class TestServerChecks
    {
        public static ICheckLink<ICheck<IEnumerable<HttpRequestMessage>>> HasMadeCalls(this ICheck<IEnumerable<HttpRequestMessage>> context)
        {
            ExtensibilityHelper.BeginCheck(context)
                .FailIfNull()
                .FailWhen(sut => !sut.Any(), "No calls were made, though at least 1 expected.")
                .OnNegate("Calls were made, but non expected.")
                .EndCheck();
            return ExtensibilityHelper.BuildCheckLink(context);
        }
    }

    public class RequestsAsserter
    {
        private IEnumerable<HttpRequestMessage> httpRequests;
        private List<string> conditions = new List<string>();

        public RequestsAsserter(IEnumerable<HttpRequestMessage> httpRequests)
        {
            this.httpRequests = httpRequests;
        }

        public RequestsAsserter WithVersion(string version)
        {
            return With(x => x.HasVersion(version), $"version {version}");
        }

        public RequestsAsserter WithMethod(HttpMethod httpMethod)
        {
            return With(x => x.HasMethod(httpMethod), $"method {httpMethod.Method}");
        }

        public RequestsAsserter With(Func<HttpRequestMessage, bool> predicate, string condition = null)
        {
            if(!string.IsNullOrEmpty(condition))
            {
                conditions.Add(condition);
            }

            httpRequests = httpRequests.Where(predicate).ToList();
            Assert();
            return this;
        }

        private void Assert()
        {
            var pass = httpRequests.Any();

            if(!pass)
            {
                throw new HttpRequestAssertException(conditions, null, httpRequests.Count());
            }
        }
    }

    internal class HttpRequestAssertException : Exception
    {
        public HttpRequestAssertException(IEnumerable<string> conditions, int? expectedRequests, int actualRequests)
            :base(BuildMessage(conditions, expectedRequests, actualRequests))
        {

        }

        private static string BuildMessage(IEnumerable<string> conditions, int? expectedRequests, int actualRequests)
        {
            var expected = expectedRequests switch
            {
                null => "any requests to be made",
                0 => "no requests to be made",
                1 => "one request to be made",
                _ => $"{expectedRequests} requests to be made"
            };

            var actual = actualRequests switch
            {
                0 => "no requests were made",
                1 => "one request was made",
                _ => $"{actualRequests} requests were made"
            };

            if(conditions.Any())
            {
                expected += " with " + string.Join(" and ", conditions);
            }

            return $"Expected {expected}, but {actual}.";
        }
    }

    internal class TimeoutHttpResponseMessage : HttpResponseMessage
    {
    }
}
