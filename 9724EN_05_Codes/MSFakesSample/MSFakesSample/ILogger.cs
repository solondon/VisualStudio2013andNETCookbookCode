using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSFakesSample
{
    public interface ILogger
    {
        bool IsLoggerEnabled { get; set; }

        void Log(string message);
    }
}
