﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public  class DirectionCalculator : IDirectionCalculator
    {
        public  double CalculateDirection(TrackData prevData, TrackData currData)
        {
            if (currData.Tag == prevData.Tag)
            {
                double directionInRadians = Math.Atan2(currData.X - prevData.X, currData.Y - prevData.Y);
                return RadiansToDegrees(directionInRadians);
            }
            return 0;
        }

        public  double RadiansToDegrees(double radians)
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