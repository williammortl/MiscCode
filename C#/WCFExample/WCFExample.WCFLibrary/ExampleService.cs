using System;
using WCFExample.Logging;

namespace WCFExample.WCFLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class ExampleService : IExampleService
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            composite.StringValue += (composite.BoolValue == true) ? " - you sent true" : " - you sent false";
            return composite;
        }
    }
}
