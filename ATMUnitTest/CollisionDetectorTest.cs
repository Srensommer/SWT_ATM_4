using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM;
using NUnit.Framework;

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
        [Test]
        public void CollisionDetectorNoCollisionVerticalDistance()
        {
            TrackData dummyTrackData1 = new TrackData("X1", 10000, 10000, 5000, new DateTime());
            TrackData dummyTrackData2 = new TrackData("X2", 10000, 10000, 5300, new DateTime());
            List<TrackData> trackList = new List<TrackData>{dummyTrackData1, dummyTrackData2};
            List<String> testList = new List<string>();
            testList = uut.SeperationCheck(trackList);
            Assert.IsEmpty(testList);
        }

        [Test]
        public void CollisionDetectorNoCollisionHorizontalPlane()
        {
            TrackData dummyTrackData1 = new TrackData("X1", 10000, 10000, 5000, new DateTime());
            TrackData dummyTrackData2 = new TrackData("X2", 15000, 10000, 5400, new DateTime());
            List<TrackData> trackList = new List<TrackData> { dummyTrackData1, dummyTrackData2 };
            List<String> testList = new List<string>();
            testList = uut.SeperationCheck(trackList);
            Assert.IsEmpty(testList);
        }

        [Test]
        public void CollisionDetectorCollision()
        {
            TrackData dummyTrackData1 = new TrackData("X1", 10000, 10000, 5000, new DateTime());
            TrackData dummyTrackData2 = new TrackData("X2", 11000, 11000, 5200, new DateTime());
            List<TrackData> trackList = new List<TrackData> { dummyTrackData1, dummyTrackData2 };
            List<String> tagList = new List<String> { dummyTrackData1.Tag, dummyTrackData2.Tag };
            List<String> uutList = uut.SeperationCheck(trackList);
            Assert.AreEqual(tagList, uutList);
        }
    }
}