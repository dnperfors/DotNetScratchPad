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
using System.Runtime.CompilerServices;
using LearningLinq;
using Xunit;

namespace LearningLinqTests
{
    public class DistinctTests
    {
        [Fact]
        public void Should_throw_ArgumentNullException_when_source_is_null()
        {
            IEnumerable<string> source = null;
            Assert.Throws<ArgumentNullException>(() => source.Distinct());
            Assert.Throws<ArgumentNullException>(() => source.Distinct(null));
        }

        [Fact]
        public void Should_return_values_only_once()
        {
            IEnumerable<string> source = new string[] { "a", "b", "b", "c", "d", "a", "c", "c" };
            IEnumerable<string> expected = new string[] { "a", "b", "c", "d" };

            Assert.Equal(expected, source.Distinct());
            Assert.Equal(expected, source.Distinct(null));
        }

        [Fact]
        public void Should_return_values_only_once_according_to_own_equalityComparer()
        {
            IEnumerable<string> source = new string[] { "a", "b", "B", "c", "d", "a", "C", "C" };
            IEnumerable<string> expected = new string[] { "a", "b", "c", "d" };

            Assert.Equal(expected, source.Distinct(StringComparer.OrdinalIgnoreCase));
        }

        private class CustomEqualityComparer : IEqualityComparer<int>
        {
            public bool Equals(int x, int y)
            {
                return object.ReferenceEquals(x, y);
            }

            public int GetHashCode(int obj)
            {
                return RuntimeHelpers.GetHashCode(obj);
            }
        }
    }
}
