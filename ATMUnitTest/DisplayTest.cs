using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM;
using NUnit.Framework;
using NSubstitute;
using TransponderReceiver;

namespace ATMUnitTest
{
    [TestFixture]
    class DisplayTest
    {
        private IDisplay _uut;
        private StringWriter _fakeConsole;

        [SetUp]
        public void Setup()
        {
            _uut = new Display();
            _fakeConsole = new StringWriter();

            //Arrange - Console output captured in StringWriter
            Console.SetOut(_fakeConsole);
        }

        [Test]
        public void TestDisplay()
        {
            // Arrange
            Dictionary<string, FlightData> testData = new Dictionary<string, FlightData>();

            TrackData trackData = new TrackData(
                "TEST420",
                420, 111, 9000,
                new DateTime(2020, 1, 2, 10, 20, 30, 400));
            
            FlightData flightData = new FlightData(trackData);
            flightData.Velocity = 999;
            flightData.CompassCourse = 10;
            
            testData.Add(flightData.Tag, flightData);


            string expected = "TEST420 420 111 9000 2/1/2020 10:20:30 Course 10 Velocity 999\r\n";

            // Act
            _uut.Render(testData);

            // Assert
            Assert.AreEqual(expected, _fakeConsole.ToString());
        }
    }
}
