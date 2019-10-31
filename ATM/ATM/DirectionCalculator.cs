using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public class DirectionCalculator
    {
        public double CalculateDirection(List<TrackData> prevData, List<TrackData> currData)
        {
            foreach (var currDataTrack in currData)
            {
                foreach (var prevDataTrack in prevData)
                {
                    if (currDataTrack.Tag == prevDataTrack.Tag)
                    {
                        double directionInRadians = Math.Atan2(currDataTrack.X - prevDataTrack.X, currDataTrack.Y - prevDataTrack.Y);
                        return RadiansToDegrees(directionInRadians);
                    }
                    return 0;
                }
            }
            return 0;
        }
        public double RadiansToDegrees(double radians)
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