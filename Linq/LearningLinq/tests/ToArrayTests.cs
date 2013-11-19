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
using Moq;

namespace LearningLinqTests
{
    public class ToArrayTests
    {
        [Fact]
        public void ToArray_NullSource_ThrowsArgumentNullException()
        {
            int[] source = null;

            Assert.Throws<ArgumentNullException>(() => source.ToArray());
        }

        [Fact]
        public void ToList_ListSource_ReturnsListWithSameValues()
        {
            int[] source = { 1, 2, 3 };
            var result = source.ToArray();
            Assert.Equal(source, result);
        }

        [Fact]
        public void ToArray_ListSource_ReturnsDifferentInstance()
        {
            int[] source = { 1, 2, 3 };
            var result = source.ToArray();
            Assert.NotSame(source, result);
        }

        [Fact]
        public void ToArray_EnumerableSourceWithCalculation_WillThrowDivideByZeroExceptionWhenIterating()
        {
            IEnumerable<int> source = new int[] { 2, 1, 0 }.Select(x => 10 / x);
            Assert.Throws<DivideByZeroException>(() => source.ToArray());
        }

        [Fact]
        public void ToArray_MockedICollectionSource_ShouldNotCallGetEnumerator()
        {
            var source = new Mock<ICollection<int>>();
            source.Object.ToArray();
            source.Verify(x => x.GetEnumerator(), Times.Never());
        }
    }
}
