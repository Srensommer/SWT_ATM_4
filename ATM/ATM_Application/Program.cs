using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM;
using TransponderReceiver;

namespace ATM_Application
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create receiver with factory
            ITransponderReceiver receiver = TransponderReceiverFactory.CreateTransponderDataReceiver();

            //Decoder for decoding input format to data format
            IDecoder decoder = new ATM.Decoder(receiver);

            //Wait for user to close the console
            char input = System.Console.ReadKey().KeyChar;
            while (input != 'x')
            {
                input = System.Console.ReadKey().KeyChar;
            }
        }
    }

    public class ATM_Placeholder
    {
        //placeholder for NUnit tests

    }
}
