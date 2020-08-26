using CRMService.Data;
using CRMService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRMService.Controllers
{
    public class ContactsController : Controller
    {
        public ActionResult Index(string contactid=null)
        {
            try
            {
                var actualAreas = DB_BLL.GetUserAreas();
                if ((from a in actualAreas select a).Where(x => x.HrefLink == "/contacts/").FirstOrDefault() == null)
                    return View();

                using (var context = new CRMServiceEntities())
                {
                    if (contactid == null)
                    {
                        var allContacts = (from a in context.WorkContact select a).AsEnumerable();

                        List<ContactModel> contactsList = new List<ContactModel>();

                        foreach (var cont in allContacts)
                        {
                            contactsList.Add(new ContactModel()
                            {
                                ContactId = cont.Id.ToString(),
                                Lastname = cont.lastname,
                                Firstname = cont.firstname,
                                Workphone = cont.workphone,
                                Workplace = cont.workplace,
                                City = cont.city,
                                Address = cont.address
                            });
                        }

                        return View(contactsList);
                    }
                    else
                    {
                        var contactIdInt = 0;

                        try
                        {
                            contactIdInt = Int32.Parse(contactid);
                        }
                        catch (Exception exc) { }

                        var actualContact = (from a in context.WorkContact select a).Where(x => x.Id == contactIdInt).FirstOrDefault();
                        if (actualContact != null)
                        {
                            List<ContactModel> contactsList = new List<ContactModel>();

                            contactsList.Add(new ContactModel()
                            {
                                ContactId = actualContact.Id.ToString(),
                                Lastname = actualContact.lastname,
                                Firstname = actualContact.firstname,
                                Workphone = actualContact.workphone,
                                Workplace = actualContact.workplace,
                                City = actualContact.city,
                                Address = actualContact.address
                            });

                            return View(contactsList);
                        }
                    }                    
                }
            }
            catch (Exception exc) { }

            return View();
        }
    }
}
