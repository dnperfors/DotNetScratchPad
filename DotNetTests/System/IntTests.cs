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
using System.Linq;
using Xunit;

namespace DotNetTests.System
{
    public class IntTests
    {
        [Fact]
        public void Should_overflow_to_minvalue_when_maxvalue_plus_one()
        {
            int value = int.MaxValue;
            Assert.Equal(int.MinValue, value + 1);
        }

        [Fact]
        public void Should_throw_OverflowException_when_maxcalue_plus_one_in_checked_mode()
        {
            Assert.Throws<OverflowException>(() =>
            {
                checked
                {
                    int value = int.MaxValue;
                    value++;
                }
            });
        }

        [Fact]
        public void Working_of_the_plusplus_operator()
        {
            int value = 6;
            Assert.Equal(6, value++);
            Assert.Equal(7, value);
            Assert.Equal(8, ++value);
        }

        [Fact]
        public void Working_of_the_minusminus_operator()
        {
            int value = 6;
            Assert.Equal(6, value--);
            Assert.Equal(5, value);
            Assert.Equal(4, --value);
        }
    }
}
