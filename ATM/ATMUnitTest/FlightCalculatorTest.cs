using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM;
using NUnit.Framework;

namespace ATMUnitTest
{

    [TestFixture]
    class FLightCalculatorTest
    {
        private IFlightCalculator uut;

        [SetUp]
        public void Setup()
        {
            uut = new FlightCalculator();
        }
        [Test]
        public void CollisionDetectorNoCollisionVerticalDistance()
        {
            Dictionary<String, FlightData> flightData = new Dictionary<String, FlightData>();
            List<TrackData> trackData = new List<TrackData>();


            flightData =  uut.Calculate(flightData, trackData);

        }


    }
}