using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMagic.Logging
{
    public class ConsoleLogger : ISimpleLogger
    {
        public void Log(string value)
        {
            Console.WriteLine(value);
        }

        public void LogError(string value)
        {
            Console.WriteLine("Error: " + value);
        }
    }
}
