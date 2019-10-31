﻿using System;
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
        private int dummyX2 = 20010;
        private int dummyY = 50000;
        private int dummyAltitude = 2000;
        private DateTime dummyTimestamp = new DateTime(1969, 6, 9, 4, 20, 8);
        private DateTime dummyTimestamp2 = new DateTime(1969, 6, 9, 4, 20, 9);
        private DateTime dummyTimestamp3 = new DateTime(1969, 6, 9, 4, 20, 13);
        [SetUp]
        public void Setup()
        {

        }
        [Test]
        public void VelocityCalculatorReturns0()
        {
            TrackData dummyTrackData1 = new TrackData(dummyTag, dummyX, dummyY, dummyAltitude, dummyTimestamp);
            TrackData dummyTrackData2 = new TrackData(dummyTag, dummyX, dummyY, dummyAltitude, dummyTimestamp2);
            Assert.AreEqual(0, VelocityCalculator.CalculateSpeed(dummyTrackData1, dummyTrackData2));
        }
        [Test]
        public void VelocityCalculatorReturns10()
        {
            TrackData dummyTrackData1 = new TrackData(dummyTag, dummyX, dummyY, dummyAltitude, dummyTimestamp);
            TrackData dummyTrackData2 = new TrackData(dummyTag, dummyX2, dummyY, dummyAltitude, dummyTimestamp2);
            Assert.AreEqual(10, VelocityCalculator.CalculateSpeed(dummyTrackData1, dummyTrackData2));
        }
        [Test]
        public void VelocityCalculatorReturns2()
        {
            TrackData dummyTrackData1 = new TrackData(dummyTag, dummyX, dummyY, dummyAltitude, dummyTimestamp);
            TrackData dummyTrackData2 = new TrackData(dummyTag, dummyX2, dummyY, dummyAltitude, dummyTimestamp3);
            Assert.AreEqual(2, VelocityCalculator.CalculateSpeed(dummyTrackData1, dummyTrackData2));
        }

        // TODO: VelocityCalculatorDivideByZeroException
    }
}