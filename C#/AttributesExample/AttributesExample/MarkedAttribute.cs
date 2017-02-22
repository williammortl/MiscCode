namespace AttributesExample
{
    using System;

    /// <summary>
    /// Testing a custom attribute
    /// </summary>
    public class MarkedAttribute : Attribute
    {

        /// <summary>
        /// Is this marked?
        /// </summary>
        public bool IsMarked = false;

        /// <summary>
        /// To string
        /// </summary>
        /// <returns>string representation</returns>
        public override string ToString()
        {
            return string.Format("This class/method is {0}marked.", (this.IsMarked == true) ? string.Empty : "NOT ");
        }
    }
}
