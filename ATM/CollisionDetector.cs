using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public class CollisionDetector : ICollisionDetector
    {
        private List<String> _collisonTagList = new List<string>();
        private string path = "..\\..\\..\\Log.txt";


        public CollisionDetector()
        {
            string createText = "LogFile" + Environment.NewLine + Environment.NewLine;
            File.WriteAllText(path, createText);
        }


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
            string appendText = "Time of occurrence: " + new DateTime() + ", Tags: " + track1.Tag + ", " + track2.Tag + Environment.NewLine;
            File.AppendAllText(path, appendText);
        }

    }
}
