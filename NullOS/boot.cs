using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NullOS
{
    internal class boot
    {
        static void Main(string[] args)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.DarkGray; Console.Write("BOOTLOADER"); Console.ForegroundColor = ConsoleColor.White; Console.Write(": ["); Console.ForegroundColor = ConsoleColor.Green; Console.Write("SUCCESS"); Console.ForegroundColor = ConsoleColor.White; Console.Write("] LOADED.\n");
                var core = new core();
                core.core_main();
                Console.ForegroundColor = ConsoleColor.DarkGray; Console.Write("BOOTLOADER"); Console.ForegroundColor = ConsoleColor.White; Console.Write(": ["); ; Console.ForegroundColor = ConsoleColor.Magenta; Console.Write("EXITED"); Console.ForegroundColor = ConsoleColor.White; Console.Write("] CODE: 0 ["); Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write("0x0"); Console.ForegroundColor = ConsoleColor.White; Console.Write("]\n");
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray; Console.Write("BOOTLOADER"); Console.ForegroundColor = ConsoleColor.White; Console.Write(": ["); Console.ForegroundColor = ConsoleColor.Red; Console.Write("ERROR"); Console.ForegroundColor = ConsoleColor.White; Console.Write("] KERNEL_PANIC\n");
                Console.WriteLine(e.ToString());
            }
        }

    }
}