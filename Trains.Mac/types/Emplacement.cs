using System;
namespace Trains.Mac.types
{
    public struct Emplacement
    {
        private int _ligne, _position;

        public int Ligne
        {
            get
            {
                return _ligne;
            }

            set
            {
                this._ligne = value;
            }
        }

        public int PositionLigne
        {
            get
            {
                return _position;
            }

            set
            {
                this._position = value;
            }
        }

        public Emplacement(int pLigne, int pPosition){
            _ligne = pLigne;
            _position = pPosition;
        }



    }
}
