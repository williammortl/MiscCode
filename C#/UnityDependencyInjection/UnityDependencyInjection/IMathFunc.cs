namespace UnityDependencyInjection
{

    /// <summary>
    /// Interface for a math value
    /// </summary>
    public interface IMathFunc
    {

        /// <summary>
        /// Math function
        /// </summary>
        /// <param name="val1">first value</param>
        /// <param name="val2">second value</param>
        /// <returns>result</returns>
        double MathFunc(double val1, double val2);
    }
}
