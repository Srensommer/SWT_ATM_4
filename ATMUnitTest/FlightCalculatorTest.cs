using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM;
using NSubstitute.Core.DependencyInjection;
using NUnit.Framework;
using NSubstitute;


namespace ATMUnitTest
{

    [TestFixture]
    class FlightCalculatorTest
    {
        private IFlightCalculator uut;

        private IVelocityCalculator _fakeVelocityCalculator;
        private IDirectionCalculator _fakeDirectionCalculator;
        private ICollisionDetector _fakeCollisionDetector;

        [SetUp]
        public void Setup()
        {
            _fakeVelocityCalculator = Substitute.For<IVelocityCalculator>();
            _fakeDirectionCalculator = Substitute.For<IDirectionCalculator>();
            _fakeCollisionDetector = Substitute.For<ICollisionDetector>();

            _fakeVelocityCalculator.CalculateSpeed(Arg.Any<TrackData>(), Arg.Any<TrackData>()).Returns(1000);
            _fakeDirectionCalculator.CalculateDirection(Arg.Any<TrackData>(), Arg.Any<TrackData>()).Returns(45);
            _fakeCollisionDetector.SeperationCheck(Arg.Any<List<TrackData>>()).Returns(new List<String>());


            uut = new FlightCalculator(_fakeVelocityCalculator, _fakeDirectionCalculator, _fakeCollisionDetector);
        }


        [Test]
        public void FlightCalculatorCreateFlights()
        {
            Dictionary<String, FlightData> flightData = new Dictionary<String, FlightData>();
            TrackData track1 = new TrackData("test1", 10000, 10000, 5500, new DateTime());
            TrackData track2 = new TrackData("test2", 10000, 10000, 5500, new DateTime());

            List<TrackData> trackData = new List<TrackData>{track1, track2};

            uut.Calculate(flightData, trackData);
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
            TrackData track2 = new TrackData("test", 11000, 10000, 5500, new DateTime(2019, 10, 31, 10, 0, 1));
            
            List<TrackData> trackData = new List<TrackData> { track1 };
            uut.Calculate(flightData, trackData);
            trackData = new List<TrackData>{track2};
            uut.Calculate(flightData, trackData);

            flightData.TryGetValue("test", out FlightData result);

            Assert.AreEqual(1000, result.Velocity);
            _fakeVelocityCalculator.Received().CalculateSpeed(track1, track2);
        }

        [Test]
        public void FlightCalculatorCalcDirection()
        {
            Dictionary<String, FlightData> flightData = new Dictionary<String, FlightData>();
            TrackData track1 = new TrackData("test", 10000, 10000, 5500, new DateTime(2019, 10, 31, 10, 0, 0));
            List<TrackData> trackData = new List<TrackData> { track1 };
            uut.Calculate(flightData, trackData);
            TrackData track2 = new TrackData("test", 11000, 11000, 5500, new DateTime(2019, 10, 31, 10, 0, 1));
            trackData = new List<TrackData> { track2 };
            uut.Calculate(flightData, trackData);

            flightData.TryGetValue("test", out FlightData result);

            Assert.AreEqual(45, result.CompassCourse);
            _fakeDirectionCalculator.Received().CalculateDirection(track1, track2);
        }

        [Test]
        public void FlightCalculatorCollision()
        {
            Dictionary<String, FlightData> flightData = new Dictionary<String, FlightData>();
            TrackData track1 = new TrackData("test", 10000, 10000, 5500, new DateTime(2019, 10, 31, 10, 0, 0));
            TrackData track2 = new TrackData("test", 11000, 11000, 5500, new DateTime(2019, 10, 31, 10, 0, 1));

            List<TrackData> trackData = new List<TrackData> { track1, track2 };
            List<String> result = new List<string> {track1.Tag, track2.Tag};

            _fakeCollisionDetector.SeperationCheck(Arg.Any<List<TrackData>>()).Returns(result);

            uut.Calculate(flightData, trackData);

            _fakeCollisionDetector.Received().SeperationCheck(trackData);

            foreach (KeyValuePair<String, FlightData> entry in flightData)
            {
                Assert.IsTrue(entry.Value.CollisionFlag);
            }
        }


        [Test]
        public void FlightCalculatorNoCollision()
        {
            Dictionary<String, FlightData> flightData = new Dictionary<String, FlightData>();
            TrackData track1 = new TrackData("test", 10000, 10000, 5500, new DateTime(2019, 10, 31, 10, 0, 0));
            TrackData track2 = new TrackData("test", 11000, 11000, 5500, new DateTime(2019, 10, 31, 10, 0, 1));

            List<TrackData> trackData = new List<TrackData> { track1, track2 };
            List<String> result = new List<string> { track1.Tag, track2.Tag };

            _fakeCollisionDetector.SeperationCheck(Arg.Any<List<TrackData>>()).Returns(new List<string>());

            uut.Calculate(flightData, trackData);

            _fakeCollisionDetector.Received().SeperationCheck(trackData);

            foreach (KeyValuePair<String, FlightData> entry in flightData)
            {
                Assert.IsFalse(entry.Value.CollisionFlag);
            }
        }
    }
}