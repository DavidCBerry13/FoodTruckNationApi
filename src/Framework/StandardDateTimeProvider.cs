using System;
using System.Collections.Generic;
using System.Text;

namespace Framework
{

    /// <summary>
    /// Standard DateTimeProvider that just returns the values from DateTime.Now/DateTime.UtcNow as the current Date/Time
    /// </summary>
    public class StandardDateTimeProvider : IDateTimeProvider
    {
        public DateTime CurrentDateTime => DateTime.Now;

        public DateTime CurrentUtcDateTime => DateTime.UtcNow;
    }
}
