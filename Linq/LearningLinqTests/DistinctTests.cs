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
            IEnumerable<string> source = null;
            Assert.Throws<ArgumentNullException>(() => source.Distinct());
            Assert.Throws<ArgumentNullException>(() => source.Distinct(null));
        }

        [Fact]
        public void Should_return_values_only_once()
        {
            IEnumerable<string> source = new string[] { "a", "b", "b", "c", "d", "a", "c", "c" };
            IEnumerable<string> expected = new string[] { "a", "b", "c", "d" };

            Assert.Equal(expected, source.Distinct());
            Assert.Equal(expected, source.Distinct(null));
        }

        [Fact]
        public void Should_return_values_only_once_according_to_own_equalityComparer()
        {
            IEnumerable<string> source = new string[] { "a", "b", "B", "c", "d", "a", "C", "C" };
            IEnumerable<string> expected = new string[] { "a", "b", "c", "d" };

            Assert.Equal(expected, source.Distinct(StringComparer.OrdinalIgnoreCase));
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
