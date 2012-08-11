using System;
using System.Collections.Generic;
using LearningLinq;
using Xunit;

namespace LearningLinqTests
{
    public class UnionTests
    {
        [Fact]
        public void Union_firstIsNull_ThrowsArgumentNullException()
        {
            IEnumerable<string> first = null;
            IEnumerable<string> second = new string[] { };
            Assert.Throws<ArgumentNullException>(() => first.Union(second));
            Assert.Throws<ArgumentNullException>(() => first.Union(second, StringComparer.OrdinalIgnoreCase));
        }
        [Fact]
        public void Union_secondIsNull_ThrowsArgumentNullException()
        {
            IEnumerable<string> first = new string[] { };
            IEnumerable<string> second = null;
            Assert.Throws<ArgumentNullException>(() => first.Union(second));
            Assert.Throws<ArgumentNullException>(() => first.Union(second, StringComparer.OrdinalIgnoreCase));
        }

        [Fact]
        public void Union_TwoFullCollection_ReturnsOneCollectionWithElementNotYetReturned()
        {
            string[] first = { "a", "b", "c", "b" };
            string[] second = { "d", "a", "A", "d", "D" };

            Assert.Equal(new string[] { "a", "b", "c", "d", "A", "D" }, first.Union(second));
            Assert.Equal(new string[] { "a", "b", "c", "d" }, first.Union(second, StringComparer.OrdinalIgnoreCase));
        }

        [Fact]
        public void Union_OneEmptyCollectionOneFilledCollection_ReturnsOneCollectionWithElementNotYetReturned()
        {
            string[] first = { };
            string[] second = { "d", "a", "A", "d", "D" };

            Assert.Equal(new string[] { "d", "a", "A", "D" }, first.Union(second));
            Assert.Equal(new string[] { "d", "a" }, first.Union(second, StringComparer.OrdinalIgnoreCase));
        }
    }
}
