using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM;
using NUnit.Framework;
using ATM_Application;
using TransponderReceiver;

namespace ATMUnitTest
{

    [TestFixture]
    class DecodeTest
    {
        private IDecoder _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new ATM.Decoder();
            //lav dummy decoder og dummy data
            //Tests does nothing so far.
        }
        [TestCase("normal")]
        public void Decode_CalledWithRawTransponderDataEventArgs_ReturnsTrackDataList(string seed)
        {
            List<TrackData> expectedTrackData = createTestDataList(seed);
            RawTransponderDataEventArgs testData = CreateTestDataRaw(seed);

            List<TrackData> lol = _uut.Decode(testData);

            Assert.AreEqual(expectedTrackData[0].Tag, _uut.Decode(testData)[0].Tag);
            Assert.AreEqual(expectedTrackData[0].Timestamp, _uut.Decode(testData)[0].Timestamp);
            Assert.AreEqual(expectedTrackData[0].Altitude, _uut.Decode(testData)[0].Altitude);
            Assert.AreEqual(expectedTrackData[0].X, _uut.Decode(testData)[0].X);
            Assert.AreEqual(expectedTrackData[0].Y, _uut.Decode(testData)[0].Y);
        }

        private RawTransponderDataEventArgs CreateTestDataRaw(string seed)
        {
            List<string> testDataString = new List<string>();
            switch (seed)
            {
                case "normal":
                    testDataString.Add("PEPE69;420;69;1337;20151006213456789");
                    return new RawTransponderDataEventArgs(testDataString);

                default:
                    return new RawTransponderDataEventArgs(testDataString);
            }
        }
        private List<TrackData> createTestDataList(string seed)
        {
            List<TrackData> testData = new List<TrackData>();
            switch (seed)
            {
                case "normal":
                    testData.Add(new TrackData("PEPE69", 420, 69, 1337, new DateTime(2015, 10, 06, 21, 34, 56, 789)));
                    return testData;

                default:
                    return testData;
            }
        }
    }
}