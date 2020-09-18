using CRMService.Data;
using CRMService.Data.A28;
using CRMService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRMService.Controllers {
    public class ContactsController : Controller {


        public List<ObjCust> SearchContacts(string searchString) {
            if (string.IsNullOrEmpty(searchString))
                return null;
            else {
                List<ObjCust> objCusts = new List<ObjCust>();
                //требуется определить как искать по имени или телефону
                using (ObjCustContext objCustContext = new ObjCustContext()) {
                    List<ObjCust> t = objCustContext.ObjCust.Where(x => x.RecordID != null && x.UserNumber != null).ToList();
                    List<ObjCust> t1 = new List<ObjCust>();
                    List<ObjectA28> objs = new List<ObjectA28>();
                    using (ObjectContext context = new ObjectContext()) {
                        objs = context.ObjectA28.Where(x => x.RecordDeleted == false).ToList();
                    }
                    foreach (ObjCust item in t) {
                        if (objs.Any(x => x.ObjectID == item.ObjectID && x.RecordDeleted == false))
                            t1.Add(item);
                    }
                    string result = "";
                    string text = "";
                    foreach (char item in searchString.ToCharArray()) {
                        if (char.IsDigit(item))
                            result += item;
                        else if (char.IsLetter(item) || item == ' ')
                            text += item;
                    }
                    result = CRMService.Helpers.HelpersMethods.NormalizePhone(result);
                    //foreach (ObjCust item in t) {
                    //    if (item.)
                    //}

                    if (!string.IsNullOrEmpty(result)) {
                        if (result.Length == 11 && (result.Substring(0, 2) == "89" || result.Substring(0, 2) == "79")) { //значит у нас номер телефона мобильный
                            objCusts = t1.Where(x =>
                                    x.N_ObjCustPhone1 == result
                                  || x.N_ObjCustPhone2 == result
                                  || x.N_ObjCustPhone3 == result
                                  || x.N_ObjCustPhone4 == result
                                  || x.N_ObjCustPhone5 == result
                                  || x.N_ObjCustTitle.Contains(result)
                                  && x.ObjectNumber != -1
                            ).ToList();
                        }
                        if (result.Length == 7) { //значит у нас номер телефона городской
                            objCusts = t1.Where(x => x.N_ObjCustPhone1 == result
                                  || x.N_ObjCustPhone2 == result
                                  || x.N_ObjCustPhone3 == result
                                  || x.N_ObjCustPhone4 == result
                                  || x.N_ObjCustPhone5 == result
                                  || x.N_ObjCustTitle.Contains(result)
                                  && x.ObjectNumber != -1
                            ).ToList();
                        }
                    }
                    else if (!string.IsNullOrEmpty(text)) { //ищем по имени
                        objCusts = t1.Where(x => x.ObjCustName.ToLower().Contains(text.ToLower())
                            || x.ObjCustTitle.ToLower().Contains(text.ToLower())
                        ).ToList();
                    }
                    //поиск по телефону
                    using (ObjAdminContext objAdminContext = new ObjAdminContext()) {
                        List<ObjAdmin> Fulladmins = objAdminContext.ObjAdmin.Where(x => x.RecordDeleted == false).ToList();
                        List<ObjAdmin> admins = Fulladmins.Where(x => x.N_AdminPhone == result).ToList();
                        if (admins.Any()) {
                            foreach (ObjAdmin item in admins) {
                                using (ObjectContext objectContext = new ObjectContext()) {
                                    foreach (ObjectA28 _item in objectContext.ObjectA28.Where(x => x.ObjAdminID == item.ObjAdminID)) {
                                        if (objCusts.Any(x => x.ObjectID == _item.ObjectID)) {
                                            objCusts.FirstOrDefault(x => x.ObjectID == _item.ObjectID).N_ObjCustTitle += Environment.NewLine + "Администратор(старый лк)";
                                        }
                                        else
                                            objCusts.Add(new ObjCust() { N_ObjCustPhone1 = item.AdminPhone, ObjCustName = item.AdminName, N_ObjCustTitle = "Администратор ЛК", ObjectID = _item.ObjectID, ObjectNumber = _item.ObjectNumber });
                                    }
                                }
                            }
                        }
                        else if (!string.IsNullOrEmpty(text)) {
                            List<ObjAdmin> admins1 = Fulladmins.Where(x => x.AdminName.ToLower().Contains(text.ToLower())).ToList();
                            if (admins1.Any()) {
                                foreach (ObjAdmin item in admins1) {
                                    using (ObjectContext objectContext = new ObjectContext()) {
                                        foreach (ObjectA28 _item in objectContext.ObjectA28.Where(x => x.ObjAdminID == item.ObjAdminID)) {
                                            objCusts.Add(new ObjCust() { N_ObjCustPhone1 = item.AdminPhone, ObjCustName = item.AdminName, N_ObjCustTitle = "Администратор ЛК", ObjectID = _item.ObjectID, ObjectNumber = _item.ObjectNumber });
                                        }
                                    }
                                }
                            }
                        }
                    }
                    return objCusts;
                }
            }
        }
        public ActionResult Index(string searchString = "") {
            List<ObjCust> objCusts = new List<ObjCust>();
            objCusts = SearchContacts(searchString);
            if (objCusts == null) {
                objCusts = new List<ObjCust>();
                objCusts.Add(new ObjCust() { RecordID = -1, ObjCustTitle = "", ObjCustName = "", ObjCustPhone1 = "", ObjCustPhone2 = "", ObjCustPhone3 = "", ObjCustPhone4 = "", ObjCustPhone5 = "", ObjCustAddress = "", Role = "", ObjectNumber = -1 });
            }
            return View(objCusts);
        }
    }
}
