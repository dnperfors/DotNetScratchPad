///   Copyright 2012 David Perfors
///
///   Licensed under the Apache License, Version 2.0 (the "License");
///   you may not use this file except in compliance with the License.
///   You may obtain a copy of the License at
///
///       http://www.apache.org/licenses/LICENSE-2.0
///
///   Unless required by applicable law or agreed to in writing, software
///   distributed under the License is distributed on an "AS IS" BASIS,
///   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
///   See the License for the specific language governing permissions and
///   limitations under the License.

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
            Enumerable.Range(start, count);
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
