﻿using CRMService.Data;
using CRMService.Data.A28;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CRMService
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Database.SetInitializer<DeviceTypeContext>(null);

            //var objectSyncThread = new Thread(ObjectSyncThreadMethod);
            //objectSyncThread.Start();
        }


        public static void ObjectSyncThreadMethod()
        {
            while(true)
            {
                //DB_BLL.ObjectSync();

                //Thread.Sleep(TimeSpan.FromMinutes(30));       //ВКЛЮЧИТЬ В РЕЛИЗЕ!!!!!
            }
        }
    }
}