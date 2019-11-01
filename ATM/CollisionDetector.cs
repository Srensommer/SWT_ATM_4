using System;
using System.CodeDom;
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
        List<string> collisionDisplayList = new List<string>();

        private readonly ILogger _logger;

        public CollisionDetector()
        {
            _logger = new Logger();
        }

        public Tuple<List<String>, List<String>> SeperationCheck(List<TrackData> trackList)
        {
            List<string> collisionList = new List<string>();

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
                        string collisionString = GenerateCollisionString(track1, track2);
                        collisionDisplayList.Add(collisionString);
                        _logger.PrintToFile(collisionString);
                    }
                    
                    collisionList.Add(track1.Tag);
                    collisionList.Add(track2.Tag);
                }
            }
            _collisonTagList = collisionList;

            RemoveCollisionStrings(_collisonTagList, collisionDisplayList);

            return Tuple.Create(_collisonTagList, collisionDisplayList);
        }

        private String GenerateCollisionString(TrackData track1, TrackData track2)
        {
            string appendText = "Time of occurrence: " + track1.Timestamp + ", Tags: " + track1.Tag + ", " + track2.Tag + Environment.NewLine;
            return appendText;
        }

        private void RemoveCollisionStrings(List<string> collisionsTags, List<string> collisionStringList)
        {
            List<string> newCollisionDisplayList = new List<string>();

            foreach (string collision in collisionDisplayList)
            {
                foreach (string tag in collisionsTags)
                {
                    if (collision.Contains(tag))
                    {
                        newCollisionDisplayList.Add(collision);
                    }
                }
            }
            collisionDisplayList = newCollisionDisplayList;
        }
    }
}
