using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Framework.ApiUtil.Results
{

    /// <summary>
    /// Represents an HTTP 500 Internal Server Error response with a provided message 
    /// </summary>
    /// <remarks>
    /// Caution should be used to not disclose too many details in the error message since
    /// this information can be used by hackers to gain insights about how the system worked
    /// and why an error occured
    /// </remarks>
    public class InternalServerErrorObjectResult : ObjectResult
    {

        public InternalServerErrorObjectResult(object value) : base(value)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }

    }
}
