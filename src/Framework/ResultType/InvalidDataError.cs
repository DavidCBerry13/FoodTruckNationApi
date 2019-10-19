using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.ResultType
{
    public class InvalidDataError : Error
    {

        public InvalidDataError(String message) : base(message)
        {

        }

    }
}
