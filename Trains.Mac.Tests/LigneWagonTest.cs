using System;
using System.Collections.Generic;
using Xunit;

namespace Trains.Mac.Tests
{
    public class LigneWagonTest
    {
        public LigneWagonTest()
        {
        }

        [Fact]
        public void TestLigneWagonContientPasWagonDestinationZ()
        {
            LigneWagon ligneWagon = new LigneWagon("0000ACDGC");

            var destination = 'Z';

            Assert.Equal(false, ligneWagon.ContientWagonDestination(destination));
        }

        [Fact]
        public void TestLigneWagonContientWagonDestinationG()
        {
            LigneWagon ligneWagon = new LigneWagon("0000ACDGC");

            var destination = 'G';

            Assert.Equal(true, ligneWagon.ContientWagonDestination(destination));
        }

        [Fact]
        public void TestPositionDuWagonDestinationC()
        {
            LigneWagon ligneWagon = new LigneWagon("0000ACDGC");

            var destination = 'C';
            int resultat = 5;

            int retour = ligneWagon.PositionWagonDestination(destination);

            Assert.Equal(retour, resultat);
        }

        [Fact]
        public void TestCanMoveDirectlyWagon()
        {
            LigneWagon ligneWagon1 = new LigneWagon("0000ACDGC");
            LigneWagon ligneWagon2 = new LigneWagon("AVCDACDGC");

            var destination1 = 4; //A
            var destination2 = 5; //C
            var destination3 = 0; //A

            Assert.Equal(true, ligneWagon1.CanMoveDirectly(destination1));
            Assert.Equal(false, ligneWagon1.CanMoveDirectly(destination2));
            Assert.Equal(true, ligneWagon2.CanMoveDirectly(destination3));
        }

        [Fact]
        public void TestViderEspaceRemorqueOccuperWagonA()
        {
            LigneWagon ligneWagon = new LigneWagon("0000ACDGC");

            var destination = 4; //A

            Wagon wagon = ligneWagon.EnleverWagonRemorque(destination);

            Assert.Equal("00000CDGC", ligneWagon.ToString());
            Assert.Equal('A', wagon.Destination);
        }

        [Fact]
        public void TestNbrEspaceVide()
        {
            LigneWagon ligneWagon = new LigneWagon("0000ACDGC");

            Assert.Equal(4, ligneWagon.NbrEspaceLibre);
        }

        [Fact]
        public void TestAjouterWagonTest()
        {
            LigneWagon ligneWagon = new LigneWagon("0000ACDGC");

            Wagon wagon = new Wagon('Z');

            ligneWagon.AjouterWagon(wagon);

            Assert.Equal(ligneWagon.ToString(), "000ZACDGC");
        }

    }
}
