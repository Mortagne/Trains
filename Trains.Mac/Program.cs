using System;
using System.IO;

namespace Trains.Mac
{
    class Program
    {

        private static string[] _gareTriageInitiale = null;
        private static char _choixDestination;


        static void Main(string[] args)
        {
            bool flagExecution = true;
            char choixAction;

            EcrireEntete();

            while (flagExecution)
            {
                choixAction = Char.ToUpper(Console.ReadKey().KeyChar);
                switch (choixAction)
                {
                    case 'Q':
                        flagExecution = false;
                        break;
                    case 'C':
                        ChargementFichier();
                        break;
                    case 'G':
                        OptionG_Traitement();
                        break;
                    default:
                        Console.WriteLine("Option invalide");
                        Console.WriteLine("Veuillez appuyer sur une touche pour continuer ...");
                        Console.ReadKey();
                        Console.Clear();
                        EcrireEntete();
                        break;
                }
            }

            _gareTriageInitiale = null;
        }


        /// <summary>
        /// Ecrires the entete.
        /// Liste des actions possibles
        /// </summary>
        private static void EcrireEntete()
        {
            Console.WriteLine(" #####                                                       #######                               ");
            Console.WriteLine("#     # #   #  ####  ##### ###### #    #    #####  ######       #    #####  #   ##    ####  ###### ");
            Console.WriteLine("#        # #  #        #   #      ##  ##    #    # #            #    #    # #  #  #  #    # #      ");
            Console.WriteLine(" #####    #    ####    #   #####  # ## #    #    # #####        #    #    # # #    # #      #####  ");
            Console.WriteLine("      #   #        #   #   #      #    #    #    # #            #    #####  # ###### #  ### #      ");
            Console.WriteLine("#     #   #   #    #   #   #      #    #    #    # #            #    #   #  # #    # #    # #      ");
            Console.WriteLine(" #####    #    ####    #   ###### #    #    #####  ######       #    #    # # #    #  ####  ###### ");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("---------------------------------------------------------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine("ACTIONS :");
            Console.WriteLine("C - chargement fichier");
            Console.WriteLine("G - générer le plan");
            Console.WriteLine("Q - quitter");
            Console.WriteLine("");
            Console.Write("Votre choix : ");
        }

        /// <summary>
        /// Ecrires the menu.
        /// Réaffichage du menu avec les actions possibles
        /// </summary>
        private static void EcrireMenu()
        {
            Console.WriteLine("");
            Console.WriteLine("ACTIONS :");
            Console.WriteLine("C - chargement fichier");
            Console.WriteLine("G - générer le plan");
            Console.WriteLine("Q - quitter");
            Console.WriteLine("");
            Console.Write("Votre choix : ");
        }
                  
        /// <summary>
        /// Chargements du fichier.
        /// Lecture du fichier si valide
        /// Affichage de la gare de triage
        /// Proposition d'exécution du plan tout de suite
        /// </summary>
        private static void ChargementFichier(){
            Console.WriteLine();
            Console.WriteLine("CHARGEMENT DU FICHIER");
            Console.WriteLine("Veuillez nous indiquer le chemin du fichier de données :");
            string chaine = Console.ReadLine();

            try
            {
                if (chaine.Length > 0 && File.Exists(chaine))
                {
                    _gareTriageInitiale = File.ReadAllLines(chaine);
                }
            }
            catch(Exception ex){
                Console.WriteLine("Le fichier est invalide, Raison[{0}]", ex.Message);
            }

            Console.WriteLine("Voici le plan initiale de la gare de triage :");

            Console.WriteLine("-------------");
            foreach(String s in _gareTriageInitiale){                
                Console.WriteLine(s);
            }
            Console.WriteLine("-------------");

            Console.Write("Désirez-vous générer le plan (Action 'G') maintenant (O/N) ?");

            char choix = Char.ToUpper(Console.ReadKey().KeyChar);
            if(choix.Equals('O')){
                OptionG_Traitement();
            }
            else{
                EcrireMenu();
            }
        }

        /// <summary>
        /// Options the g traitement.
        /// Sequence d'exécution dans le cas de l'option G (Génération du plan)
        /// </summary>
        private static void OptionG_Traitement()
        {
            if (_gareTriageInitiale != null)
            {
                ChoixDestination();
                ExecutionPlanDeTriage();
                _gareTriageInitiale = null;
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Vous devez charger un fichier en premier.");
            }

            Console.WriteLine("Veuillez appuyer sur une touche pour continuer ...");
            Console.ReadKey();
            Console.Clear();
            EcrireEntete();
        }

        /// <summary>
        /// Choixs de la destination.
        /// </summary>
        private static void ChoixDestination(){
            Console.WriteLine();

            bool flagCharValide = false;

            while(!flagCharValide){
                Console.Write("Pour quel destination vous voulez trier la gare ?");
                _choixDestination = Console.ReadKey().KeyChar;

                if (Char.IsLetter(_choixDestination))
                {
                    _choixDestination = Char.ToUpper(_choixDestination);
                    flagCharValide = true;
                }
                else {
                    Console.WriteLine("Caractère invalide {0}...", _choixDestination);
                }
            }

        }

        /// <summary>
        /// Executions du plan de triage.
        /// Appel de "TrainStarter"
        /// </summary>
        private static void ExecutionPlanDeTriage(){
            TrainsStarter trainsStarter = new TrainsStarter();

            string chaineRetour = trainsStarter.Start(_gareTriageInitiale, _choixDestination);

            if(chaineRetour.Length > 0){
                Console.WriteLine();
                Console.WriteLine("Le système a trouver la solution suivante :");
                Console.WriteLine(chaineRetour);
                Console.WriteLine();
            }
            else{
                Console.WriteLine();
                Console.WriteLine("Le système a trouver aucune solution ...");
                Console.WriteLine(chaineRetour);
                Console.WriteLine();
            }
        }

    }
}
