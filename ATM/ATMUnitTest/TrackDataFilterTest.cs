using ATM;
using NUnit.Framework;
using NSubstitute;
using TransponderReceiver;
using System.Collections.Generic;
using System;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ATMUnitTest
{
    [TestFixture]
    
    public class FilterTest
    {
        private TrackDataFilter uut;
        private string dummyTag = "ATR423";
        private int dummyX = 20000;
        private int dummyY = 50000;
        private int dummyAltitude = 2000;
        private DateTime dummyTimestamp = new DateTime(1994, 6, 9, 4, 20, 9);
        [SetUp]
        public void Setup()
        {
            uut = new TrackDataFilter();

        }
        [Test]
        public void FilterTrackRemoveTrackWrongTagTest()
        {
            TrackData dummyTrackDataAway = new TrackData("X", dummyX, dummyY, dummyAltitude, dummyTimestamp);
            List<TrackData> dummyTrackList = new List<TrackData>{ dummyTrackDataAway };
            Assert.IsEmpty(uut.Filter(dummyTrackList));
        }
        [Test]
        public void FilterTrackRemoveTrackWrongXTest()
        {
            TrackData dummyTrackDataAway = new TrackData(dummyTag, 0, dummyY, dummyAltitude, dummyTimestamp);
            List<TrackData> dummyTrackList = new List<TrackData> { dummyTrackDataAway };
            Assert.IsEmpty(uut.Filter(dummyTrackList));
        }
        [Test]
        public void FilterTrackRemoveTrackWrongYTest()
        {
            TrackData dummyTrackDataAway = new TrackData(dummyTag, dummyX, 9483002, dummyAltitude, dummyTimestamp);
            List<TrackData> dummyTrackList = new List<TrackData> { dummyTrackDataAway };
            Assert.IsEmpty(uut.Filter(dummyTrackList));
        }
        [Test]
        public void FilterTrackRemoveTrackWrongZTest()
        {
            TrackData dummyTrackDataAway = new TrackData(dummyTag, dummyX, dummyY, -39, dummyTimestamp);
            List<TrackData> dummyTrackList = new List<TrackData> { dummyTrackDataAway };
            Assert.IsEmpty(uut.Filter(dummyTrackList));
        }
        [Test]
        public void FilterTrackStayTest()
        {
            TrackData dummyTrackDataStay = new TrackData(dummyTag, dummyX, dummyY, dummyAltitude, dummyTimestamp);
            List<TrackData> dummyTrackList = new List<TrackData>{ dummyTrackDataStay };
            Assert.IsNotEmpty(uut.Filter(dummyTrackList));
        }
        [Test]
        public void FilterTrackOneOfTwoStayTest()
        {
            TrackData dummyTrackDataStay = new TrackData(dummyTag, dummyX, dummyY, dummyAltitude, dummyTimestamp);
            TrackData dummyTrackDataAway = new TrackData(dummyTag, 2039495, dummyY, dummyAltitude, dummyTimestamp);
            List<TrackData> dummyTrackList = new List<TrackData> { dummyTrackDataStay, dummyTrackDataAway };
            Assert.AreEqual(1, uut.Filter(dummyTrackList).Count);
        }
        [Test]
        public void FilterTrackTwoOfTwoStayTest()
        {
            TrackData dummyTrackDataStay = new TrackData(dummyTag, dummyX, dummyY, dummyAltitude, dummyTimestamp);
            List<TrackData> dummyTrackList = new List<TrackData> { dummyTrackDataStay, dummyTrackDataStay };
            Assert.AreEqual(2, uut.Filter(dummyTrackList).Count);
        }
    }

}
  //          testData.Add("ATR423;39045;12932;14000;20151006213456789");
    //        testData.Add("BCD123;10005;85890;12000;20151006213456789");
      //      testData.Add("XYZ987;25059;75654;4000;20151006213456789");