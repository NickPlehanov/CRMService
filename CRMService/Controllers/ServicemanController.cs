using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRMService.Data;
using CRMService.Models;

namespace CRMService.Controllers
{
    public class ServicemanController : Controller
    {
        public ActionResult Index()
        {
            //try
            //{
            //    using (var context = new CRMServiceEntities())
            //    {
            //        var servicemans = (from s in context.Serviceman join se in context.ServicemanExtension 
            //                           on s.ServicemanId equals se.ServicemanId select new { s, se }).Where(x => x.s.DeletionStateCode == 0).ToList();

            //        var servicemansList = new List<ServicemanModel>();

            //        //ViewBag.Servicemans = servicemans;

            //        foreach(var s in servicemans)
            //        {
            //            servicemansList.Add(new ServicemanModel() 
            //            { 
            //                ServicemanId = s.se.ServicemanId.ToString(),
            //                Name = s.se.Name
            //            });
            //        }

            //        return View(servicemansList);
        
            //    }
            //}
            //catch (Exception exc) { }


            return View();
        }
        
        [HttpGet]
        public ActionResult Create()
        {
            return View(new ServicemanModel());
        }

        [HttpPost]
        public ActionResult Create(ServicemanModel serviceman)
        {
            try
            {
                DB_BLL.ServicemanDB(serviceman, 1);

                ViewBag.Success = "Техник '" + serviceman.Name + "' добавлен";
                return View();
            }
            catch (Exception exc) 
            {
                return View(new ServicemanModel());
            }            
        }



        [HttpGet]
        //public ActionResult Edit(string servicemanId)
        public ActionResult Edit(string servicemanId)
        {
            //try
            //{
            //    var strId = new Guid(servicemanId);

            //    using (var context = new CRMServiceEntities())
            //    {
            //        var sman = (from s in context.ServicemanExtension select s).Where(x => x.ServicemanId == strId).FirstOrDefault();
    
            //        if (sman != null)
            //        {
            //            return View(new ServicemanModel() { Name = sman.Name, Phone = sman.Phone, password = sman.Password });
            //        }
            //    }
            //}
            //catch (Exception exc) { }
                        

            return View(new ServicemanModel());
        }

        [HttpPost]
        public ActionResult Edit(ServicemanModel serviceman)
        {
            //try
            //{
            //    DB_BLL.ServicemanDB(serviceman, 2);

            //    ViewBag.Success = "Запись успешно изменена";
            //    return View();
            //}
            //catch (Exception exc)
            //{
            return View(new ServicemanModel());
            //}  
        }

    }
}
