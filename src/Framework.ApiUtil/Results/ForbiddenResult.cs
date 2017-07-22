using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Framework.ApiUtil.Results
{

    /// <summary>
    /// Returns a status 403 (Forbidden) result with no message
    /// </summary>
    public class ForbiddenResult : StatusCodeResult
    {

        public ForbiddenResult() : base(StatusCodes.Status403Forbidden)
        {

        }

    }
}
