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
    public class SkipTests
    {
        [Fact]
        public void Skip_NullSource_ThrowsArgumentNullException()
        {
            int[] source = null;

            Assert.Throws<ArgumentNullException>(() => source.Skip(3));
        }

        [Fact]
        public void Skip_CountIs3_ReturnFirst3Elements()
        {
            var source = Enumerable.Range(1, 5);

            var result = source.Skip(3);

            Assert.Equal(new int[] { 4, 5 }, result);
        }

        [Fact]
        public void Skip_Count0_ReturnsEmpty()
        {
            var source = Enumerable.Range(1, 10);
            var result = source.Skip(0);
            Assert.Equal(source, result);
        }

        [Fact]
        public void Skip_CountLessThan0_ReturnsEmpty()
        {
            var source = Enumerable.Range(1, 10);
            var result = source.Skip(-1);
            Assert.Equal(source, result);
        }

        [Fact]
        public void Skip_Count10_ReturnCompleteCollection()
        {
            var source = Enumerable.Range(1, 10);
            var result = source.Skip(10);
            Assert.Empty(result);
        }

        [Fact]
        public void Skip_CountGreaterThanCount_ReturnCompleteCollection()
        {
            var source = Enumerable.Range(1, 10);
            var result = source.Skip(11);
            Assert.Empty(result);
        }
    }
}
