using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.ApiUtil.Models
{

    /// <summary>
    /// Model class to return a field level error, typically as the result of the field failing validation
    /// </summary>
    /// <remarks>
    /// Typically an array of these objects are returned when an incoming object fails validation
    /// </remarks>
    public class RequestErrorModel
    {

        /// <summary>
        /// The name of the field with an issue.
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// A message containing a description of the error
        /// </summary>
        public string Message { get; set; }

    }
}
