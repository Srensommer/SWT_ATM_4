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

        private List<TrackData> _fakeTrackData;
        private RawTransponderDataEventArgs _fakeEventArgs;

        [SetUp]
        public void Setup()
        {
            //Arrange
            _fakeDisplay = Substitute.For<IDisplay>();
            _fakeTrackDataFilter = Substitute.For<ITrackDataFilter>();
            _fakeDecoder = Substitute.For<IDecoder>();
            _fakeReceiver = Substitute.For<ITransponderReceiver>();

            _fakeEventArgs = new RawTransponderDataEventArgs(new List<string>());
            _fakeTrackData = new List<TrackData>();

            //Fake decoder should return fakeTrackdata when called with fakeEventArgs
            _fakeDecoder.Decode(_fakeEventArgs).Returns(_fakeTrackData);

            _fakeTrackDataFilter.Filter(_fakeTrackData).Returns(_fakeTrackData);

            _uut = new ATMController(_fakeDecoder, _fakeTrackDataFilter, _fakeDisplay, _fakeReceiver);
        }

        [Test]
        public void ATMController_EventIsRaised_AllDependenciesCalled()
        {
            //Act
            _fakeReceiver.TransponderDataReady += Raise.EventWith(new object(), _fakeEventArgs);

            //Assert

            //Decoder called with correct EventArgs
            _fakeDecoder.Received().Decode(_fakeEventArgs);

            //Filter called with correct track data
            _fakeTrackDataFilter.Received().Filter(_fakeTrackData);

            //Display called with correct track data
            _fakeDisplay.Received().Clear();
            _fakeDisplay.Received().Render(_fakeTrackData);
        }
    }
}
