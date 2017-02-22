namespace AttributesExample
{
    using System;

    /// <summary>
    /// Testing a custom attribute, second attribute
    /// </summary>
    public class CheckedAttribute : Attribute
    {

        /// <summary>
        /// Is this checked?
        /// </summary>
        public bool IsChecked = false;

        /// <summary>
        /// To string
        /// </summary>
        /// <returns>string representation</returns>
        public override string ToString()
        {
            return string.Format("This class/method is {0}checked.", (this.IsChecked == true) ? string.Empty : "NOT ");
        }
    }
}
