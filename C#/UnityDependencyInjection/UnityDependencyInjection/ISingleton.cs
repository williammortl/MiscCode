namespace UnityDependencyInjection
{

    /// <summary>
    /// Interface for a singleton
    /// </summary>
    public interface ISingleton
    {

        /// <summary>
        /// Sets a singleton value
        /// </summary>
        /// <param name="val">the value</param>
        void SetVal(string val);

        /// <summary>
        /// Retrieves the singleton value
        /// </summary>
        /// <returns>the value</returns>
        string GetVal();
    }
}
