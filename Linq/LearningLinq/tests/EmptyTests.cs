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
    public class EmptyTests
    {
        [Fact]
        public void Should_return_empty_enumerable()
        {
            IEnumerable<int> emptyEnumerable = Enumerable.Empty<int>();

            Assert.Empty(emptyEnumerable);
        }

        [Fact]
        public void Should_always_return_the_same_instance()
        {
            Assert.Same(Enumerable.Empty<int>(), Enumerable.Empty<int>());
            Assert.Same(Enumerable.Empty<long>(), Enumerable.Empty<long>());
            Assert.Same(Enumerable.Empty<double>(), Enumerable.Empty<double>());
            Assert.Same(Enumerable.Empty<float>(), Enumerable.Empty<float>());
            Assert.Same(Enumerable.Empty<char>(), Enumerable.Empty<char>());
            Assert.NotSame(Enumerable.Empty<int>(), Enumerable.Empty<long>());
        }
    }
}
