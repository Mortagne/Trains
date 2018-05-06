using System;
using System.Collections.Generic;
using Trains.Mac.types;

namespace Trains.Mac
{
    public class GareTriageSystem
    {
        CoursTriage _coursTriage = new CoursTriage();
        char _destinationFinal;

        String lesMouvements = "";


        public GareTriageSystem(string[] trainLines, char destination)
        {
            _coursTriage.ajouterLignes(trainLines);
            _destinationFinal = destination;
        }

        public string retourSolution()
        {
            try
            {
                if (!ValiderSiWagonDestinationExiste(_coursTriage, _destinationFinal))
                    return "";

                bool enTraitement = true;
                while (enTraitement){
                    Emplacement? positionWagonDestination = TrouverPositionWagonDestination(_coursTriage, _destinationFinal);

                    if(positionWagonDestination != null){
                        effectuerDeplacement(_coursTriage, (Emplacement)positionWagonDestination, _destinationFinal);
                    }
                    else{
                        enTraitement = false;
                    }
                }

                return lesMouvements;
            }
            catch(Exception ex){
                Console.WriteLine(ex.Message);
                return "";
            }

        }

        private void effectuerDeplacement(CoursTriage varCoursTriage, Emplacement varPositionWagonDestination, char varDestinationFinal)
        {
            if ( !varCoursTriage.CanMoveDirectly(varPositionWagonDestination))
            {
                effectuerDeplacementRemorque(varCoursTriage, varPositionWagonDestination, varDestinationFinal);
            }

            if(effectuerDeplacementRemorqueLigneFinal(varCoursTriage, varPositionWagonDestination, varDestinationFinal)){
                throw new Exception("incapable d'enlever le wagon");
            }
        }

        private bool effectuerDeplacementRemorque(CoursTriage varCoursTriage, Emplacement pEmplacementWagon, char pDestinationFinal)
        {
            bool valRetour = false;

            int nbrWagonsDevant = varCoursTriage.CombiensWagonDevantWagonDestination(pEmplacementWagon, pDestinationFinal);

            int? positionLigneDeplacement = varCoursTriage.LigneEspacesLibre(pEmplacementWagon.Ligne, nbrWagonsDevant);

            if (positionLigneDeplacement == null)
                throw new Exception("aucune ligne avec des espaces libres");


            List<Wagon> lstWagons = new List<Wagon>();

            for (int i = nbrWagonsDevant; i > 0; i--)
            {
                Wagon wagonDeplacement = new Wagon(varCoursTriage.EnleverWagonRemorque(pEmplacementWagon.Ligne, pEmplacementWagon.PositionLigne - i));
                lstWagons.Add(wagonDeplacement);

                // La remorque ne peut pas avoir plus de 3 wagons à la fois.
                if( lstWagons.Count >= 3){
                    break;
                }
            }

            varCoursTriage.AjouterWagonsDeplacementRemorques(lstWagons, (int)positionLigneDeplacement);

            EnregistrerMouvementRemorque(lstWagons, pEmplacementWagon.Ligne, positionLigneDeplacement);

            valRetour = true;


            return valRetour;
        }

        private bool effectuerDeplacementRemorqueLigneFinal(CoursTriage varCoursTriage, Emplacement pEmplacementWagon, char pDestinationFinal)
        {
            bool valRetour = false;

            int nbrWagonsMemeDestinationApres = varCoursTriage.CombiensWagonApresMemeDestination(pEmplacementWagon, pDestinationFinal);

            List<Wagon> lstWagons = new List<Wagon>();

            for (int i = 0; i <= nbrWagonsMemeDestinationApres; i++){
                Wagon wagonDeplacement = new Wagon(varCoursTriage.EnleverWagonRemorque(pEmplacementWagon.Ligne, pEmplacementWagon.PositionLigne + i));
                lstWagons.Add(wagonDeplacement);

                // La remorque ne peut pas avoir plus de 3 wagons à la fois.
                if (lstWagons.Count >= 3)
                {
                    break;
                }
            }

            EnregistrerMouvementRemorqueFinal(lstWagons, pEmplacementWagon.Ligne, 0);

            valRetour = true;

            return valRetour;
        }

        private void EnregistrerMouvementRemorque(List<Wagon> lstWagons, int varLigne, int? positionLigneDeplacement)
        {
            string lstDestinationWagon = "";
            foreach(Wagon w in lstWagons){
                if (w != null)
                {
                    lstDestinationWagon += w.Destination.ToString();
                }
            }
            if (lesMouvements.Length == 0)
            {
                lesMouvements += lstDestinationWagon.ToString() + "," + (varLigne + 1).ToString() + "," + (positionLigneDeplacement + 1).ToString();
            }
            else
            {
                lesMouvements += ";" + lstDestinationWagon.ToString() + "," + (varLigne + 1).ToString() + "," + (positionLigneDeplacement + 1).ToString();
            }
        }

        private void EnregistrerMouvementRemorqueFinal(List<Wagon> lstWagons, int varLigne, int? positionLigneDeplacement)
        {
            string lstDestinationWagon = "";
            foreach (Wagon w in lstWagons)
            {
                if (w != null)
                {
                    lstDestinationWagon += w.Destination.ToString();
                }
            }
            if (lesMouvements.Length == 0)
            {
                lesMouvements += lstDestinationWagon.ToString() + "," + (varLigne + 1).ToString() + "," + (positionLigneDeplacement).ToString();
            }
            else
            {
                lesMouvements += ";" + lstDestinationWagon.ToString() + "," + (varLigne + 1).ToString() + "," + (positionLigneDeplacement).ToString();
            }
        }

        private void EnregistrerMouvement(char varDestinationFinal, int ligne)
        {
            if (lesMouvements.Length == 0)
            {
                lesMouvements += varDestinationFinal.ToString() + "," + (ligne + 1).ToString() + ",0";
            }
            else
            {
                lesMouvements += ";" + varDestinationFinal.ToString() + "," + (ligne + 1).ToString() + ",0";
            }
        }



        private Emplacement? TrouverPositionWagonDestination(CoursTriage varCoursTriage, char varDestination)
        {
            return varCoursTriage.TrouverPositionWagonDestination(varDestination);
        }

        private bool ValiderSiWagonDestinationExiste(CoursTriage varCoursTriage, char varDestination){
            return varCoursTriage.WagonDestinationExiste(varDestination);
        }
    }
}
