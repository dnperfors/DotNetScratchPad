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
    public class WhereTests
    {
        [Fact]
        public void Should_throw_ArgumentNullException_when_source_is_null()
        {
            IEnumerable<int> source = null;
            Assert.Throws<ArgumentNullException>(() => source.Where(x => x < 4));
            Assert.Throws<ArgumentNullException>(() => source.Where((x, index) => x < 4));
        }

        [Fact]
        public void Should_throw_ArgumentNullException_when_predicate_is_null()
        {
            IEnumerable<int> source = new int[] { 2, 5, 3, 7, 1, 4 };
            Assert.Throws<ArgumentNullException>(() => source.Where((Func<int, bool>)null));
            Assert.Throws<ArgumentNullException>(() => source.Where((Func<int, int, bool>)null));
        }

        [Fact]
        public void Should_return_correct_result()
        {
            IEnumerable<int> source = new int[] { 2, 5, 3, 7, 1, 4 };
            Assert.Equal(new int[] { 2, 3, 1 }, source.Where(x => x < 4));
            Assert.Equal(new int[] { 2, 3, 1 }, source.Where((x, index) => x < 4));
        }

        [Fact]
        public void Should_give_index_to_predicate()
        {
            IEnumerable<int> source = new int[] { 2, 5, 3, 7, 1, 4 };

            Assert.Equal(new int[] { 1, 4 }, source.Where((x, index) => x < index));
        }
    }
}
