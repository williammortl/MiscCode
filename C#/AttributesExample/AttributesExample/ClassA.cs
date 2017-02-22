namespace AttributesExample
{

    /// <summary>
    /// Testing attributes
    /// </summary>
    [Marked(IsMarked = false)]
    class ClassA : ITestInt
    {

        /// <summary>
        /// Adds 2 numbers
        /// </summary>
        /// <param name="num1">num1</param>
        /// <param name="num2">num2</param>
        /// <returns>num1 + num2</returns>
        public int add(int num1, int num2)
        {
            return num1 + num2;
        }

        /// <summary>
        /// Subtracts num2 from num1
        /// </summary>
        /// <param name="num1">num1</param>
        /// <param name="num2">num2</param>
        /// <returns>num1 - num2</returns>
        [Marked(IsMarked = true)]
        public int sub(int num1, int num2)
        {
            return num1 - num2;
        }
    }
}
