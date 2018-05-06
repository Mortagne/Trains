using System;
using System.Collections.Generic;
using Trains.Mac.types;
using Xunit;

namespace Trains.Mac.Tests
{
    public class CoursTriageTest
    {
        public CoursTriageTest()
        {
        }

        [Fact]
        public void TestLectureLigne()
        {
            var ct = new CoursTriage();

            var test = new[] { "0000ACDGC" };

            ct.ajouterLignes(test);

           Assert.Equal(ct.Lignes(), test);

        }

        [Fact]
        public void TestLectureDeuxLignes()
        {
            var ct = new CoursTriage();

            var test = new[] { "0000ACDGC", "0000000DG" };

            ct.ajouterLignes(test);

            Assert.Equal(ct.Lignes(), test);
        }

        [Fact]
        public void TestDestinationExistePas()
        {
            var ct = new CoursTriage();
            var test = new[] { "0000ACDGC", "0000000DG" };
            ct.ajouterLignes(test);

            char destination = 'Z';

            Assert.Equal(ct.WagonDestinationExiste(destination), false);

        }

        [Fact]
        public void TestDestinationExiste()
        {
            var ct = new CoursTriage();
            var test = new[] { "0000ACDGC", "0000000DG" };
            ct.ajouterLignes(test);

            char destination = 'A';

            Assert.Equal(ct.WagonDestinationExiste(destination), true);

        }

        [Fact]
        public void TrouverPositionWagon_A()
        {
            var ct = new CoursTriage();
            var test = new[] { "0000ACDGC", "0000000DG" };
            ct.ajouterLignes(test);

            char destination = 'A';

            Emplacement positionWagon = (Emplacement)ct.TrouverPositionWagonDestination(destination);
            Emplacement validation = new Emplacement(0, 4);

            Assert.Equal(positionWagon, validation);

        }


        [Fact]
        public void CanMoveDirectlyWagon_A()
        {
            var ct = new CoursTriage();
            var test = new[] { "0000ACDGC", "0000000DG" };
            ct.ajouterLignes(test);

            Emplacement emplacement = new Emplacement(0, 4);

            Assert.Equal(ct.CanMoveDirectly(emplacement), true);

        }

        [Fact]
        public void CantMoveDirectlyWagon_C()
        {
            var ct = new CoursTriage();
            var test = new[] { "0000ACDGC", "0000000DG" };
            ct.ajouterLignes(test);

            Emplacement emplacement = new Emplacement(0, 5);
            Assert.Equal(ct.CanMoveDirectly(emplacement), false);
        }


        [Fact]
        public void CanMoveDirectlyWagon_C()
        {
            var ct = new CoursTriage();
            var test = new[] { "0000ACDGC", "0000000DG" };
            ct.ajouterLignes(test);

            Emplacement emplacement = new Emplacement(0, 5);

            Assert.NotEqual(ct.CanMoveDirectly(emplacement), true);

        }

        [Fact]
        public void EnleverWagon_Z_ImpossibleWagonDevant()
        {
            var ct = new CoursTriage();
            var test = new[] { "0000ACDGC", "0000000DZ" };
            var testResultat = new[] { "0000ACDGC", "0000000DZ" };
            ct.ajouterLignes(test);

            Emplacement emplacement = new Emplacement(1, 8);

            Assert.Equal(ct.EnleverWagon(emplacement), false);

            Assert.Equal(ct.Lignes(), testResultat);

        }

        [Fact]
        public void EnleverWagon_Z()
        {
            var ct = new CoursTriage();
            var test = new[] { "0000ACDGC", "00000000Z" };
            var testResultat = new[] { "0000ACDGC", "000000000" };
            ct.ajouterLignes(test);

            Emplacement emplacement = new Emplacement(1, 8);

            Assert.Equal(ct.EnleverWagon(emplacement), true);
            Assert.Equal(ct.Lignes(), testResultat);

        }

        [Fact]
        public void CombienWagonsDevant_Z()
        {
            var ct = new CoursTriage();
            var test = new[] { "0000ACDGZ", "00000DAZB" };
            ct.ajouterLignes(test);

            Emplacement emplacement1 = new Emplacement(0, 8);
            Emplacement emplacement2 = new Emplacement(1, 7);

            Assert.Equal(4, ct.CombiensWagonDevantWagonDestination(emplacement1, 'Z'));
            Assert.Equal(2, ct.CombiensWagonDevantWagonDestination(emplacement2, 'Z'));
        }

        [Fact]
        public void CombienWagonsDevant_surLignePlaine()
        {
            var ct = new CoursTriage();
            var test = new[] { "DGACBDGE" };
            ct.ajouterLignes(test);

            Emplacement emplacement1 = new Emplacement(0, 3);
            Emplacement emplacement2 = new Emplacement(0, 8);

            Assert.Equal(3, ct.CombiensWagonDevantWagonDestination(emplacement1, 'C'));
            Assert.Equal(7, ct.CombiensWagonDevantWagonDestination(emplacement2, 'E'));
        }

        [Fact]
        public void CombienWagonsApres_MemeDestination()
        {
            var ct = new CoursTriage();
            var test = new[] { "0000CCDB" };
            ct.ajouterLignes(test);

            Emplacement emplacement1 = new Emplacement(0, 4);

            Assert.Equal(2, ct.CombiensWagonApresMemeDestination(emplacement1, 'C'));
        }

        [Fact]
        public void CombienWagonsApres_MemeDestinationUnique()
        {
            var ct = new CoursTriage();
            var test = new[] { "0000CDDB" };
            ct.ajouterLignes(test);

            Emplacement emplacement1 = new Emplacement(0, 4);

            Assert.Equal(0, ct.CombiensWagonApresMemeDestination(emplacement1, 'C'));
        }

        [Fact]
        public void CombienWagonsApres_MemeDestination_FULL()
        {
            var ct = new CoursTriage();
            var test = new[] { "0000CCCC" };
            ct.ajouterLignes(test);

            Emplacement emplacement1 = new Emplacement(0, 4);

            Assert.Equal(4, ct.CombiensWagonApresMemeDestination(emplacement1, 'C'));
        }

        [Fact]
        public void LigneAvecPlusDe2EspaceLibres()
        {
            var ct = new CoursTriage();
            var test = new[] { "0000ACDGZF", "ABCDEDAZB", "0BCDEFADF" };
            ct.ajouterLignes(test);


            Assert.Equal(ct.LigneEspacesLibre(2, 2), 0);
        }

        [Fact]
        public void AucuneLigneAvecPlusDe2EspaceLibres()
        {
            var ct = new CoursTriage();
            var test = new[] { "0ABAACDGZF", "ABCDEDAZB", "0BCDEFADF" };
            ct.ajouterLignes(test);


            Assert.Equal(ct.LigneEspacesLibre(2, 2).HasValue, false);
        }

        [Fact]
        public void AjouterUneListeWagonSurLignes()
        {
            var ct = new CoursTriage();
            var test = new[] { "0000ACDGZF", "ABCDEDAZB", "0BCDEFADF" };
            var testResultat = new[] { "00ABACDGZF", "ABCDEDAZB", "0BCDEFADF" };
            ct.ajouterLignes(test);

            Wagon a1 = new Wagon('A');
            Wagon b1 = new Wagon('B');
            List<Wagon> lst = new List<Wagon> { a1, b1 };

            ct.AjouterWagonsDeplacementRemorques(lst, 0);

            Assert.Equal(ct.Lignes(), testResultat);
        }
    }
}
