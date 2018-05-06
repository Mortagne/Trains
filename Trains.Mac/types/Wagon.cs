using System;
namespace Trains.Mac
{
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    public class Wagon
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        public Wagon()
        {            
        }

        public Wagon(Char varDestination){
            //_position = varPosition;
            Destination = varDestination;
        }

        public Wagon(Wagon varWagon)
        {
            Destination = varWagon.Destination;
        }

        //private int _position;
        internal Char _destination;

        //public int Position { get => _position; set => _position = value; }
        public char Destination { get => _destination; set => _destination = value; }

        //public void Initialisation()Char varDestination){
        //    //_position = varPosition;
        //    Destination = varDestination;
        //}



        public bool estVide(){
            if (_destination.Equals('0'))
            {
                return true;
            }
            else{
                return false;
            }
        }

        public void LibererEspace()
        {
            _destination = '0';
        }

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
