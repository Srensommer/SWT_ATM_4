using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ATM
{
    public class FlightCalculator : IFlightCalculator
    {
        private readonly ICollisionDetector _collisionDetector;
        private readonly IVelocityCalculator _velocityCalculator;
        private readonly IDirectionCalculator _directionCalculator;
        public FlightCalculator(IVelocityCalculator velocityCalculator, IDirectionCalculator directionCalculator, ICollisionDetector collisionDetector)
        {
            _collisionDetector = collisionDetector;
            _velocityCalculator = velocityCalculator;
            _directionCalculator = directionCalculator;
        }

        public Dictionary<string, FlightData> Calculate(Dictionary<String, FlightData> flightData, List<TrackData> trackData)
        {
            List<String> collisionList = _collisionDetector.SeperationCheck(trackData);

            foreach (TrackData track in trackData)
            {
                if (flightData.TryGetValue(track.Tag, out FlightData flight))
                {
                    flight.Velocity = _velocityCalculator.CalculateSpeed(flight.CurrentTrackData, track);
                    flight.CompassCourse = _directionCalculator.CalculateDirection(flight.CurrentTrackData, track);
                    flight.CollisionFlag = collisionList.Contains(flight.Tag);
                    flight.CurrentTrackData = track;
                }
                else
                {
                    flightData.Add(track.Tag, new FlightData(track));
                }

            }

            Dictionary<string, FlightData> newFlightData = new Dictionary<string, FlightData>();
            foreach (KeyValuePair<string, FlightData> entry in flightData)
            {
                if (trackData.Contains(entry.Value.CurrentTrackData))
                {
                    newFlightData.Add(entry.Key, entry.Value);
                }
            }

            return newFlightData;
        }

    }
}
