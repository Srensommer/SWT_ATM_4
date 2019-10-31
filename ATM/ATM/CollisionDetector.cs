using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public class CollisionDetector
    {
        private List<String> _collisonTagList = new List<string>();
        public List<String> SeperationCheck(List<TrackData> trackList)
        {
            List<String> collisionList = new List<String>();

            foreach (TrackData track1 in trackList)
            {
                foreach (TrackData track2 in trackList)
                {
                    if (Math.Sqrt(Math.Abs(track1.Y - track2.Y) + Math.Abs(track1.X - track2.X)) < 5000)
                    {
                        if (Math.Abs(track1.Altitude - track2.Altitude) < 300)
                        {
                            if (track1 != track2)
                            {
                                if (!((_collisonTagList.Contains(track1.Tag) && _collisonTagList.Contains(track2.Tag) ||
                                       (collisionList.Contains(track1.Tag) && collisionList.Contains(track2.Tag)))))
                                {
                                    collisionList.Add(track1.Tag);
                                    collisionList.Add(track2.Tag);
                                    printToFile(track1, track2);
                                }
                            }
                        }
                    }
                }
            }

            _collisonTagList = collisionList;
            
            return collisionList;
        }
        private void printToFile(TrackData track1, TrackData track2)
        {
        //Log hvornår seperation condition skete. Dette skal indeholde tid for sep con. og tag for de pågældende fly
        //Måske den her i virkeligheden skal have sin egen klasse..
        }

    }
}
