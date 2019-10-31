using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public static class VelocityCalculator
    {
        public static double CalculateSpeed(TrackData prevData, TrackData currData)
        {
            if (currData.Tag == prevData.Tag)
            {
               int diffX = Math.Abs(prevData.X - currData.X);
               int diffY = Math.Abs(prevData.Y - currData.Y);
               var diffTime = Math.Abs((prevData.Timestamp - currData.Timestamp).TotalSeconds);
               if (diffTime != 0)
                {
                    double velocity = Math.Sqrt(Math.Pow(diffX, 2) + Math.Pow(diffY, 2)) / diffTime;
                    return velocity;
                }
            }
            return 0;
            //return 0;
        }
        //BigBooBoo test - sæt det her i main med et breakpoint, og se at IT NO WORK GOOD
        //var penis = new HorizontalSpeedCalculator();
        //TrackData xD = new TrackData("123TAG", 13000, 54000, 2000, DateTime.Now);
        //System.Threading.Thread.Sleep(5000);
        //TrackData xD2 = new TrackData("123TAG", 13000, 54050, 2000, DateTime.Now);
        //System.Console.WriteLine(penis.CalculateSpeed(xD, xD2));
    }
}