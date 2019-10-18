using System;
using System.Collections.Generic;
using System.Text;

namespace Framework
{
    /// <summary>
    /// Represents the results of a service operation that needs to return some sort of data
    /// </summary>
    /// <remarks>
    /// This class cannot be instantiated irectly by application code.  Instead use one
    /// of the provided static factory methods to create the appropriate result object
    /// </remarks>
    /// <typeparam name="T"></typeparam>
    public class EntityResult<T> 
    {

        internal EntityResult(ResultCode status, string errorMessage, T entity)           
        {
            ResultStatus = status;
            ErrorMessage = errorMessage;
            Value = entity;
        }

        #region Properties

        public ResultCode ResultStatus { get; private set; }

        public string ErrorMessage { get; private set; }

        public T Value { get; private set; }

        #endregion


        #region Static Factory Helpers


        public static EntityResult<T> Success(T value)
        {
            return new EntityResult<T>(ResultCode.SUCCESS, string.Empty, value);
        }

        public static EntityResult<T> Failure(string errorMessage)
        {
            return new EntityResult<T>(ResultCode.FAILURE, errorMessage, default(T));
        }


        public static EntityResult<T> NotFound(string errorMessage)
        {
            return new EntityResult<T>(ResultCode.NOTFOUND, errorMessage, default(T));
        }


        public static EntityResult<T> Forbidden(string errorMessage) 
        {
            return new EntityResult<T>(ResultCode.FORBIDDEN, errorMessage, default(T));
        }

        public static EntityResult<T> Error(string errorMessage)
        {
            return new EntityResult<T>(ResultCode.ERROR, errorMessage, default(T));
        }

        #endregion

    }
}
