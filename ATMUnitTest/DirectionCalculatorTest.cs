﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ATM;
using NUnit.Framework;

namespace ATMUnitTest
{
    [TestFixture]
    public class DirectionCalculatorTest
    {
        private readonly string dummyTag = "ATR423";
        private readonly int dummyX = 20000;
        private readonly int dummyY = 50000;
        private readonly int dummyAltitude = 2000;
        private readonly DateTime dummyTimestamp = new DateTime(1994, 6, 9, 4, 20, 9);

        private IDirectionCalculator uut;
        [SetUp]
        public void Setup()
        {  
            uut = new DirectionCalculator();
        }
        [Test]
        public void DirectionCalculatorNoMovementNoTimeReturns0Test()
        {
            TrackData prev = new TrackData(dummyTag, dummyX, dummyY, dummyAltitude, dummyTimestamp);
            TrackData curr = new TrackData(dummyTag, dummyX, dummyY, dummyAltitude, dummyTimestamp);
            Assert.AreEqual(0, uut.CalculateDirection(prev, curr));
        }
        [Test]
        public void DirectionCalculatorPositiveMovementOnXReturns90Test()
        {
            TrackData prev = new TrackData(dummyTag, dummyX, dummyY, dummyAltitude, dummyTimestamp);
            TrackData curr = new TrackData(dummyTag, 20001, dummyY, dummyAltitude, dummyTimestamp);
            Assert.AreEqual(90, uut.CalculateDirection(prev, curr));
        }
        [Test]
        public void DirectionCalculatorNegativeMovementOnYReturns180Test()
        {
            TrackData prev = new TrackData(dummyTag, dummyX, dummyY, dummyAltitude, dummyTimestamp);
            TrackData curr = new TrackData(dummyTag, dummyX, 49999, dummyAltitude, dummyTimestamp);
            Assert.AreEqual(180, uut.CalculateDirection(prev, curr));
        }
        [Test]
        public void DirectionCalculatorNegativeMovementOnXReturns270Test()
        {
            TrackData prev = new TrackData(dummyTag, dummyX, dummyY, dummyAltitude, dummyTimestamp);
            TrackData curr = new TrackData(dummyTag, 19999, dummyY, dummyAltitude, dummyTimestamp);
            Assert.AreEqual(270, uut.CalculateDirection(prev, curr));
        }
        [Test]
        public void DirectionCalculatorPositiveMovementOnYReturns0Test()
        {
            TrackData prev = new TrackData(dummyTag, dummyX, dummyY, dummyAltitude, dummyTimestamp);
            TrackData curr = new TrackData(dummyTag, dummyX, 50001, dummyAltitude, dummyTimestamp);
            Assert.AreEqual(0, uut.CalculateDirection(prev, curr));
        }
        [Test]
        public void DirectionCalculator1PositiveMovementStepOnXandYReturns45Test()
        {
            TrackData prev = new TrackData(dummyTag, dummyX, dummyY, dummyAltitude, dummyTimestamp);
            TrackData curr = new TrackData(dummyTag, 20001, 50001, dummyAltitude, dummyTimestamp);
            Assert.AreEqual(45, uut.CalculateDirection(prev, curr));
        }
        [Test]
        public void DirectionCalculator1PositiveMovementStepOnX2StepsOnYReturns27Test()
        {
            TrackData prev = new TrackData(dummyTag, dummyX, dummyY, dummyAltitude, dummyTimestamp);
            TrackData curr = new TrackData(dummyTag, 20001, 50002, dummyAltitude, dummyTimestamp);
            Assert.AreEqual(27, uut.CalculateDirection(prev, curr));
        }
    }
}
