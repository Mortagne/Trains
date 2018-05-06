using System;
using System.Collections.Generic;

namespace Trains.Mac
{
    public class LigneWagon
    {
        public LigneWagon()
        {
            _ligneWagon = new List<Wagon>();
        }

        public LigneWagon(string varChaine) : this()
        {
            foreach(char c in varChaine){
                Wagon w = new Wagon(c);
                _ligneWagon.Add(w);

                if (w.estVide()) _nbrEspaceLibre++;
            }
        }

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

        public bool estVide(){
            return _ligneWagon.Count != 0 ? false : true;
        }

		public override string ToString()
		{
            var chaineRetour = "";

            foreach(Wagon w in _ligneWagon){
                chaineRetour += w.Destination.ToString();
            }

            return chaineRetour;
		}

        public bool ContientWagonDestination(char paramDestination)
        {
            Wagon wagonDestination = new Wagon(paramDestination);

            return _ligneWagon.Exists((Wagon obj) => obj.Destination.Equals(paramDestination));
        }

        public int PositionWagonDestination(Char varDestination)
        {
            int lstPosition = -1;

            int i = 0;
            foreach (Wagon w in _ligneWagon)
            {
                if (w.Destination.Equals(varDestination)){
                    lstPosition = i;
                    break;
                }
                i++;
            }
            return lstPosition;
        }

        public void EnleverWagon(int positionWagon)
        {
            Wagon wagon = _ligneWagon[positionWagon];

            wagon.LibererEspace();

            _nbrEspaceLibre++;
        }

        public bool CanMoveDirectly(int positionWagon)
        {

            Wagon wagonEnAvant;
            if ((positionWagon - 1) >= 0)
            {
                wagonEnAvant = _ligneWagon[positionWagon - 1];
                return wagonEnAvant.estVide();
            }
            else{
                return true; // nous sommes au début de la ligne, donc peut-être déplacer
            }
        }

        public bool StatutWagon(int positionWagon)
        {
            Wagon wagon = _ligneWagon[positionWagon];

            return wagon.estVide();
        }

        public Wagon EnleverWagonRemorque(int positionWagon)
        {
            Wagon wagon = new Wagon(_ligneWagon[positionWagon]);

            _ligneWagon[positionWagon].LibererEspace();

            _nbrEspaceLibre++;

            return wagon;
        }

        public void AjouterWagon(Wagon wagon)
        {
            var nbrWagon = _ligneWagon.Count - 1;

            for (int i = nbrWagon; i >= 0; i--){

                if(_ligneWagon[i].estVide()){
                    _ligneWagon[i].Destination = wagon.Destination;
                    _nbrEspaceLibre--;
                    break;
                }
            }
        }

	}
}
