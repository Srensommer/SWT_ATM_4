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

        public List<String> SeperationCheck(List<TrackData> trackList)
        {
            List<String> collisionList = new List<String>();

            foreach (TrackData track1 in trackList)
            {
                foreach (TrackData track2 in trackList)
                {
                    if (Math.Sqrt(Math.Abs(Math.Pow(track1.Y - track2.Y, 2)) + Math.Abs(Math.Pow(track1.X - track2.X, 2))) >= 5000) continue;
                    if (Math.Abs(track1.Altitude - track2.Altitude) >= 300) continue;
                    if (track1 == track2) continue;
                    if (collisionList.Contains(track1.Tag) && collisionList.Contains(track2.Tag)) continue;
                    if (!(_collisonTagList.Contains(track1.Tag) && _collisonTagList.Contains(track2.Tag)))
                    {
                        GenerateConditionString(track1, track2);
                    }
                    
                    collisionList.Add(track1.Tag);
                    collisionList.Add(track2.Tag);
                }
            }

            _collisonTagList = collisionList;
            
            return collisionList;
        }

        private String GenerateConditionString(TrackData track1, TrackData track2)
        {
            string appendText = "Time of occurrence: " + track1.Timestamp + ", Tags: " + track1.Tag + ", " + track2.Tag + Environment.NewLine;
            return appendText;
        }

    }
}
