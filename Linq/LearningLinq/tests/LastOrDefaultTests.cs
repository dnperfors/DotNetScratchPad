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
using Moq;
using Xunit;

namespace LearningLinqTests
{
    public class LastOrDefaultTests
    {
        [Fact]
        public void Should_throw_ArgumentNullException_when_source_is_null()
        {
            IEnumerable<int> source = null;
            Assert.Throws<ArgumentNullException>(() => source.LastOrDefault());
            Assert.Throws<ArgumentNullException>(() => source.LastOrDefault(x => x > 3));
        }

        [Fact]
        public void Should_throw_ArgumentNullException_when_predicate_is_null()
        {
            Assert.Throws<ArgumentNullException>(() => new LinkedList<int>().LastOrDefault(null));
        }

        [Fact]
        public void Should_return_default_value_when_source_is_empty()
        {
            var source = new LinkedList<int>();
            Assert.Equal(default(int), source.LastOrDefault());
            Assert.Equal(default(int), source.LastOrDefault(x => x > 3));
        }

        [Fact]
        public void Should_return_only_value_when_source_has_one_value()
        {
            var source = new LinkedList<int>(new int[] { 5 });
            Assert.Equal(5, source.LastOrDefault());
        }

        [Fact]
        public void Should_return_last_value_when_source_has_more_than_one_value()
        {
            var source = new LinkedList<int>(new int[] { 5, 10, 14 });
            Assert.Equal(14, source.LastOrDefault());
        }

        [Fact]
        public void Should_return_only_element_matchin_predicate()
        {
            var source = new LinkedList<int>(new int[] { 5 });
            Assert.Equal(5, source.LastOrDefault(x => x > 3));
        }

        [Fact]
        public void Should_return_default_value_when_only_element_doesnot_match_perdicate()
        {
            var source = new LinkedList<int>(new int[] { 3 });
            Assert.Equal(default(int), source.LastOrDefault(x => x > 3));
        }

        [Fact]
        public void Should_return_last_element_matchin_predicate()
        {
            var source = new LinkedList<int>(new int[] { 15, 5, 10 });
            Assert.Equal(10, source.LastOrDefault(x => x > 3));
        }

        [Fact]
        public void Should_return_default_value_when_no_elements_match_perdicate()
        {
            var source = new LinkedList<int>(new int[] { 1, 3, 2 });
            Assert.Equal(default(int), source.LastOrDefault(x => x > 3));
        }

        [Fact]
        public void Should_not_iterate_when_source_is_IList()
        {
            var source = new Mock<IList<int>>();
            source.Setup(x => x.Count).Returns(4);
            source.Setup(x => x[4 - 1]).Returns(5);

            Assert.Equal(5, source.Object.LastOrDefault());
            source.Verify(x => x.GetEnumerator(), Times.Never());
        }
    }
}
