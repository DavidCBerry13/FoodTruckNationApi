using System;
using System.Collections.Generic;
using System.Text;

namespace FoodTruckNation.AppServices.Framework
{
    public class Result
    {

        internal Result(ResultCode status, String errorMessage)
        {
            this.ResultStatus = status;
            this.ErrorMessage = errorMessage;
        }

        public ResultCode ResultStatus { get; private set; }

        public String ErrorMessage { get; private set; }




        public static Result Success()
        {
            return new Result(ResultCode.SUCCESS, String.Empty);
        }

        public static Result Failure(String errorMessage)
        {
            return new Result(ResultCode.FAILURE, errorMessage);
        }


        public static Result NotFound(String errorMessage)
        {
            return new Result(ResultCode.NOTFOUND, errorMessage);
        }

        public static Result Forbidden(String errorMessage)
        {
            return new Result(ResultCode.FORBIDDEN, errorMessage);
        }

        public static Result Error(String errorMessage)
        {
            return new Result(ResultCode.ERROR, errorMessage);
        }



    }
}
