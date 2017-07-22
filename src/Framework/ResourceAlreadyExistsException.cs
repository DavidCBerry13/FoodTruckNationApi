using System;
using System.Collections.Generic;
using System.Text;

namespace Framework
{

    /// <summary>
    /// 
    /// </summary>
    public class ResourceAlreadyExistsException : Exception
    {

        public ResourceAlreadyExistsException(String message) : base(message)
        {
            
        }
    }
}
