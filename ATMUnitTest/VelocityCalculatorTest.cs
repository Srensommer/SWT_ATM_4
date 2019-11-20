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
        private string dummyTag = "ATR423";
        private int dummyX = 20000;
        private int dummyY = 50000;
        private int dummyAltitude = 2000;
        private DateTime dummyTimestamp = new DateTime(1969, 6, 9, 4, 20, 8);
        private DateTime dummyTimestamp2 = new DateTime(1969, 6, 9, 4, 20, 9);
        private DateTime dummyTimestamp4 = new DateTime(1969, 6, 9, 4, 20, 10);
        private DateTime dummyTimestamp3 = new DateTime(1969, 6, 9, 4, 20, 13);

        private IVelocityCalculator uut;

        [SetUp]
        public void Setup()
        {
            uut = new VelocityCalculator();
        }
        [Test]
        public void VelocityCalculatorSameDataReturns0()
        {
            TrackData dummyTrackData1 = new TrackData(dummyTag, dummyX, dummyY, dummyAltitude, dummyTimestamp);
            TrackData dummyTrackData2 = new TrackData(dummyTag, dummyX, dummyY, dummyAltitude, dummyTimestamp2);
            Assert.AreEqual(0, uut.CalculateSpeed(dummyTrackData1, dummyTrackData2));
        }
        [Test]
        public void VelocityCalculatorOnlyDifferenceInTimeReturns0()
        {
            TrackData dummyTrackData1 = new TrackData(dummyTag, dummyX, dummyY, dummyAltitude, dummyTimestamp);
            TrackData dummyTrackData2 = new TrackData(dummyTag, 20010, dummyY, dummyAltitude, dummyTimestamp2);
            Assert.AreEqual(10, uut.CalculateSpeed(dummyTrackData1, dummyTrackData2));
        }
        [Test]
        public void VelocityCalculator10MeterIn5SecondsReturns2()
        {
            TrackData dummyTrackData1 = new TrackData(dummyTag, dummyX, dummyY, dummyAltitude, dummyTimestamp);
            TrackData dummyTrackData2 = new TrackData(dummyTag, 20010, dummyY, dummyAltitude, dummyTimestamp3);
            Assert.AreEqual(2, uut.CalculateSpeed(dummyTrackData1, dummyTrackData2));
        }
        [Test]
        public void VelocityCalculator100MeterIn5SecondsReturns2()
        {
            TrackData dummyTrackData1 = new TrackData(dummyTag, dummyX, dummyY, dummyAltitude, dummyTimestamp);
            TrackData dummyTrackData2 = new TrackData(dummyTag, 20010, dummyY, dummyAltitude, dummyTimestamp3);
            Assert.AreEqual(2, uut.CalculateSpeed(dummyTrackData1, dummyTrackData2));
        }
        [Test]
        public void VelocityCalculator100MeterIn5SecondsReturn2()
        {
            TrackData dummyTrackData1 = new TrackData(dummyTag, dummyX, dummyY, dummyAltitude, dummyTimestamp);
            TrackData dummyTrackData2 = new TrackData(dummyTag, 20003, 50004, dummyAltitude, dummyTimestamp4);
            Assert.AreEqual(2.5, uut.CalculateSpeed(dummyTrackData1, dummyTrackData2));
        }
    }
}