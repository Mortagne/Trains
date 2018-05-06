using System;
using Xunit;

namespace Trains.Mac.Tests
{
    public class UnitTest
    {
        public UnitTest()
        {
            this._trainStarter = new TrainsStarter();
        }

        private ITrainsStarter _trainStarter;

        [Fact]
        public void TestExampleCase1()
        {
            var test = new[] { "0000ACDGC", "0000000DG" };

            var result = this._trainStarter.Start(test, 'C');

            Assert.Equal("A,1,2;C,1,0;DG,1,2;C,1,0", result);
        }

        [Fact]
        public void TestExampleCase2()
        {
            var test = new[] { "0000AGCAG", "00DCACGDG" };

            var result = this._trainStarter.Start(test, 'C');

            Assert.Equal("AG,1,2;C,1,0;AGD,2,1;C,2,0;A,2,1;C,2,0", result);
        }
    }
}
