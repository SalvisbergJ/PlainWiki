using System;
using Xunit;
using PlainWiki
namespace Tests
{
    public class HomeControllerTest
    {
        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(6)]
        public void MyFirstTheory(int value)
        {
            Assert.True(IsOdd(value));
        }

        bool IsOdd(int value)
        {
            return value % 2 == 1;
        }
    }
}
