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

        public string Tag => CurrentTrackData.Tag;
        public int X => CurrentTrackData.X;
        public int Y => CurrentTrackData.Y;
        public int Altitude => CurrentTrackData.Altitude;
        public DateTime Timestamp => CurrentTrackData.Timestamp;



        public FlightData(TrackData trackData)
        {
            CurrentTrackData = trackData;
            CollisionFlag = false;
        }

    }
};