using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.ApiUtil.Results
{
    public class ConflictObjectResult : ObjectResult
    {

        public ConflictObjectResult(object value) : base(value)
        {
            StatusCode = StatusCodes.Status409Conflict;
        }
    }
}
