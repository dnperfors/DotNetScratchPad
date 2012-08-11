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
    public class UnionTests
    {
        [Fact]
        public void Union_firstIsNull_ThrowsArgumentNullException()
        {
            IEnumerable<string> first = null;
            IEnumerable<string> second = new string[] { };
            Assert.Throws<ArgumentNullException>(() => first.Union(second));
            Assert.Throws<ArgumentNullException>(() => first.Union(second, StringComparer.OrdinalIgnoreCase));
        }
        [Fact]
        public void Union_secondIsNull_ThrowsArgumentNullException()
        {
            IEnumerable<string> first = new string[] { };
            IEnumerable<string> second = null;
            Assert.Throws<ArgumentNullException>(() => first.Union(second));
            Assert.Throws<ArgumentNullException>(() => first.Union(second, StringComparer.OrdinalIgnoreCase));
        }

        [Fact]
        public void Union_TwoFullCollection_ReturnsOneCollectionWithElementNotYetReturned()
        {
            string[] first = { "a", "b", "c", "b" };
            string[] second = { "d", "a", "A", "d", "D" };

            Assert.Equal(new string[] { "a", "b", "c", "d", "A", "D" }, first.Union(second));
            Assert.Equal(new string[] { "a", "b", "c", "d" }, first.Union(second, StringComparer.OrdinalIgnoreCase));
        }

        [Fact]
        public void Union_OneEmptyCollectionOneFilledCollection_ReturnsOneCollectionWithElementNotYetReturned()
        {
            string[] first = { };
            string[] second = { "d", "a", "A", "d", "D" };

            Assert.Equal(new string[] { "d", "a", "A", "D" }, first.Union(second));
            Assert.Equal(new string[] { "d", "a" }, first.Union(second, StringComparer.OrdinalIgnoreCase));
        }
    }
}
