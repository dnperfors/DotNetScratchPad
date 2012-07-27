using System;
using System.Collections.Generic;
using Xunit;
using LearningLinq;
using Xunit.Extensions;

namespace LearningLinqTests
{
    public class RepeatTests
    {
        [Fact]
        public void Should_throw_ArgumentOutOfRangeException_when_count_is_negative()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Enumerable.Repeat(4, -1));
        }

        [Theory]
        [InlineData(new int[] { 5, 5, 5, 5, 5 }, 5, 5)]
        [InlineData(new object[] { null, null }, null, 2)]
        [InlineData(new char[] {}, 'x', 0)]
        public void Should_correctly_return_the_same_value_multiple_times<T>(T[] expected, T element, int count)
        {
            Assert.Equal(expected, Enumerable.Repeat(element, count));
        }
    }
}
