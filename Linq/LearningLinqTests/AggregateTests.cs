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

namespace LearningLinqTests
{
    public class AggregateTests
    {
        [Fact]
        public void Should_throw_ArgumentNullException_when_source_is_null()
        {
            IEnumerable<int> source = null;
            Assert.Throws<ArgumentNullException>(() => source.Aggregate((x, y) => x + y));
            Assert.Throws<ArgumentNullException>(() => source.Aggregate(3, (x, y) => x + y));
            Assert.Throws<ArgumentNullException>(() => source.Aggregate(3, (x, y) => x + y, result => result.ToString()));
        }

        [Fact]
        public void Should_throw_ArgumentNullException_when_func_is_null()
        {
            IEnumerable<int> source = new int[] { 1, 3 };
            Assert.Throws<ArgumentNullException>(() => source.Aggregate(null));
            Assert.Throws<ArgumentNullException>(() => source.Aggregate(3, null));
            Assert.Throws<ArgumentNullException>(() => source.Aggregate(3, null, result => result.ToString()));
        }

        [Fact]
        public void Should_throw_ArgumentNullException_when_resultSelector_is_null()
        {
            IEnumerable<int> source = new int[] { 1, 3 };
            Assert.Throws<ArgumentNullException>(() => source.Aggregate(3, (x, y) => x + y, (Func<int, int>)null));
        }

        [Fact]
        public void Should_throw_InvalidOperationException_when_source_is_empty()
        {
            IEnumerable<int> source = new int[0];
            Assert.Throws<InvalidOperationException>(() => source.Aggregate((x, y) => x + y));
        }

        [Fact]
        public void Should_return_correct_value_when_using_simple_aggregate()
        {
            IEnumerable<int> source = new int[] { 1, 4, 5 };
            Assert.Equal(17, source.Aggregate((current, value) => current * 2 + value));
        }

        [Fact]
        public void Should_return_correct_value_when_using_seed()
        {
            IEnumerable<int> source = new int[] { 1, 4, 5 };
            Assert.Equal(57, source.Aggregate(5, (current, value) => current * 2 + value));
        }

        [Fact]
        public void Should_return_correct_value_when_using_projection()
        {
            IEnumerable<int> source = new int[] { 1, 4, 5 };
            Assert.Equal("57", source.Aggregate(5, (current, value) => current * 2 + value, result => result.ToString()));
        }
    }
}
