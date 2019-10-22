using ATM;
using NUnit.Framework;
using NSubstitute;
using TransponderReceiver;
using ATM_Application;
using System.Collections.Generic;
using System;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMUnitTest
{
    [TestFixture]
    public class FilterTest
    {
        private string dummyTag = "ATR423";
        private int dummyX1 = 200000000;
        private int dummyX2 = 2000;
        private int dummyY = 5000;
        private int dummyAltitude = 2000;
        private DateTime dummyTimestamp = new DateTime(1994, 30, 9, 4, 20, 69);
        TrackData dummyTrackDataAway = new TrackData(dummyTag, dummyX1, dummyY, dummyAltitude, dummyTimestamp);
        TrackData dummyTrackDataStay = new TrackData(dummyTag, dummyX2, dummyY, dummyAltitude, dummyTimestamp);
        List<TrackData> dummyData = new List<TrackData>(new List<TrackData> { dummyTrackDataAway, dummyTrackDataStay });
        List<TrackData> ddAway = new List<TrackData>(new List<TrackData> { dummyTrackDataAway });
        [SetUp]
        public void Setup()
        {
            ITrackDataFilter uut = Substitute.For<TrackDataFilter>();

        }

        [TestCase(ddAway)]
        public void FilterTrackTestRemove(List<TrackData> TestList)
        {
            Assert.IsEmpty(TestList);
        }
    }
}

 //           List<string> testData = new List<string>();
  //          testData.Add("ATR423;39045;12932;14000;20151006213456789");
    //        testData.Add("BCD123;10005;85890;12000;20151006213456789");
      //      testData.Add("XYZ987;25059;75654;4000;20151006213456789");