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
        private ICollisionDetector _fakeCollisionDetector;
        private ITrackDataFilter _fakeTrackDataFilter;
        private IFlightCalculator _fakeFlightCalculator;
        private IDecoder _fakeDecoder;
        private ITransponderReceiver _fakeReceiver;

        private Tuple<List<String>, List<String>> _fakeSeperationData;
        private List<TrackData> _fakeTrackData;
        private Dictionary<string, FlightData> _fakeFlightData;
        private RawTransponderDataEventArgs _fakeEventArgs;

        [SetUp]
        public void Setup()
        {
            //Arrange
            _fakeDisplay = Substitute.For<IDisplay>();
            _fakeTrackDataFilter = Substitute.For<ITrackDataFilter>();
            _fakeCollisionDetector = Substitute.For<ICollisionDetector>();
            _fakeDecoder = Substitute.For<IDecoder>();
            _fakeFlightCalculator = Substitute.For<IFlightCalculator>();
            _fakeReceiver = Substitute.For<ITransponderReceiver>();

            _fakeEventArgs = new RawTransponderDataEventArgs(new List<string>());
            _fakeTrackData = new List<TrackData>();
            _fakeFlightData = new Dictionary<string, FlightData>();
            _fakeSeperationData = new Tuple<List<string>, List<string>>(new List<string>(), new List<string>());

            //Fake decoder should return fake Trackdata when called with fakeEventArgs
            _fakeDecoder.Decode(_fakeEventArgs).Returns(_fakeTrackData);

            _fakeTrackDataFilter.Filter(_fakeTrackData).Returns(_fakeTrackData);

            _fakeFlightCalculator.Calculate(Arg.Any<Dictionary<string, FlightData>>(), Arg.Any<List<TrackData>>()).Returns(_fakeFlightData);
            _fakeCollisionDetector.SeperationCheck(Arg.Any<List<TrackData>>())
                .Returns(_fakeSeperationData);

            _uut = new ATMController(
                _fakeDecoder, 
                _fakeTrackDataFilter, 
                _fakeCollisionDetector,
                _fakeDisplay, 
                _fakeReceiver,
                _fakeFlightCalculator);
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

            //Tests that flight calculator is called
            _fakeFlightCalculator.Received().Calculate(Arg.Any<Dictionary<string, FlightData>>(), _fakeTrackData);

            //Display called with correct track data
            _fakeDisplay.Received().Clear();
            _fakeDisplay.Received().Render(Arg.Any<Dictionary<string, FlightData>>(), Arg.Any<List<string>>());
        }
    }
}
