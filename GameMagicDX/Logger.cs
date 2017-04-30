using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMagic
{
    public static class Logger
    {
        public static void Log(string value)
        {
            Console.WriteLine(value);
        }

        public static void LogError(string value)
        {
            Console.WriteLine("Error: " + value);
        }
    }
}
