using System;
using System.Collections.Generic;

namespace ATM
{
    public interface IFlightCalculator
    {
        Dictionary<string, FlightData> Calculate(Dictionary<String, FlightData> flightData, List<TrackData> trackData);
    }
}