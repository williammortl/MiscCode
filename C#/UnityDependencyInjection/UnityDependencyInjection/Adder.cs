namespace UnityDependencyInjection
{

    /// <summary>
    /// Adder as an IMathFunc
    /// </summary>
    public class Adder : IMathFunc
    {

        /// <summary>
        /// Math function
        /// </summary>
        /// <param name="val1">first value</param>
        /// <param name="val2">second value</param>
        /// <returns>result</returns>
        public double MathFunc(double val1, double val2)
        {
            return val1 + val2;
        }
    }
}
