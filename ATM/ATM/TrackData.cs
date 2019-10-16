using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public class TrackData
    {
        public string Tag { get; }
        public int X { get; }
        public int Y { get; }
        public int Altitude { get; }
        public DateTime Timestamp { get; }



        public TrackData(string tag, int x, int y, int altitude, DateTime timestamp)
        {
            Tag = tag;
            X = x;
            Y = y;
            Altitude = altitude;
            Timestamp = timestamp;
        }
    }
}
