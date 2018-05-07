using System;
using Xunit;

namespace Trains.Mac.Tests
{
    public class GareTriageSystemTest
    {
        public GareTriageSystemTest()
        {
            
        }

        private GareTriageSystem _gts;

        [Fact]
        public void TestValidationWagonNonDestination_SolutionImpossible(){
            var test = new[] { "0000ACDGC", "0000000DG" };

            _gts = new GareTriageSystem(test, 'Z');

            Assert.Equal("", _gts.retourSolution());
        }

        [Fact]
        public void TestValidationWagonDestination_A()
        {
            var test = new[] { "0000ACDGC", "0000000DG" };

            _gts = new GareTriageSystem(test, 'A');

            String result = _gts.retourSolution();

            Assert.Equal("A,1,0", result);
        }

        [Fact]
        public void TestValidationNombreMinimumDe2LignesDansGare()
        {
            var test = new[] { "0000000DG" };

            _gts = new GareTriageSystem(test, 'D');

            String result = _gts.retourSolution();

            Assert.Equal("", result);
        }

        [Fact]
        public void TestValidationWagonDestination_Z()
        {
            var test = new[] { "0000ACDGC", "0000000DZ" };

            _gts = new GareTriageSystem(test, 'Z');

            String result = _gts.retourSolution();

            Assert.Equal("D,2,1;Z,2,0", result);
        }

        [Fact]
        public void TestExampleCase1()
        {
            var test = new[] { "0000ACDGC", "0000000DG" };

            _gts = new GareTriageSystem(test, 'C');

            var result = _gts.retourSolution();

            Assert.Equal("A,1,2;C,1,0;DG,1,2;C,1,0", result);
        }

        [Fact]
        public void TestExampleCase2()
        {
            var test = new[] { "0000AGCAG", "00DCACGDG" };

            _gts = new GareTriageSystem(test, 'C');

            var result = _gts.retourSolution();

            Assert.Equal("AG,1,2;C,1,0;AGD,2,1;C,2,0;A,2,1;C,2,0", result);
        }

        [Fact]
        public void TestExampleCase3()
        {
            var test = new[] { "0000ACDGC", "0000000DG" };

            _gts = new GareTriageSystem(test, 'G');

            var result = _gts.retourSolution();

            Assert.Equal("ACD,1,2;G,1,0;ACD,2,1;D,2,1;G,2,0", result);
        }

        [Fact]
        public void TestExampleCase4()
        {
            var test = new[] { "0000ACCDGC", "0000000DG" };

            _gts = new GareTriageSystem(test, 'C');

            var result = _gts.retourSolution();

            Assert.Equal("A,1,2;CC,1,0;DG,1,2;C,1,0", result);
        }

        [Fact]
        public void TestExampleCase5()
        {
            var test = new[] { "0000ABCCCCC", "0000000DG" };

            _gts = new GareTriageSystem(test, 'C');

            var result = _gts.retourSolution();

            Assert.Equal("AB,1,2;CCC,1,0;CC,1,0", result);
        }
    }
}
