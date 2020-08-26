using CRMService.Data;
using CRMService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.DirectoryServices.AccountManagement;
using System.Text.RegularExpressions;

namespace CRMService.Controllers
{
    public class GuardObjectController : Controller
    {
        //private IEnumerable<dynamic> GetSearchObjects(string searchStr, int flag)
        private List<CRMService.Data.Object> GetSearchObjects(string searchParam, string searchStr, int flag)
        {
            try
            {
                //IEnumerable<dynamic> objects;
                var objects = new List<CRMService.Data.Object>();

                using (var context = new A28Entities())
                {
                    switch(searchParam)
                    {
                        case "1":
                            {

                                var dateIn = "";

                                var obj = (from o in context.Object
                                              join e in context.ObjExtField
                                                on o.ObjectID equals e.ObjectID
                                              join ef in context.ExtField
                                                on e.ExtFieldID equals ef.ExtFieldID
                                              select new { o, e, ef }).Where(x => x.e.ExtFieldID == 119 && x.e.ExtFieldValue == searchStr).AsEnumerable();

                                foreach(var o in obj)
                                {
                                    objects.Add(o.o);
                                }

                                break;
                            }
                        case "2":   //номер
                            {
                                int decObjNumber = -1;

                                try
                                {
                                    if (flag == 0)
                                        decObjNumber = Convert.ToInt32(searchStr, 16);
                                }
                                catch (Exception exc) { }


                                if (decObjNumber != -1)
                                {
                                    objects = (from o in context.Object select o)
                                    .Where(x => x.RecordDeleted == false && x.ObjectNumber == decObjNumber)
                                    .OrderBy(x => x.ObjectNumber)
                                    .ToList();
                                }

                                break;
                            }
                        case "3":   //название
                            {
                                objects = (from o in context.Object select o)
                                    .Where(x => x.RecordDeleted == false && x.Name.Contains(searchStr))
                                    .OrderBy(x => x.ObjectNumber)
                                    .ToList();

                                break;
                            }
                        case "4":   //адрес
                            {
                            //objects = (from o in context.Object select o)
                            //    .Where(x => x.RecordDeleted == false && x.Address.Contains(searchStr))
                            //    .OrderBy(x => x.ObjectNumber)
                            //    .ToList();
                            if (string.IsNullOrEmpty(searchStr) || string.IsNullOrWhiteSpace(searchStr))
                                throw new Exception("Строка поиска не может быть пустой");
                            string[] srch = new string[10];
                            srch = searchStr.Split(',');
                            string tmp = srch[0].Trim().ToLower();
                            objects = (from o in context.Object select o)
                                    .Where(x => x.RecordDeleted == false && x.Address.ToLower().Contains(tmp))
                                    .OrderBy(x => x.ObjectNumber)
                                    .ToList();
                            for (int i = 1; i < srch.Count(); i++) {
                                tmp = srch[i].Trim().ToLower();
                                objects = (from o in objects select o)
                                    .Where(x => x.RecordDeleted == false && x.Address.ToLower().Contains(tmp))
                                    .OrderBy(x => x.ObjectNumber)
                                    .ToList();
                            }


                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }






                    //int decObjNumber = -1;

                    //try
                    //{
                    //    if (flag == 0)
                    //        decObjNumber = Convert.ToInt32(searchStr, 16);                        
                    //}
                    //catch (Exception exc) { }


                    //if (decObjNumber != -1)
                    //{
                    //    objects = (from o in context.Object select o)
                    //    .Where(x => x.RecordDeleted == false && x.ObjectNumber == decObjNumber)
                    //    .OrderBy(x => x.ObjectNumber)
                    //        //.Skip((pageNumber - 1) * pageSize)
                    //        //.Take(pageSize)
                    //    .ToList();                        
                    //}
                    //else
                    //{
                    //    objects = (from o in context.Object select o)
                    //    .Where(x => x.RecordDeleted == false && (x.Name.Contains(searchStr) || x.Address.Contains(searchStr)))
                    //    .OrderBy(x => x.ObjectNumber)
                    //        //.Skip((pageNumber - 1) * pageSize)
                    //        //.Take(pageSize)
                    //    .ToList();
                    //}

                    return objects;
                }                
            }
            catch (Exception exc) { return null; }
        }

        public ActionResult Index(int? page, string searchParam = "", string searchString = "")
        {
            try
            {
                var actualAreas = DB_BLL.GetUserAreas();
                if ((from a in actualAreas select a).Where(x => x.HrefLink == "/guardobject/").FirstOrDefault() == null)
                    return View();

                int pageSize = 3000;
                int pageNumber = (page ?? 1);

                //using (var context = new CRMServiceEntities())
                using (var context = new A28Entities())
                {
                    int count = 0;
                    //IEnumerable<dynamic> tmpObjects = null;
                    //IEnumerable<dynamic> objects = null;

                    var objects1 = new List<CRMService.Data.Object>();
                    var tmpObjects = new List<CRMService.Data.Object>();

                    if (string.IsNullOrEmpty(searchString) && (searchParam == "0" || searchParam == ""))
                    {
                        //count = context.Object.Where(x => x.RecordDeleted == false).Count();
                        objects1 = (from o in context.Object select o).Where(x => x.RecordDeleted == false).OrderBy(x => x.ObjectNumber).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                        //objects1 = (List<CRMService.Data.Object>)objects;

                        

                        //count = context.GuardObjectExtention.Count();
                        //objects = (from g in context.GuardObject
                        //           join ge in context.GuardObjectExtention
                        //               on g.GuardObjectId equals ge.GuardObjectId
                        //           select new { g, ge }).Where(x => x.g.DeletionStateCode == 0).OrderBy(x => x.ge.Number).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                    }
                    else
                    {

                        //List<CRMService.Data.Object> objectsList = new List<Data.Object>();

                        //var searchArray = searchString.Split('*');
                        //foreach(var str in searchArray)
                        {
                            //if (!(string.IsNullOrEmpty(str)))
                            {
                                //IEnumerable<dynamic> temp = null;
                                var temp = new List<CRMService.Data.Object>();

                                //if (searchArray.Count () > 1)
                                    temp = GetSearchObjects(searchParam, searchString, 0);
                                //else
                                    //temp = GetSearchObjects(str, 0);

                                if (tmpObjects.Count == 0)
                                {
                                    tmpObjects = temp;
                                    objects1 = temp;
                                }
                                else
                                {
                                    if (temp.Count() > 0)
                                    {
                                        objects1 = null;
                                    }
                                    foreach(var t in temp)
                                    {
                                        var isExist = (from i in tmpObjects select i).Where(x => x == t).FirstOrDefault();
                                        if (isExist != null)
                                        {
                                            //objects1 += isExist;
                                            objects1.Add(isExist);
                                        }
                                    }
                                }
                            }
                        }



                        count = objects1.Count();
                        objects1 = objects1.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                        
                                      
                        
                        

                        //objects = (from g in context.GuardObject
                        //           join ge in context.GuardObjectExtention
                        //               on g.GuardObjectId equals ge.GuardObjectId
                        //           select new { g, ge }).Where(x => x.g.DeletionStateCode == 0 && x.ge.NumberHex == objNumber).OrderBy(x => x.ge.Number).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                        //count = objects.Count();
                    }


                    var GuardObjectList = new List<GuardObjectModel>();

                    if (!(string.IsNullOrEmpty(searchString)))
                    {
                        //ViewBag.Servicemans = servicemans;
                        
                        foreach (var g in objects1)
                        {
                            var dateIn = "";

                            var objExt = (from e in context.ObjExtField
                                          join ef in context.ExtField
                                          on e.ExtFieldID equals ef.ExtFieldID
                                          select new { e, ef }).Where(x => x.e.ObjectID == g.ObjectID).AsEnumerable();

                            var objExtDict = new Dictionary<string, string>();
                            foreach (var e in objExt)
                            {
                                if (e.e.ExtFieldID == 119)
                                {
                                    dateIn = e.e.ExtFieldValue;
                                }
                            }

                            ///////////////////////////////////////////



                            var objectNumberHexTmp = Convert.ToInt32(g.ObjectNumber.ToString(), 10);
                            var objectNumberHex = Convert.ToInt32(objectNumberHexTmp.ToString(), 10);

                            GuardObjectList.Add(new GuardObjectModel()
                            {
                                //GuardObjectId = g.ge.GuardObjectId.ToString(),
                                //Name = g.ge.Name

                                GuardObjectId = g.ObjectID.ToString(),
                                //Number = objectNumberHex.ToString(),
                                //DateIn = string.IsNullOrEmpty(dateIn) ? "-" : DateTime.Parse(dateIn).ToString("dd.MM.yyyy"),
                                DateIn = !DateTime.TryParse(dateIn,out _) ? string.IsNullOrEmpty(dateIn) ? "-" : dateIn : DateTime.Parse(dateIn).ToString("dd.MM.yyyy"),
                                Number = g.ObjectNumber.ToString("X"),
                                Name = g.Name.ToString(),
                                Address = string.IsNullOrEmpty(g.Address.ToString()) ? "" : g.Address.ToString()
                            });

                            GuardObjectList = GuardObjectList.OrderByDescending(x => x.DateIn).ToList<GuardObjectModel>();
                        }
                    }

                    var model = new StaticPagedList<GuardObjectModel>(GuardObjectList, pageNumber, pageSize, count);

                    ViewBag.SearchString = searchString;                   


                    //int pageSize = 50;
                    //IEnumerable<GuardObjectModel> guardObjectsPerPages = GuardObjectList.Skip((page - 1) * pageSize).Take(pageSize);
                    //PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = GuardObjectList.Count };
                    //IndexViewGuardObjectModel ivgom = new IndexViewGuardObjectModel { PageInfo = pageInfo, GuardObjects = guardObjectsPerPages };

                    //return View(GuardObjectList.ToPagedList(page, pageSize));
                    return View(model);
                    //return View(ivgom);

                }
            }
            catch (Exception exc) {
                var e = exc.Message;
            }


            return View();
        }


        public ActionResult Info(string guardobjectId)
        {
            try
            {
                var actualAreas = DB_BLL.GetUserAreas();
                if ((from a in actualAreas select a).Where(x => x.HrefLink == "/guardobject/").FirstOrDefault() == null)
                    return View();

                try
                {
                    ViewBag.CurrUser = User.Identity.Name.ToString();  //System.DirectoryServices.AccountManagement.GroupPrincipal.
                    //string userName = User.Identity.Name;
                    //var context = new PrincipalContext(ContextType.Domain, "VZ");
                    //UserPrincipal user = UserPrincipal.FindByIdentity(context, userName);

                    //ViewBag.CurrUser = user.Name;

                }
                catch (Exception exc) { ViewBag.CurrUser = exc.Message; }
                                
                ViewBag.DTNow = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");

                //var strId = new Guid(guardobjectId);
                var strId = Int32.Parse(guardobjectId);

                using (var context = new A28Entities())
                {
                    var obj = (from s in context.Object
                               join e in context.EventTemp on s.EventTemplateID equals e.EventTemplateID
                               join t in context.ObjType on s.ObjTypeID equals t.ObjTypeID
                               select new { s, e, t }).Where(x => x.s.RecordDeleted == false && x.s.ObjectID == strId).FirstOrDefault();

                    var objExt = (from e in context.ObjExtField 
                                  join ef in context.ExtField
                                  on e.ExtFieldID equals ef.ExtFieldID select new {e, ef}).Where(x => x.e.ObjectID == obj.s.ObjectID).AsEnumerable();

                    var objExtDict = new Dictionary<string, string>();
                    foreach (var e in objExt)
                    {
                        if ((e.e.ExtFieldID == 113) || (e.e.ExtFieldID == 119) || (e.e.ExtFieldID == 145) || (e.e.ExtFieldID == 147) || (e.e.ExtFieldID == 149))
                        {
                            objExtDict.Add(e.ef.ExtFieldName, e.e.ExtFieldValue);
                        }
                    }



                    //поля на отправку СМС и ЛК
                    var actualObj = (from a in context.Object select a).Where(x => x.ObjectID == strId).FirstOrDefault();
                    if (actualObj != null)
                    {
                        //СМС
                        var epCustomers = (from e in context.EPCustomer select e).Where(x => (x.BeginningNumber <= actualObj.ObjectNumber) && (x.EndNumber >= actualObj.ObjectNumber)).AsEnumerable();
                        
                        var smsParams = "";

                        foreach(var ep in epCustomers)
                        {
                            var procs = (from p in context.EP select p).Where(x => (x.ProcID == ep.OwnerRecordID) && (x.ProcGroupID == 30) && (x.Enabled == true)).AsEnumerable();
                            
                            foreach(var proc in procs)
                            {
                                var procParams = "";

                                try
                                {
                                    procParams = proc.Params.Split(';')[0].Split('=')[1] + "\n"; 
                                }
                                catch (Exception exc) { }

                                smsParams += procParams;
                            }
                        }

                        objExtDict.Add("Отправка СМС", smsParams);

                        //Доп. услуги


                        //ЛК(старый)
                        var objAdmin = (from a in context.ObjAdmin select a).Where(x => x.ObjAdminID == actualObj.ObjAdminID /*&& x.ObjectID == strId*/).FirstOrDefault();                        
                        if (objAdmin != null)
                        {
                            objExtDict.Add("Администратор (старый ЛК)", objAdmin.AdminName + "   " + "тел. " + objAdmin.AdminPhone + "   " + "email: " + objAdmin.AdminEmail);
                        }
                        //else
                        //{
                        //    objExtDict.Add("Администратор (старый ЛК)", "");
                        //}
                        //ЛК MyAlarm
                        //2 - пользователь
                        //1 - администратор
                        var objAdminMyAlarm = (from a in context.ObjCust select a).Where(x => x.MyAlarmUserRole == 1 && x.ObjectID== strId);
                        if (objAdminMyAlarm.Any()) {
                            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
                            foreach (var item in objAdminMyAlarm) {
                                stringBuilder.AppendLine(item.ObjCustName + "   " + "тел. " + item.ObjCustPhone1);
                            }
                            objExtDict.Add("Администратор(ы) (MyAlarm)", stringBuilder.ToString());
                        }
                        else {
                            var objUsersMyAlarm = (from a in context.ObjCust select a).Where(x => x.MyAlarmUserRole == 2 && x.ObjectID == strId);
                            if (objUsersMyAlarm.Any()) {
                                System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
                                foreach (var item in objUsersMyAlarm) {
                                    stringBuilder.AppendLine(item.ObjCustName + "   " + "тел. " + item.ObjCustPhone1);
                                }
                                objExtDict.Add("Пользователь(и) (MyAlarm)",stringBuilder.ToString());
                            }
                        }
                        if (objAdmin==null && objAdminMyAlarm.Count()==0)
                            objExtDict.Add("Администратор(ы)", " - ");
                    }


                    ///////////////////////////




                    var objOwners = (from o in context.ObjCust select o).Where(x => x.ObjectID == obj.s.ObjectID).OrderBy(x => x.UserNumber).AsEnumerable();

                    var objOwnersList = new List<ObjCust>();
                    foreach(var owner in objOwners)
                    {
                        objOwnersList.Add(owner);
                    }


                    if (obj != null)
                    {
                        return View(new GuardObjectModel()
                            {
                                Name = obj.s.Name,
                                Address = obj.s.Address,
                                Number = obj.s.ObjectNumber.ToString("X"),
                                Sygnalizations = (obj.s.IsArm == true ? "Охранная " : "") + (obj.s.IsFire == true ? "Пожарная " : "") + (obj.s.IsPanic == true ? "Тревожная кнопка " : ""),
                                ObjectType = obj.t.ObjTypeName,
                                EventTemplate = obj.e.EventTemplateName,
                                ControlTime = obj.s.ControlTime == 0 ? "не задано" : obj.s.ControlTime.ToString(),
                                ObjectPassword = obj.s.ObjectPassword,
                                Phone1 = obj.s.Phone1,
                                Phone2 = obj.s.Phone2,
                                Phone3 = obj.s.Phone3,
                                ExtFields = objExtDict,
                                Owners = objOwnersList,
                                RemoteProgrammingGUID = string.IsNullOrEmpty(obj.s.TransmitterID) ? "нет" : "да"
                            });
                    }
                }




                //using (var context = new CRMServiceEntities())
                //{
                //    var obj = (from s in context.GuardObjectExtention select s).Where(x => x.GuardObjectId == strId).FirstOrDefault();

                //    if (obj != null)
                //    {
                //        return View(new GuardObjectModel() 
                //        { 
                //            Name = obj.Name,
                //            Address = obj.Address, 
                //            Number = obj.Number.ToString(), 
                //            NumberHex = obj.NumberHex,
                //            Sygnalizations = (obj.IsArm == true ? "Охранная " : "") + (obj.IsFire == true ? "Пожарная " : "") + (obj.IsPanic == true ? "Тревожная кнопка " : ""),
                //            ObjectType = obj.ObjTypeId.ToString(),
                //            EventTemplate = obj.EventTemplateId.ToString(),
                //            ControlTime = obj.ControlTime.HasValue ? (obj.ControlTime == 0 ? "не задано" : obj.ControlTime.ToString()) : "не задано",
                //            ObjectPassword = obj.ObjectPassword,
                //            Phone1 = obj.Phone1,
                //            Phone2 = obj.Phone2,
                //            Phone3 = obj.Phone3
                //        });
                //    }
                //}
            }
            catch (Exception exc) 
            {
                ViewBag.ExceptionMessage = exc.Message;
            }



            return View();
        }

    }
}
