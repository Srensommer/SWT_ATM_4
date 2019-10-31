using System;
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
        private string dummyTag = "ATR423";
        private int dummyX = 20000;
        private int dummyY = 50000;
        private int dummyAltitude = 2000;
        private DateTime dummyTimestamp = new DateTime(1994, 6, 9, 4, 20, 9);
        private DirectionCalculator uut;
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
    }
}
