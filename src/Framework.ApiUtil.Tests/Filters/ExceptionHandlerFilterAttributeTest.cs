using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Framework.ApiUtil.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Filters;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Framework.ApiUtil.Models;
using System.Security;
using Framework.ApiUtil.Results;
using Framework.Exceptions;

namespace Framework.ApiUtil.Tests.Filters
{
    public class ExceptionHandlerFilterAttributeTest
    {


        [Fact]
        public void TestObjectNotFoundExceptionReturnsNotFoundResult()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<ExceptionHandlerFilterAttribute>>();
            var logger = loggerMock.Object;

            var loggerFactoryMock = new Mock<ILoggerFactory>();
           
            loggerFactoryMock.Setup(
                lf => lf.CreateLogger(It.IsAny<String>()))
               .Returns(logger);

            var notFoundMessage = "The object was not found";
            var exception = new ObjectNotFoundException(notFoundMessage);

            var httpContextMock = new Mock<HttpContext>();
            var routeData = new RouteData();             
            var actionDescriptor = new ActionDescriptor();
            var actionContext = new ActionContext(httpContextMock.Object, routeData, actionDescriptor);

            var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>());
            exceptionContext.Exception = exception;
            exceptionContext.ExceptionHandled = false;

            // Act
            var filter = new ExceptionHandlerFilterAttribute(loggerFactoryMock.Object);
            filter.OnException(exceptionContext);

            // Assert
            Assert.NotNull(exceptionContext.Result);
            Assert.IsType<NotFoundObjectResult>(exceptionContext.Result);

            var resultObject = (NotFoundObjectResult)(exceptionContext.Result);
            Assert.IsType<ApiMessageModel>(resultObject.Value);
            Assert.Equal(notFoundMessage, ((ApiMessageModel)resultObject.Value).Message );

            // Commented out because LogWarning is an extension method.  Need to figure out how to test
            //loggerMock.Verify(l => l.LogWarning(It.IsAny<EventId>(), exception, notFoundMessage), Times.Once);
        }


        [Fact]
        public void TestSecurityExceptionReturnsNotFoundResult()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<ExceptionHandlerFilterAttribute>>();
            var logger = loggerMock.Object;

            var loggerFactoryMock = new Mock<ILoggerFactory>();

            loggerFactoryMock.Setup(
                lf => lf.CreateLogger(It.IsAny<String>()))
               .Returns(logger);

            var exception = new SecurityException("A security message");
            
            var httpContextMock = new Mock<HttpContext>();
            var routeData = new RouteData();
            var actionDescriptor = new ActionDescriptor();
            var actionContext = new ActionContext(httpContextMock.Object, routeData, actionDescriptor);

            var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>());
            exceptionContext.Exception = exception;
            exceptionContext.ExceptionHandled = false;

            // Act
            var filter = new ExceptionHandlerFilterAttribute(loggerFactoryMock.Object);
            filter.OnException(exceptionContext);

            // Assert
            Assert.NotNull(exceptionContext.Result);
            Assert.IsType<ForbiddenObjectResult>(exceptionContext.Result);

            String expectedMessage = "Access to this resource is forbidden";
            var resultObject = (ForbiddenObjectResult)(exceptionContext.Result);
            Assert.IsType<ApiMessageModel>(resultObject.Value);
            Assert.Equal(expectedMessage, ((ApiMessageModel)resultObject.Value).Message);

            // Commented out because LogWarning is an extension method.  Need to figure out how to test
            //loggerMock.Verify(l => l.LogWarning(It.IsAny<EventId>(), exception, notFoundMessage), Times.Once);
        }



        [Fact]
        public void TestResourceAlreadyExistsExceptionReturnsConflictResult()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<ExceptionHandlerFilterAttribute>>();
            var logger = loggerMock.Object;

            var loggerFactoryMock = new Mock<ILoggerFactory>();

            loggerFactoryMock.Setup(
                lf => lf.CreateLogger(It.IsAny<String>()))
               .Returns(logger);

            String exceptionMessage = "The resource already exists";
            var exception = new ResourceAlreadyExistsException(exceptionMessage);

            var httpContextMock = new Mock<HttpContext>();
            var routeData = new RouteData();
            var actionDescriptor = new ActionDescriptor();
            var actionContext = new ActionContext(httpContextMock.Object, routeData, actionDescriptor);

            var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>());
            exceptionContext.Exception = exception;
            exceptionContext.ExceptionHandled = false;

            // Act
            var filter = new ExceptionHandlerFilterAttribute(loggerFactoryMock.Object);
            filter.OnException(exceptionContext);

            // Assert
            Assert.NotNull(exceptionContext.Result);
            Assert.IsType<ConflictObjectResult>(exceptionContext.Result);

            var resultObject = (ConflictObjectResult)(exceptionContext.Result);
            Assert.IsType<ApiMessageModel>(resultObject.Value);
            Assert.Equal(exceptionMessage, ((ApiMessageModel)resultObject.Value).Message);

            // Commented out because LogWarning is an extension method.  Need to figure out how to test
            //loggerMock.Verify(l => l.LogWarning(It.IsAny<EventId>(), exception, notFoundMessage), Times.Once);
        }



        [Fact]
        public void TestGeneralExceptionReturnsInternalServerErrorResult()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<ExceptionHandlerFilterAttribute>>();
            var logger = loggerMock.Object;

            var loggerFactoryMock = new Mock<ILoggerFactory>();

            loggerFactoryMock.Setup(
                lf => lf.CreateLogger(It.IsAny<String>()))
               .Returns(logger);

            var exception = new Exception("Something went horribly wrong");

            var httpContextMock = new Mock<HttpContext>();
            var routeData = new RouteData();
            var actionDescriptor = new ActionDescriptor();
            var actionContext = new ActionContext(httpContextMock.Object, routeData, actionDescriptor);

            var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>());
            exceptionContext.Exception = exception;
            exceptionContext.ExceptionHandled = false;

            // Act
            var filter = new ExceptionHandlerFilterAttribute(loggerFactoryMock.Object);
            filter.OnException(exceptionContext);

            // Assert
            Assert.NotNull(exceptionContext.Result);
            Assert.IsType<InternalServerErrorObjectResult>(exceptionContext.Result);

            String expectedMessage = "An error has occurred on the server.  The error has been logged so it can be investigated by our support teams";
            var resultObject = (InternalServerErrorObjectResult)(exceptionContext.Result);
            Assert.IsType<ApiMessageModel>(resultObject.Value);
            Assert.Equal(expectedMessage, ((ApiMessageModel)resultObject.Value).Message);

            // Commented out because LogWarning is an extension method.  Need to figure out how to test
            //loggerMock.Verify(l => l.LogWarning(It.IsAny<EventId>(), exception, notFoundMessage), Times.Once);
        }


    }
}
