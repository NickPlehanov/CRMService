using CRMService.Data;
using CRMService.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRMService.Controllers
{
    public class ServiceorderController : Controller
    {
        public ActionResult Index(int? page, string searchNumber = "")
        {
            try
            {
                int pageSize = 50;
                int pageNumber = (page ?? 1);

                using (var context = new CRMServiceEntities())
                {
                    int count = 0;
                    IEnumerable<dynamic> orders;

                    if (string.IsNullOrEmpty(searchNumber))
                    {
                        count = context.ServiceorderExtension.Count();
                        orders = (from g in context.Serviceorder
                                   join ge in context.ServiceorderExtension
                                       on g.ServiceorderId equals ge.ServiceorderId
                                   select new { g, ge }).Where(x => x.g.DeletionStateCode == 0).OrderByDescending(x => x.ge.Date).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                    }
                    else
                    {
                        int? objNumber = -1;

                        try
                        {
                            objNumber = Int32.Parse(searchNumber);
                        }
                        catch (Exception exc) { }

                        //запроси по этому нмоеру объект из БД
                        var actualObject = (from a in context.GuardObject
                                            join ae in context.GuardObjectExtention
                                                on a.GuardObjectId equals ae.GuardObjectId
                                            select new { a, ae }).Where(x => x.ae.Number == objNumber && x.a.DeletionStateCode == 0).FirstOrDefault();
                        if (actualObject != null)
                        {
                            orders = (from g in context.Serviceorder
                                       join ge in context.ServiceorderExtension
                                           on g.ServiceorderId equals ge.ServiceorderId
                                       select new { g, ge }).Where(x => x.g.DeletionStateCode == 0 && x.ge.GuardObjectId == actualObject.a.GuardObjectId)
                                       .OrderByDescending(x => x.ge.Date).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                            count = orders.Count();
                        }
                        else
                        {
                            return View();
                        }                        
                    }



                    var ServiceorderList = new List<ServiceorderModel>();

                    //ViewBag.Servicemans = servicemans;

                    foreach (var g in orders)
                    {
                        ServiceorderList.Add(new ServiceorderModel()
                        {
                            ServiceorderId = g.ge.ServiceorderId.ToString(),
                            Name = g.ge.Name
                        });
                    }

                    var model = new StaticPagedList<ServiceorderModel>(ServiceorderList, pageNumber, pageSize, count);

                    return View(model);
                }
            }
            catch (Exception exc) { }


            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new ServiceorderModel());
        }

        [HttpPost]
        public ActionResult Create(ServiceorderModel serviceorder)
        {
            try
            {
                DB_BLL.ServiceorderDB(serviceorder, 1);

                ViewBag.Success = "Заявка '" + serviceorder.Name + "' создана";
                return View();
            }
            catch (Exception exc)
            {
                return View(new ServiceorderModel());
            }
        }

    }
}
