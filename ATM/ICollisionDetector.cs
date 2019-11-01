using System;
using System.Collections.Generic;

namespace ATM
{
    public interface ICollisionDetector
    {
        Tuple<List<String>, List<String>> SeperationCheck(List<TrackData> trackList);
    }
}