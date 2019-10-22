using ATM;
using NUnit.Framework;
using NSubstitute;
using TransponderReceiver;
using ATM_Application;
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
        private int dummyX1 = 200000000;
        private int dummyX2 = 2000;
        private int dummyY = 5000;
        private int dummyAltitude = 2000;
        private DateTime dummyTimestamp = new DateTime(1994, 6, 9, 4, 20, 9);
        [SetUp]
        public void Setup()
        {
            uut = new TrackDataFilter();

        }
        [Test]
        public void FilterTrackRemoveTest()
        {
            TrackData dummyTrackDataAway = new TrackData(dummyTag, dummyX1, dummyY, dummyAltitude, dummyTimestamp);
            List<TrackData> ddAway = new List<TrackData>{ dummyTrackDataAway };
            Assert.IsEmpty(uut.Filter(ddAway));
        }

        [Test]
        public void FilterTrackStayTest()
        {
            TrackData dummyTrackDataStay = new TrackData(dummyTag, dummyX2, dummyY, dummyAltitude, dummyTimestamp);
            List<TrackData> ddStay = new List<TrackData>{ dummyTrackDataStay };
            Assert.IsNotEmpty(uut.Filter(ddStay));
        }
        [Test]
        public void FilterTrackOneStayTest()
        {
            TrackData dummyTrackDataStay = new TrackData(dummyTag, dummyX2, dummyY, dummyAltitude, dummyTimestamp);
            List<TrackData> ddStay = new List<TrackData> { dummyTrackDataStay };
            Assert.AreEqual(uut.Filter(ddStay).Count,1);
        }
    }

}
  //          testData.Add("ATR423;39045;12932;14000;20151006213456789");
    //        testData.Add("BCD123;10005;85890;12000;20151006213456789");
      //      testData.Add("XYZ987;25059;75654;4000;20151006213456789");