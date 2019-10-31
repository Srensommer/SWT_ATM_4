using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public  class VelocityCalculator : IVelocityCalculator
    {
        public double CalculateSpeed(TrackData prevData, TrackData currData)
        {
               int diffX = Math.Abs(prevData.X - currData.X);
               int diffY = Math.Abs(prevData.Y - currData.Y);
               var diffTime = Math.Abs((prevData.Timestamp - currData.Timestamp).TotalSeconds);
                if (diffTime != 0)
                {
                   double velocity = Math.Sqrt(Math.Pow(diffX, 2) + Math.Pow(diffY, 2)) / diffTime;
                   return velocity;
                }
               return 0;
        }
        
    }
}