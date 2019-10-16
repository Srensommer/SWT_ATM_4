using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM_Application;

namespace ATM
{
    public class TrackDataFilter : ITrackDataFilter
    {
        public List<TrackData> Filter(List<TrackData> data)
        {
            //Does absolutely nothing rn
            return data;
        }
    }
}
