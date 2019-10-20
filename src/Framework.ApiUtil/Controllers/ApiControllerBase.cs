using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Framework.ApiUtil.Models;
using Framework.Exceptions;
using Framework.ResultType;

namespace Framework.ApiUtil.Controllers
{

    /// <summary>
    /// Base controller class to encapsulate functionality common to all API controllers
    /// </summary>
    [ApiController]
    public abstract class ApiControllerBase : ControllerBase, IActionFilter
    {


        public ApiControllerBase(ILogger<ApiControllerBase> logger, IMapper mapper) : base()
        {
            _logger = logger;
            _mapper = mapper;
        }


        protected ILogger _logger;
        protected IMapper _mapper;







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
            var model = _mapper.Map<TObject, TModel>(conflictingObject);
            var message = new ConcurrencyErrorModel<TModel>()
            {
                Message = concurrencyException.Message,
                CurrentObject = model
            };
            return new ConflictObjectResult(message);
        }


        protected virtual ActionResult CreateConcurrencyConflictErrorResult<TEntity, TModel>(ConcurrencyError<TEntity> concurrencyError)
        {
            var model = _mapper.Map<TEntity, TModel>(concurrencyError.ConflictingObject);
            var message = new ConcurrencyErrorModel<TModel>()
            {
                Message = concurrencyError.Message,
                CurrentObject = model
            };
            return new ConflictObjectResult(message);
        }

        protected virtual ActionResult CreateObjectExistsConflictErrorResult<TEntity, TModel>(ObjectAlreadyExistsError<TEntity> objectExistsError)
        {
            var model = _mapper.Map<TEntity, TModel>(objectExistsError.ExistingObject);
            var message = new ConcurrencyErrorModel<TModel>()
            {
                Message = objectExistsError.Message,
                CurrentObject = model
            };
            return new ConflictObjectResult(message);
        }


        /// <summary>
        /// Creates an HTTP 200 (OK) response for successful result objects mapping the entity object(s) to mapping objects
        /// or creates an appropriate Error response  if the provided result object is not marked as successful
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        protected ActionResult CreateResponse<TEntity, TModel>(Result<TEntity> result)
        {
            Func<TEntity, ActionResult> function = (entity) =>
            {
                var model = _mapper.Map<TEntity, TModel>(result.Value);
                return Ok(model);
            };

            return CreateResponse<TEntity, TModel>(result, function);
        }


        protected ActionResult CreateResponse<TEntity, TModel>(Result<TEntity> result, Func<TEntity, ActionResult> successFunction)
        {
            if (result.IsSuccess)
            {
                return successFunction(result.Value);
            }
            else
            {
                return MapErrorResult<TEntity, TModel>(result);
            }
        }


        protected internal ActionResult MapErrorResult<TEntity, TModel>(Result result)
        {
            switch (result.Error)
            {
                case InvalidDataError error:
                    return BadRequest(new ApiMessageModel() { Message = error.Message });
                case ObjectNotFoundError error:
                    return NotFound(new ApiMessageModel() { Message = error.Message });
                case ObjectAlreadyExistsError<TEntity> error:
                    return CreateObjectExistsConflictErrorResult<TEntity, TModel>(error);
                case ConcurrencyError<TEntity> error:
                    return CreateConcurrencyConflictErrorResult<TEntity, TModel>(error);
                default:
                    return this.InternalServerError(new ApiMessageModel() { Message = "An unexpected error has occured.  The error has been logged and is being investigated" });
            }
        }


        protected internal ActionResult MapErrorResult(Result result)
        {
            switch (result.Error)
            {
                case InvalidDataError e:
                    return BadRequest(new ApiMessageModel() { Message = e.Message });                    
                case ObjectNotFoundError e:
                    return NotFound(new ApiMessageModel() { Message = e.Message });
                default:
                    return this.InternalServerError(new ApiMessageModel() { Message = "An unexpected error has occured.  The error has been logged and is being investigated" });
            }
        }


        [NonAction]
        public void OnActionExecuting(ActionExecutingContext context)
        {
            context.HttpContext.Items.Add("URL_HELPER", Url);
        }

        [NonAction]
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }
    }
}
