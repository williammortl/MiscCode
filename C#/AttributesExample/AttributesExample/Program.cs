namespace AttributesExample
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
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
        /// <param name="args">arguments</param>
        static void Main(string[] args)
        {

            // dump all attributes for ClassA
            Program.DumpAttributes(typeof(ClassA));
            foreach(MethodInfo mi in (typeof(ClassA).GetMethods()))
            {
                Program.DumpAttributes(mi);
            }
            Console.WriteLine();

            // dump all attributes for ClassB
            Program.DumpAttributes(typeof(ClassB));
            foreach (MethodInfo mi in (typeof(ClassB).GetMethods()))
            {
                Program.DumpAttributes(mi);
            }
            Console.WriteLine();

            // wait
            string s = Console.ReadLine();
        }

        /// <summary>
        /// Dumps all attributes
        /// </summary>
        /// <param name="member">the member to examine</param>
        private static void DumpAttributes(MemberInfo member)
        {
            foreach (object attr in member.GetCustomAttributes(true))
            {
                Console.WriteLine(string.Format("Member: {0} - Attribute: {1}", member.Name, attr.ToString()));
            }
        }
    }
}
