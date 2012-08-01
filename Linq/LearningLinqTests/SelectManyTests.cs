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
    public class SelectManyTests
    {
        [Fact]
        public void Should_throw_ArgumentNullException_when_source_is_null()
        {
            IEnumerable<int> source = null;
            Assert.Throws<ArgumentNullException>(() => source.SelectMany(x => x.ToString().ToCharArray()));
            Assert.Throws<ArgumentNullException>(() => source.SelectMany((x, index) => (x + index).ToString().ToCharArray()));
            Assert.Throws<ArgumentNullException>(() => source.SelectMany(x => x.ToString().ToCharArray(), (x, c) => x + ": " + c));
            Assert.Throws<ArgumentNullException>(() => source.SelectMany((x, index) => (x + index).ToString().ToCharArray(), (x, c) => x + ": " + c));
        }

        [Fact]
        public void Should_throw_ArguementNullException_when_selector_is_null()
        {
            IEnumerable<int> source = new int[] { 3, 5, 20, 15 };

            Assert.Throws<ArgumentNullException>(() => source.SelectMany((Func<int, IEnumerable<char>>)null));
            Assert.Throws<ArgumentNullException>(() => source.SelectMany((Func<int, int, IEnumerable<char>>)null));
            Assert.Throws<ArgumentNullException>(() => source.SelectMany((Func<int, IEnumerable<char>>)null, (x, c) => x + ": " + c));
            Assert.Throws<ArgumentNullException>(() => source.SelectMany((Func<int, int, IEnumerable<char>>)null, (x, c) => x + ": " + c));
        }

        [Fact]
        public void Should_throw_ArgumentNullException_when_resultSelector_is_null()
        {
            IEnumerable<int> source = new int[] { 3, 5, 20, 15 };

            Assert.Throws<ArgumentNullException>(() => source.SelectMany(x => x.ToString().ToCharArray(), (Func<int, char, string>)null));
            Assert.Throws<ArgumentNullException>(() => source.SelectMany((x, index) => (x + index).ToString().ToCharArray(), (Func<int, char, string>)null));
        }

        [Fact]
        public void Should_do_a_simple_select_many()
        {
            IEnumerable<int> source = new int[] { 3, 5, 20, 15 };
            IEnumerable<char> expected = new char[] { '3', '5', '2', '0', '1', '5' };

            Assert.Equal(expected, source.SelectMany(x => x.ToString().ToCharArray()));
        }

        [Fact]
        public void Should_do_a_simple_SelectMany_with_index()
        {
            IEnumerable<int> source = new int[] { 3, 5, 20, 15 };
            IEnumerable<char> expected = new char[] { '3', '6', '2', '2', '1', '8' };

            Assert.Equal(expected, source.SelectMany((x, index) => (x + index).ToString().ToCharArray()));
        }

        [Fact]
        public void Should_flatten_with_projection()
        {
            IEnumerable<int> source = new int[] { 3, 5, 20, 15 };
            IEnumerable<string> expected = new string[] { "3: 3", "5: 5", "20: 2", "20: 0", "15: 1", "15: 5" };

            Assert.Equal(expected, source.SelectMany(x => x.ToString().ToCharArray(), (x, c) => x + ": " + c));
        }

        [Fact]
        public void Should_flatten_with_projection_and_index()
        {
            IEnumerable<int> source = new int[] { 3, 5, 20, 15 };
            IEnumerable<string> expected = new string[] { "3: 3", "5: 6", "20: 2", "20: 2", "15: 1", "15: 8" };

            Assert.Equal(expected, source.SelectMany((x, index) => (x + index).ToString().ToCharArray(), (x, c) => x + ": " + c));
        }
    }
}
