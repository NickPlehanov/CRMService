using CRMService.Data;
using CRMService.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace CRMService.Controllers {
    public class IndexController : Controller {
        public ActionResult Index() {
            var areasList = new List<AreaModel>();

            try {

                // DB_BLL.UsersSync();


                // //var currentUser = Environment.UserDomainName + "\\" + Environment.UserName; //User.Identity.Name.ToString();
                var currentUser = System.Web.HttpContext.Current.User.Identity.Name;

                // //ViewBag.CurrentUser = User.Identity.Name;// currentUser;
                // //var currentUser = "VZ\\nik";

                ViewBag.CurrentUser = currentUser;

                /*using (var context = new CRMServiceEntities())
                {
                    var isUser = (from a in context.User select a).Where(x => x.UserDomainName == currentUser).FirstOrDefault();

                    if (isUser != null)
                    {
                        //значит такой пользователь в базе есть, смотрим его права

                        var userId = isUser.UserId;

                        var actualAreas = (from a in context.Area
                                           join au in context.UserArea
                                               on a.AreaId equals au.AreaId
                                           select new { a, au }).Where(x => x.au.UserId == userId).AsEnumerable();


                        foreach(var actualArea in actualAreas)
                        {
                            areasList.Add(new AreaModel() { 
                                Name = actualArea.a.AreaName,
                                HrefLink = actualArea.a.AreaHrefLink
                            });
                        }
                */
                //TMP
                areasList.Add(new AreaModel() {
                    Name = "Объекты",
                    HrefLink = "/guardobject/"
                });

                areasList.Add(new AreaModel() {
                    Name = "Поиск контактов (поиск по телефону и имени)",
                    HrefLink = "/contacts/"
                });
                if (currentUser.Contains(@"VZ\pna"))
                    areasList.Add(new AreaModel() {
                        Name = "Обходные листы",
                        HrefLink = "/bypass/"
                    });
                if (currentUser.Contains(@"VZ\pna"))
                    areasList.Add(new AreaModel() {
                        Name = "Опоздания ГБР",
                        HrefLink = "/GBRLate/"
                    });
                /////


                return View(areasList);/*
                 //   }
              //  }*/

                //return View(DB_BLL.GetUserAreas());

            }
            catch (Exception exc) { }

            return View();
        }

    }
}
