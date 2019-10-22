using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM_Application;

namespace ATM
{
    public class TrackDataFilter : ITrackDataFilter
    {
        private int minX = 0, maxX = 900000;
        private int minY = 0, maxY = 900000;
        private int minZ = 0, maxZ = 200000;

        public List<TrackData> Filter(List<TrackData> data)
        {
            List<TrackData> tempTracks = new List<TrackData>();
            foreach (TrackData element in data)
            {
                if (minX < element.X && maxX > element.X)
                {
                    if (minY < element.Y && maxY > element.Y)
                    {
                        if (minZ < element.Altitude && maxZ > element.Altitude)
                        {
                            tempTracks.Add(element);
                        }
                    }
                }
                else
                {
                    break;
                }
            }
            return tempTracks;
        }
    }
}
