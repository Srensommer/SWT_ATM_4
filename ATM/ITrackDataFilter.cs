using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM;

namespace ATM
{
    public interface ITrackDataFilter
    {
        List<TrackData> Filter(List<TrackData> data);
    }
}
