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
        private IDecoder _decoder;
        private ITrackDataFilter _filter;
        private IFlightCalculator _flightCalculator;
        private ICollisionDetector _collisionDetector;
        private IDisplay _display;
        private ITransponderReceiver _receiver;

        private Dictionary<string, FlightData> _data;

        public ATMController(IDecoder decoder, ITrackDataFilter filter, ICollisionDetector collisionDetector, IDisplay display, ITransponderReceiver receiver, IFlightCalculator flightCalculator)
        {
            _decoder = decoder;
            _filter = filter;
            _flightCalculator = flightCalculator;
            _collisionDetector = collisionDetector;
            _display = display;
            _receiver = receiver;

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
