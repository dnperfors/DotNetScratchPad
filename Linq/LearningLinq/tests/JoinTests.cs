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
    public class JoinTests
    {
        [Fact]
        public void Join_outerSequenceIsNull_ThrowsArgumentNullException()
        {
            int[] outer = null;
            string[] inner = { "bee", "giraffe", "tiger", "badger", "ox", "cat", "dog" };

            Assert.Throws<ArgumentNullException>(() => outer.Join(inner,
                                   outerElement => outerElement,
                                   innerElement => innerElement.Length,
                                   (outerElement, innerElement) => outerElement + ":" + innerElement));
            Assert.Throws<ArgumentNullException>(() => outer.Join(inner,
                                   outerElement => outerElement,
                                   innerElement => innerElement.Length,
                                   (outerElement, innerElement) => outerElement + ":" + innerElement,
                                   EqualityComparer<int>.Default));
        }

        [Fact]
        public void Join_innerSequenceIsNull_ThrowsArgumentNullException()
        {
            int[] outer = { 5, 3, 7 };
            string[] inner = null;

            Assert.Throws<ArgumentNullException>(() => outer.Join(inner,
                       outerElement => outerElement,
                       innerElement => innerElement.Length,
                       (outerElement, innerElement) => outerElement + ":" + innerElement));
            Assert.Throws<ArgumentNullException>(() => outer.Join(inner,
                                   outerElement => outerElement,
                                   innerElement => innerElement.Length,
                                   (outerElement, innerElement) => outerElement + ":" + innerElement,
                                   EqualityComparer<int>.Default));
        }

        [Fact]
        public void Join_outerKeySelectorIsNull_ThrowsArgumentNullException()
        {
            int[] outer = { 5, 3, 7 };
            string[] inner = { "bee", "giraffe", "tiger", "badger", "ox", "cat", "dog" };

            Assert.Throws<ArgumentNullException>(() => outer.Join(inner,
                                   null,
                                   innerElement => innerElement.Length,
                                   (outerElement, innerElement) => outerElement + ":" + innerElement));
            Assert.Throws<ArgumentNullException>(() => outer.Join(inner,
                                   null,
                                   innerElement => innerElement.Length,
                                   (outerElement, innerElement) => outerElement + ":" + innerElement,
                                   EqualityComparer<int>.Default));
        }

        [Fact]
        public void Join_innerKeySelectorIsNull_ThrowsArgumentNullException()
        {
            int[] outer = { 5, 3, 7 };
            string[] inner = { "bee", "giraffe", "tiger", "badger", "ox", "cat", "dog" };

            Assert.Throws<ArgumentNullException>(() => outer.Join(inner,
                                   outerElement => outerElement,
                                   null,
                                   (outerElement, innerElement) => outerElement + ":" + innerElement));
            Assert.Throws<ArgumentNullException>(() => outer.Join(inner,
                                   outerElement => outerElement,
                                   null,
                                   (outerElement, innerElement) => outerElement + ":" + innerElement,
                                   EqualityComparer<int>.Default));
        }

        [Fact]
        public void Join_resultSelectorIsNull_ThrowsArgumentNullException()
        {
            int[] outer = { 5, 3, 7 };
            string[] inner = { "bee", "giraffe", "tiger", "badger", "ox", "cat", "dog" };

            Assert.Throws<ArgumentNullException>(() => outer.Join(inner,
                                   outerElement => outerElement,
                                   innerElement => innerElement.Length,
                                   (Func<int, string, string>)null));
            Assert.Throws<ArgumentNullException>(() => outer.Join(inner,
                                   outerElement => outerElement,
                                   innerElement => innerElement.Length,
                                   (Func<int, string, string>)null,
                                   EqualityComparer<int>.Default));
        }

        [Fact]
        public void Join_SameSourceTypesNoComparers_ReturnCorrectSequence()
        {
            string[] outer = { "aa", "ab", "ac" };
            string[] inner = { "ba", "bb", "bc", "ab" };

            var result = outer.Join(inner,
                                    outerElement => outerElement[1],
                                    innerElement => innerElement[1],
                                    (outerElement, innerElement) => outerElement + innerElement);
            string[] expected = { "aaba", "abbb", "abab", "acbc" };

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Join_SameSourceTypeCustomComparers_ReturnCorrectSequence()
        {
            string[] outer = { "aa", "AB", "ac" };
            string[] inner = { "BA", "bb", "bc", "AB" };

            var result = outer.Join(inner,
                                    outerElement => outerElement[1].ToString(),
                                    innerElement => innerElement[1].ToString(),
                                    (outerElement, innerElement) => outerElement + innerElement,
                                    StringComparer.OrdinalIgnoreCase);
            string[] expected = { "aaBA", "ABbb", "ABAB", "acbc" };

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Join_DifferentSourceTypes_ReturnCorrectSequence()
        {
            int[] outer = { 5, 3, 7 };
            string[] inner = { "bee", "giraffe", "tiger", "badger", "ox", "cat", "dog" };

            var result = outer.Join(inner, outerElement => outerElement, innerElement => innerElement.Length, (outerElement, innerElement) => outerElement + ":" + innerElement);

            Assert.Equal(new string[]{"5:tiger", "3:bee", "3:cat", "3:dog", "7:giraffe"}, result);
        }
    }
}
