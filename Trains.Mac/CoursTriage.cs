using System;
using System.Collections.Generic;
using Trains.Mac.types;

namespace Trains.Mac
{
    public class CoursTriage
    {
        const int NB_LIGNE = 6;

        private List<LigneWagon> _lignesCours;

        //private LigneWagon _lignesSortie;

        public CoursTriage()
        {
            _lignesCours = new List<LigneWagon>();
            //_lignesSortie = new LigneWagon();
            
        }

        public void ajouterLignes(string[] varTableauLignes)
        {
            if (varTableauLignes.Length > NB_LIGNE)
                throw new Exception("Trop de valeur");
            
            foreach(String str in varTableauLignes){
                LigneWagon ligneWagon = new LigneWagon(str);
                _lignesCours.Add(ligneWagon);
            }
        }

        public IEnumerable<string> Lignes()
        {
            List<String> valRetour = new List<string>();

            foreach(LigneWagon lw in _lignesCours){
                if (lw != null)
                {
                    valRetour.Add(lw.ToString());
                }
            }

            return valRetour.ToArray();
        }

        public Emplacement? TrouverPositionWagonDestination(char varDestination)
        {
            Emplacement? emplacement = null;

            int i = 0; //Ligne courante
            foreach (LigneWagon lw in _lignesCours)
            {
                int positionWagon = lw.PositionWagonDestination(varDestination);

                if(positionWagon != -1){
                    emplacement = new Emplacement(i, positionWagon);
                    break;
                }                        
                i++;
            }

            return emplacement;
        }

        public bool EnleverWagon(Emplacement pEmplacementWagon)
        {
            LigneWagon lw = _lignesCours[pEmplacementWagon.Ligne];

            if (pEmplacementWagon.PositionLigne >= 0 && lw.CanMoveDirectly(pEmplacementWagon.PositionLigne))
            {
                lw.EnleverWagon(pEmplacementWagon.PositionLigne);
            }
            else{
                return false;
            }

            return true;
        }

        public Wagon EnleverWagonRemorque(int ligne, int positionWagon)
        {
            LigneWagon lw = _lignesCours[ligne];
            Wagon wagonPourRemorque = null;

            if (positionWagon >= 0 && lw.CanMoveDirectly(positionWagon))
            {
                wagonPourRemorque = new Wagon(lw.EnleverWagonRemorque(positionWagon));
            }
            else
            {
                return wagonPourRemorque;
            }

            return wagonPourRemorque;
        }

        public bool CanMoveDirectly(Emplacement pEmplacement)
        {
            LigneWagon lw = _lignesCours[pEmplacement.Ligne];

            return lw.CanMoveDirectly(pEmplacement.PositionLigne);
        }

        public bool WagonDestinationExiste(char varDestination)
        {
            bool flagRetour = false;

            foreach(LigneWagon lw in _lignesCours){
                if (lw != null)
                {
                    flagRetour = lw.ContientWagonDestination(varDestination);
                }

                if (flagRetour)
                    break;
            }

            return flagRetour;
        }

        public int? LigneEspacesLibre(int varLigne, int nbrWagonsDevant)
        {
            int? posLigneEspaceLibre = null;

            int nbrLigneTotal = _lignesCours.Count;

            for (int i = 0; i < nbrLigneTotal; i++){
                if (i == varLigne)
                    continue;

                if(_lignesCours[i].NbrEspaceLibre >= nbrWagonsDevant){
                    posLigneEspaceLibre = i;
                    break;
                }
            }

            return posLigneEspaceLibre;
        }

        public void AjouterWagonsDeplacementRemorques(List<Wagon> lstWagons, int positionLigneDeplacement)
        {
            LigneWagon ligneWagon = _lignesCours[positionLigneDeplacement];

            var nbrAjoutWagon = lstWagons.Count - 1;

            for (int i = nbrAjoutWagon; i >= 0; i--){
                ligneWagon.AjouterWagon(lstWagons[i]);
                
            }
        }

        public int CombiensWagonDevantWagonDestination(Emplacement pEmplacementWagon, char pDestinationFinal)
        {
            int nbrWagons = 0;

            List<Wagon> lstWagons = _lignesCours[pEmplacementWagon.Ligne].Ligne;

            foreach(Wagon w in lstWagons){
                if( !w.estVide() && !w.Destination.Equals(pDestinationFinal)){
                    nbrWagons++;
                }
                else if (w.Destination.Equals(pDestinationFinal)){
                    break;
                }
            }

            return nbrWagons;
        }

        public int CombiensWagonApresMemeDestination(Emplacement pEmplacementWagon, char pDestinationFinal)
        {
            int nbrWagons = 0;

            List<Wagon> lstWagons = _lignesCours[pEmplacementWagon.Ligne].Ligne;

            for (int i = (pEmplacementWagon.PositionLigne+1); i < lstWagons.Count; i++){
                if(lstWagons[i].Destination.Equals(pDestinationFinal)){
                    nbrWagons++;
                }
                else{
                    break;
                }
            }

            return nbrWagons;
        }
    }
}
