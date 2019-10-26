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
        public void Returns180()
        {
            TrackData curr = new TrackData(dummyTag, dummyX, dummyY, dummyAltitude, dummyTimestamp);
            TrackData prev = new TrackData(dummyTag, dummyX, 60000, dummyAltitude, dummyTimestamp);
            Assert.AreEqual(Math.PI,uut.CalculateDirection(prev,curr));
        }
    }
}
