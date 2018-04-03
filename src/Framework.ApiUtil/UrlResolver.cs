using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.ApiUtil
{
    /// <summary>
    /// Custom resolver class to help create URLs in API model objects to support HATEOS
    /// </summary>
    /// <remarks>
    /// This class is used by Automapper to resolve URLs in API model objects during the mapping
    /// process.  To work, the controller needs to put an IUrlHelper object in the HttpContext Items
    /// collection with the key of "URL_HELPER".  In addition, when setting up the map to the Model
    /// object, the map will specify a RouteUrlInfo object as the source member in order to supply 
    /// the name of the route and any parameters needed by the route to use at mapping resolution time.
    /// <para>
    /// Then, when Automapper maps the maps the object, the Resolve() method of this class is called.  
    /// This method fetches the IUrlHelper object from the HttpContext and calls the Link() method in
    /// order to generate the URL link.  Fed into the link method are the route name and the route
    /// parameters, allowing the IUrlHelper to create the full URL link and return it.
    /// </para>
    /// <para>
    /// Note that the TSource and TDestination types are both declared as object types to allow URL resolution
    /// from and to any object type.  This allows us to have one resolver object type that we can use for any
    /// combination of objects and not need a separate resolver class for each domain object/model object pair.
    /// </para>
    /// </remarks>
    public class UrlResolver : IMemberValueResolver<object, object, RouteUrlInfo, String>
    {
        /// <summary>
        /// Creates a new Urlresolver class to be used during mapping resolution to create URL links
        /// </summary>
        /// <param name="httpContextAccessor">An IHttpContextAccessor object that allows this class to
        /// fetch the HttpContext of the current request</param>
        public UrlResolver(IHttpContextAccessor httpContextAccessor)
        {
            var httpContext = httpContextAccessor.HttpContext;
            this.urlHelper = (IUrlHelper)httpContext.Items["URL_HELPER"];
        }

        // This is here so the TestUrlResolver can extend the class
        protected UrlResolver()
        {

        }


        private readonly IUrlHelper urlHelper;

        /// <summary>
        /// Resolves a URL in a modul object by using a route name and route paramters in the supplied RouteUrlInfo object
        /// </summary>
        /// <param name="source">The source object we are mapping from.  Not used in this implementation</param>
        /// <param name="destination">The destination object we are mapping to.  Not used in this implementation</param>
        /// <param name="sourceMember">The RouteUrlInfo object of the route name and parameters we want to generate a URL for</param>
        /// <param name="destMember"></param>
        /// <param name="context"></param>
        /// <returns>A String of the generated URL</returns>
        public virtual string Resolve(object source, object destination, RouteUrlInfo sourceMember, string destMember, ResolutionContext context)
        {
            return this.urlHelper.Link(sourceMember.RouteName, sourceMember.RouteParams);
        }
    }
}
