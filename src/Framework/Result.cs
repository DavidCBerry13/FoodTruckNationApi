using System;
using System.Collections.Generic;
using System.Text;

namespace Framework
{
    public class Result
    {

        internal Result(ResultCode status, string errorMessage)
        {
            ResultStatus = status;
            ErrorMessage = errorMessage;
        }

        public ResultCode ResultStatus { get; private set; }

        public string ErrorMessage { get; private set; }




        public static Result Success()
        {
            return new Result(ResultCode.SUCCESS, string.Empty);
        }

        public static Result Failure(string errorMessage)
        {
            return new Result(ResultCode.FAILURE, errorMessage);
        }


        public static Result NotFound(string errorMessage)
        {
            return new Result(ResultCode.NOTFOUND, errorMessage);
        }

        public static Result Forbidden(string errorMessage)
        {
            return new Result(ResultCode.FORBIDDEN, errorMessage);
        }

        public static Result Error(string errorMessage)
        {
            return new Result(ResultCode.ERROR, errorMessage);
        }



    }
}
