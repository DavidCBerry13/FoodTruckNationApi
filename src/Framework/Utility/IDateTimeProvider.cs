using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Utility
{

    /// <summary>
    /// Simple interface to wrap getting the current date, so we can inject our own date as needed for testing and not be 
    /// directly dependant on DateTime.Now()
    /// </summary>
    public interface IDateTimeProvider
    {

        DateTime CurrentUtcDateTime { get; }

        DateTime CurrentDateTime { get;  }

        DateTime Today { get; }
    }
}
