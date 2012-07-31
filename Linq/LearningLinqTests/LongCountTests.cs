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
    public class LongCountTests
    {
        [Fact]
        public void Should_throw_ArgumentNullException_when_source_is_null()
        {
            IEnumerable<int> source = null;
            Assert.Throws<ArgumentNullException>(() => source.LongCount());
            Assert.Throws<ArgumentNullException>(() => source.LongCount(x => x > 3));
        }

        [Fact]
        public void Should_throw_ArguementNullException_when_predicate_is_null()
        {
            IEnumerable<int> source = Enumerable.Range(0, 5);
            Assert.Throws<ArgumentNullException>(() => source.LongCount((Func<int, bool>)null));
        }

        [Fact]
        public void Should_count_with_predicate()
        {
            IEnumerable<int> source = Enumerable.Range(1, 10);
            Assert.Equal(5, source.LongCount(x => x > 5));
        }

        [Fact]
        public void Should_simply_count_IEnumerable()
        {
            Assert.Equal(10, Enumerable.Range(1, 10).LongCount());
        }

        [Fact(Skip = "Takes to long.")]
        public void Should_not_throw_OverflowExcpetion_when_there_are_more_than_intMaxValue_items()
        {
            IEnumerable<int> source = Enumerable.Range(0, int.MaxValue).Concat(Enumerable.Range(0, 1));
            Assert.Equal(int.MaxValue + 1L, source.LongCount());
        }

        [Fact(Skip = "Takes to long.")]
        public void Should_throw_OverflowExcpetion_when_there_are_more_than_intMaxValue_items_and_using_predicate()
        {
            IEnumerable<int> source = Enumerable.Range(0, int.MaxValue).Concat(Enumerable.Range(0, 1));
            Assert.Equal(int.MaxValue + 1L, source.LongCount(x => x > -1));
        }
    }
}
