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
    public class SequenceEqualTests
    {
        [Fact]
        public void SequenceEqual_NullFirst_ThrowArgumentNullException()
        {
            int[] first = null;
            int[] second = { };

            Assert.Throws<ArgumentNullException>(() => first.SequenceEqual(second));
            Assert.Throws<ArgumentNullException>(() => first.SequenceEqual(second, null));
        }

        [Fact]
        public void SequenceEqual_NullSecond_ThrowArgumentNullException()
        {
            int[] first = { };
            int[] second = null;

            Assert.Throws<ArgumentNullException>(() => first.SequenceEqual(second));
            Assert.Throws<ArgumentNullException>(() => first.SequenceEqual(second, null));
        }

        [Fact]
        public void SequenceEqual_EmptyFirstEmptySecond_ShouldBeEqual()
        {
            int[] first = { };
            int[] second = { };

            Assert.True(first.SequenceEqual(second));
        }

        [Fact]
        public void SequenceEqual_EmptyFirstNonEmptySecond_ShouldNotBeEqual()
        {
            int[] first = { };
            int[] second = { 1 };

            Assert.False(first.SequenceEqual(second));
        }

        [Fact]
        public void SequenceEqual_SameFirstSameSecond_ShouldBeEqual()
        {
            var first = Enumerable.Range(1, 5);
            var second = Enumerable.Range(1, 5);

            Assert.True(first.SequenceEqual(second));
        }

        [Fact]
        public void SequenceEqual_DifferentCollections_ShouldNotBeEqual()
        {
            int[] first = { 1, 2 };
            int[] second = { 2, 1 };

            Assert.False(first.SequenceEqual(second));
        }

        [Fact]
        public void SequenceEqual_SameCollectionsCustomComparer_ShouldBeEqual()
        {
            string[] first = { "one", "two" };
            string[] second = { "ONE", "TWO" };

            Assert.True(first.SequenceEqual(second, StringComparer.OrdinalIgnoreCase));
        }
    }
}
