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
    public class DefaultIfEmptyTests
    {
        [Fact]
        public void Should_throw_ArgumentNullException_when_source_is_null()
        {
            IEnumerable<int> source = null;
            Assert.Throws<ArgumentNullException>(() => source.DefaultIfEmpty());
            Assert.Throws<ArgumentNullException>(() => source.DefaultIfEmpty(-1));
        }

        [Fact]
        public void Should_return_default_value_when_source_is_empty()
        {
            Assert.Equal(new int[] { default(int) }, new int[0].DefaultIfEmpty());
            Assert.Equal(new int[] { -1 }, new int[0].DefaultIfEmpty(-1));
        }

        [Fact]
        public void Should_return_source_when_source_is_not_empty()
        {
            IEnumerable<int> source = Enumerable.Range(1, 20);
            Assert.Equal(source, source.DefaultIfEmpty());
            Assert.Equal(source, source.DefaultIfEmpty(-1));
        }
    }
}
