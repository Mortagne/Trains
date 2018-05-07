using System;
namespace Trains.Mac
{
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    public class Wagon
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        private Char _destination;

        public char Destination { get => _destination; set => _destination = value; }

        public Wagon()
        {            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Trains.Mac.Wagon"/> class.
        /// </summary>
        /// <param name="varDestination">Variable destination.</param>
        public Wagon(Char varDestination){
            Destination = varDestination;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Trains.Mac.Wagon"/> class.
        /// </summary>
        /// <param name="varWagon">Variable wagon.</param>
        public Wagon(Wagon varWagon)
        {
            Destination = varWagon.Destination;
        }

        /// <summary>
        /// Regarde si la destination du Wagon est vide
        /// </summary>
        /// <returns><c>true</c>, Si destination = '0', <c>false</c> Sinon.</returns>
        public bool estVide(){
            if (_destination.Equals('0'))
            {
                return true;
            }
            else{
                return false;
            }
        }

        /// <summary>
        /// Liberer l'espace
        /// Marquer le Wagon avec une destination de '0'
        /// </summary>
        public void LibererEspace()
        {
            _destination = '0';
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="T:Trains.Mac.Wagon"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="T:Trains.Mac.Wagon"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current <see cref="T:Trains.Mac.Wagon"/>;
        /// otherwise, <c>false</c>.</returns>
        public override bool Equals(Object obj)
        {
            // Check for null values and compare run-time types.
            if (obj == null || GetType() != obj.GetType())
                return false;

            Wagon p = (Wagon)obj;
            return (_destination.Equals(p._destination));
        }
    }
}
