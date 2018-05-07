using System;
using System.Collections.Generic;
using Trains.Mac.types;

namespace Trains.Mac
{
    public class CoursTriage
    {
        private List<LigneWagon> _lignesCours;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Trains.Mac.CoursTriage"/> class.
        /// </summary>
        public CoursTriage()
        {
            _lignesCours = new List<LigneWagon>();
        }

        /// <summary>
        /// Ajouters les lignes de la cours de triage.
        /// </summary>
        /// <param name="pTableauLignes">Les lignes sous forme de tableau de String</param>
        public void ajouterLignes(string[] pTableauLignes)
        {
            foreach(String str in pTableauLignes){
                LigneWagon ligneWagon = new LigneWagon(str);
                _lignesCours.Add(ligneWagon);
            }
        }


        /// <summary>
        /// Retourne les lignes sous forme de Liste de String
        /// </summary>
        /// <returns>The lignes.</returns>
        public IEnumerable<string> LignesFormatString()
        {
            List<String> lstRetour = new List<string>();

            foreach(LigneWagon lw in _lignesCours){
                lstRetour.Add(lw.ToString());
            }

            return lstRetour.ToArray();
        }


        /// <summary>
        /// Trouver la première position du Wagon de destination
        /// </summary>
        /// <returns>Emplacement? du Wagon</returns>
        /// <param name="pDestination">pDestination.</param>
        public Emplacement? TrouverPositionWagonDestination(char pDestination)
        {
            Emplacement? emplacement = null;

            int i = 0; //Ligne courante
            foreach (LigneWagon lw in _lignesCours)
            {
                int positionWagon = lw.PositionWagonDestination(pDestination);

                if(positionWagon != -1){
                    emplacement = new Emplacement(i, positionWagon);
                    break;
                }                        
                i++;
            }

            return emplacement;
        }


        /// <summary>
        /// Enlever une Wagon de la ligne pendant l'étape Remorque
        /// </summary>
        /// <returns>Wagon enlever</returns>
        /// <param name="pEmp">Emplacement du Wagon a enlever</param>
        public Wagon EnleverWagonRemorque(Emplacement pEmp)
        {
            LigneWagon lw = _lignesCours[pEmp.Ligne];
            Wagon wagonPourRemorque = null;

            if (pEmp.PositionLigne >= 0 && lw.CanMoveDirectly(pEmp.PositionLigne))
            {
                wagonPourRemorque = new Wagon(lw.EnleverWagonRemorque(pEmp.PositionLigne));
            }
            else
            {
                return wagonPourRemorque;
            }

            return wagonPourRemorque;
        }


        /// <summary>
        /// Est-ce qu'un Wagon d'une ligne peut-être déplacer directement
        /// </summary>
        /// <returns><c>true</c>, if move directly was caned, <c>false</c> otherwise.</returns>
        /// <param name="pEmplacement">Emplacement du Wagon</param>
        public bool CanMoveDirectly(Emplacement pEmplacement)
        {
            LigneWagon lw = _lignesCours[pEmplacement.Ligne];
            return lw.CanMoveDirectly(pEmplacement.PositionLigne);
        }


        /// <summary>
        /// Est-ce qu'un Wagon avec la Destination pDestination existe dans la gare
        /// </summary>
        /// <returns><c>true</c>, if destination existe, <c>false</c> otherwise.</returns>
        /// <param name="pDestination">char pDestination</param>
        public bool WagonDestinationExiste(char pDestination)
        {
            bool flagRetourTrouver = false;

            foreach (LigneWagon lw in _lignesCours) {
                flagRetourTrouver = lw.ContientWagonDestination(pDestination);

                if (flagRetourTrouver)
                    break;
            }

            return flagRetourTrouver;
        }


        /// <summary>
        /// Retourne la ligne qui a assez d'espaces Libre pour recevoir pNbrWagons
        /// </summary>
        /// <returns>Ligne avec assez d'espace, sinon null</returns>
        /// <param name="pLigneActuel">Ligne de travail actuel</param>
        /// <param name="pNbrWagons">nbr de Wagons a déplacer</param>
        public int? LigneContientAssezEspacesLibre(int pLigneActuel, int pNbrWagons)
        {
            int? posLigneEspaceLibre = null;

            int nbrLigneTotal = _lignesCours.Count;

            for (int i = 0; i < nbrLigneTotal; i++){
                if (i == pLigneActuel)
                    continue;

                if(_lignesCours[i].NbrEspaceLibre >= pNbrWagons){
                    posLigneEspaceLibre = i;
                    break;
                }
            }

            return posLigneEspaceLibre;
        }


        /// <summary>
        /// Ajouter une liste de Wagons déplacer pendant l'étape Remorques.
        /// </summary>
        /// <param name="pLstWagons">Liste de Wagons</param>
        /// <param name="pPositionLigne">Position ligne deplacement.</param>
        public void AjouterWagonsDeplacementRemorques(List<Wagon> pLstWagons, int pPositionLigne)
        {
            LigneWagon ligneWagon = _lignesCours[pPositionLigne];

            var nbrAjoutWagon = pLstWagons.Count - 1;

            for (int i = nbrAjoutWagon; i >= 0; i--){
                ligneWagon.AjouterWagon(pLstWagons[i]);
                
            }
        }


        /// <summary>
        /// Combiens de Wagon sont devant le Wagon de Destination
        /// </summary>
        /// <returns>Nombre de Wagon</returns>
        /// <param name="pEmplacementWagon">Emplacement</param>
        /// <param name="pDestinationFinal">char Destination</param>
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


        /// <summary>
        /// Combiens de Wagon sont après le Wagon de Destination
        /// </summary>
        /// <returns>Nombre de Wagon</returns>
        /// <param name="pEmplacementWagon">Emplacement</param>
        /// <param name="pDestinationFinal">char Destination</param>
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


        /// <summary>
        /// Retourne le nombre de Ligne dans la Gare
        /// </summary>
        /// <returns>nombre de lignes</returns>
        public int CombiensDeLignePossedeGare(){
            return _lignesCours.Count;
        }
    }
}
