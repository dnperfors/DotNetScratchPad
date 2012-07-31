using System;
using System.Collections.Generic;
using LearningLinq;
using Xunit;
using Xunit.Extensions;

namespace LearningLinqTests
{
    public class RangeTests
    {
        [Fact]
        public void Should_throw_ArgumentOutOfRangeException_when_count_is_less_than_0()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Enumerable.Range(0, -1));
        }

        [Theory]
        [InlineData(int.MaxValue, int.MaxValue)]
        [InlineData(2, int.MaxValue)]
        [InlineData(int.MaxValue, 2)]
        public void Should_throw_ArguementOutOfRangeException_when_start_plus_count_minus_1_is_larger_than_IntMaxValue(int start, int count)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Enumerable.Range(start, count));
        }

        [Theory]
        [InlineData(int.MaxValue, 0)]
        [InlineData(0, int.MaxValue)]
        public void Should_not_throw_exception_when_start_plus_count_is_less_or_equal_to_IntMaxValue(int start, int count)
        {
            Assert.DoesNotThrow(() => Enumerable.Range(start, count));
        }

        [Theory]
        [InlineData(2, 3, new int[] { 2, 3, 4 })]
        [InlineData(-2, 4, new int[] { -2, -1, 0, 1 })]
        [InlineData(5, 0, new int[] { })]
        [InlineData(int.MaxValue, 1, new int[] { int.MaxValue })]
        public void Should_return_correct_values(int start, int count, int[] expected)
        {
            Assert.Equal(expected, Enumerable.Range(start, count));
        }
    }
}
