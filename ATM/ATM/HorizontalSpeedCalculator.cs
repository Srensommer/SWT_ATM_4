﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public class HorizontalSpeedCalculator
    {
        public double CalculateSpeed(TrackData prevData, TrackData currData)
        {
            int diffX = Math.Abs(prevData.X - currData.X);
            int diffY = Math.Abs(prevData.Y - currData.Y);
            var diffTime = Math.Abs((prevData.Timestamp - currData.Timestamp).TotalSeconds);
            double velocity = Math.Sqrt(diffX^2 + diffY^2)/diffTime;
            return velocity;
        }
        //BigBooBoo test - sæt det her i main med et breakpoint, og se at IT NO WORK GOOD
        //var penis = new HorizontalSpeedCalculator();
        //TrackData xD = new TrackData("123TAG", 13000, 54000, 2000, DateTime.Now);
        //System.Threading.Thread.Sleep(5000);
        //TrackData xD2 = new TrackData("123TAG", 13000, 54050, 2000, DateTime.Now);
        //System.Console.WriteLine(penis.CalculateSpeed(xD, xD2));
    }
}