using CRMService.Data;
using CRMService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using CRMService.Data.A28;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;

namespace CRMService.Controllers {
    public class GuardObjectController : Controller {
        private List<Data.A28.ObjectA28> GetSearchObjects(int searchParam, string searchStr) {
            try {
                var objects = new List<Data.A28.ObjectA28>();

                using (ObjectContext objectContext = new ObjectContext()) {
                    using (ObjExtFieldContext objExtFieldContext = new ObjExtFieldContext()) {
                        switch (searchParam) {
                            //дата ввода
                            case 1: {
                                List<ObjExtField> _objextfields = objExtFieldContext.ObjExtField.Where(y => y.ExtFieldID == 119 && y.ExtFieldValue == searchStr).ToList<ObjExtField>();
                                if (_objextfields.Any())
                                    foreach (Data.A28.ObjExtField item in _objextfields)
                                        objects.Add(objectContext.ObjectA28.FirstOrDefault(x => x.ObjectID == item.ObjectID && x.RecordDeleted == false));
                                break;
                            }
                            //номер объекта
                            case 2: {
                                int objNum = Convert.ToInt32(searchStr, 16);
                                foreach (Data.A28.ObjectA28 item in objectContext.ObjectA28.Where(x => x.ObjectNumber == objNum && x.RecordDeleted == false)) {
                                    objects.Add(item);
                                }
                                break;
                            }
                            //название
                            case 3: {
                                foreach (Data.A28.ObjectA28 item in objectContext.ObjectA28.Where(x => x.Name.ToLower().Contains(searchStr) && x.RecordDeleted == false)) {
                                    objects.Add(item);
                                }
                                break;
                            }
                            //адрес
                            case 4: {
                                if (string.IsNullOrEmpty(searchStr) || string.IsNullOrWhiteSpace(searchStr))
                                    throw new Exception("Строка поиска не может быть пустой");
                                string[] srch = new string[10];
                                srch = searchStr.Split(',');
                                string tmp = srch[0].Trim().ToLower();
                                List<Data.A28.ObjectA28> _o = objectContext.ObjectA28.Where(x => x.Address.ToLower().Contains(tmp) && x.RecordDeleted == false).ToList<Data.A28.ObjectA28>();
                                for (int i = 1; i < srch.Count(); i++) {
                                    tmp = srch[i].Trim().ToLower();
                                    _o = _o.Where(x => x.Address.ToLower().Contains(tmp)).ToList<Data.A28.ObjectA28>();
                                }
                                foreach (Data.A28.ObjectA28 item in _o) {
                                    objects.Add(item);
                                }
                                break;
                            }
                            default: break;
                        }
                        return objects;
                    }
                }


                //using (var context = new A28Entity()) {
                //    switch (searchParam) {
                //        case "1": {

                //            var dateIn = "";

                //            var obj = (from o in context.Object
                //                       join e in context.ObjExtField
                //                         on o.ObjectID equals e.ObjectID
                //                       join ef in context.ExtField
                //                         on e.ExtFieldID equals ef.ExtFieldID
                //                       select new { o, e, ef }).Where(x => x.e.ExtFieldID == 119 && x.e.ExtFieldValue == searchStr).AsEnumerable();

                //            foreach (var o in obj) {
                //                objects.Add(o.o);
                //            }

                //            break;
                //        }
                //        case "2":   //номер
                //            {
                //            int decObjNumber = -1;

                //            try {
                //                if (flag == 0)
                //                    decObjNumber = Convert.ToInt32(searchStr, 16);
                //            }
                //            catch (Exception exc) { }


                //            if (decObjNumber != -1) {
                //                objects = (from o in context.Object select o)
                //                .Where(x => x.RecordDeleted == false && x.ObjectNumber == decObjNumber)
                //                .OrderBy(x => x.ObjectNumber)
                //                .ToList();
                //            }

                //            break;
                //        }
                //        case "3":   //название
                //            {
                //            objects = (from o in context.Object select o)
                //                .Where(x => x.RecordDeleted == false && x.Name.Contains(searchStr))
                //                .OrderBy(x => x.ObjectNumber)
                //                .ToList();

                //            break;
                //        }
                //        case "4":   //адрес
                //            {
                //            //objects = (from o in context.Object select o)
                //            //    .Where(x => x.RecordDeleted == false && x.Address.Contains(searchStr))
                //            //    .OrderBy(x => x.ObjectNumber)
                //            //    .ToList();
                //            if (string.IsNullOrEmpty(searchStr) || string.IsNullOrWhiteSpace(searchStr))
                //                throw new Exception("Строка поиска не может быть пустой");
                //            string[] srch = new string[10];
                //            srch = searchStr.Split(',');
                //            string tmp = srch[0].Trim().ToLower();
                //            objects = (from o in context.Object select o)
                //                    .Where(x => x.RecordDeleted == false && x.Address.ToLower().Contains(tmp))
                //                    .OrderBy(x => x.ObjectNumber)
                //                    .ToList();
                //            for (int i = 1; i < srch.Count(); i++) {
                //                tmp = srch[i].Trim().ToLower();
                //                objects = (from o in objects select o)
                //                    .Where(x => x.RecordDeleted == false && x.Address.ToLower().Contains(tmp))
                //                    .OrderBy(x => x.ObjectNumber)
                //                    .ToList();
                //            }


                //            break;
                //        }
                //        default: {
                //            break;
                //        }
                //    }






                //    //int decObjNumber = -1;

                //    //try
                //    //{
                //    //    if (flag == 0)
                //    //        decObjNumber = Convert.ToInt32(searchStr, 16);                        
                //    //}
                //    //catch (Exception exc) { }


                //    //if (decObjNumber != -1)
                //    //{
                //    //    objects = (from o in context.Object select o)
                //    //    .Where(x => x.RecordDeleted == false && x.ObjectNumber == decObjNumber)
                //    //    .OrderBy(x => x.ObjectNumber)
                //    //        //.Skip((pageNumber - 1) * pageSize)
                //    //        //.Take(pageSize)
                //    //    .ToList();                        
                //    //}
                //    //else
                //    //{
                //    //    objects = (from o in context.Object select o)
                //    //    .Where(x => x.RecordDeleted == false && (x.Name.Contains(searchStr) || x.Address.Contains(searchStr)))
                //    //    .OrderBy(x => x.ObjectNumber)
                //    //        //.Skip((pageNumber - 1) * pageSize)
                //    //        //.Take(pageSize)
                //    //    .ToList();
                //    //}

                //    return objects;
                //}                
            }
            catch (Exception exc) { return null; }
        }
        private DateTime? ExtractDate(string @in) {
            string result = "";
            if (!string.IsNullOrEmpty(@in)) {
                foreach (char item in @in.ToCharArray()) {
                    if (char.IsDigit(item) || item == '.')
                        result += item;
                }
                DateTime r;
                if (DateTime.TryParse(result, out r))
                    return r.Date;
                else
                    return null;
            }
            else
                return null;
        }
        private string ExtractDate2(string @in) {
            string result = "";
            if (!string.IsNullOrEmpty(@in)) {
                foreach (char item in @in.ToCharArray()) {
                    if (char.IsDigit(item) || item == '.')
                        result += item;
                }
                DateTime r;
                if (DateTime.TryParse(result, out r))
                    return r.ToShortDateString();
                else
                    return null;
            }
            else
                return null;
        }

        public ActionResult Index(int? page, int searchParam = 0, string searchString = "") {
            try {
                List<Data.A28.ObjectA28> objects = GetSearchObjects(searchParam, searchString);
                List<GuardObjectModel> GuardObjectList = new List<GuardObjectModel>();

                if (objects.Any()) {
                    using (ObjExtFieldContext objExtFieldContext = new ObjExtFieldContext()) {
                        foreach (Data.A28.ObjectA28 item in objects) {
                            List<ObjExtField> exObj = objExtFieldContext.ObjExtField.Where(x => x.ObjectID == item.ObjectID).ToList<ObjExtField>();
                            //var t = DateTime.TryParse(objExtFieldContext.ObjExtField.FirstOrDefault(x => x.ExtFieldID == 119 && x.ObjectID == item.ObjectID).ExtFieldValue, out _) ?
                            //        DateTime.Parse(objExtFieldContext.ObjExtField.FirstOrDefault(x => x.ExtFieldID == 119 && x.ObjectID == item.ObjectID).ExtFieldValue).ToString("dd.MM.yyyy") :
                            //        string.IsNullOrEmpty(objExtFieldContext.ObjExtField.FirstOrDefault(x => x.ExtFieldID == 119 && x.ObjectID == item.ObjectID).ExtFieldValue) ?
                            //            " - " : objExtFieldContext.ObjExtField.FirstOrDefault(x => x.ExtFieldID == 119 && x.ObjectID == item.ObjectID).ExtFieldValue;
                            //var t = exObj.FirstOrDefault(y => y.ExtFieldID == 119).ExtFieldValue;
                            DateTime _dateIn;
                            GuardObjectList.Add(new GuardObjectModel() {
                                GuardObjectId = item.ObjectID.ToString(),
                                /*DateIn = DateTime.TryParse(objExtFieldContext.ObjExtField.FirstOrDefault(x => x.ExtFieldID == 119 && x.ObjectID == item.ObjectID).ExtFieldValue, out _) ?
                                    DateTime.Parse(objExtFieldContext.ObjExtField.FirstOrDefault(x => x.ExtFieldID == 119 && x.ObjectID == item.ObjectID).ExtFieldValue).ToString("dd.MM.yyyy") :
                                    string.IsNullOrEmpty(objExtFieldContext.ObjExtField.FirstOrDefault(x => x.ExtFieldID == 119 && x.ObjectID == item.ObjectID).ExtFieldValue) ?
                                        " - " : objExtFieldContext.ObjExtField.FirstOrDefault(x => x.ExtFieldID == 119 && x.ObjectID == item.ObjectID).ExtFieldValue,*/
                                _DateIn = exObj.Any(x => x.ExtFieldID == 119) ?
                                    DateTime.TryParse(exObj.FirstOrDefault(x => x.ExtFieldID == 119).ExtFieldValue, out _dateIn) ? _dateIn.Date /*new DateTime(_dateIn.Year, _dateIn.Month, _dateIn.Day)*/:
                                        ExtractDate(exObj.FirstOrDefault(x => x.ExtFieldID == 119).ExtFieldValue) :
                                null,
                                DateIn = exObj.Any(x => x.ExtFieldID == 119) ?
                                    DateTime.TryParse(exObj.FirstOrDefault(x => x.ExtFieldID == 119).ExtFieldValue, out _dateIn) ? _dateIn.ToShortDateString() :
                                        ExtractDate2(exObj.FirstOrDefault(x => x.ExtFieldID == 119).ExtFieldValue) :
                                null,
                                //TODO: вероятно требуется обратное преобразование из базы андромеды
                                //Number = item.ObjectNumber.ToString(),
                                Number = Convert.ToString(item.ObjectNumber, 16),
                                Name = item.Name.ToString(),
                                Address = item.Address
                            });
                        }
                    }
                }
                if (GuardObjectList.Any()) {
                    var model = new StaticPagedList<GuardObjectModel>(GuardObjectList.OrderBy(x => x._DateIn), (page ?? 1), 3000, objects.Count);

                    ViewBag.SearchString = searchString;
                    return View(model);
                }
                else {
                    GuardObjectList.Add(new GuardObjectModel() {
                        GuardObjectId = " ",
                        _DateIn = null,
                        DateIn = " ",
                        Number = " ",
                        Name = " ",
                        Address = " "
                    });

                    var model = new StaticPagedList<GuardObjectModel>(GuardObjectList, (page ?? 1), 3000, objects.Count);

                    ViewBag.SearchString = searchString;
                    return View(model);
                }

                //GuardObjectList.Add(new GuardObjectModel() {
                //    GuardObjectId = g.ObjectID.ToString(),
                //    DateIn = !DateTime.TryParse(dateIn, out _) ? string.IsNullOrEmpty(dateIn) ? "-" : dateIn : DateTime.Parse(dateIn).ToString("dd.MM.yyyy"),
                //    Number = g.ObjectNumber.ToString("X"),
                //    Name = g.Name.ToString(),
                //    Address = string.IsNullOrEmpty(g.Address.ToString()) ? "" : g.Address.ToString()
                //});

                //var actualAreas = DB_BLL.GetUserAreas();
                //if ((from a in actualAreas select a).Where(x => x.HrefLink == "/guardobject/").FirstOrDefault() == null)
                //    return View();



                //using (var context = new CRMServiceEntities())
                //using (var context = new A28Entity())
                //{
                //    int count = 0;
                //IEnumerable<dynamic> tmpObjects = null;
                //IEnumerable<dynamic> objects = null;

                //var objects1 = new List<Data.A28.Object>();
                //var tmpObjects = new List<Data.A28.Object>();

                //if (string.IsNullOrEmpty(searchString) && (searchParam == "0" || searchParam == ""))
                //{
                //    objects1 = (from o in context.Object select o).Where(x => x.RecordDeleted == false).OrderBy(x => x.ObjectNumber).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                //}
                //else
                //{
                //    {
                //        {
                //            var temp = new List<Data.A28.Object>();
                //                temp = GetSearchObjects(searchParam, searchString, 0);

                //            if (tmpObjects.Count == 0)
                //            {
                //                tmpObjects = temp;
                //                objects1 = temp;
                //            }
                //            else
                //            {
                //                if (temp.Count() > 0)
                //                {
                //                    objects1 = null;
                //                }
                //                foreach(var t in temp)
                //                {
                //                    var isExist = (from i in tmpObjects select i).Where(x => x == t).FirstOrDefault();
                //                    if (isExist != null)
                //                    {
                //                        objects1.Add(isExist);
                //                    }
                //                }
                //            }
                //        }
                //    }
                //    count = objects1.Count();
                //    objects1 = objects1.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                //}


                //var GuardObjectList = new List<GuardObjectModel>();

                //if (!(string.IsNullOrEmpty(searchString)))
                //{
                //    //ViewBag.Servicemans = servicemans;

                //    foreach (var g in objects1)
                //    {
                //        var dateIn = "";

                //        var objExt = (from e in context.ObjExtField
                //                      join ef in context.ExtField
                //                      on e.ExtFieldID equals ef.ExtFieldID
                //                      select new { e, ef }).Where(x => x.e.ObjectID == g.ObjectID).AsEnumerable();

                //        var objExtDict = new Dictionary<string, string>();
                //        foreach (var e in objExt)
                //        {
                //            if (e.e.ExtFieldID == 119)
                //            {
                //                dateIn = e.e.ExtFieldValue;
                //            }
                //        }

                //        ///////////////////////////////////////////



                //        var objectNumberHexTmp = Convert.ToInt32(g.ObjectNumber.ToString(), 10);
                //        var objectNumberHex = Convert.ToInt32(objectNumberHexTmp.ToString(), 10);

                //        GuardObjectList.Add(new GuardObjectModel()
                //        {
                //            GuardObjectId = g.ObjectID.ToString(),
                //            DateIn = !DateTime.TryParse(dateIn,out _) ? string.IsNullOrEmpty(dateIn) ? "-" : dateIn : DateTime.Parse(dateIn).ToString("dd.MM.yyyy"),
                //            Number = g.ObjectNumber.ToString("X"),
                //            Name = g.Name.ToString(),
                //            Address = string.IsNullOrEmpty(g.Address.ToString()) ? "" : g.Address.ToString()
                //        });

                //        GuardObjectList = GuardObjectList.OrderByDescending(x => x.DateIn).ToList<GuardObjectModel>();
                //    }
                //}

                //var model = new StaticPagedList<GuardObjectModel>(GuardObjectList, pageNumber, pageSize, count);

                //    ViewBag.SearchString = searchString;                   


                //int pageSize = 50;
                //IEnumerable<GuardObjectModel> guardObjectsPerPages = GuardObjectList.Skip((page - 1) * pageSize).Take(pageSize);
                //PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = GuardObjectList.Count };
                //IndexViewGuardObjectModel ivgom = new IndexViewGuardObjectModel { PageInfo = pageInfo, GuardObjects = guardObjectsPerPages };

                //return View(GuardObjectList.ToPagedList(page, pageSize));
                //return View(model);
                //return View(ivgom);

                //}
            }
            catch (Exception exc) {
                var e = exc.Message;
            }


            return View();
        }


        public ActionResult Info(string guardobjectId) {
            try {
                //var actualAreas = DB_BLL.GetUserAreas();
                //if ((from a in actualAreas select a).Where(x => x.HrefLink == "/guardobject/").FirstOrDefault() == null)
                //    return View();

                //try
                //{
                //    ViewBag.CurrUser = User.Identity.Name.ToString(); 
                //}
                //catch (Exception exc) { ViewBag.CurrUser = exc.Message; }

                //ViewBag.DTNow = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
                var strId = Int32.Parse(guardobjectId);

                List<Data.A28.ObjectA28> objects = new List<Data.A28.ObjectA28>();
                List<EventTemp> eventTemps = new List<EventTemp>();
                List<ObjType> objTypes = new List<ObjType>();
                List<ObjExtField> objExtFields = new List<ObjExtField>();
                List<ExtField> extFields = new List<ExtField>();

                List<_extFields> _ExtFields = new List<_extFields>();
                List<_additionalServices> _AdditionalServices = new List<_additionalServices>();

                using (ObjectContext objectContext = new ObjectContext()) {
                    using (EventTempContext eventTempContext = new EventTempContext()) {
                        using (ObjTypeContext objTypeContext = new ObjTypeContext()) {
                            using (ObjExtFieldContext objExtFieldContext = new ObjExtFieldContext()) {
                                using (ExtFieldContext extFieldContext = new ExtFieldContext()) {
                                    int r = int.Parse(guardobjectId);
                                    objects = objectContext.ObjectA28.Where(x => x.RecordDeleted == false && x.ObjectID == r).ToList();
                                    foreach (Data.A28.ObjectA28 item in objects) {
                                        eventTemps = eventTempContext.EventTemp.Where(x => x.RecordDeleted == false && x.EventTemplateID == item.EventTemplateID).ToList();
                                        objTypes = objTypeContext.ObjType.Where(x => x.RecordDeleted == false && x.ObjTypeID == item.ObjTypeID).ToList();
                                        objExtFields = objExtFieldContext.ObjExtField.Where(x => x.ObjectID == item.ObjectID).ToList();
                                        foreach (ObjExtField _item in objExtFields) {
                                            if ((extFieldContext.ExtField.FirstOrDefault(x => x.RecordDeleted == false && x.ExtFieldID == _item.ExtFieldID).ExtFieldID == 113)
                                                //|| (extFieldContext.ExtField.FirstOrDefault(x => x.RecordDeleted == false && x.ExtFieldID == _item.ExtFieldID).ExtFieldID == 119)
                                                || (extFieldContext.ExtField.FirstOrDefault(x => x.RecordDeleted == false && x.ExtFieldID == _item.ExtFieldID).ExtFieldID == 145)
                                                || (extFieldContext.ExtField.FirstOrDefault(x => x.RecordDeleted == false && x.ExtFieldID == _item.ExtFieldID).ExtFieldID == 147)
                                                || (extFieldContext.ExtField.FirstOrDefault(x => x.RecordDeleted == false && x.ExtFieldID == _item.ExtFieldID).ExtFieldID == 149)) {
                                                _ExtFields.Add(new _extFields(
                                                extFieldContext.ExtField.FirstOrDefault(x => x.RecordDeleted == false && x.ExtFieldID == _item.ExtFieldID).ExtFieldName,
                                                _item.ExtFieldValue
                                                ));
                                            }
                                            if (extFieldContext.ExtField.FirstOrDefault(x => x.RecordDeleted == false && x.ExtFieldID == _item.ExtFieldID).ExtFieldID == 119) {
                                                DateTime _dt;
                                                _ExtFields.Add(new _extFields(
                                                extFieldContext.ExtField.FirstOrDefault(x => x.RecordDeleted == false && x.ExtFieldID == _item.ExtFieldID).ExtFieldName,
                                                DateTime.TryParse(_item.ExtFieldValue, out _dt) ? _dt.ToString("dd.MM.yyyy") : _item.ExtFieldValue
                                                ));
                                            }
                                            //extFields = extFieldContext.ExtField.Where(x => x.RecordDeleted == false && x.ExtFieldID == _item.ExtFieldID).ToList();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                List<EPCustomer> ePCustomers = new List<EPCustomer>();
                List<EP> ePs = new List<EP>();
                using (EPCustomerContext ePCustomerContext = new EPCustomerContext()) {
                    int onum = objects.FirstOrDefault().ObjectNumber;
                    ePCustomers = ePCustomerContext.EPCustomer.Where(x => x.BeginningNumber <= onum && x.EndNumber >= onum).ToList();
                    using (EPContext ePContext = new EPContext()) {
                        foreach (EPCustomer item in ePCustomers) {
                            foreach (EP _item in ePContext.EP.Where(x => x.ProcID == item.OwnerRecordID && x.Enabled == true && x.ProcGroupID == 30))
                                ePs.Add(_item);
                            //TODO: Вероятно номер телефона мы уже получили и дальше не надо ничего делать с смсками.
                        }
                    }
                }
                if (ePs.Any())
                    _AdditionalServices.Add(new _additionalServices("СМС", true));
                List<ObjAdmin> objAdmins = new List<ObjAdmin>();
                using (ObjAdminContext objAdminContext = new ObjAdminContext()) {
                    int objAdminID = 0;
                    int.TryParse(objects.FirstOrDefault().ObjAdminID.ToString(), out objAdminID);
                    if (objAdminID != 0)
                        objAdmins = objAdminContext.ObjAdmin.Where(x => x.RecordDeleted == false && x.ObjAdminID == objAdminID).ToList();
                }
                List<ObjCust> objCustsAdmins = new List<ObjCust>();
                int objID = objects.FirstOrDefault().ObjectID;
                using (ObjCustContext objCustContext = new ObjCustContext()) {
                    objCustsAdmins = objCustContext.ObjCust.Where(x => x.ObjectID == objID && (x.MyAlarmUserRole == 1 || x.MyAlarmUserRole == 2)).ToList();
                }
                if (objAdmins.Any()) {
                    foreach (ObjAdmin admin in objAdmins) {
                        objCustsAdmins.Add(new ObjCust() { ObjCustName = admin.AdminName, ObjCustPhone1 = admin.AdminPhone, Role = "Администратор(старый ЛК)" });
                    }
                }
                if (!objAdmins.Any() && !objCustsAdmins.Any())
                    objCustsAdmins.Add(new ObjCust() { ObjCustName = "Администраторов и пользователей ", ObjCustPhone1 = " нет" });

                List<ObjCust> objCusts = new List<ObjCust>();
                //int objID = objects.FirstOrDefault().ObjectID;
                using (ObjCustContext objCustContext = new ObjCustContext()) {
                    objCusts = objCustContext.ObjCust.Where(x => x.ObjectID == objID).ToList();
                }
                List<ObjSchedule> objSchedules = new List<ObjSchedule>();
                using (ObjScheduleContext objScheduleContext = new ObjScheduleContext()) {
                    objSchedules = objScheduleContext.ObjSchedule.Where(x => x.ObjectID == objID).ToList();
                    if (objSchedules.Any())
                        if (objects.Any(x => x.ArmSchedule_EarlyArm == true || x.ArmSchedule_LaterArm == true || x.ArmSchedule_EarlyDisarm == true || x.ArmSchedule_LaterDisarm == true))
                            _AdditionalServices.Add(new _additionalServices("КР", true));

                    //if (objSchedules.Any()) {
                    //    _AdditionalServices.Add(new _additionalServices("КР", true));
                    //}
                    //foreach (ObjectA28 item in objects) {
                    //    if (item.ArmSchedule_EarlyArm)
                    //        _AdditionalServices.Add(new _additionalServices("Взятие раньше времени", true));
                    //    if (item.ArmSchedule_EarlyDisarm)
                    //        _AdditionalServices.Add(new _additionalServices("Снятие раньше времени", true));
                    //    if (item.ArmSchedule_LaterArm)
                    //        _AdditionalServices.Add(new _additionalServices("Взятие позже времени", true));
                    //    if (item.ArmSchedule_LaterDisarm)
                    //        _AdditionalServices.Add(new _additionalServices("Снятие позже времени", true));
                    //}
                }

                return View(new GuardObjectModel() {
                    Name = objects.FirstOrDefault().Name,
                    Address = objects.FirstOrDefault().Address,
                    //Number = objects.FirstOrDefault().ObjectNumber.ToString(),
                    Number = Convert.ToString(objects.FirstOrDefault().ObjectNumber, 16),
                    Sygnalizations = (objects.FirstOrDefault().IsArm ? " ОС " : null) + (objects.FirstOrDefault().IsFire ? " ПС " : null) + (objects.FirstOrDefault().IsPanic ? " ТРС " : null),
                    ObjectType = objTypes.FirstOrDefault().ObjTypeName,
                    EventTemplate = eventTemps.FirstOrDefault().EventTemplateName,
                    ControlTime = int.TryParse(objects.FirstOrDefault().ControlTime.ToString(), out _) ? objects.FirstOrDefault().ControlTime.ToString() : "нет данных",
                    ObjectPassword = objects.FirstOrDefault().ObjectPassword,
                    Phone1 = objects.FirstOrDefault().Phone1,
                    Phone2 = objects.FirstOrDefault().Phone2,
                    Phone3 = objects.FirstOrDefault().Phone3,
                    ExtFields = _ExtFields,
                    Owners = objCusts,
                    RemoteProgrammingGUID = string.IsNullOrEmpty(objects.FirstOrDefault().TransmitterID) ? "да" : "нет",
                    CustAdmins = objCustsAdmins,
                    SendSMS = ePs,
                    addServices = _AdditionalServices

                    //Name = obj.s.Name,
                    //Address = obj.s.Address,
                    //Number = obj.s.ObjectNumber.ToString("X"),
                    //Sygnalizations = (obj.s.IsArm == true ? "Охранная " : "") + (obj.s.IsFire == true ? "Пожарная " : "") + (obj.s.IsPanic == true ? "Тревожная кнопка " : ""),
                    //ObjectType = obj.t.ObjTypeName,
                    //EventTemplate = obj.e.EventTemplateName,
                    //ControlTime = obj.s.ControlTime == 0 ? "не задано" : obj.s.ControlTime.ToString(),
                    //ObjectPassword = obj.s.ObjectPassword,
                    //Phone1 = obj.s.Phone1,
                    //Phone2 = obj.s.Phone2,
                    //Phone3 = obj.s.Phone3,
                    //ExtFields = objExtDict,
                    //Owners = objOwnersList,
                    //RemoteProgrammingGUID = string.IsNullOrEmpty(obj.s.TransmitterID) ? "нет" : "да"
                });



                //using (var context = new A28Entities()) {
                //    var obj = (from s in context.Object
                //               join e in context.EventTemp on s.EventTemplateID equals e.EventTemplateID
                //               join t in context.ObjType on s.ObjTypeID equals t.ObjTypeID
                //               select new { s, e, t }).Where(x => x.s.RecordDeleted == false && x.s.ObjectID == strId).FirstOrDefault();

                //    var objExt = (from e in context.ObjExtField
                //                  join ef in context.ExtField
                //                  on e.ExtFieldID equals ef.ExtFieldID select new { e, ef }).Where(x => x.e.ObjectID == obj.s.ObjectID).AsEnumerable();

                //    var objExtDict = new Dictionary<string, string>();
                //    foreach (var e in objExt) {
                //        if ((e.e.ExtFieldID == 113) || (e.e.ExtFieldID == 119) || (e.e.ExtFieldID == 145) || (e.e.ExtFieldID == 147) || (e.e.ExtFieldID == 149)) {
                //            objExtDict.Add(e.ef.ExtFieldName, e.e.ExtFieldValue);
                //        }
                //    }



                //    //поля на отправку СМС и ЛК
                //    var actualObj = (from a in context.Object select a).Where(x => x.ObjectID == strId).FirstOrDefault();
                //    if (actualObj != null) {
                //        //СМС
                //        var epCustomers = (from e in context.EPCustomer select e).Where(x => (x.BeginningNumber <= actualObj.ObjectNumber) && (x.EndNumber >= actualObj.ObjectNumber)).AsEnumerable();

                //        var smsParams = "";

                //        foreach (var ep in epCustomers) {
                //            var procs = (from p in context.EP select p).Where(x => (x.ProcID == ep.OwnerRecordID) && (x.ProcGroupID == 30) && (x.Enabled == true)).AsEnumerable();

                //            foreach (var proc in procs) {
                //                var procParams = "";

                //                try {
                //                    procParams = proc.Params.Split(';')[0].Split('=')[1] + "\n";
                //                }
                //                catch (Exception exc) { }

                //                smsParams += procParams;
                //            }
                //        }

                //        objExtDict.Add("Отправка СМС", smsParams);

                //        //Доп. услуги


                //        //ЛК(старый)
                //        var objAdmin = (from a in context.ObjAdmin select a).Where(x => x.ObjAdminID == actualObj.ObjAdminID /*&& x.ObjectID == strId*/).FirstOrDefault();
                //        if (objAdmin != null) {
                //            objExtDict.Add("Администратор (старый ЛК)", objAdmin.AdminName + "   " + "тел. " + objAdmin.AdminPhone + "   " + "email: " + objAdmin.AdminEmail);
                //        }
                //        //else
                //        //{
                //        //    objExtDict.Add("Администратор (старый ЛК)", "");
                //        //}
                //        //ЛК MyAlarm
                //        //2 - пользователь
                //        //1 - администратор
                //        var objAdminMyAlarm = (from a in context.ObjCust select a).Where(x => x.MyAlarmUserRole == 1 && x.ObjectID == strId);
                //        if (objAdminMyAlarm.Any()) {
                //            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
                //            foreach (var item in objAdminMyAlarm) {
                //                stringBuilder.AppendLine(item.ObjCustName + "   " + "тел. " + item.ObjCustPhone1);
                //            }
                //            objExtDict.Add("Администратор(ы) (MyAlarm)", stringBuilder.ToString());
                //        }
                //        else {
                //            var objUsersMyAlarm = (from a in context.ObjCust select a).Where(x => x.MyAlarmUserRole == 2 && x.ObjectID == strId);
                //            if (objUsersMyAlarm.Any()) {
                //                System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
                //                foreach (var item in objUsersMyAlarm) {
                //                    stringBuilder.AppendLine(item.ObjCustName + "   " + "тел. " + item.ObjCustPhone1);
                //                }
                //                objExtDict.Add("Пользователь(и) (MyAlarm)", stringBuilder.ToString());
                //            }
                //        }
                //        if (objAdmin == null && objAdminMyAlarm.Count() == 0)
                //            objExtDict.Add("Администратор(ы)", " - ");
                //    }


                //    ///////////////////////////




                //    var objOwners = (from o in context.ObjCust select o).Where(x => x.ObjectID == obj.s.ObjectID).OrderBy(x => x.UserNumber).AsEnumerable();

                //    var objOwnersList = new List<ObjCust>();
                //    foreach (var owner in objOwners) {
                //        objOwnersList.Add(owner);
                //    }


                //    if (obj != null) {
                //        return View(new GuardObjectModel() {
                //            Name = obj.s.Name,
                //            Address = obj.s.Address,
                //            Number = obj.s.ObjectNumber.ToString("X"),
                //            Sygnalizations = (obj.s.IsArm == true ? "Охранная " : "") + (obj.s.IsFire == true ? "Пожарная " : "") + (obj.s.IsPanic == true ? "Тревожная кнопка " : ""),
                //            ObjectType = obj.t.ObjTypeName,
                //            EventTemplate = obj.e.EventTemplateName,
                //            ControlTime = obj.s.ControlTime == 0 ? "не задано" : obj.s.ControlTime.ToString(),
                //            ObjectPassword = obj.s.ObjectPassword,
                //            Phone1 = obj.s.Phone1,
                //            Phone2 = obj.s.Phone2,
                //            Phone3 = obj.s.Phone3,
                //            ExtFields = objExtDict,
                //            Owners = objOwnersList,
                //            RemoteProgrammingGUID = string.IsNullOrEmpty(obj.s.TransmitterID) ? "нет" : "да"
                //        });
                //    }
                //}




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
            catch (Exception exc) {
                ViewBag.ExceptionMessage = exc.Message;
            }



            return View();
        }

    }
}
