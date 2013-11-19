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
    public class ToListTests
    {
        [Fact]
        public void ToList_NullSource_ThrowsArgumentNullException()
        {
            int[] source = null;

            Assert.Throws<ArgumentNullException>(() => source.ToList());
        }

        [Fact]
        public void ToList_ListSource_ReturnsListWithSameValues()
        {
            List<int> source = new List<int>() { 1, 2, 3 };
            List<int> result = source.ToList<int>();
            Assert.Equal(source, result);
        }

        [Fact]
        public void ToList_ListSource_ReturnsDifferentInstance()
        {
            List<int> source = new List<int>() { 1, 2, 3 };
            List<int> result = source.ToList<int>();
            Assert.NotSame(source, result);
        }

        [Fact]
        public void ToList_EnumerableSourceWithCalculation_WillThrowDivideByZeroExceptionWhenIterating()
        {
            IEnumerable<int> source = new int[] { 2, 1, 0 }.Select(x => 10 / x);
            Assert.Throws<DivideByZeroException>(()=> source.ToList());
        }

        [Fact]
        public void ToList_MockedICollectionSource_ShouldNotCallGetEnumerator()
        {
            var source = new Mock<ICollection<int>>();
            source.Object.ToList();
            source.Verify(x => x.GetEnumerator(), Times.Never());
        }
    }
}
