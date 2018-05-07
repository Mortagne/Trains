using System;
using System.Collections.Generic;

namespace Trains.Mac
{
    public class LigneWagon
    {
        private List<Wagon> _ligneWagon;
        private int _nbrEspaceLibre = 0;

        public int NbrEspaceLibre
        {
            get
            {
                return _nbrEspaceLibre;
            }
        }

        public List<Wagon> Ligne
        {
            get
            {
                return _ligneWagon;
            }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="T:Trains.Mac.LigneWagon"/> class.
        /// </summary>
        public LigneWagon()
        {
            _ligneWagon = new List<Wagon>();
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="T:Trains.Mac.LigneWagon"/> class.
        /// </summary>
        /// <param name="pChaineWagons">string represantant une ligne de wagon en gare</param>
        public LigneWagon(string pChaineWagons) : this()
        {
            foreach(char c in pChaineWagons){
                Wagon w = new Wagon(c);

                _ligneWagon.Add(w);

                if (w.estVide()) { 
                    _nbrEspaceLibre++; 
                }
            }
        }


        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:Trains.Mac.LigneWagon"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:Trains.Mac.LigneWagon"/>.</returns>
		public override string ToString()
		{
            var chaineRetour = "";

            foreach(Wagon w in _ligneWagon){
                chaineRetour += w.Destination.ToString();
            }

            return chaineRetour;
		}


        /// <summary>
        /// Est-ce que la ligne contient un Wagon avec la destination "char pDestination"
        /// </summary>
        /// <returns><c>true</c>, if wagon destination was contiented, <c>false</c> otherwise.</returns>
        /// <param name="pDestination">char pDestination.</param>
        public bool ContientWagonDestination(char pDestination)
        {
            Wagon wagonDestination = new Wagon(pDestination);

            return _ligneWagon.Exists((Wagon obj) => obj.Destination.Equals(pDestination));
        }


        /// <summary>
        /// Position du premier Wagon de "pDestination" dans la ligne.
        /// </summary>
        /// <returns>int position</returns>
        /// <param name="pDestination">pDestination</param>
        public int PositionWagonDestination(Char pDestination)
        {
            int lstPosition = -1; //Valeur initiale si non trouver

            int i = 0;
            foreach (Wagon w in _ligneWagon)
            {
                if (w.Destination.Equals(pDestination)){
                    lstPosition = i;
                    break;
                }
                i++;
            }
            return lstPosition;
        }


        /// <summary>
        /// Regarde si la Wagon devant peut-être déplacer directement.
        /// </summary>
        /// <returns><c>true</c>, if move directly was caned, <c>false</c> otherwise.</returns>
        /// <param name="pPositionWagon">pPositionWagon</param>
        public bool CanMoveDirectly(int pPositionWagon)
        {
            Wagon wagonEnAvant;
            if ((pPositionWagon - 1) >= 0)
            {
                wagonEnAvant = _ligneWagon[pPositionWagon - 1];
                return wagonEnAvant.estVide();
            }
            else{
                return true; // nous sommes au début de la ligne, donc peut-être déplacer
            }
        }


        /// <summary>
        /// Enlever un Wagon de la ligne pendant l'étape de la remorque
        /// </summary>
        /// <returns>Wagon enlever</returns>
        /// <param name="pPositionWagon">pPositionWagon</param>
        public Wagon EnleverWagonRemorque(int pPositionWagon)
        {
            Wagon wagon = new Wagon(_ligneWagon[pPositionWagon]);

            _ligneWagon[pPositionWagon].LibererEspace();

            _nbrEspaceLibre++;

            return wagon;
        }


        /// <summary>
        /// Ajouter un object Wagon
        /// </summary>
        /// <param name="pWagon">Wagon</param>
        public void AjouterWagon(Wagon pWagon)
        {
            var nbrWagon = _ligneWagon.Count - 1;

            for (int i = nbrWagon; i >= 0; i--){

                if(_ligneWagon[i].estVide()){
                    _ligneWagon[i].Destination = pWagon.Destination;
                    _nbrEspaceLibre--;
                    break;
                }
            }
        }

	}
}
