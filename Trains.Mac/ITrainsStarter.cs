using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trains
{
    public interface ITrainsStarter
    {
        /// <summary>
        /// Cette méthode sera appelée par les tests.
        /// </summary>
        /// <param name="trainLines">Un array de string avec les lignes de trains.</param>
        /// <param name="destination">La lettre de la destination.</param>
        /// <returns>Retourne une liste de mouvements à faire pour obtenir la solution.</returns>
        string Start(string[] trainLines, char destination);
    }
}
