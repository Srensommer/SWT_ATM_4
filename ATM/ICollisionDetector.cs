using System;
using System.Collections.Generic;

namespace ATM
{
    public interface ICollisionDetector
    {
        List<String> SeperationCheck(List<TrackData> trackList);
    }
}