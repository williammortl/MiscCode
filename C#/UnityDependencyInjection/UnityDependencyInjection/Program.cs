namespace UnityDependencyInjection
{
    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.Configuration;
    using System;
    using System.Configuration;

    /// <summary>
    /// The main entry point for the application
    /// </summary>
    class Program
    {

        /// <summary>
        /// Section for unity
        /// </summary>
        public const string SECTION_UNITY = "unity";

        /// <summary>
        /// app settings key for the container to use
        /// </summary>
        private const string APP_SETTINGS_KEY_CONTAINER = "ContainerToUse";

        /// <summary>
        /// The one and only unity container
        /// </summary>
        private static UnityContainer container = null;

        /// <summary>
        /// Main entry funciton
        /// </summary>
        /// <param name="args">command line args</param>
        static void Main(string[] args)
        {

            // initial resolutions
            ISingleton s = Program.GetContainer().Resolve<ISingleton>();
            IMathFunc m = Program.GetContainer().Resolve<IMathFunc>();
            s.SetVal(m.MathFunc(1.1, 2.2).ToString());
            s = null;

            // test singleton
            s = Program.GetContainer().Resolve<ISingleton>();
            Console.WriteLine(string.Format("Singleton value: {0} (should be 3.3)", s.GetVal()));
            Console.ReadLine();
        }

        /// <summary>
        /// Returns the Unity container, can throw Exception
        /// </summary>
        /// <param name="containerName">the name of the container to load</param>
        /// <returns>the core container</returns>
        static UnityContainer GetContainer(String containerName)
        {

            // load if neccessary
            if (Program.container == null)
            {
                try
                {
                    Program.container = new UnityContainer();
                    UnityConfigurationSection section = (UnityConfigurationSection)ConfigurationManager.GetSection(Program.SECTION_UNITY);
                    section.Configure(Program.container, containerName);
                }
                catch (Exception e)
                {
                    throw new Exception(string.Format("Error getting container {0}: {1}", containerName, e.ToString()));
                }
            }

            return Program.container;
        }

        /// <summary>
        /// Returns the Unity container specified in the app.config file, can throw Exception
        /// </summary>
        /// <returns>the core container</returns>
        static UnityContainer GetContainer()
        {
            return Program.GetContainer(ConfigurationManager.AppSettings[Program.APP_SETTINGS_KEY_CONTAINER]);
        }
    }
}
