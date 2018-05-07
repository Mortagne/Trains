using System;
using Xunit;

namespace Trains.Mac.Tests
{
    public class WagonTest
    {
        public WagonTest()
        {
        }

        [Fact]
        public void TestWagonVide()
        {
            Wagon w = new Wagon();
            w.Destination = '0';

            Assert.Equal(true, w.estVide());
        }

        [Fact]
        public void TestWagonDestinationA()
        {
            Wagon w = new Wagon();
            w.Destination = 'A';

            Assert.Equal(false, w.estVide());
            Assert.Equal('A', w.Destination);
        }

        [Fact]
        public void TestWagonAEstDeplacer()
        {
            Wagon w = new Wagon();
            w.Destination = 'A';

            Assert.Equal(false, w.estVide());
            Assert.Equal('A', w.Destination);

            w.LibererEspace();

            Assert.Equal(true, w.estVide());
            Assert.Equal('0', w.Destination);
        }

        [Fact]
        public void TestCreationWagonParObjet_quiEstDeplacer()
        {
            Wagon a1 = new Wagon('A');
            Wagon a2 = new Wagon(a1);

            Assert.Equal(a1, a2);

            a1.LibererEspace();

            Assert.NotEqual(a1, a2);
        }
    }
}
