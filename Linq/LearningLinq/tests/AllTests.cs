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
    public class AllTests
    {
        [Fact]
        public void Should_throw_ArgumentNullException_when_source_is_null()
        {
            IEnumerable<int> source = null;
            Assert.Throws<ArgumentNullException>(() => source.All(x => true));
        }

        [Fact]
        public void Should_throw_ArgumentNullException_when_predicate_is_null()
        {
            Assert.Throws<ArgumentNullException>(() => (new int[] { 1, 2, 3 }).All((Func<int,bool>)null));
        }

        [Fact]
        public void Should_return_true_when_source_is_empty()
        {
            Assert.Equal(true, new int[0].All(x => x < 0));
        }

        [Fact]
        public void Should_return_false_when_no_elements_match_predicate()
        {
            Assert.Equal(false, new int[] { 1, 2, 3 }.All(x => x < 0));
        }

        [Fact]
        public void Should_return_false_when_some_elements_match_predicate()
        {
            Assert.Equal(false, new int[] { 1, 2, 3 }.All(x => (x % 2) == 0));
        }

        [Fact]
        public void Should_return_true_when_all_elements_match_predicate()
        {
            Assert.Equal(true, new int[] { 2, 4, 6 }.All(x => (x % 2) == 0));
        }

        [Fact]
        public void Should_stop_enumerating_as_soon_as_predicate_matches()
        {
            IEnumerable<int> source = new int[] { 10, 2, 0, 3 };
            var query = source.Select(x => 10 / x);
            query.Any(x => x > 2);
        }
    }
}
