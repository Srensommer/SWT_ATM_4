using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
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
            _fakeFlightCalculator.Calculate(Arg.Any<Dictionary<string, FlightData>>(), Arg.Any<List<TrackData>>())
                .Returns(_fakeCalculatedData);

            //Seperation detector returns _fakeSeperationData
            _fakeCollisionDetector.SeperationCheck(Arg.Any<List<TrackData>>())
                .Returns(_fakeSeperationData);

            ControllerFactory fakeFactory = new ControllerFactory(
                _fakeDecoder,
                _fakeTrackDataFilter,
                _fakeCollisionDetector,
                _fakeDisplay,
                _fakeReceiver,
                _fakeFlightCalculator);

            _uut = new ATMController(fakeFactory);
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

            // Display receives the correct data from calculator and collision detector
            _fakeDisplay.Received().Clear();
            _fakeDisplay.Received().Render(_fakeCalculatedData, _fakeSeperationData.Item2);
        }

        [Test]
        public void ATMController_EventIsRaised_DataIsPassedFromEventToDecoder()
        {
            // Arrange
            _fakeEventArgs = new RawTransponderDataEventArgs(new List<string>
            {
                "AYE334;12345;12345;12345;20190101010101010",
                "BYE331;41242;12345;12345;20190101010101010",
                "HMM221;12521;12345;12345;20190101010101010"
            });

            // Act - event is raised
            _fakeReceiver.TransponderDataReady += Raise.EventWith(new object (), _fakeEventArgs);

            //Assert

            //Decoder stub is passed correct eventargs
            _fakeDecoder.Received().Decode(Arg.Is<RawTransponderDataEventArgs>(x =>
            
                x.TransponderData[0] == "AYE334;12345;12345;12345;20190101010101010" &&
                x.TransponderData[1] == "BYE331;41242;12345;12345;20190101010101010" &&
                x.TransponderData[2] == "HMM221;12521;12345;12345;20190101010101010"
            ));
        }

        [Test]
        public void ATMController_EventIsRaised_DataIsPassedFromDecoderToFilter()
        {
            // Arrange

            _fakeEventArgs = new RawTransponderDataEventArgs(new List<string>());

            // Act

            //Fake decoder should return fake Trackdata when called with fakeEventArgs
            _fakeDecoder.Decode(_fakeEventArgs).Returns(_fakeTrackData = new List<TrackData>
            {
                new TrackData("AYE334", 12345, 54321, 5000, new DateTime(year:2000, month:10, day: 9, hour: 8, minute: 7, second: 6, millisecond: 5)),
                new TrackData("BYE331", 12121, 23232, 5001, new DateTime(year:2001, month:11, day: 10, hour: 9, minute: 8, second: 7, millisecond: 6)),
                new TrackData("HMM221", 34343, 45454, 5002, new DateTime(year:2002, month:12, day: 11, hour: 10, minute: 9, second: 8, millisecond: 7))
            });

            //Raise event
            _fakeReceiver.TransponderDataReady += Raise.EventWith(new object(), _fakeEventArgs);

            // Assert
            _fakeTrackDataFilter.Received().Filter(Arg.Is<List<TrackData>>(x =>
                x[0].Tag == "AYE334" && x[0].X == 12345 && x[0].Y == 54321 && x[0].Altitude == 5000 &&
                x[0].Timestamp.Year == 2000 && x[0].Timestamp.Month == 10 && x[0].Timestamp.Day == 9 && x[0].Timestamp.Hour == 8 &&
                x[0].Timestamp.Minute == 7 && x[0].Timestamp.Second == 6 && x[0].Timestamp.Millisecond == 5 &&

                x[1].Tag == "BYE331" && x[1].X == 12121 && x[1].Y == 23232 && x[1].Altitude == 5001 &&
                x[1].Timestamp.Year == 2001 && x[1].Timestamp.Month == 11 && x[1].Timestamp.Day == 10 && x[1].Timestamp.Hour == 9 &&
                x[1].Timestamp.Minute == 8 && x[1].Timestamp.Second == 7 && x[1].Timestamp.Millisecond == 6 &&

                x[2].Tag == "HMM221" && x[2].X == 34343 && x[2].Y == 45454 && x[2].Altitude == 5002 &&
                x[2].Timestamp.Year == 2002 && x[2].Timestamp.Month == 12 && x[2].Timestamp.Day == 11 && x[2].Timestamp.Hour == 10 &&
                x[2].Timestamp.Minute == 9 && x[2].Timestamp.Second == 8 && x[2].Timestamp.Millisecond == 7
            ));
        }

        [Test]
        public void ATMController_EventIsRaised_DataIsPassedFromFilterToFlightCalculator()
        {
            // Arrange
            _fakeEventArgs = new RawTransponderDataEventArgs(new List<string>());

            // Act

            //Fake Filter should return fake Trackdata when called with Trackdata
            _fakeTrackDataFilter.Filter(Arg.Any<List<TrackData>>()).Returns(_fakeTrackData = new List<TrackData>
            {
                new TrackData("AYE334", 12345, 54321, 5000, new DateTime(year:2000, month:10, day: 9, hour: 8, minute: 7, second: 6, millisecond: 5)),
                new TrackData("BYE331", 12346, 54322, 6000, new DateTime(year:2001, month:11, day: 10, hour: 9, minute: 8, second: 7, millisecond: 6)),
                new TrackData("HMM221", 12347, 54323, 7000, new DateTime(year:2002, month:12, day: 11, hour: 10, minute: 9, second: 8, millisecond: 7))
            });



            //Raise event
            _fakeReceiver.TransponderDataReady += Raise.EventWith(new object(), _fakeEventArgs);

            // Assert
            _fakeFlightCalculator.Received().Calculate(NSubstitute.Arg.Any<Dictionary<String, FlightData>>(), Arg.Is<List<TrackData>>(x =>
                x[0].Tag == "AYE334" && 
                x[0].X == 12345 && 
                x[0].Y == 54321 && 
                x[0].Altitude == 5000 && 
                x[0].Timestamp == new DateTime(2000, 10,9, 8,7, 6,5) &&

                x[1].Tag == "BYE331" &&
                x[1].X == 12346 &&
                x[1].Y == 54322 &&
                x[1].Altitude == 6000 &&
                x[1].Timestamp == new DateTime(2001, 11,10, 9, 8, 7, 6) &&

                x[2].Tag == "HMM221" &&
                x[2].X == 12347 &&
                x[2].Y == 54323 &&
                x[2].Altitude == 7000 &&
                x[2].Timestamp == new DateTime(2002,12, 11, 10, 9, 8, 7)
                ));
        }
    }
}