using System;
using System.Collections.Generic;
using System.Text;

namespace Framework
{
    public class ResultCode
    {

        private ResultCode(String name, bool success)
        {
            this.Name = name;
            this.IsSuccessful = success;
        }


        public String Name { get; private set; }

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
