using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSFakesSample
{
    public class DiagonizeStubs
    {
        public string GetEventName(ILogger logger)
        {
            if (logger.IsLoggerEnabled)
                logger.Log("GetEventName method is called");

            return "Sample Event";
        }
    }
}
