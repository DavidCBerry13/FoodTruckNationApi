using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Filters;

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
    }
}
