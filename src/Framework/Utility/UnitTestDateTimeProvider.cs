using System;

namespace Framework.Utility
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

        private readonly DateTime _specifiedDateTime;
        private readonly DateTime _specifiedDateTimeUtc;


        public DateTime CurrentUtcDateTime => _specifiedDateTimeUtc;

        public DateTime CurrentDateTime => _specifiedDateTime;

        public DateTime Today => _specifiedDateTime.Date;
    }
}
