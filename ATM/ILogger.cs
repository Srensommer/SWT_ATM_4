using System.Collections.Generic;

namespace ATM
{
    public interface ILogger
    {
        void LogCollision(List<string> logList);
    }
}