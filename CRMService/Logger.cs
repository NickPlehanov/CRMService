using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace CRMService
{
    public static class Logger
    {
        public static void WriteString(string prefix, string fileName, string text)
        {
            StreamWriter sw;
            var fi = new FileInfo(prefix + @"/" + fileName);

            sw = fi.AppendText();
            sw.WriteLine(text);

            sw.Close();
        }

        public static string GetLogFileName()
        {
            var name = DateTime.Now.ToString("dd-MM-yyyy") + ".txt";

            return name;
        }
    }
}