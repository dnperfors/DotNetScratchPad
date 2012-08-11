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
        [InlineData(new char[] { }, 'x', 0)]
        public void Should_correctly_return_the_same_value_multiple_times<T>(T[] expected, T element, int count)
        {
            Assert.Equal(expected, Enumerable.Repeat(element, count));
        }
    }
}
