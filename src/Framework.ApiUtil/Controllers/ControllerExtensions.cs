using System;
using Microsoft.AspNetCore.Mvc;
using Framework.ApiUtil.Results;

namespace Framework.ApiUtil.Controllers
{


    /// <summary>
    /// Class containing controller extension methods, mostly helpers for returning difference status code results
    /// </summary>
    public static class ControllerExtensions
    {

        /// <summary>
        /// Returns a forbidden (HTTP 403) result with no additional data
        /// </summary>
        /// <param name="controller">A Controller object this method is being called against</param>
        /// <returns>A ForbiddenResult object representing an HTTP 403 response</returns>
        public static IActionResult Forbidden(this Controller controller)
        {
            return new ForbiddenResult();
        }

        /// <summary>
        /// Returns a forbidden (HTTP 403) result with the speccifed value object included in the response
        /// </summary>
        /// <param name="controller">A Controller object this method is being called against</param>
        /// <param name="value">A FobiddenObjectResult object representing an HTTP 403 response with a payload describing 
        /// why the request was rejected</param>
        /// <returns></returns>
        public static IActionResult Forbidden(this Controller controller, object value)
        {
            return new ForbiddenObjectResult(value);
        }


        public static IActionResult InternalServerError(this Controller controller)
        {
            return new InternalServerErrorResult();
        }

        public static IActionResult InternalServerError(this Controller controller, object value)
        {
            return new InternalServerErrorObjectResult(value);
        }


    }
}
