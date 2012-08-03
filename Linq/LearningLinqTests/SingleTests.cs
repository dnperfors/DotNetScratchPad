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
    public class SingleTests
    {
        [Fact]
        public void Should_throw_ArgumentNullException_when_source_is_null()
        {
            IEnumerable<int> source = null;
            Assert.Throws<ArgumentNullException>(() => source.Single());
            Assert.Throws<ArgumentNullException>(() => source.Single(x => x > 5));
        }

        [Fact]
        public void Should_throw_ArgumentNullException_when_predicate_is_null()
        {
            Assert.Throws<ArgumentNullException>(() => new int[1].Single(null));
        }

        [Fact]
        public void Should_throw_InvalidOperationException_when_source_is_empty()
        {
            Assert.Throws<InvalidOperationException>(() => new int[0].Single());
            Assert.Throws<InvalidOperationException>(() => new int[0].Single(x => x > 5));
        }

        [Fact]
        public void Should_return_element_when_source_contains_only_one_element()
        {
            Assert.Equal(5, new int[] { 5 }.Single());
        }

        [Fact]
        public void Should_throw_InvalidOperationException_when_source_contains_more_than_one_element()
        {
            Assert.Throws<InvalidOperationException>(() => new int[] { 3, 5, 7 }.Single());
        }

        [Fact]
        public void Should_return_element_when_source_contains_only_one_element_and_it_matches_predicate()
        {
            Assert.Equal(7, new int[] { 7 }.Single(x => x > 5));
        }

        [Fact]
        public void Should_throw_InvalidOperationException_when_source_contains_only_one_element_and_it_didnot_match_predicate()
        {
            Assert.Throws<InvalidOperationException>(() => new int[] { 5 }.Single(x => x > 5));
        }

        [Fact]
        public void Should_return_only_matching_element_when_source_contains_multiple_elements_matching_predicate()
        {
            Assert.Equal(7, new int[] { 5, 7, 3 }.Single(x => x > 5));
        }

        [Fact]
        public void Should_throw_InvalidOperationException_when_source_contains_multiple_elements_matching_predicate()
        {
            Assert.Throws<InvalidOperationException>(() => new int[] { 5, 7, 9 }.Single(x => x > 5));
        }

        [Fact]
        public void Should_throw_InvalidOperationException_when_source_contains_only_elements_not_matching_predicate()
        {
            Assert.Throws<InvalidOperationException>(() => new int[] { 1, 3, 5 }.Single(x => x > 5));
        }
    }
}
