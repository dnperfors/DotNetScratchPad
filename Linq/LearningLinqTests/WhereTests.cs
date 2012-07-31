using System;
using System.Collections.Generic;
using LearningLinq;
using Xunit;

namespace LearningLinqTests
{
    public class WhereTests
    {
        [Fact]
        public void Should_throw_ArgumentNullException_when_source_is_null()
        {
            IEnumerable<int> source = null;
            Assert.Throws<ArgumentNullException>(() => source.Where(x => x < 4));
            Assert.Throws<ArgumentNullException>(() => source.Where((x, index) => x < 4));
        }

        [Fact]
        public void Should_throw_ArgumentNullException_when_predicate_is_null()
        {
            IEnumerable<int> source = new int[] { 2, 5, 3, 7, 1, 4 };
            Assert.Throws<ArgumentNullException>(() => source.Where((Func<int, bool>)null));
            Assert.Throws<ArgumentNullException>(() => source.Where((Func<int, int, bool>)null));
        }

        [Fact]
        public void Should_return_correct_result()
        {
            IEnumerable<int> source = new int[] { 2, 5, 3, 7, 1, 4 };
            Assert.Equal(new int[] { 2, 3, 1 }, source.Where(x => x < 4));
            Assert.Equal(new int[] { 2, 3, 1 }, source.Where((x, index) => x < 4));
        }

        [Fact]
        public void Should_give_index_to_predicate()
        {
            IEnumerable<int> source = new int[] { 2, 5, 3, 7, 1, 4 };

            Assert.Equal(new int[] { 1, 4 }, source.Where((x, index) => x < index));
        }
    }
}
