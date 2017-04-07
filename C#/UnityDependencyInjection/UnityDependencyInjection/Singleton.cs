namespace UnityDependencyInjection
{

    /// <summary>
    /// Concrete singleton class
    /// </summary>
    public class Singleton : ISingleton
    {

        /// <summary>
        /// value to store
        /// </summary>
        private string theValue;

        /// <summary>
        /// Default constructor
        /// </summary>
        public Singleton()
        {
            this.theValue = string.Empty;
        }

        /// <summary>
        /// Sets a singleton value
        /// </summary>
        /// <param name="val">the value</param>
        public void SetVal(string val)
        {
            this.theValue = val;
        }

        /// <summary>
        /// Retrieves the singleton value
        /// </summary>
        /// <returns>the value</returns>
        public string GetVal()
        {
            return this.theValue;
        }
    }
}
