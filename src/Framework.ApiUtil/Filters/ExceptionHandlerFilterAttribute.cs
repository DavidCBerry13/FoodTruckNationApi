using Framework.ApiUtil.Models;
using Framework.ApiUtil.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace Framework.ApiUtil.Filters
{

    /// <summary>
    /// Common Exception filter for handling exceptions.  Looks for standard exception types and returns the appropriate
    /// HTTP response based on those types
    /// </summary>
    public class ExceptionHandlerFilterAttribute : ExceptionFilterAttribute
    {
        public ExceptionHandlerFilterAttribute(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger(GetType().FullName);
        }

        private readonly ILogger _logger;



        

        /// <summary>
        /// Method that is executed when an uncaught exception has been thrown from a controller.  This method provides
        /// a centralized place where we can handle and log these exceptions
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
            Exception ex = context.Exception;    // For convenience

            string logMessage = $"{ex.GetType().Name} occured in action {context.ActionDescriptor.DisplayName} - {ex.Message}";

            if (ex is InvalidDataException)
            {
                _logger.LogError(new EventId(400), ex, logMessage);

                var message = new ApiMessageModel() { Message = ex.Message };
                context.Result = new BadRequestObjectResult(message);
            }
            else if (ex is SecurityException)
            {
                _logger.LogError(new EventId(403), ex, logMessage);

                var message = new ApiMessageModel() { Message = "Access to this resource is forbidden" };
                context.Result = new ForbiddenObjectResult(message);
            }
            else if (ex is ResourceAlreadyExistsException)
            {
                _logger.LogWarning(new EventId(409), ex, logMessage);

                var message = new ApiMessageModel() { Message = ex.Message };
                context.Result = new ConflictObjectResult(message);
            }
            else if (ex is ObjectNotFoundException)
            {
                _logger.LogWarning(new EventId(404), ex, logMessage);

                var message = new ApiMessageModel() { Message = ex.Message };
                context.Result = new NotFoundObjectResult(message);
            }
            else
            {
                _logger.LogError(new EventId(500), ex, logMessage);

                var message = new ApiMessageModel() { Message = "An error has occurred on the server.  The error has been logged so it can be investigated by our support teams" };
                context.Result = new InternalServerErrorObjectResult(message);
            }

            context.ExceptionHandled = true;
        }



    }
}
