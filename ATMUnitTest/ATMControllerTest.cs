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
        private List<TrackData> _fakeFilteredData;
        private Dictionary<string, FlightData> _emptyFlightData;
        private Dictionary<string, FlightData> _fakeFlightData;
        private Dictionary<string, FlightData> _fakeCalculatedData;
        private RawTransponderDataEventArgs _fakeEventArgs;

        [SetUp]
        public void Setup()
        {
            //Arrange
            _fakeDisplay = Substitute.For<IDisplay>();
            _fakeTrackDataFilter = Substitute.For<ITrackDataFilter>();
            _fakeFlightCalculator = Substitute.For<IFlightCalculator>();
            _fakeCollisionDetector = Substitute.For<ICollisionDetector>();
            _fakeDecoder = Substitute.For<IDecoder>();
            _fakeReceiver = Substitute.For<ITransponderReceiver>();

            _fakeEventArgs = new RawTransponderDataEventArgs(new List<string>());
            _fakeTrackData = new List<TrackData>();
            _fakeFilteredData = new List<TrackData>();
            _emptyFlightData = new Dictionary<string, FlightData>();
            _fakeFlightData = new Dictionary<string, FlightData>(); //TODO: non empty
            _fakeCalculatedData = new Dictionary<string, FlightData>();
            _fakeSeperationData = new Tuple<List<string>, List<string>>(new List<string>(), new List<string>());

            //Fake decoder should return fake Trackdata when called with fakeEventArgs
            _fakeDecoder.Decode(_fakeEventArgs).Returns(_fakeTrackData);

            //Filter returns _fakeFilteredData
            _fakeTrackDataFilter.Filter(_fakeTrackData).Returns(_fakeFilteredData);

            //FlightCalculator returns _fakeFlightData
            _fakeFlightCalculator.Calculate(Arg.Any<Dictionary<string, FlightData>>(), Arg.Any<List<TrackData>>()).Returns(_fakeCalculatedData);

            //Seperation detector returns _fakeSeperationData
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
        public void ATMController_EventIsRaised_DataIsPassedCorrectly()
        {
            //Act - event is raised
            _fakeReceiver.TransponderDataReady += Raise.EventWith(new object(), _fakeEventArgs);

            //Assert
            
            //Decoder stub is passed correct eventargs
            _fakeDecoder.Received().Decode(_fakeEventArgs);

            //Filter is passed trackdata correctly from decoder
            _fakeTrackDataFilter.Received().Filter(_fakeTrackData);

            //Calculator is called with data from filter and empty flight data(first time)
            _fakeFlightCalculator.Received().Calculate(
                Arg.Any<Dictionary<string, FlightData>>(), 
                Arg.Is<List<TrackData>>(d => ReferenceEquals(d, _fakeFilteredData)));


            //Data from calculator is passed into collision detector
            _fakeCollisionDetector.Received().SeperationCheck(
                Arg.Is<List<TrackData>>(_fakeFilteredData)
                );

            //
            _fakeDisplay.Received().Clear();
            _fakeDisplay.Received().Render(_fakeCalculatedData, _fakeSeperationData.Item2);
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


//TODO: Test that flight calculator holds _data
