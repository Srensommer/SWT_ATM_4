using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM;

namespace TransponderReceiver
{
    public class Display : IDisplay
    {
        public void Render(List<TrackData> trackData)
        {
            System.Console.Clear();
            foreach (TrackData track in trackData)
            {
                System.Console.WriteLine($"{track.Tag} {track.X} {track.Y} {track.Altitude} {track.Timestamp}");
            }
        }
    }
}
