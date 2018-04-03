using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.ApiUtil
{

    /// <summary>
    /// Encapsulates the route data needed to generate a URL on an API model object during mapping resolution
    /// </summary>
    /// <remarks>
    /// The IUrlHelper object will need two things to generate a URL Link.  The name of the route and and
    /// parameters for the route.  This object encapsulates both pieces of information so it can be used by
    /// the UrlResolver class to generate the actual URL.
    /// </remarks>
    public class RouteUrlInfo
    {

        public String RouteName { get; set; }

        public object RouteParams { get; set; }

    }
}
