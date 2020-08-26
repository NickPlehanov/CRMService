using CRMService.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRMService.Controllers
{
    public class AdminPanelController : Controller
    {
        public ActionResult Index()
        {




            return View();
        }

        public ActionResult AddUser(string userFullName = "", string userDomainName = "", bool Area1 = false, bool Area2 = false)
        {
            try
            {
                if (!string.IsNullOrEmpty(userDomainName))
                {
                    using (var context = new CRMServiceEntities())
                    {
                        var newUser = new User() { UserFullname = userFullName, UserDomainName = userDomainName };

                        context.User.Add(newUser);

                        context.SaveChanges();

                        var newUserId = newUser.UserId;

                        if (Area1)
                        {
                            context.UserArea.Add(new UserArea() { 
                                UserId = newUserId,
                                AreaId = 1
                            });
                        }
                        if (Area2)
                        {
                            context.UserArea.Add(new UserArea()
                            {
                                UserId = newUserId,
                                AreaId = 2
                            });
                        }

                        context.SaveChanges();
                    }

                   return Redirect("/AdminPanel/Index");
                }
            }
            catch (Exception exc) { }

            return View();
        }

    }
}
