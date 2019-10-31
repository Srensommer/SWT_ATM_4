using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM;

namespace ATM
{
    public class Display : IDisplay
    {
        public void Render(Dictionary<string, FlightData> flightData)
        {
            foreach (FlightData flight in flightData.Values)
            {
                if (flight.CollisionFlag)
                {
                    System.Console.WriteLine($"Oh no, collision on flight { flight.Tag }");
                }
            }
            foreach (FlightData flight in flightData.Values)
            {
                System.Console.WriteLine($"{flight.Tag} {flight.X} {flight.Y} {flight.Altitude} " +
                                         $"{flight.Timestamp.Day}/{flight.Timestamp.Month}/{flight.Timestamp.Year} " +
                                         $"{ flight.Timestamp:hh:mm:ss}" +
                                         $" Course { flight.CompassCourse}" +
                                         $" Velocity { flight.Velocity }");
            }
        }

        public void Clear()
        {
            System.Console.Clear();
        }
    }
}
