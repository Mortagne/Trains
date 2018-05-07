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

           Assert.Equal(test, ct.LignesFormatString());
        }

        [Fact]
        public void TestLectureDeuxLignes()
        {
            var ct = new CoursTriage();

            var test = new[] { "0000ACDGC", "0000000DG" };

            ct.ajouterLignes(test);

            Assert.Equal(test, ct.LignesFormatString());
        }

        [Fact]
        public void TestDestinationExistePas()
        {
            var ct = new CoursTriage();
            var test = new[] { "0000ACDGC", "0000000DG" };
            ct.ajouterLignes(test);

            char destination = 'Z';

            Assert.Equal(false, ct.WagonDestinationExiste(destination));
        }

        [Fact]
        public void TestDestinationExiste()
        {
            var ct = new CoursTriage();
            var test = new[] { "0000ACDGC", "0000000DG" };
            ct.ajouterLignes(test);

            char destination = 'A';

            Assert.Equal(true, ct.WagonDestinationExiste(destination));
        }

        [Fact]
        public void TestTrouverPositionWagon_A()
        {
            var ct = new CoursTriage();
            var test = new[] { "0000ACDGC", "0000000DG" };
            ct.ajouterLignes(test);

            char destination = 'A';

            Emplacement positionWagon = (Emplacement)ct.TrouverPositionWagonDestination(destination);
            Emplacement validation = new Emplacement(0, 4);

            Assert.Equal(validation, positionWagon);
        }


        [Fact]
        public void TestCanMoveDirectlyWagon_A()
        {
            var ct = new CoursTriage();
            var test = new[] { "0000ACDGC", "0000000DG" };
            ct.ajouterLignes(test);

            Emplacement emplacement = new Emplacement(0, 4);

            Assert.Equal(true, ct.CanMoveDirectly(emplacement));
        }

        [Fact]
        public void TestCantMoveDirectlyWagon_C()
        {
            var ct = new CoursTriage();
            var test = new[] { "0000ACDGC", "0000000DG" };
            ct.ajouterLignes(test);

            Emplacement emplacement = new Emplacement(0, 5);
            Assert.Equal(false, ct.CanMoveDirectly(emplacement));
        }

        [Fact]
        public void TestCombienWagonsDevant_Z()
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
        public void TestCombienWagonsDevant_surLignePlaine()
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
        public void TestCombienWagonsApres_MemeDestination()
        {
            var ct = new CoursTriage();
            var test = new[] { "0000CCDB" };
            ct.ajouterLignes(test);

            Emplacement emplacement1 = new Emplacement(0, 4);

            Assert.Equal(1, ct.CombiensWagonApresMemeDestination(emplacement1, 'C'));
        }

        [Fact]
        public void TestCombienWagonsApres_MemeDestinationUnique()
        {
            var ct = new CoursTriage();
            var test = new[] { "0000CDDB" };
            ct.ajouterLignes(test);

            Emplacement emplacement1 = new Emplacement(0, 4);

            Assert.Equal(0, ct.CombiensWagonApresMemeDestination(emplacement1, 'C'));
        }

        [Fact]
        public void TestCombienWagonsApres_MemeDestination_FULL()
        {
            var ct = new CoursTriage();
            var test = new[] { "0000CCCC" };
            ct.ajouterLignes(test);

            Emplacement emplacement1 = new Emplacement(0, 4);

            Assert.Equal(3, ct.CombiensWagonApresMemeDestination(emplacement1, 'C'));
        }

        [Fact]
        public void TestLigneAvecPlusDe2EspaceLibres()
        {
            var ct = new CoursTriage();
            var test = new[] { "0000ACDGZF", "ABCDEDAZB", "0BCDEFADF" };
            ct.ajouterLignes(test);


            Assert.Equal(0, ct.LigneContientAssezEspacesLibre(2, 2));
        }

        [Fact]
        public void TestAucuneLigneAvecPlusDe2EspaceLibres()
        {
            var ct = new CoursTriage();
            var test = new[] { "0ABAACDGZF", "ABCDEDAZB", "0BCDEFADF" };
            ct.ajouterLignes(test);


            Assert.Equal(false, ct.LigneContientAssezEspacesLibre(2, 2).HasValue);
        }

        [Fact]
        public void TestAjouterUneListeWagonSurLignes()
        {
            var ct = new CoursTriage();
            var test = new[] { "0000ACDGZF", "ABCDEDAZB", "0BCDEFADF" };
            var testResultat = new[] { "00ABACDGZF", "ABCDEDAZB", "0BCDEFADF" };
            ct.ajouterLignes(test);

            Wagon a1 = new Wagon('A');
            Wagon b1 = new Wagon('B');
            List<Wagon> lst = new List<Wagon> { a1, b1 };

            ct.AjouterWagonsDeplacementRemorques(lst, 0);

            Assert.Equal(testResultat, ct.LignesFormatString());
        }

        [Fact]
        public void TestNombreDeLignes()
        {
            var ct = new CoursTriage();
            var test = new[] { "0ABAACDGZF", "ABCDEDAZB", "0BCDEFADF" };
            ct.ajouterLignes(test);


            Assert.Equal(3, ct.CombiensDeLignePossedeGare());
        }
    }
}
