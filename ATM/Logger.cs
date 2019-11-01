using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public class Logger : ILogger
    {

        private string path = "..\\..\\..\\Log.txt";

        public Logger()
        {
            string createText = "LogFile" + Environment.NewLine + Environment.NewLine;
            File.WriteAllText(path, createText);
        }

        public void PrintToFile(string log)
        {
            File.AppendAllText(path, log);
        }
    }
}
