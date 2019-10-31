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
    public class VelocityCalculatorTest
    {
        private VelocityCalculator uut;
        private string dummyTag = "ATR423";
        private int dummyX = 20000;
        private int dummyX2 = 20010;
        private int dummyY = 50000;
        private int dummyAltitude = 2000;
        private DateTime dummyTimestamp = new DateTime(1969, 6, 9, 4, 20, 8);
        private DateTime dummyTimestamp2 = new DateTime(1969, 6, 9, 4, 20, 9);
        private DateTime dummyTimestamp3 = new DateTime(1969, 6, 9, 4, 20, 13);
        [SetUp]
        public void Setup()
        {
            uut = new VelocityCalculator();

        }
        [Test]
        public void VelocityCalculatorSameDataReturns0()
        {
            TrackData dummyTrackData1 = new TrackData(dummyTag, dummyX, dummyY, dummyAltitude, dummyTimestamp);
            var testList1 = new List<TrackData> { dummyTrackData1 };
            var testList2 = new List<TrackData> { dummyTrackData1 };
            Assert.AreEqual(0, uut.CalculateSpeed(testList1, testList2));
        }
        [Test]
        public void VelocityCalculatorOnlyDifferenceInTimeReturns0()
        {
            TrackData dummyTrackData1 = new TrackData(dummyTag, dummyX, dummyY, dummyAltitude, dummyTimestamp);
            TrackData dummyTrackData2 = new TrackData(dummyTag, dummyX, dummyY, dummyAltitude, dummyTimestamp2);
            var testList1 = new List<TrackData>{ dummyTrackData1};
            var testList2 = new List<TrackData>{ dummyTrackData2};
            Assert.AreEqual(0, uut.CalculateSpeed(testList1, testList2));
        }
        [Test]
        public void VelocityCalculator10MeterIn5SecondsReturns2()
        {
            TrackData dummyTrackData1 = new TrackData(dummyTag, dummyX, dummyY, dummyAltitude, dummyTimestamp);
            TrackData dummyTrackData2 = new TrackData(dummyTag, dummyX2, dummyY, dummyAltitude, dummyTimestamp3);
            var testList1 = new List<TrackData> { dummyTrackData1 };
            var testList2 = new List<TrackData> { dummyTrackData2 };
            Assert.AreEqual(2, uut.CalculateSpeed(testList1, testList2));
        }

        // TODO: VelocityCalculatorDivideByZeroException
    }
}