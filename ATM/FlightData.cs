using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public class FlightData
    {
        public TrackData CurrentTrackData { get; set; }
        public bool CollisionFlag {get; set; }
        public double Velocity { get; set; }
        public double CompassCourse { get; set; }
        public String Tag { get; private set; }

        public FlightData(TrackData trackData)
        {
            Tag = trackData.Tag;
            CurrentTrackData = trackData;
            CollisionFlag = false;
        }

    }
};