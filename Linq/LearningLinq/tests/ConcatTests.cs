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
    public class ConcatTests
    {
        [Fact]
        public void Should_throw_ArgumentNullException_when_first_is_null()
        {
            IEnumerable<int> first = null;
            IEnumerable<int> second = Enumerable.Range(11, 10);

            Assert.Throws<ArgumentNullException>(() => first.Concat(second));
        }

        [Fact]
        public void Should_throw_ArgumentNullException_when_second_is_null()
        {
            IEnumerable<int> first = Enumerable.Range(1, 10);
            IEnumerable<int> second = null;

            Assert.Throws<ArgumentNullException>(() => first.Concat(second));
        }

        [Fact]
        public void Should_concat_two_versions_together()
        {
            IEnumerable<int> first = Enumerable.Range(1, 10);
            IEnumerable<int> second = Enumerable.Range(11, 10);


            Assert.Equal(Enumerable.Range(1, 20), first.Concat(second));
        }
    }
}
