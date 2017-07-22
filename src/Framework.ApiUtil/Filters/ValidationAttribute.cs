using Framework.ApiUtil.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.ApiUtil.Filters
{

    /// <summary>
    /// Action filter to validate incoming request data
    /// </summary>
    public class ValidationAttribute : ActionFilterAttribute
    {

        /// <summary>
        /// Checks if the ModelState is valid before the action is executed.  If not, an HTTP 400 (Bad Request) 
        /// response is returned with details of the validation failure
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            base.OnActionExecuting(actionContext);

            if (!actionContext.ModelState.IsValid)
            {
                var errors = actionContext.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .Select(e => new RequestErrorModel() { Field = e.Key, Message = e.Value.Errors.First().ErrorMessage })                    
                    .ToArray();

                actionContext.Result = new BadRequestObjectResult(errors);
            }

        }



    }
}
