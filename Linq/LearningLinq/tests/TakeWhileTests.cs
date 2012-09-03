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
    public class TakeWhileTests
    {
        [Fact]
        public void TakeWhileNoIndex_NullSource_ThrowArgumentNullException()
        {
            int[] source = null;

            Assert.Throws<ArgumentNullException>(() => source.TakeWhile(x => x > 0));
        }

        [Fact]
        public void TakeWhileNoIndex_NullPredicate_ThrowsArgumentNullException()
        {
            int[] source = new int[] { 1, 2, 3 };
            Func<int, bool> predicate = null;

            Assert.Throws<ArgumentNullException>(() => source.TakeWhile(predicate));
        }

        [Fact]
        public void TakeWhileNoIndex_FirstElementFailingPredicate_EmptyResult()
        {
            string[] source = { "zero", "one", "two", "three", "four" };

            var result = source.TakeWhile(x => x.Length > 4);
            Assert.Empty(result);
        }

        [Fact]
        public void TakeWhileNoIndex_MatchingFirstThreeElements_NonEmptyResult()
        {
            string[] source = { "zero", "one", "two", "three", "four" };

            var result = source.TakeWhile(x => x.Length < 5);
            Assert.Equal(new string[] {"zero", "one", "two"}, result);
        }

        [Fact]
        public void TakeWhileNoIndex_MatchingAllElements_NonEmptyResult()
        {
            string[] source = { "zero", "one", "two", "three", "four" };

            var result = source.TakeWhile(x => x.Length < 10);
            Assert.Equal(source, result);
        }

        [Fact]
        public void TakeWhileIndex_NullSource_ThrowArgumentNullException()
        {
            int[] source = null;

            Assert.Throws<ArgumentNullException>(() => source.TakeWhile((x, index) => x > index));
        }

        [Fact]
        public void TakeWhileIndex_NullPredicate_ThrowsArgumentNullException()
        {
            int[] source = new int[] { 1, 2, 3 };
            Func<int, int, bool> predicate = null;

            Assert.Throws<ArgumentNullException>(() => source.TakeWhile(predicate));
        }

        [Fact]
        public void TakeWhileIndex_FirstElementFailingPredicate_EmptyResult()
        {
            string[] source = { "zero", "one", "two", "three", "four" };

            var result = source.TakeWhile((x, index) => x.Length < index);
            Assert.Empty(result);
        }

        [Fact]
        public void TakeWhileIndex_MatchingFirstThreeElements_NonEmptyResult()
        {
            string[] source = { "zero", "one", "two", "three", "four" };

            var result = source.TakeWhile((x, index) => x.Length > index);
            Assert.Equal(new string[] { "zero", "one", "two", "three" }, result);
        }

        [Fact]
        public void TakeWhileIndex_MatchingAllElements_NonEmptyResult()
        {
            string[] source = { "zero", "one", "two", "three", "four" };

            var result = source.TakeWhile((x, index) => x.Length < index + 10);
            Assert.Equal(source, result);
        }
    }
}
