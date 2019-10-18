using System;
using System.Collections.Generic;
using System.Text;

namespace Framework
{
    public class ResultCode
    {

        private ResultCode(string name, bool success)
        {
            Name = name;
            IsSuccessful = success;
        }


        public string Name { get; private set; }

        public bool IsSuccessful { get; private set; }


        public static implicit operator bool(ResultCode rc)
        {
            return rc.IsSuccessful;
        }



        public static readonly ResultCode SUCCESS = new ResultCode("Success", true);

        public static readonly ResultCode FAILURE = new ResultCode("Failure", false);

        public static readonly ResultCode NOTFOUND = new ResultCode("Not Found", false);

        public static readonly ResultCode FORBIDDEN = new ResultCode("Forbidden", false);

        public static readonly ResultCode ERROR = new ResultCode("Error", false);

    }
}
