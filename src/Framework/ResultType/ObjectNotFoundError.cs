using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.ResultType
{
    public class ObjectNotFoundError : Error
    {


        public ObjectNotFoundError(String message) : base(message)
        {

        }
    }
}
