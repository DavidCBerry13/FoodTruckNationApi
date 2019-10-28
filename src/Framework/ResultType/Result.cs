using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.ResultType
{
    /// <summary>
    /// Represents the result of an operation that does not need to return data
    /// </summary>
    /// <remarks>
    /// <para>
    /// A result type is an idea from functional programming , a monadic type
    /// that can hold a return value or an error code.  To check if the operation
    /// (method) succeeded, you check the IsSuccess property.  If this property
    /// is false, you can get an object describing the error that occurred from
    /// the Error property.  The object in the Error property must inherit from
    /// the Error class (also in this namespace).  By returning different types
    /// of Error objects, calling code can make use of pattern matching in C#
    /// to handle different errors as needed.
    /// </para>
    /// <para>
    /// Result objects cannot be instantiated directly.  Insted use the static factory
    /// methods of Success() or Failure() as appropriate.  
    /// </para>
    /// </remarks>
    public class Result
    {

        /// <summary>
        /// Indicates if the operation was successful or not
        /// </summary>
        public bool IsSuccess { get; private set; }

        /// <summary>
        /// Property containing an Error object if the operation failed
        /// </summary>
        public Error Error { get; private set; }


        /// <summary>
        /// Creates a new result object.  This constructor is protected to insure
        /// Result objects are only created by their static factory methods
        /// </summary>
        /// <param name="success">A bool indicating the success of failure of the call</param>
        /// <param name="error">An Error object representing an error that occured.  Should be null for successful operations</param>
        protected Result(bool success, Error error)
        {
            IsSuccess = success;
            Error = error;
        }


        /// <summary>
        /// Creates a successful Result object that does not return any data
        /// </summary>
        /// <returns></returns>
        public static Result Success()
        {
            return new Result(true, NoError.NO_ERROR);
        }


        /// <summary>
        /// Creates a successful result object that returns an object of type TValue
        /// </summary>
        /// <typeparam name="TValue">The type of the return data wrapped by thei Result object</typeparam>
        /// <param name="value">A TValue object of the product of the operation</param>
        /// <returns>A Result object where IsSuccess is true wrapping the provided TValue object</returns>
        public static Result<TValue> Success<TValue>(TValue value)
        {
            return new Result<TValue>(true, value, NoError.NO_ERROR);
        }


        /// <summary>
        /// Creates a result object representing a failure with a message indicating what went wrong
        /// </summary>
        /// <param name="errorMessage">A string describng the error.  This String will be encapulated in an Error object within the Result object</param>
        /// <returns></returns>
        public static Result Failure(string errorMessage)
        {
            return new Result(false, new Error(errorMessage));
        }


        public static Result Failure(Error error)
        {
            return new Result(false, error);
        }

        /// <summary>
        /// Creates a result object representing a failure with a message indicating what went wrong
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="error"></param>
        /// <returns></returns>
        public static Result<TValue> Failure<TValue>(Error error) 
        {
            return new Result<TValue>(false, default(TValue), error);
        }


        public static Result<TValue> Failure<TValue>(string errorMessage)
        {
            return new Result<TValue>(false, default(TValue), new Error(errorMessage));
        }

    }




    /// <summary>
    /// Represents a Result type object for a method that returns data
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public class Result<TValue> : Result
    {

        public TValue Value { get; private set; }


        protected internal Result(bool success, TValue value, Error error) : base(success, error)
        {
            Value = value;
        }

    }



    

}
