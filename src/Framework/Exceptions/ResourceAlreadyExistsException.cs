using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Exceptions
{

    /// <summary>
    /// Exception to be thrown when the application attempts to create an object or resource that already exists
    /// </summary>
    /// <remarks>
    /// This exception will most often times be thrown from a service layer, though could also be thrown from
    /// within a domain object as well.  It indicates that the resource the user is attempting to create already
    /// exists, and therefore the existing operation cannot be completed.  It is up to the service layer or domain
    /// layer code to determine that the new resource is the same/in conflict with an existing resource.
    /// <para>
    /// The idea of creating this standard exception to be used whenever this type of conflict occurs is so 
    /// that we can then write cross cutting code in a construct like a custom ExceptionFilterAttribute to handle
    /// these exceptions in a standard way (like returning a 409 conflict HTTP response) rather than haiving
    /// to write try/catch/finally blocks for every controller action
    /// </para>
    /// </remarks>
    public class ResourceAlreadyExistsException : Exception
    {

        /// <summary>
        /// Creates a new ResourceAlreadyExistsException with the specified message
        /// </summary>
        /// <param name="message">A String of a message that describes the conflict with an existing resource</param>
        public ResourceAlreadyExistsException(String message) : base(message)
        {
            
        }
    }
}
