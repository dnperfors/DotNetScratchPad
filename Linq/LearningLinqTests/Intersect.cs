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
    public class IntersectTests
    {
        [Fact]
        public void Intersect_FirstCollectionNull_throwArgumentNullException()
        {
            string[] first = null;
            string[] second = { };

            Assert.Throws<ArgumentNullException>(() => first.Intersect(second));
            Assert.Throws<ArgumentNullException>(() => first.Intersect(second, StringComparer.OrdinalIgnoreCase));
        }

        [Fact]
        public void Intersect_SecondCollectionNull_throwArgumentNullException()
        {
            string[] first = { };
            string[] second = null;

            Assert.Throws<ArgumentNullException>(() => first.Intersect(second));
            Assert.Throws<ArgumentNullException>(() => first.Intersect(second, StringComparer.OrdinalIgnoreCase));
        }

        [Fact]
        public void Intersect_twoDifferentCollections_ReturnElementsThatAreInBothCollections()
        {
            string[] first = { "a", "b", "c", "A", "C" };
            string[] second = { "a", "c", "d", "D", "C" };

            Assert.Equal(new string[] { "a", "c", "C" }, first.Intersect(second));
            Assert.Equal(new string[]{ "a", "c"}, first.Intersect(second, StringComparer.OrdinalIgnoreCase));
        }
    }
}
