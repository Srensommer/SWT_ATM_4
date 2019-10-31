using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public static class DirectionCalculator
    {
        public static double CalculateDirection(TrackData prevData, TrackData currData)
        {
            double directionInRadians = Math.Atan2(currData.X - prevData.X, currData.Y - prevData.Y);
            return RadiansToDegrees(directionInRadians);
        }
        public static double RadiansToDegrees(double radians)
        {
            double degrees = 180 / Math.PI * radians;
            if (degrees<0)
            {
                degrees = degrees+360;
            }
            return degrees;
        }
    }
}