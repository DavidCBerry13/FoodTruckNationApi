using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Exceptions
{

    /// <summary>
    /// Exception type to be used when a service cannot complete a requested operation
    /// because the data supplied to the service is invalid in some way
    /// </summary>
    public class InvalidDataException : Exception
    {

        /// <summary>
        /// Creates a new InvalidDataException with the specified message
        /// </summary>
        /// <param name="message">A String of a message that describes the reason why the operation could not be completed</param>
        public InvalidDataException(String message) : base(message)
        {

        }

    }
}
