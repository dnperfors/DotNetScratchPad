using System;
using System.Collections.Generic;
using LearningLinq;
using Xunit;

namespace LearningLinqTests
{
    public class SelectTests
    {
        [Fact]
        public void Should_throw_ArgumentNullException_when_source_is_null()
        {
            IEnumerable<int> source = null;
            Assert.Throws<ArgumentNullException>(() => source.Select(x => x * x));
            Assert.Throws<ArgumentNullException>(() => source.Select((x, y) => x * y));
        }

        [Fact]
        public void Should_throw_ArgumentNullException_when_selector_is_null()
        {
            IEnumerable<int> source = Enumerable.Range(1, 4);
            Assert.Throws<ArgumentNullException>(() => source.Select((Func<int, bool>)null));
            Assert.Throws<ArgumentNullException>(() => source.Select((Func<int, int, bool>)null));
        }

        [Fact]
        public void Should_return_correct_values()
        {
            IEnumerable<int> source = Enumerable.Range(1, 4);
            Assert.Equal(new int[] { 1, 4, 9, 16 }, source.Select(x => x * x));
            Assert.Equal(new int[] { 0, 2, 6, 12 }, source.Select((x, index) => x * index));
        }

        [Fact]
        public void Should_be_able_to_convert_valuetypes()
        {
            IEnumerable<int> source = Enumerable.Range(1, 4);
            Assert.Equal(new string[] { "1", "2", "3", "4" }, source.Select(x => x.ToString()));
            Assert.Equal(new string[] { "1", "2", "3", "4" }, source.Select((x, index) => x.ToString()));
        }
    }
}
