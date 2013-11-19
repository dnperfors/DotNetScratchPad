using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DotNetTests.Moq
{
    public interface ITestClass
    {
        IEnumerable<int> GetEnumerable();
    }
    public class MoqTests
    {
        [Fact]
        public void Moq_should_return_empty_collection_for_enumerables()
        {
            var mock = new Mock<ITestClass>();

            Assert.NotNull(mock.Object.GetEnumerable());
        }
    }
}
