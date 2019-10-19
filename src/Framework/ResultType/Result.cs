using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.ResultType
{

    public class Result
    {

        public bool IsSuccess { get; private set; }

        public Error Error { get; private set; }

        protected Result(bool success, Error error)
        {
            IsSuccess = success;
            Error = error;
        }



        public static Result Success()
        {
            return new Result(true, NoError.NO_ERROR);
        }


        public static Result<TValue> Success<TValue>(TValue value)
        {
            return new Result<TValue>(true, value, NoError.NO_ERROR);
        }


        public static Result Failure(string errorMessage)
        {
            return new Result(false, new Error(errorMessage));
        }

        public static Result Failure<TValue>(Error error) 
        {
            return new Result<TValue>(false, default(TValue), error);
        }



        public static Result<TValue> Failure<TValue>(string errorMessage)
        {
            return new Result<TValue>(false, default(TValue), new Error(errorMessage));
        }


    }





    public class Result<TValue> : Result
    {

        public TValue Value { get; private set; }


        protected internal Result(bool success, TValue value, Error error) : base(success, error)
        {
            Value = value;
        }

    }



    

}
