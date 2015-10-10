using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.ConsoleListener
{
    internal class ConsoleListener
    {
        public static void WriteInConsole(string message)
        {
            ConsoleTraceListener consoleTracer;
            consoleTracer = new ConsoleTraceListener(true);
            consoleTracer.WriteLine(message);
            consoleTracer.Close();
        }
    }
}