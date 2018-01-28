using AutoMapper;
using Framework.ApiUtil;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodTruckNationApi.Test
{

    /// <summary>
    /// Simple UrlResolver to use during Unit Testing as ASP.NETs IUrlResolver is not availble
    /// </summary>
    /// <remarks>
    /// When running unit tests of controllers, domain objects have to be mapped to ViewModel
    /// objects which is done via Automapper.  However, since the view models may contain links
    /// (to support HATEOS), a custom UrlResolver object is used, which needs the HttpContext
    /// to get the IUrlHelper from ASP.NET.  Of course, HttpContext is not available when running
    /// unit tests, so the test fail with a null pointer exception.
    /// <para>
    /// This class is a very simple stand-in for UrlResolver, basically taking every link and just 
    /// returning the value as empty string for now.  In the future, this class will be changed
    /// to actually substitute the URL parameters in and make a realistic looking test URL, but
    /// for now, this allows tests to be written against the controller and not fail.
    /// </para>
    /// </remarks>
    public class TestUrlResolver : UrlResolver
    {



        public override string Resolve(object source, object destination, RouteUrlInfo sourceMember, string destMember, ResolutionContext context)
        {
            return "";
        }


    }
}
