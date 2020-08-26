using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMService
{
    public static class Pather
    {
        public static string GetExecutablePath()
        {
            var actualPath = "";

            try
            {
                var fullPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                var paths = fullPath.Split('\\');

                for (var i = 0; i < paths.Length - 1; i++)
                    actualPath += paths[i] + "\\";

                actualPath = actualPath.Remove(actualPath.Length - 1, 1) + "\\";
            }
            catch (Exception e)
            { }

            return actualPath;
        }
    }
}