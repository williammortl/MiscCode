namespace WCFClient
{
    using ExampleService;
    using System;

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

            // create client
            ExampleServiceClient client = new ExampleServiceClient();

            // make calls and print results
            string tmp = client.GetData(17);
            Console.WriteLine(tmp);
            CompositeType ct = new ExampleService.CompositeType() { BoolValue = true, StringValue = "goodbye" };
            tmp = client.GetDataUsingDataContract(ct).StringValue;
            Console.WriteLine(tmp);

            // wait for key
            string s = Console.ReadLine();
        }
    }
}
