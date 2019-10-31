using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM;

namespace ATM
{
    public class TrackDataFilter : ITrackDataFilter
    {
        private int minX = 10000, maxX = 90000;
        private int minY = 10000, maxY = 90000;
        private int minZ = 500, maxZ = 20000;

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
                            if (element.Tag.Count() == 6)
                            {
                                tempTracks.Add(element);
                            }
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
