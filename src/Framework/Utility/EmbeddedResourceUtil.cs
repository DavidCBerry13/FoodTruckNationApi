using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Framework.Utility
{
    public static class EmbeddedResourceUtil
    {


        public static string ReadEmbeddedResourceTextFile(this Assembly assembly, string filename)
        {
            var resourceName = $"{assembly.GetName().Name}.{filename}";
           
            using (StreamReader reader = new StreamReader(assembly.GetManifestResourceStream(resourceName)))
            {
                string contents = reader.ReadToEnd();
                return contents;
            }            
        }





    }
}
