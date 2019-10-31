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

            List<TrackData> actualTrackData = _uut.Decode(testData);

            for (int i = 0; i < actualTrackData.Count(); i++)
            {
                Assert.AreEqual(expectedTrackData[i].Tag, actualTrackData[i].Tag);
                Assert.AreEqual(expectedTrackData[i].X, actualTrackData[i].X);
                Assert.AreEqual(expectedTrackData[i].Y, actualTrackData[i].Y);
                Assert.AreEqual(expectedTrackData[i].Altitude, actualTrackData[i].Altitude);
                Assert.AreEqual(expectedTrackData[i].Timestamp, actualTrackData[i].Timestamp);
            }
        }

        private RawTransponderDataEventArgs CreateTestDataRaw(string seed)
        {
            List<string> testDataString = new List<string>();
            switch (seed)
            {
                case "normal":
                    testDataString.Add("PEPE69;420;69;1337;20151006213456789");
                    testDataString.Add("FUN;111;22;4141;11111111111111111");


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
                    testData.Add(new TrackData("FUN", 111, 22, 4141, new DateTime(1111, 11, 11, 11, 11, 11, 111)));
                    return testData;

                default:
                    return testData;
            }
        }
    }
}