using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public class VelocityCalculator
    {
        public List<TrackData> CalculateSpeed(List<TrackData> prevData, List<TrackData> currData)
        {
            foreach (var currDataTrack in currData)
            {
                foreach (var prevDataTrack in prevData)
                {
                    if (currDataTrack.Tag == prevDataTrack.Tag)
                    {
                        var TrackWVelocity = new List<TrackData> { };
                        int diffX = Math.Abs(prevDataTrack.X - currDataTrack.X);
                        int diffY = Math.Abs(prevDataTrack.Y - currDataTrack.Y);
                        var diffTime = Math.Abs((prevDataTrack.Timestamp - currDataTrack.Timestamp).TotalSeconds);
                        if (diffTime != 0)
                        {
                            double velocity = Math.Sqrt(Math.Pow(diffX, 2) + Math.Pow(diffY, 2)) / diffTime;
                            currDataTrack.Velocity = velocity;
                            //TrackWVelocity.Add();
                        }
                        //return 0;
                    }
                }
            }

            return prevData;
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