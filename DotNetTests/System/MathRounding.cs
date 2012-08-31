using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace DotNetTests.System
{
    public class MathRounding
    {
        [Fact]
        public void Should_round_correctly()
        {
            Assert.Equal(2, Math.Round(2.5m, 0, MidpointRounding.ToEven));
            Assert.Equal(4, Math.Round(3.5m, 0, MidpointRounding.ToEven));

            Assert.Equal(2, decimal.Round(2.5m, 0));
            Assert.Equal(3, decimal.Round(2.6m, 0));
            Assert.Equal(3, decimal.Round(2.51m, 0));

            Assert.Equal(3, Math.Round(2.5m, 0, MidpointRounding.AwayFromZero));
            Assert.Equal(4, Math.Round(3.5m, 0, MidpointRounding.AwayFromZero));
        }
    }
}
