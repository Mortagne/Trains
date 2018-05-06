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
        public void WagonVide()
        {
            Wagon w = new Wagon();

            w.Destination = '0';

            //var test = new[] { "0000ACDGC" };

            Assert.Equal(w.estVide(), true);
        }

        [Fact]
        public void WagonDestinationA()
        {
            Wagon w = new Wagon();

            w.Destination = 'A';

            //var test = new[] { "0000ACDGC" };

            Assert.Equal(w.estVide(), false);
            Assert.Equal(w.Destination, 'A');
        }

        [Fact]
        public void WagonDestinationC()
        {
            Wagon w = new Wagon();

            w.Destination = 'C';

            //var test = new[] { "0000ACDGC" };

            Assert.NotEqual(w.estVide(), true);
            Assert.NotEqual(w.Destination, 'A');
        }

        [Fact]
        public void WagonAEstDeplacer()
        {
            Wagon w = new Wagon();

            w.Destination = 'A';

            //var test = new[] { "0000ACDGC" };

            Assert.Equal(w.estVide(), false);
            Assert.Equal(w.Destination, 'A');

            w.LibererEspace();

            Assert.Equal(w.estVide(), true);
            Assert.Equal(w.Destination, '0');
        }

        [Fact]
        public void CreationWagonParObjet_quiEstDeplacer()
        {
            Wagon a1 = new Wagon('A');

            Wagon a2 = new Wagon(a1);

            Assert.Equal(a1, a2);

            a1.LibererEspace();

            Assert.NotEqual(a1, a2);

        }
    }
}
