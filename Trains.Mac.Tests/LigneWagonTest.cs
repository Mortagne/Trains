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
        public void LigneWagonVide()
        {
            LigneWagon ligneWagon = new LigneWagon();

            //var test = new[] { "0000ACDGC" };

            Assert.Equal(ligneWagon.estVide(), true);
        }

        [Fact]
        public void LigneWagonNonVide_String_0000ACDGC()
        {
            LigneWagon ligneWagon = new LigneWagon("0000ACDGC");

            var test = "0000ACDGC";

            Assert.Equal(ligneWagon.estVide(), false);
            Assert.Equal(ligneWagon.ToString(), test);
        }

        [Fact]
        public void LigneWagonContientPasWagonDestinationZ()
        {
            LigneWagon ligneWagon = new LigneWagon("0000ACDGC");

            var destination = 'Z';

            Assert.Equal(ligneWagon.ContientWagonDestination(destination), false);
        }

        [Fact]
        public void LigneWagonContientWagonDestinationG()
        {
            LigneWagon ligneWagon = new LigneWagon("0000ACDGC");

            var destination = 'G';

            Assert.Equal(ligneWagon.ContientWagonDestination(destination), true);
        }

        [Fact]
        public void PositionDuWagonDestinationA()
        {
            LigneWagon ligneWagon = new LigneWagon("0000ACDGC");

            var destination = 'A';
            int resultat = 4;

            int retour = ligneWagon.PositionWagonDestination(destination);

            Assert.Equal(retour, resultat);
        }

        [Fact]
        public void PositionDuWagonDestinationC()
        {
            LigneWagon ligneWagon = new LigneWagon("0000ACDGC");

            var destination = 'C';
            int resultat = 5;

            int retour = ligneWagon.PositionWagonDestination(destination);

            Assert.Equal(retour, resultat);
        }

        [Fact]
        public void CanMoveDirectlyWagon()
        {
            LigneWagon ligneWagon1 = new LigneWagon("0000ACDGC");
            LigneWagon ligneWagon2 = new LigneWagon("AVCDACDGC");

            var destination1 = 4;
            var destination2 = 5;
            var destination3 = 0;

            Assert.Equal(true, ligneWagon1.CanMoveDirectly(destination1));
            Assert.Equal(false, ligneWagon1.CanMoveDirectly(destination2));
            Assert.Equal(true, ligneWagon2.CanMoveDirectly(destination3));
        }

        [Fact]
        public void TestStatutWagon()
        {
            LigneWagon ligneWagon = new LigneWagon("0000ACDGC");

            var destination1 = 5;
            var destination2 = 1;

            Assert.Equal(false, ligneWagon.StatutWagon(destination1));
            Assert.Equal(true, ligneWagon.StatutWagon(destination2));
        }

        [Fact]
        public void CanMoveDirectlyWagonC()
        {
            LigneWagon ligneWagon = new LigneWagon("0000ACDGC");

            var destination = 5;

            Assert.NotEqual(ligneWagon.CanMoveDirectly(destination), true);
        }

        [Fact]
        public void ViderEspaceOccuperWagonA()
        {
            LigneWagon ligneWagon = new LigneWagon("0000ACDGC");

            var destination = 4;

            ligneWagon.EnleverWagon(destination);

            Assert.Equal(ligneWagon.ToString(), "00000CDGC");
        }

        [Fact]
        public void ViderEspaceRemorqueOccuperWagonA()
        {
            LigneWagon ligneWagon = new LigneWagon("0000ACDGC");

            var destination = 4;

            Wagon wagon = ligneWagon.EnleverWagonRemorque(destination);

            Assert.Equal(ligneWagon.ToString(), "00000CDGC");
            Assert.Equal(wagon.Destination, 'A');
        }

        [Fact]
        public void NbrEspaceVide()
        {
            LigneWagon ligneWagon = new LigneWagon("0000ACDGC");

            Assert.Equal(ligneWagon.NbrEspaceLibre, 4);
        }

        [Fact]
        public void AjouterWagonTest()
        {
            LigneWagon ligneWagon = new LigneWagon("0000ACDGC");

            Wagon wagon = new Wagon('Z');

            ligneWagon.AjouterWagon(wagon);

            Assert.Equal(ligneWagon.ToString(), "000ZACDGC");

        }

    }
}
