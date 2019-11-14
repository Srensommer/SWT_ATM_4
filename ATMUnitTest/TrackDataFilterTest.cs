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
        //Boundary Value Analysis
        //Lower valid boundaries tested together with higher valid boundaries - Should at always let the filtered track pass
        [TestCase(10000, 10000, 500, 1)]
        [TestCase(90000, 10000, 500, 1)]
        [TestCase(10000, 90000, 500, 1)]
        [TestCase(10000, 10000, 20000, 1)]
        [TestCase(90000, 90000, 500, 1)]
        [TestCase(90000, 10000, 20000, 1)]
        [TestCase(10000, 90000, 20000, 1)]
        [TestCase(90000, 90000, 20000, 1)]
        //Higher invalid boundaries tested with lower valid boundaries - should always filter track away
        [TestCase(90001, 10000, 500, 0)]
        [TestCase(10000, 90001, 500, 0)]
        [TestCase(10000, 10000, 20001, 0)]
        [TestCase(90001, 90000, 500, 0)]
        [TestCase(90001, 10000, 20001, 0)]
        [TestCase(10000, 90001, 20001, 0)]
        [TestCase(90001, 90001, 20000, 0)]
        [TestCase(90001, 90001, 20001, 0)]
        //Lower invalid boundaries tested with higher valid boundaries - should always filter track away
        [TestCase(9999, 9999, 499, 0)]
        [TestCase(90000, 9999, 499, 0)]
        [TestCase(9999, 90000, 499, 0)]
        [TestCase(9999, 9999, 20000, 0)]
        [TestCase(90000, 90000, 499, 0)]
        [TestCase(90000, 9999, 20000, 0)]
        [TestCase(9999, 90000, 20000, 0)]
        //Lower invalid boundaries tested with higher invalid boundaries - should always filter track away
        [TestCase(90001, 9999, 499, 0)]
        [TestCase(9999, 90001, 499, 0)]
        [TestCase(9999, 9999, 20001, 0)]
        [TestCase(90001, 90001, 499, 0)]
        [TestCase(90001, 9999, 20001, 0)]
        [TestCase(9999, 90001, 20001, 0)]
        //A few tests with Lower valid and nominal value - should always let the filtered track pass
        [TestCase(50000, 10000, 500, 1)]
        [TestCase(10000, 50000, 500, 1)]
        [TestCase(10000, 10000, 10250, 1)]
        //A few tests with higher valid and nominal value - should always let the filtered track pass
        [TestCase(50000, 90000, 20000, 1)]
        [TestCase(90000, 50000, 20000, 1)]
        [TestCase(90000, 90000, 10250, 1)]
        public void FilterTracksReturnsRightAmountOfTracksTest(int Xtest, int Ytest, int altitudetest, int expectedAmountOfTracks)
        {
            TrackData dummyTrackData = new TrackData("TAG123", Xtest, Ytest, altitudetest, dummyTimestamp);
            List<TrackData> dummyTrackDataList = new List<TrackData>();
            dummyTrackDataList.Add(dummyTrackData);
            Assert.AreEqual(expectedAmountOfTracks, uut.Filter(dummyTrackDataList).Count);
        }




        [Test]
        public void FilterTrackRemoveTrackWrongTagTest()
        {
            TrackData dummyTrackDataAway = new TrackData("X", dummyX, dummyY, dummyAltitude, dummyTimestamp);
            List<TrackData> dummyTrackList = new List<TrackData>{ dummyTrackDataAway };
            Assert.IsEmpty(uut.Filter(dummyTrackList));
        }
        [Test]
        public void FilterTrackRemoveTrackNegativeXTest()
        {
            TrackData dummyTrackDataAway = new TrackData(dummyTag, -20, dummyY, dummyAltitude, dummyTimestamp);
            List<TrackData> dummyTrackList = new List<TrackData> { dummyTrackDataAway };
            Assert.IsEmpty(uut.Filter(dummyTrackList));
        }
        [Test]
        public void FilterTrackRemoveTrackNegativeYTest()
        {
            TrackData dummyTrackDataAway = new TrackData(dummyTag, dummyX, -23, dummyAltitude, dummyTimestamp);
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