using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM;
using Castle.Components.DictionaryAdapter;
using NUnit.Framework;
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
        }

        [Test()]
        public void Decode_CalledWithRawTransponderDataEventArgs_ReturnsTrackDataListWithSameX()
        {
            List<string> testData = new List<string>
            {
                "AYE334;12345;12345;12345;20190101010101010",
                "BYE334;11345;11345;11345;20190201010101010"
            };
            List<TrackData> expectedData = new List<TrackData>();
            expectedData.Add(new TrackData("AYE334", 12345, 12345, 12345, new DateTime(2019, 01, 01, 01, 01, 01, 010)));
            expectedData.Add(new TrackData("BYE334", 11345, 11345, 11345, new DateTime(2019, 02, 01, 01, 01, 01, 010)));
            RawTransponderDataEventArgs testTransponderData = new RawTransponderDataEventArgs(testData);
            List<TrackData> actualData = new List<TrackData>();
            actualData = _uut.Decode(testTransponderData);
            CollectionAssert.AreEqual(expectedData, actualData, new TrackDataComparer());
        }

        private class TrackDataComparer : Comparer<TrackData>
        {
            public override int Compare(TrackData x, TrackData y)
            {
                return x.X.CompareTo(y.X);
            }
        }

        [Test()]
        public void Decode_CalledWithRawTransponderData_ReturnsTrackDataListWithSameFirstTrackDataTag()
        {
            List<string> testData = new List<string>
            {
                "AYE334;12345;12345;12345;20190101010101010"
            };
            List<TrackData> expectedData = new List<TrackData>();
            expectedData.Add(new TrackData("AYE334", 12345, 12345, 12345, new DateTime(2019, 01, 01, 01, 01, 01, 010)));
            RawTransponderDataEventArgs testTransponderData = new RawTransponderDataEventArgs(testData);
            List<TrackData> actualData = new List<TrackData>();
            actualData = _uut.Decode(testTransponderData);
            Assert.AreEqual(expectedData[0].Tag,actualData[0].Tag);
        }
    }
}