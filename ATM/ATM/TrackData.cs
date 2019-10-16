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
        private readonly string _tag;
        private int _x, _y, _altitude;
        private DateTime _timestamp;

        public TrackData(string tag, int x, int y, int altitude, DateTime timestamp)
        {
            _tag = tag;
            _x = x;
            _y = y;
            _altitude = altitude;
            _timestamp = timestamp;
        }
    }
}
