using System.Collections.Generic;

namespace ATM
{
    public interface ILogger
    {
        void PrintToFile(List<string> logList);
    }
}