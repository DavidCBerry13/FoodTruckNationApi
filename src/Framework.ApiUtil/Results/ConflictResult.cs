using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.ApiUtil.Results
{
    public class ConflictResult : StatusCodeResult
    {

        public ConflictResult() : base(StatusCodes.Status409Conflict)
        {

        }

    }
}
