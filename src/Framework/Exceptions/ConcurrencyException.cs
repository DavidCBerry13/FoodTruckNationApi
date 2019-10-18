using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Exceptions
{


    public abstract class ConcurrencyException : Exception
    {
        protected ConcurrencyException(string message, object value) : base(message)
        {

        }

        public object CurrentObject { get; set; }

    }


    public class ConcurrencyException<T> : ConcurrencyException
    {
        public ConcurrencyException(string message, T currentObject) 
            : base(message, currentObject)
        {
            TypedObject = currentObject;
        }


        public T TypedObject { get; private set; }

    }
}
