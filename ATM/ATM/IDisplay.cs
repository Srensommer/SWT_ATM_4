using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM;

namespace TransponderReceiver
{
    public interface IDisplay
    {
        void Render(List<TrackData> trackData);
    }
}
