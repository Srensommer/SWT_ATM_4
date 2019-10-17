using ATM;
using NUnit.Framework;
using NSubstitute;
using TransponderReceiver;
using ATM_Application;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMUnitTest
{
    [TestFixture]
    class FilterTest
    {
        //Dummy data fra Franks pdf.
        private ITrackDataFilter _trackDataFilter;
        private string dummyTag = "ATR423";
        private int dummyX1 = 200000000;
        private int dummyX2 = 2000;
        private int dummyY = 5000;
        private int dummyAltitude = 2000;
        private DateTime dummyTimestamp = new DateTime(1994, 30, 9, 4, 20, 69);

        [SetUp]
        public void Setup()
        {
            _trackDataFilter = new TrackDataFilter();
            TrackData dummyTrackDataAway = new TrackData(dummyTag, dummyX1, dummyY, dummyAltitude, dummyTimestamp);
            TrackData dummyTrackDataStay = new TrackData(dummyTag, dummyX2, dummyY, dummyAltitude, dummyTimestamp);
            List<TrackData> dummyData = new List<TrackData>(new List<TrackData>{ dummyTrackDataAway, dummyTrackDataStay });
        }
        [TestCase()]
        public void FilterTrackAwayTest()
        {
            //Vis at filteret filtrerer dummyTrackDataAway væk
        }
        public void FilterTrackStaysTest()
        {
            //Vis at filteret lader dummyTrackDataStay være
        }
    }
}