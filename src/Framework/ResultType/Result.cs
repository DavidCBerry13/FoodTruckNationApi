using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.ResultType
{

    public class Result
    {

        protected Result()
        {

        }

        public static Result<Error> Success()
        {
            return new Result<Error>(true, NoError.NO_ERROR);
        }


        public static Result<TValue, Error> Success<TValue>(TValue value)
        {
            return new Result<TValue, Error>(true, value, NoError.NO_ERROR);
        }


        public static Result<Error> Failure(string errorMessage)
        {
            return new Result<Error>(false, new Error(errorMessage));
        }

        public static Result<TError> Failure<TError>(TError error) where TError : Error
        {
            return new Result<TError>(false, error);
        }



        public static Result<TValue, Error> Failure<TValue>(string errorMessage)
        {
            return new Result<TValue, Error>(false, default(TValue), new Error(errorMessage));
        }

        public static Result<TValue, TError> Failure<TValue, TError>(TError error) where TError : Error
        {
            return new Result<TValue, TError>(false, default(TValue), error);
        }


    }


    public class Result<TError> where TError : Error
    {

        protected internal Result(bool success, TError error)
        {
            IsSuccess = success;
            Error = error;
        }


        public bool IsSuccess { get; private set; }

        public TError Error { get; private set; }

    }



    public class Result<TValue, TError> : Result<TError> where TError : Error
    {

        public TValue Value { get; private set; }


        protected internal Result(bool success, TValue value, TError error) : base(success, error)
        {
            Value = value;
        }

    }



    

}
