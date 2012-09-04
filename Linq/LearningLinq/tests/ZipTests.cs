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
    public class ZipTests
    {
        [Fact]
        public void Zip_NullFirst_ThrowArgumentNullException()
        {
            int[] first = null;
            int[] second = { };
            Func<int, int, int> resultSelector = (x, y) => x + y;

            Assert.Throws<ArgumentNullException>(() => first.Zip(second, resultSelector));
        }

        [Fact]
        public void Zip_NullSecond_ThrowArgumentNullException()
        {
            int[] first = { };
            int[] second = null;
            Func<int, int, int> resultSelector = (x, y) => x + y;

            Assert.Throws<ArgumentNullException>(() => first.Zip(second, resultSelector));
        }

        [Fact]
        public void Zip_NullResultSelector_ThrowArgumentNullException()
        {
            int[] first = { };
            int[] second = { };
            Func<int, int, int> resultSelector = null;

            Assert.Throws<ArgumentNullException>(() => first.Zip(second, resultSelector));
        }

        [Fact]
        public void Zip_SameFirstAndSecond_ReturnCorrectResult()
        {
            int[] source = { 1, 2, 3, 4 };
            Func<int, int, int> resultSelector = (x, y) => x + y;

            int[] expectedResult = { 2, 4, 6, 8 };
            var result = source.Zip(source, resultSelector);
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Zip_FirstIsSmallerThanSecond_ReturnAllElementsOfFirst()
        {
            int[] first = { 3, 2, 1 };
            int[] second = { 3, 2, 1, 0 };
            var result = first.Zip(second, (x, y) => x / y);
            Assert.Equal(new int[] { 1, 1, 1 }, result);
        }

        [Fact]
        public void Zip_SecondSmallerThanFirst_ReturnAllElementsOfSecond()
        {
            int[] first = { 3, 2, 1, 0 };
            int[] second = { 3, 2, 1 };
            var result = first.Zip(second, (x, y) => y / x);
            Assert.Equal(new int[] { 1, 1, 1 }, result);
        }
    }
}
