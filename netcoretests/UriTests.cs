using System;
using Xunit;

namespace netcoretests
{
    public class UriTests
    {
        [Theory]
        [InlineData("http://example.com", "/get", "http://example.com/get")]
        [InlineData("http://example.com/test", "/get", "http://example.com/get")]
        [InlineData("http://example.com/test", "get", "http://example.com/get")]
        [InlineData("http://example.com/test/", "get", "http://example.com/test/get")]
        public void Combine_TwoUris_ReturnsCombinedUri(string baseUriString, string relativeUriString, string expectedUriString)
        {
            Uri baseUri = new Uri(baseUriString);
            Uri relativeUri = new Uri(relativeUriString, UriKind.Relative);
            Uri expectedUri = new Uri(expectedUriString);

            var result = new Uri(baseUri, relativeUri);

            Assert.Equal(expectedUri, result);
        }
    }
}
