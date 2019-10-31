using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;

namespace ATM
{
    public class Decoder : IDecoder
    {
        public List<TrackData> Decode(RawTransponderDataEventArgs e)
        {
            List<TrackData> data = new List<TrackData>();

            foreach (var track in e.TransponderData)
            {
                data.Add(decodeTrack(track));
            }

            return data;
        }

        private TrackData decodeTrack(string trackString)
        {
            var parameters = trackString.Split(';');

            string tag = parameters[0];
            int x = Int32.Parse(parameters[1]);
            int y = Int32.Parse(parameters[2]);
            int altitude = Int32.Parse(parameters[3]);
            string timestampString = parameters[4];


            DateTime timestamp = new DateTime(
                year: Int32.Parse(timestampString.Substring(0, 4)),
                month: Int32.Parse(timestampString.Substring(4, 2)),
                day: Int32.Parse(timestampString.Substring(6, 2)),
                hour: Int32.Parse(timestampString.Substring(8, 2)),
                minute: Int32.Parse(timestampString.Substring(10, 2)),
                second: Int32.Parse(timestampString.Substring(12, 2)),
                millisecond: Int32.Parse(timestampString.Substring(14, 3)));

            TrackData trackData = new TrackData(tag, x, y, altitude, timestamp);
            return trackData;
        }
    }
}
