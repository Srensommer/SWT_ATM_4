using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM;
using TransponderReceiver;

namespace ATM_Application
{
    public class ATMController
    {
        private IDecoder _decoder;
        private ITrackDataFilter _filter;
        private IDisplay _display;
        private ITransponderReceiver _receiver;

        public ATMController(IDecoder decoder, ITrackDataFilter filter, IDisplay display, ITransponderReceiver receiver)
        {
            _decoder = decoder;
            _filter = filter;
            _display = display;
            _receiver = receiver;

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

            //Display Data
            _display.Render(trackData);
        }
    }
}
