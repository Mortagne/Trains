using System;
using System.Collections.Generic;
using Trains.Mac.types;

namespace Trains.Mac
{
    public class GareTriageSystem
    {
        private CoursTriage _coursTriage = new CoursTriage();
        private char _destinationFinal;

        private String _lesMouvements = "";


        /// <summary>
        /// Initializes a new instance of the <see cref="T:Trains.Mac.GareTriageSystem"/> class.
        /// </summary>
        /// <param name="pGareLignes">tableau de String contenant le schema de la Gare de départ</param>
        /// <param name="pDestination">charactère de Destination</param>
        public GareTriageSystem(string[] pGareLignes, char pDestination)
        {
            _coursTriage.ajouterLignes(pGareLignes);
            _destinationFinal = pDestination;
        }

        /// <summary>
        /// Coeur principale du system
        /// </summary>
        /// <returns>string, si solution possible, sinon string vide.</returns>
        public string retourSolution()
        {
            try
            {
                // Impossible de faire des déplacements si nous avons qu'une ligne
                if (_coursTriage.CombiensDeLignePossedeGare() < 2)
                    return "";

                // Est que la destination est dans la gare.
                if (!ValiderSiWagonDestinationExiste(_coursTriage, _destinationFinal))
                    return "";

                //Tant qu'il y a des Wagon Destination, effectuer le traitement.
                bool enTraitement = true;
                while (enTraitement){

                    //Trouver le premier Wagon de Destination
                    Emplacement? positionWagonDestination = TrouverPositionWagonDestination(_coursTriage, _destinationFinal);

                    if(positionWagonDestination != null){
                        DemarrageDesDeplacements(_coursTriage, (Emplacement)positionWagonDestination, _destinationFinal);
                    }
                    else{
                        enTraitement = false;
                    }
                }

                return _lesMouvements;
            }
            catch(Exception ex){
                Console.WriteLine();
                Console.WriteLine("Erreur {0}", ex.Message);
                Console.WriteLine();
                return "";
            }

        }


        /// <summary>
        /// Coeur du système pour la gestion des déplacements
        /// </summary>
        /// <param name="pCoursTriage">Cours de Triage</param>
        /// <param name="pEmplacementWagon">Emplacement du premier Wagon de destination</param>
        /// <param name="pCharDestination">Caractère de la destination</param>
        private void DemarrageDesDeplacements(CoursTriage pCoursTriage, Emplacement pEmplacementWagon, char pCharDestination)
        {
            if ( !pCoursTriage.CanMoveDirectly(pEmplacementWagon) )
            {
                //Si je ne peux pas le déplacer le Wagon directement
                effectuerDeplacementRemorque(pCoursTriage, pEmplacementWagon, pCharDestination);
            }
            // déplacement du/des Wagons en ligne final.
            else {
                effectuerDeplacementRemorqueLigneFinal(pCoursTriage, pEmplacementWagon, pCharDestination);
            }
        }

        /// <summary>
        /// On déplace les Wagons pour faire de la place au Wagon Destination
        /// </summary>
        /// <returns><c>true</c>, if deplacement was effectuered, <c>false</c> otherwise.</returns>
        /// <param name="pCoursTriage">Cours de Triage</param>
        /// <param name="pEmplacementWagon">Emplacement du premier Wagon de destination</param>
        /// <param name="pCharDestination">Caractère de la destination</param>
        private void effectuerDeplacementRemorque(CoursTriage pCoursTriage, Emplacement pEmplacementWagon, char pCharDestination)
        {
            int nbrWagonsDevant = pCoursTriage.CombiensWagonDevantWagonDestination(pEmplacementWagon, pCharDestination);

            // Peut-on déplacer les Wagons sur une autre ligne ?
            int? positionLigneDeplacement = pCoursTriage.LigneContientAssezEspacesLibre(pEmplacementWagon.Ligne, nbrWagonsDevant);
            if (positionLigneDeplacement == null)
                throw new Exception("aucune ligne avec des espaces libres");

            //Liste des Wagons déplacés
            List<Wagon> lstWagons = new List<Wagon>();

            for (int i = nbrWagonsDevant; i > 0; i--){
                Emplacement emplacement = new Emplacement(pEmplacementWagon.Ligne, pEmplacementWagon.PositionLigne - i);
                Wagon wagonDeplacement = new Wagon(pCoursTriage.EnleverWagonRemorque(emplacement));
                lstWagons.Add(wagonDeplacement);

                // La remorque ne peut pas avoir plus de 3 wagons à la fois.
                if( lstWagons.Count >= 3){
                    break;
                }
            }

            // Ajout sur la nouvelle lignes les Wagons déplacés.
            pCoursTriage.AjouterWagonsDeplacementRemorques(lstWagons, (int)positionLigneDeplacement);

            // Enregistrement des actions
            EnregistrerMouvementRemorque(lstWagons, pEmplacementWagon.Ligne, (int)positionLigneDeplacement);
        }


        /// <summary>
        /// Effectuer les déplacements vers la ligne de train dans l'étape Remorque
        /// </summary>
        /// <param name="pCoursTriage">Cours de Triage</param>
        /// <param name="pEmplacementWagon">Emplacement du premier Wagon de destination</param>
        /// <param name="pCharDestination">Caractère de la destination</param>
        private void effectuerDeplacementRemorqueLigneFinal(CoursTriage pCoursTriage, Emplacement pEmplacementWagon, char pCharDestination)
        {
            int nbrWagonsMemeDestinationApres = pCoursTriage.CombiensWagonApresMemeDestination(pEmplacementWagon, pCharDestination);

            List<Wagon> lstWagons = new List<Wagon>();

            for (int i = 0; i <= nbrWagonsMemeDestinationApres; i++){
                Emplacement emplacement = new Emplacement(pEmplacementWagon.Ligne, pEmplacementWagon.PositionLigne + i);
                Wagon wagonDeplacement = new Wagon(pCoursTriage.EnleverWagonRemorque(emplacement));
                lstWagons.Add(wagonDeplacement);

                // La remorque ne peut pas avoir plus de 3 wagons à la fois.
                if (lstWagons.Count >= 3){
                    break;
                }
            }

            // on n'a pas besoin d'ajouter les Wagons sur la ligne Finale.

            // Enregistrement des actions
            EnregistrerMouvementRemorqueFinal(lstWagons, pEmplacementWagon.Ligne, 0);
        }


        /// <summary>
        /// Enregistrement des mouvements dans l'étape Remorquage
        /// </summary>
        /// <param name="pListWagon">Liste de Wagon</param>
        /// <param name="pLigneSource">Ligne source</param>
        /// <param name="pLigneDestination">Ligne destination</param>
        private void EnregistrerMouvementRemorque(List<Wagon> pListWagon, int pLigneSource, int pLigneDestination)
        {
            string lstDestinationWagon = "";
            foreach(Wagon w in pListWagon) {
                lstDestinationWagon += w.Destination.ToString();
            }
            if (_lesMouvements.Length == 0)
            {
                _lesMouvements += lstDestinationWagon.ToString() + "," + (pLigneSource + 1).ToString() + "," + (pLigneDestination + 1).ToString();
            }
            else
            {
                _lesMouvements += ";" + lstDestinationWagon.ToString() + "," + (pLigneSource + 1).ToString() + "," + (pLigneDestination + 1).ToString();
            }
        }

        /// <summary>
        /// Enregistrement des mouvements dans l'étape Remorquage vers la ligne final
        /// </summary>
        /// <param name="pListWagon">Liste de Wagon</param>
        /// <param name="pLigneSource">Ligne source</param>
        /// <param name="pLigneDestination">Ligne destination</param>
        private void EnregistrerMouvementRemorqueFinal(List<Wagon> pListWagon, int pLigneSource, int pLigneDestination)
        {
            string lstDestinationWagon = "";
            foreach (Wagon w in pListWagon) {
                lstDestinationWagon += w.Destination.ToString();
            }
            if (_lesMouvements.Length == 0)
            {
                _lesMouvements += lstDestinationWagon.ToString() + "," + (pLigneSource + 1).ToString() + "," + (pLigneDestination).ToString();
            }
            else
            {
                _lesMouvements += ";" + lstDestinationWagon.ToString() + "," + (pLigneSource + 1).ToString() + "," + (pLigneDestination).ToString();
            }
        }

        /// <summary>
        /// Trouver le premier Wagon avec la Destination choisi
        /// </summary>
        /// <returns>Emplacement du Wagon</returns>
        /// <param name="pCoursTriage">Cours de Triage</param>
        /// <param name="pCharDestination">Caractère de Destination</param>
        private Emplacement? TrouverPositionWagonDestination(CoursTriage pCoursTriage, char pCharDestination)
        {
            return pCoursTriage.TrouverPositionWagonDestination(pCharDestination);
        }


        /// <summary>
        /// Valider si la Destination Existe pour l'un des Wagon
        /// </summary>
        /// <returns><c>true</c>, if destination existe, <c>false</c> otherwise.</returns>
        /// <param name="pCoursTriage">Cours de Triage</param>
        /// <param name="pCharDestination">Caractère de Destination</param>
        private bool ValiderSiWagonDestinationExiste(CoursTriage pCoursTriage, char pCharDestination){
            return pCoursTriage.WagonDestinationExiste(pCharDestination);
        }
    }
}
