using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.ApiUtil.Models
{

    /// <summary>
    /// Simple model class to wrap messages returned by an API, especially error messages.
    /// </summary>
    /// <remarks>
    /// This allows these messages to be returned as a JSON object rather than just a simple string
    /// and allows the ability to expand this object in the future if you want to add something like
    /// a message id, status code, etc.
    /// </remarks>
    public class ApiMessageModel
    {

        public String Message { get; set; }
    }
}
