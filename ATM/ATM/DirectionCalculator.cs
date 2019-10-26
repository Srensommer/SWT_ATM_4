using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public class DirectionCalculator
    {
        public double CalculateDirection(TrackData prevData, TrackData currData)
        {
                double direction = Math.Atan2(currData.X - prevData.X, currData.Y - prevData.Y);
                return direction;
        }
    }
}