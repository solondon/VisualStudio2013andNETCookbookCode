using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSFakesSample
{
    public class Logger : ILogger
    {
        bool isLoggerEnabled;
        public bool IsLoggerEnabled
        {
            get
            {
                return this.isLoggerEnabled;
            }
            set
            {
                this.isLoggerEnabled = value;
            }
        }

        public void Log(string message)
        {
            var logHeader = "Fakes Sample App Logger";
            var logCategory = "Application";

            if (!EventLog.SourceExists(logHeader))
                EventLog.CreateEventSource(logHeader, logCategory);

            EventLog.WriteEntry(logHeader, message);
        }
    }
}
