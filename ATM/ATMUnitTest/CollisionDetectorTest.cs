using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM;
using NUnit.Framework;
using ATM_Application;

namespace ATMUnitTest
{

    [TestFixture]
    class CollisionDetectorTest
    {
        private CollisionDetector uut;

        [SetUp]
        public void Setup()
        {
            uut = new CollisionDetector();

        }
        [TestCase()]
        public void CollisionDetectorNoCollisionAltitude()
        {
            TrackData dummyTrackData1 = new TrackData("X1", 10000, 10000, 5000, new DateTime());
            TrackData dummyTrackData2 = new TrackData("X2", 10000, 10000, 5300, new DateTime());
            Assert.IsFalse(uut.seperationCheck(dummyTrackData1, dummyTrackData2));
        }

        [TestCase()]
        public void CollisionDetectorCollisionAltitude()
        {
            TrackData dummyTrackData1 = new TrackData("X1", 10000, 10000, 5000, new DateTime());
            TrackData dummyTrackData2 = new TrackData("X2", 10000, 10000, 5200, new DateTime());
            Assert.IsTrue(uut.seperationCheck(dummyTrackData1, dummyTrackData2));
        }

        [TestCase()]
        public void CollisionDetectorNoCollisionHorizontal()
        {
            TrackData dummyTrackData1 = new TrackData("X1", 12500, 12500, 5000, new DateTime());
            TrackData dummyTrackData2 = new TrackData("X2", 10000, 10000, 5000, new DateTime());
            Assert.IsFalse(uut.seperationCheck(dummyTrackData1, dummyTrackData2));
        }

        [TestCase()]
        public void CollisionDetectorCollisionHorizontal()
        {
            TrackData dummyTrackData1 = new TrackData("X1", 12000, 12000, 5000, new DateTime());
            TrackData dummyTrackData2 = new TrackData("X2", 10000, 10000, 5000, new DateTime());
            Assert.IsTrue(uut.seperationCheck(dummyTrackData1, dummyTrackData2));
        }

    }
}