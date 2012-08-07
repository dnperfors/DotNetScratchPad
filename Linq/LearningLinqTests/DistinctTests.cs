using System;
using System.Collections.Generic;
using LearningLinq;
using Xunit;
using System.Runtime.CompilerServices;

namespace LearningLinqTests
{
    public class DistinctTests
    {
        [Fact]
        public void Should_throw_ArgumentNullException_when_source_is_null()
        {
            IEnumerable<int> source = null;
            Assert.Throws<ArgumentNullException>(() => source.Distinct());
            Assert.Throws<ArgumentNullException>(() => source.Distinct(null));
        }

        [Fact]
        public void Should_return_values_only_once()
        {
            IEnumerable<int> source = new int[] { 21, 46, 46, 55, 17, 21, 55, 55 };
            IEnumerable<int> expected = new int[] { 21, 46, 55, 17 };

            Assert.Equal(expected, source.Distinct());
            Assert.Equal(expected, source.Distinct(null));
        }

        [Fact]
        public void Should_return_values_only_once_according_to_own_equalityComparer()
        {
            IEnumerable<int> source = new int[] { 21, 46, 46, 55, 17, 21, 55, 55 };
            IEnumerable<int> expected = new int[] { 21, 46, 46, 55, 17, 21, 55, 55 };

            Assert.Equal(expected, source.Distinct(new CustomEqualityComparer()));
        }

        private class CustomEqualityComparer : IEqualityComparer<int>
        {
            public bool Equals(int x, int y)
            {
                return object.ReferenceEquals(x, y);
            }

            public int GetHashCode(int obj)
            {
                return RuntimeHelpers.GetHashCode(obj);
            }
        }
    }
}
