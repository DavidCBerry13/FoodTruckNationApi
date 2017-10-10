using System;
using System.Collections.Generic;
using System.Text;

namespace Framework
{

    /// <summary>
    /// An implementation of the IDateTimeProvider interface designed to support unit testing, 
    /// where the current date time will be set to a constant value specified in the constructor
    /// </summary>
    public class UnitTestDateTimeProvider : IDateTimeProvider
    {
        public UnitTestDateTimeProvider(DateTime specifiedDateTime)
        {
            _specifiedDateTime = specifiedDateTime;
            _specifiedDateTimeUtc = specifiedDateTime.ToUniversalTime();
        }

        private DateTime _specifiedDateTime;
        private DateTime _specifiedDateTimeUtc;


        public DateTime CurrentUtcDateTime => _specifiedDateTimeUtc;

        public DateTime CurrentDateTime => _specifiedDateTime;
    }
}
