using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM;
using NUnit.Framework;
using TransponderReceiver;

namespace ATM
{
    public class ATMController
    {
        private readonly IDecoder _decoder;
        private readonly ITrackDataFilter _filter;
        private readonly IFlightCalculator _flightCalculator;
        private readonly ICollisionDetector _collisionDetector;
        private readonly IDisplay _display;
        private readonly ITransponderReceiver _receiver;

        private Dictionary<string, FlightData> _data;

        public ATMController(ControllerFactory controllerFactory)
        {
            _decoder = controllerFactory.Decoder;
            _filter = controllerFactory.Filter;
            _flightCalculator = controllerFactory.FlightCalculator;
            _collisionDetector = controllerFactory.CollisionDetector;
            _display = controllerFactory.Display;
            _receiver = controllerFactory.Receiver;

            _data = new Dictionary<string, FlightData>();

            _receiver.TransponderDataReady += OnTransponderDataReady;
        }

        private void OnTransponderDataReady(object sender, RawTransponderDataEventArgs e)
        {
            Update(e);
        }

        //Run every time new data is present
        private void Update(RawTransponderDataEventArgs e)
        {
            //Decode Data
            List<TrackData> trackData = _decoder.Decode(e);

            //Filter data
            trackData = _filter.Filter(trackData);

            //Update existing flight data
            _data = _flightCalculator.Calculate(_data, trackData);

            //Collision Detect
            Tuple<List<string>, List<string>> collisionResult = _collisionDetector.SeperationCheck(trackData);
            List<string> collisionTags = collisionResult.Item1;
            List<string> displayCollisionList = collisionResult.Item2;


            //Set CollisionFlag on flights
            foreach (KeyValuePair<string, FlightData> entry in _data)
            {
                entry.Value.CollisionFlag = collisionTags.Contains(entry.Value.Tag);
            }

            //Display Data
            _display.Clear();
            _display.Render(_data, displayCollisionList);
        }
    }
}
