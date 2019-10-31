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
    class ATMControllerTest
    {
        private ATMController _uut;
        private IDisplay _fakeDisplay;
        private ITrackDataFilter _fakeTrackDataFilter;
        private IDecoder _fakeDecoder;
        private ITransponderReceiver _fakeReceiver;

        [SetUp]
        public void Setup()
        {
            //Arrange
            _fakeDisplay = Substitute.For<IDisplay>();
            _fakeTrackDataFilter = Substitute.For<ITrackDataFilter>();
            _fakeDecoder = Substitute.For<IDecoder>();
            _fakeReceiver = Substitute.For<ITransponderReceiver>();

            _uut = new ATMController(_fakeDecoder, _fakeTrackDataFilter, _fakeDisplay, _fakeReceiver);

            
        }

        [Test]
        public void Test()
        {
            //Act
            _fakeReceiver.TransponderDataReady += Raise.EventWith<RawTransponderDataEventArgs>();
        }
    }
}
