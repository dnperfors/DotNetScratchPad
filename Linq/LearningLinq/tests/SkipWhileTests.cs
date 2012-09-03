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
    public class SkipWhileTests
    {
        [Fact]
        public void SkipWhileNoIndex_NullSource_ThrowArgumentNullException()
        {
            int[] source = null;

            Assert.Throws<ArgumentNullException>(() => source.SkipWhile(x => x > 0));
        }

        [Fact]
        public void SkipWhileNoIndex_NullPredicate_ThrowsArgumentNullException()
        {
            int[] source = new int[] { 1, 2, 3 };
            Func<int, bool> predicate = null;

            Assert.Throws<ArgumentNullException>(() => source.SkipWhile(predicate));
        }

        [Fact]
        public void SkipWhileNoIndex_FirstElementFailingPredicate_ResultSourceCompletely()
        {
            string[] source = { "zero", "one", "two", "three", "four" };

            var result = source.SkipWhile(x => x.Length > 4);
            Assert.Equal(source, result);
        }

        [Fact]
        public void SkipWhileNoIndex_MatchingFirstThreeElements_ResultLastElements()
        {
            string[] source = { "zero", "one", "two", "three", "four" };

            var result = source.SkipWhile(x => x.Length < 5);
            Assert.Equal(new string[] { "three", "four" }, result);
        }

        [Fact]
        public void SkipWhileNoIndex_MatchingAllElements_EmptyResult()
        {
            string[] source = { "zero", "one", "two", "three", "four" };

            var result = source.SkipWhile(x => x.Length < 10);
            Assert.Empty(result);
        }

        [Fact]
        public void SkipWhileIndex_NullSource_ThrowArgumentNullException()
        {
            int[] source = null;

            Assert.Throws<ArgumentNullException>(() => source.SkipWhile((x, index) => x > index));
        }

        [Fact]
        public void SkipWhileIndex_NullPredicate_ThrowsArgumentNullException()
        {
            int[] source = new int[] { 1, 2, 3 };
            Func<int, int, bool> predicate = null;

            Assert.Throws<ArgumentNullException>(() => source.SkipWhile(predicate));
        }

        [Fact]
        public void SkipWhileIndex_FirstElementFailingPredicate_AllSource()
        {
            string[] source = { "zero", "one", "two", "three", "four" };

            var result = source.SkipWhile((x, index) => x.Length < index);
            Assert.Equal(source, result);
        }

        [Fact]
        public void SkipWhileIndex_MatchingFirstThreeElements_LastElements()
        {
            string[] source = { "zero", "one", "two", "three", "four" };

            var result = source.SkipWhile((x, index) => x.Length > index);
            Assert.Equal(new string[] { "four" }, result);
        }

        [Fact]
        public void SkipWhileIndex_MatchingAllElements_EmptyResult()
        {
            string[] source = { "zero", "one", "two", "three", "four" };

            var result = source.SkipWhile((x, index) => x.Length < index + 10);
            Assert.Empty(result);
        }
    }
}
