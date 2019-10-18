using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Exceptions
{

    /// <summary>
    /// Exception to be thrown when a requested object cannot be found
    /// </summary>
    /// <remarks>
    /// This exception is intended to be used in the service layer and used to communicate back up 
    /// to an application that the requested object could not be found.  By defining and always using
    /// a standard exception like this one for these scenarios, one can then write exception handling 
    /// code that handles these scenarios and this code can be applied as a filter across all classes
    /// rather than having to implement the same try/catch/finally blocks for each controller action
    /// </remarks>
    public class ObjectNotFoundException : Exception
    {

        /// <summary>
        /// Creates a new ObjectNotFoundException with the specified message
        /// </summary>
        /// <remarks>
        /// In this project, the message in this exception will ultimately be returned to the client so
        /// you want to make the message human readable
        /// </remarks>
        /// <param name="message">A message containing information about the identifier(s) of the object
        /// that could not be found (i.e. 'No employee with the id of 123 could be found')</param>
        public ObjectNotFoundException(String message) : base(message)
        {
            
        }

    }
}
