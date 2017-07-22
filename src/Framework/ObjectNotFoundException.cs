using System;
using System.Collections.Generic;
using System.Text;

namespace Framework
{
    public class ObjectNotFoundException : Exception
    {

        public ObjectNotFoundException(String message) : base(message)
        {
            
        }

    }
}
