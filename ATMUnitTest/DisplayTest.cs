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


            string expected = "Tag:TEST420 X:420 Y:111 A:9000 Time:2/1/2020 10:20:30 Course:10 Velocity:999\r\n";

            // Act
            _uut.Render(testData, new List<string>());

            // Assert
            Assert.AreEqual(expected, _fakeConsole.ToString());
        }

        [Test]
        public void TestDisplayWithCollisions()
        {
            //Arrange
            Dictionary<string, FlightData> testFlightData = new Dictionary<string, FlightData>();
            List<string> testLogData = new List<string>(new List<string>());

            testLogData.Add("LOG ITEM");

            string expected = "!!!!   WARNING   !!!!" +Environment.NewLine +
                              "Collisions:" + Environment.NewLine +
                              "LOG ITEM" + Environment.NewLine + Environment.NewLine + Environment.NewLine;

            //Act
            _uut.Render(testFlightData, testLogData);

            //Assert
            Assert.AreEqual(expected, _fakeConsole.ToString());
        }
    }
}