using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Framework.ApiUtil.Controllers
{
    public abstract class BaseController : Controller
    {


        public BaseController(ILoggerFactory loggerFactory, IMapper mapper) : base()
        {
            String typeName = this.GetType().FullName;
            this.logger = loggerFactory.CreateLogger(this.GetType());

            this.mapper = mapper;
        }


        protected ILogger logger;
        protected IMapper mapper;



        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            context.HttpContext.Items.Add("URL_HELPER", this.Url);
        }
    }
}
