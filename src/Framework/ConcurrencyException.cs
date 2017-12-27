using System;
using System.Collections.Generic;
using System.Text;

namespace Framework
{


    public abstract class ConcurrencyException : Exception
    {
        protected ConcurrencyException(String message, Object value) : base(message)
        {

        }

        public Object CurrentObject { get; set; }

    }


    public class ConcurrencyException<T> : ConcurrencyException
    {
        public ConcurrencyException(String message, T currentObject) 
            : base(message, currentObject)
        {
            this.TypedObject = currentObject;
        }


        public T TypedObject { get; private set; }

    }
}
