using NFluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Xunit;

namespace httpclient.testserver
{
    public class HttpRequestMethodExtensionsTests
    {
        [Theory]
        [MemberData(nameof(HttpMethods))]
        public void HasMethod_WhenRequestHasMethod_ReturnTrue(HttpMethod requestedMethod)
        {
            var sut = new HttpRequestMessage(requestedMethod, "");
            Check.That(sut.HasMethod(requestedMethod)).IsTrue();
        }

        [Theory]
        [MemberData(nameof(HttpMethodNames))]
        public void HasMethod_WhenRequestHasMethodName_ReturnTrue(HttpMethod requestedMethod, string checkedMethod)
        {
            var sut = new HttpRequestMessage(requestedMethod, "");
            Check.That(sut.HasMethod(checkedMethod)).IsTrue();
        }

        [Theory]
        [InlineData("GET")]
        [InlineData("get")]
        [InlineData("Get")]
        public void HasMethodUsingDifferentCasing(string input)
        {
            var sut = new HttpRequestMessage(HttpMethod.Get, "");
            Check.That(sut.HasMethod(input)).IsTrue();
        }

        [Fact]
        public void HasMethodWithIncorrectValue()
        {
            var sut = new HttpRequestMessage(HttpMethod.Head, "");

            Check.That(sut.HasMethod(HttpMethod.Get)).IsFalse();
        }

        private static IEnumerable<HttpMethod> AllHttpMethods => new[] { HttpMethod.Get, HttpMethod.Post, HttpMethod.Put, HttpMethod.Delete, HttpMethod.Head, HttpMethod.Options, HttpMethod.Patch, HttpMethod.Trace };
        public static IEnumerable<object[]> HttpMethods => AllHttpMethods.Select(x => new object[] { x });
        public static IEnumerable<object[]> HttpMethodNames => AllHttpMethods.Select(x => new object[] { x, x.Method });

        [Fact]
        public void HasVersion_RequestWithCorrectVersion_ReturnsTrue()
        {
            var sut = new HttpRequestMessage
            {
                Version = new Version("1.1")
            };

            Check.That(sut.HasVersion("1.1")).IsTrue();
        }

        [Fact]
        public void HasVersion_RequestWithIncorrectVersion_ReturnsFalse()
        {
            var sut = new HttpRequestMessage { Version = new Version("1.0") };

            Check.That(sut.HasVersion("1.1")).IsFalse();
        }
    }
}
