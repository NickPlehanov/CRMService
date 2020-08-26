using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CRMService
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                //url: "{action}/{id}",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Index", action = "Index", id = UrlParameter.Optional }
            );

            //routes.MapRoute(
            //    name: "Custom",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Index", action = "Index", id = UrlParameter.Optional }
            //);




            //var loggerThread = new Thread(LoggerThread);
            //loggerThread.Start();

        }

        //public static void LoggerThread()
        //{
        //    try
        //    {
        //        while(true)
        //        {
        //            Logger.WriteString(@"D:/CRMService/Logs/", Logger.GetLogFileName(), DateTime.Now.ToString("HH:mm:ss.fff"));

        //            Thread.Sleep(1000);
        //        }
        //    }
        //    catch (Exception exc) { }
        //}
    }
}