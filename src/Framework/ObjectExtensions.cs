using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework
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
        /// <param name="value">The object to be converted to JSON</param>
        /// <returns>The JSON representation the object (as a string)</returns>
        public static string ToJson(this object value, JsonSerializerSettings jsonSettings = null)
        {
            if (value == null)
                return NULL;

            try
            {
                string json = JsonConvert.SerializeObject(value, jsonSettings ?? JSON_SETTINGS);
                return json;
            }
            catch (Exception exception)
            {
                //log exception but don't throw one
                return EXCEPTION;
            }
        }

    }
}
