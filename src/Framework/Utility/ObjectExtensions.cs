using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Utility
{
    public static class ObjectExtensions
    {

        /// <summary>
        /// The string representation of null.
        /// </summary>
        private static readonly string NULL = "null";

        /// <summary>
        /// The string representation of exception.
        /// </summary>
        private static readonly string EXCEPTION = "Exception";


        private static readonly JsonSerializerSettings JSON_SETTINGS = new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        /// <summary>
        /// Converts and object to its JSON representation
        /// </summary>
        /// <reamrks>
        /// If you are using this method to serialize an object to store in a log file, you may want to
        /// make sure an exception is not thrown while calling this method.  To do this, set the supressException
        /// parameter to true.  If an exception is thrown, then a string of 'Exception' will be returned
        /// rather than throwning an exception
        /// </reamrks>
        /// <param name="value">The object to be converted to JSON</param>
        /// <param name="supressException">Boolean flag indicating if any exceptions should be supressed</param>
        /// <returns>The JSON representation the object (as a string)</returns>
        public static string ToJson(this object value, JsonSerializerSettings jsonSettings = null,
            bool supressException = false)
        {
            if (value == null)
                return NULL;

            try
            {
                string json = JsonConvert.SerializeObject(value, jsonSettings ?? JSON_SETTINGS);
                return json;
            }
            catch (Exception)
            {
                if (supressException)
                    return EXCEPTION;
                else
                    throw;
            }
        }

    }
}
