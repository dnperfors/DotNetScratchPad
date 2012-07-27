using System;
using System.Collections.Generic;
using LearningLinq;
using Xunit;

namespace LearningLinqTests
{
    public class EmptyTests
    {
        [Fact]
        public void Should_return_empty_enumerable()
        {
            IEnumerable<int> emptyEnumerable = Enumerable.Empty<int>();

            Assert.Empty(emptyEnumerable);
        }

        [Fact]
        public void Should_always_return_the_same_instance()
        {
            Assert.Same(Enumerable.Empty<int>(), Enumerable.Empty<int>());
            Assert.Same(Enumerable.Empty<long>(), Enumerable.Empty<long>());
            Assert.Same(Enumerable.Empty<double>(), Enumerable.Empty<double>());
            Assert.Same(Enumerable.Empty<float>(), Enumerable.Empty<float>());
            Assert.Same(Enumerable.Empty<char>(), Enumerable.Empty<char>());
            Assert.NotSame(Enumerable.Empty<int>(), Enumerable.Empty<long>());
        }
    }
}
