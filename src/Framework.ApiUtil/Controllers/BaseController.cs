using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Filters;
using Framework.ApiUtil.Models;
using Framework.ApiUtil.Results;

namespace Framework.ApiUtil.Controllers
{

    /// <summary>
    /// Base controller class to encapsulate functionality common to all API controllers
    /// </summary>
    public abstract class BaseController : Controller
    {


        public BaseController(ILogger<BaseController> logger, IMapper mapper) : base()
        {
            this.logger = logger;
            this.mapper = mapper;
        }


        protected ILogger logger;
        protected IMapper mapper;



        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            context.HttpContext.Items.Add("URL_HELPER", this.Url);
        }


        /// <summary>
        /// Helper Method to create a ConflictObjectResult object for when a concurrency exception occurs
        /// </summary>
        /// <remarks>
        /// This method will take in a ConcurrencyException of type T and translate it into a 
        /// ConflictObjectResult object with the CurrentObject property being a ViewModel representation
        /// of the current (conflicting) object.  This way the client can see the conflicting object
        /// such that they can make any needed changes and resubmit.
        /// </remarks>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="concurrencyException"></param>
        /// <returns></returns>
        protected virtual IActionResult CreateConcurrencyConflictErrorResult<TModel, TObject>(ConcurrencyException<TObject> concurrencyException)
        {
            var conflictingObject = concurrencyException.TypedObject;
            var model = this.mapper.Map<TObject, TModel>(conflictingObject);
            var message = new ConcurrencyErrorModel<TModel>()
            {
                Message = concurrencyException.Message,
                CurrentObject = model
            };
            return new ConflictObjectResult(message);
        }
    }
}
