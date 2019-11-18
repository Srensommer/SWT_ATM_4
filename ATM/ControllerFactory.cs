using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;

namespace ATM
{
    public class ControllerFactory
    {
        public IDecoder Decoder;
        public ITrackDataFilter Filter;
        public ICollisionDetector CollisionDetector;
        public IDisplay Display;
        public ITransponderReceiver Receiver;

        public IFlightCalculator FlightCalculator;



        public ControllerFactory()
        {
            Decoder = new Decoder();
            Filter = new TrackDataFilter();
            CollisionDetector = new CollisionDetector(new Logger());
            Display = new Display();
            Receiver = TransponderReceiverFactory.CreateTransponderDataReceiver();
            FlightCalculator =
                new FlightCalculator(new VelocityCalculator(), new DirectionCalculator());
        }

        public ControllerFactory(IDecoder decoder, ITrackDataFilter filter, ICollisionDetector collisionDetector, IDisplay display, ITransponderReceiver receiver, IFlightCalculator flightCalculator)
        {
            Decoder = decoder;
            Filter = filter;
            CollisionDetector = collisionDetector;
            Display = display;
            Receiver = receiver;
            FlightCalculator = flightCalculator;
        }
    }
}


