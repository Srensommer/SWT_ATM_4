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
        public void FlightCalculatorCreateFlights()
        {
            Dictionary<String, FlightData> flightData = new Dictionary<String, FlightData>();
            TrackData track1 = new TrackData("test1", 10000, 10000, 5500, new DateTime());
            TrackData track2 = new TrackData("test2", 10000, 10000, 5500, new DateTime());

            List<TrackData> trackData = new List<TrackData>{track1, track2};

            flightData =  uut.Calculate(flightData, trackData);
            flightData.TryGetValue("test1", out FlightData result);
            
            Assert.AreEqual(result.CurrentTrackData, track1);
            flightData.TryGetValue("test2", out result);
            Assert.AreEqual(result.CurrentTrackData, track2);
        }

        [Test]
        public void FlightCalculatorCalcSpeed()
        {
            Dictionary<String, FlightData> flightData = new Dictionary<String, FlightData>();
            TrackData track1 = new TrackData("test", 10000, 10000, 5500, new DateTime(2019, 10, 31, 10, 0, 0));
            List<TrackData> trackData = new List<TrackData> { track1 };
            flightData = uut.Calculate(flightData, trackData);
            TrackData track2 = new TrackData("test", 11000, 10000, 5500, new DateTime(2019, 10, 31, 10, 0, 1));
            trackData = new List<TrackData>{track2};
            flightData = uut.Calculate(flightData, trackData);

            flightData.TryGetValue("test", out FlightData result);

            Assert.AreEqual(1000, result.Velocity);
        }

        [Test]
        public void FlightCalculatorCalcDirection()
        {
            Dictionary<String, FlightData> flightData = new Dictionary<String, FlightData>();
            TrackData track1 = new TrackData("test", 10000, 10000, 5500, new DateTime(2019, 10, 31, 10, 0, 0));
            List<TrackData> trackData = new List<TrackData> { track1 };
            flightData = uut.Calculate(flightData, trackData);
            TrackData track2 = new TrackData("test", 11000, 11000, 5500, new DateTime(2019, 10, 31, 10, 0, 1));
            trackData = new List<TrackData> { track2 };
            flightData = uut.Calculate(flightData, trackData);

            flightData.TryGetValue("test", out FlightData result);

            Assert.AreEqual(45, result.CompassCourse);
        }


    }
}