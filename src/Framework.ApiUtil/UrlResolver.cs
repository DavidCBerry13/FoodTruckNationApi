using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.ApiUtil
{
    public class UrlResolver : IMemberValueResolver<object, object, RouteUrlInfo, String>
    {

        public UrlResolver(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContext = httpContextAccessor.HttpContext;
        }


        private HttpContext httpContext;



        //public string Resolve(RouteUrlInfo source, object destination, string destMember, ResolutionContext context)
        //{

        //}

        public string Resolve(object source, object destination, RouteUrlInfo sourceMember, string destMember, ResolutionContext context)
        {
            IUrlHelper url = (IUrlHelper)httpContext.Items["URL_HELPER"];

            return url.Link(sourceMember.RouteName, sourceMember.RouteParams);
        }
    }
}
