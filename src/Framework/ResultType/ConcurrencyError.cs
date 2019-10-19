using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.ResultType
{
    public class ConcurrencyError<T> : Error
    {

        public T ConflictingObject { get; private set; }


        public ConcurrencyError(String message, T conflictingObject) : base(message)
        {
            ConflictingObject = conflictingObject;
        }

    }
}
