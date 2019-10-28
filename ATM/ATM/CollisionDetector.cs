using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    class CollisionDetector
    {
        public bool seperationCheck(TrackData track1, TrackData track2)
        {
            //Returnerer true hvis vi skal raise en seperation condition
            if (Math.Abs(track1.Altitude-track2.Altitude) < 300)
            {
                return true;
                printToFile();
            }
            else if (Math.Sqrt(Math.Abs(track1.Y-track2.Y)+Math.Abs(track1.X-track2.X)) < 5000)
            {
                return true;
                printToFile();
            }
            return false;
            //HUSK at dette skal fjerne den seperation condition vi satte hvis denne tidligere har returneret true!
        }
        private void printToFile()
        {
        //Log hvornår seperation condition skete. Dette skal indeholde tid for sep con. og tag for de pågældende fly
        //Måske den her i virkeligheden skal have sin egen klasse..
        }

    }
}
