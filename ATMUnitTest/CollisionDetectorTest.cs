using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM;
using NSubstitute;
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
            ILogger iLogger = Substitute.For<ILogger>();
            uut = new CollisionDetector(iLogger);
        }
        [Test]
        public void CollisionDetectorNoCollisionVerticalDistance()
        {
            TrackData dummyTrackData1 = new TrackData("X1", 10000, 10000, 5000, new DateTime());
            TrackData dummyTrackData2 = new TrackData("X2", 10000, 10000, 5400, new DateTime());
            List<TrackData> trackList = new List<TrackData>{dummyTrackData1, dummyTrackData2};
            List<String> testList = new List<string>();
            Tuple<List<string>, List<string>> uutTuple = uut.SeperationCheck(trackList);
            List<string> uutList = uutTuple.Item1;
            Assert.IsEmpty(testList);
        }

        [Test]
        public void CollisionDetectorNoCollisionHorizontalPlane()
        {
            TrackData dummyTrackData1 = new TrackData("X1", 10000, 10000, 5000, new DateTime());
            TrackData dummyTrackData2 = new TrackData("X2", 15000, 10000, 5200, new DateTime());
            List<TrackData> trackList = new List<TrackData> { dummyTrackData1, dummyTrackData2 };
            List<String> testList = new List<string>();
            Tuple<List<string>, List<string>> uutTuple = uut.SeperationCheck(trackList);
            List<string> uutList = uutTuple.Item1;
            Assert.IsEmpty(testList);
        }

        [Test]
        public void CollisionDetectorCollision()
        {
            TrackData dummyTrackData1 = new TrackData("X1", 10000, 10000, 5000, new DateTime());
            TrackData dummyTrackData2 = new TrackData("X2", 11000, 11000, 5200, new DateTime());
            List<TrackData> trackList = new List<TrackData> { dummyTrackData1, dummyTrackData2 };
            List<String> tagList = new List<String> { dummyTrackData1.Tag, dummyTrackData2.Tag };
            Tuple<List<string>, List<string>> uutTuple = uut.SeperationCheck(trackList);
            List<string> uutList = uutTuple.Item1;
            Assert.AreEqual(tagList, uutList);
        }

        [Test]
        public void CollisionDetectorStillCollision()
        {
            TrackData dummyTrackData1 = new TrackData("X1", 10000, 10000, 5000, new DateTime());
            TrackData dummyTrackData2 = new TrackData("X2", 11000, 11000, 5200, new DateTime());
            List<TrackData> trackList = new List<TrackData> { dummyTrackData1, dummyTrackData2 };
            List<String> tagList = new List<String> { dummyTrackData1.Tag, dummyTrackData2.Tag };
            uut.SeperationCheck(trackList);
            dummyTrackData1 = new TrackData("X1", 11000, 11000, 5000, new DateTime());
            dummyTrackData2 = new TrackData("X2", 12000, 12000, 5200, new DateTime());
            Tuple<List<string>, List<string>> uutTuple = uut.SeperationCheck(trackList);
            List<string> uutList = uutTuple.Item1;

            Assert.AreEqual(tagList, uutList);
        }

        [Test]
        public void CollisionDetectorNoLongerCollision()
        {
            TrackData dummyTrackData1 = new TrackData("X1", 10000, 10000, 5000, new DateTime());
            TrackData dummyTrackData2 = new TrackData("X2", 10000, 10000, 5200, new DateTime());
            List<TrackData> trackList = new List<TrackData> { dummyTrackData1, dummyTrackData2 };
            uut.SeperationCheck(trackList);

            dummyTrackData1 = new TrackData("X1", 20000, 10000, 5000, new DateTime());
            dummyTrackData2 = new TrackData("X2", 10000, 10000, 5200, new DateTime());
            trackList = new List<TrackData> { dummyTrackData1, dummyTrackData2 };
            Tuple<List<string>, List<string>> uutTuple = uut.SeperationCheck(trackList);
            List<string> uutList = uutTuple.Item1;
            Assert.IsEmpty(uutList);
        }


        [Test]
        public void CollisionDetectorCollisionAgain()
        {
            TrackData dummyTrackData1 = new TrackData("X1", 10000, 10000, 5000, new DateTime());
            TrackData dummyTrackData2 = new TrackData("X2", 11000, 11000, 5200, new DateTime());
            List<TrackData> trackList = new List<TrackData> { dummyTrackData1, dummyTrackData2 };
            uut.SeperationCheck(trackList);

            dummyTrackData1 = new TrackData("X1", 20000, 10000, 5000, new DateTime());
            dummyTrackData2 = new TrackData("X2", 12000, 11000, 5200, new DateTime());
            trackList = new List<TrackData> { dummyTrackData1, dummyTrackData2 };
            uut.SeperationCheck(trackList);

            dummyTrackData1 = new TrackData("X1", 10000, 10000, 5000, new DateTime());
            dummyTrackData2 = new TrackData("X2", 11000, 11000, 5200, new DateTime());
            trackList = new List<TrackData> { dummyTrackData1, dummyTrackData2 };
            Tuple<List<string>, List<string>> uutTuple = uut.SeperationCheck(trackList);
            List<string> uutList = uutTuple.Item1;
            List<String> tagList = new List<String> { dummyTrackData1.Tag, dummyTrackData2.Tag };
            Assert.AreEqual(tagList, uutList);
        }

        [Test]
        public void GenerateCollisionString()
        {
            TrackData dummyTrackData1 = new TrackData("X1", 10000, 10000, 5000, new DateTime());
            TrackData dummyTrackData2 = new TrackData("X2", 10000, 11000, 5000, new DateTime());
            List<TrackData> trackList = new List<TrackData> { dummyTrackData1, dummyTrackData2 };

            Tuple<List<string>, List<string>> uutTuple = uut.SeperationCheck(trackList);

            Assert.AreEqual(uutTuple.Item1[0], "X1");
            Assert.AreEqual(uutTuple.Item1[1], "X2");

        }

        [Test]
        public void RemoveCollisionString()
        {
            TrackData dummyTrackData1 = new TrackData("X1", 10000, 10000, 5000, new DateTime());
            TrackData dummyTrackData2 = new TrackData("X2", 10000, 11000, 5000, new DateTime());
            List<TrackData> trackList = new List<TrackData> { dummyTrackData1, dummyTrackData2 };
            uut.SeperationCheck(trackList);

            dummyTrackData1 = new TrackData("X1", 1000, 10000, 5000, new DateTime());
            dummyTrackData2 = new TrackData("X2", 10000, 11000, 5000, new DateTime());
            trackList = new List<TrackData> { dummyTrackData1, dummyTrackData2 };
            Tuple<List<string>, List<string>> uutTuple = uut.SeperationCheck(trackList);

            Assert.IsEmpty(uutTuple.Item1);
        }
    }
}