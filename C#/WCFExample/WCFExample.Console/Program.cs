namespace WCFExample.Console
{
    using Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Console application
    /// </summary>
    class Program
    {

        /// <summary>
        /// Main entry point
        /// </summary>
        /// <param name="args">command line args</param>
        static void Main(string[] args)
        {
            Logger.LogTrace("Starting console application...");
            Console.ReadLine();
            Logger.LogTrace("Stopping console application.");
        }
    }
}
