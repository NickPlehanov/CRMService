using CRMService.Models;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Web;

namespace CRMService.Data
{
    public class DB_BLL
    {
        public static void ServicemanDB(ServicemanModel serviceman, int flag)            //1 - create, 2 - edit, 3 - delete
        {
            try
            {
                switch(flag)
                {
                    case 1:
                        {
                            ServicemanCreate(serviceman);
                            break;
                        }

                    case 2:
                        {
                            ServicemanEdit(serviceman);
                            break;
                        }
                }
            }
            catch (Exception exc)
            {
                throw new Exception();
            }
        }

        private static void ServicemanCreate(ServicemanModel serviceman)
        {
            try
            {
                using (var context = new CRMServiceEntities())
                {
                    var currentUser = (from c in context.Systemuser select c).Where(x => x.ADName == Environment.UserDomainName + @"\" + Environment.UserName).FirstOrDefault();

                    var servicemanId = Guid.NewGuid();

                    var servicemanBase = new Serviceman()
                    {
                        ServicemanId = servicemanId,
                        CreatedOn = DateTime.Now,
                        CreatedBy = currentUser.SystemuserId,
                        ModifiedOn = DateTime.Now,
                        ModifiedBy = currentUser.SystemuserId,
                        DeletionStateCode = 0
                    };

                    var servicemanExtensionBase = new ServicemanExtension()
                    {
                        ServicemanId = servicemanId,
                        Name = serviceman.Name,
                        Password = serviceman.password,
                        Phone = serviceman.Phone
                    };

                    context.Serviceman.Add(servicemanBase);
                    context.ServicemanExtension.Add(servicemanExtensionBase);

                    context.SaveChanges();
                }
            }
            catch (Exception exc) 
            {
                throw new Exception();
            }
        }

        private static void ServicemanEdit(ServicemanModel serviceman)
        {
            try
            {
                using (var context = new CRMServiceEntities())
                {
                    var currentUser = (from c in context.Systemuser select c).Where(x => x.ADName == Environment.UserDomainName + @"\" + Environment.UserName).FirstOrDefault();

                    var currentServicemanId = new Guid(serviceman.ServicemanId);

                    var currentServiceman = (from c in context.ServicemanExtension select c).Where(x => x.ServicemanId == currentServicemanId).FirstOrDefault();

                    if (currentServiceman != null)
                    {
                        currentServiceman.Name = serviceman.Name;
                        currentServiceman.Password = serviceman.password;
                        currentServiceman.Phone = serviceman.Phone;
                    }

                    context.SaveChanges();
                }
            }
            catch (Exception exc)
            {
                throw new Exception();
            }
        }



        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public static void ServiceorderDB(ServiceorderModel serviceorder, int flag)            //1 - create, 2 - edit, 3 - delete
        {
            try
            {
                switch (flag)
                {
                    case 1:
                        {
                            ServiceorderCreate(serviceorder);
                            break;
                        }

                    case 2:
                        {
                            ServiceorderEdit(serviceorder);
                            break;
                        }
                }
            }
            catch (Exception exc)
            {
                throw new Exception();
            }
        }

        private static void ServiceorderCreate(ServiceorderModel serviceorder)
        {
            try
            {
                using (var context = new CRMServiceEntities())
                {
                    var currentUser = (from c in context.Systemuser select c).Where(x => x.ADName == Environment.UserDomainName + @"\" + Environment.UserName).FirstOrDefault();

                    var serviceorderId = Guid.NewGuid();

                    var serviceorderBase = new Serviceorder()
                    {
                        ServiceorderId = serviceorderId,
                        CreatedOn = DateTime.Now,
                        CreatedBy = currentUser.SystemuserId,
                        ModifiedOn = DateTime.Now,
                        ModifiedBy = currentUser.SystemuserId,
                        DeletionStateCode = 0
                    };

                    var serviceorderExtensionBase = new ServiceorderExtension()
                    {
                        ServiceorderId = serviceorderId,
                        Name = serviceorder.Name,
                        Date = serviceorder.Date,
                        Serviceman_start = new Guid(serviceorder.ServicemanStart),
                        Serviceman_end = new Guid(serviceorder.ServicemanEnd),
                        Income = serviceorder.Income,
                        Outgone = serviceorder.Outgone,
                        Comment = serviceorder.Comment,
                        Category = serviceorder.Category,
                        Who_init = serviceorder.WhoInit,
                        Moved = serviceorder.Moved,
                        Result = serviceorder.Result,
                        //Moved_reason = serviceorder.MovedReason,
                        ResultId = serviceorder.ResultId
                    };

                    context.Serviceorder.Add(serviceorderBase);
                    context.ServiceorderExtension.Add(serviceorderExtensionBase);

                    context.SaveChanges();
                }
            }
            catch (Exception exc)
            {
                throw new Exception();
            }
        }

        private static void ServiceorderEdit(ServiceorderModel serviceorder)
        {
            try
            {
                using (var context = new CRMServiceEntities())
                {
                    var currentUser = (from c in context.Systemuser select c).Where(x => x.ADName == Environment.UserDomainName + @"\" + Environment.UserName).FirstOrDefault();

                    var currentServiceorderId = new Guid(serviceorder.ServiceorderId);

                    var currentServiceorder = (from c in context.ServiceorderExtension select c).Where(x => x.ServiceorderId == currentServiceorderId).FirstOrDefault();

                    if (currentServiceorder != null)
                    {
                        currentServiceorder.Name = serviceorder.Name;
                        currentServiceorder.Date = serviceorder.Date;
                        currentServiceorder.Serviceman_start = new Guid(serviceorder.ServicemanStart);
                        currentServiceorder.Serviceman_end = new Guid(serviceorder.ServicemanEnd);
                        currentServiceorder.Income = serviceorder.Income;
                        currentServiceorder.Outgone = serviceorder.Outgone;
                        currentServiceorder.Comment = serviceorder.Comment;
                        currentServiceorder.Category = serviceorder.Category;
                        currentServiceorder.Who_init = serviceorder.WhoInit;
                        currentServiceorder.Moved = serviceorder.Moved;
                        currentServiceorder.Result = serviceorder.Result;
                        //Moved_reason = serviceorder.MovedReason;
                        currentServiceorder.ResultId = serviceorder.ResultId;
                    }

                    context.SaveChanges();
                }
            }
            catch (Exception exc)
            {
                throw new Exception();
            }
        }


        //обновление списка пользователей из Active Directory
        public static void UsersSync()
        {
            try
            {
                //using (var crmContext = new CRMServiceEntities())
                //{
                    using (var context = new PrincipalContext(ContextType.Domain, "VZ"))
                    {
                        using (var searcher = new PrincipalSearcher(new UserPrincipal(context)))
                        {
                            foreach (var result in searcher.FindAll())
                            {
                                DirectoryEntry de = result.GetUnderlyingObject() as DirectoryEntry;

                                //var firstname = de.Properties["givenName"].Value != null ? de.Properties["givenName"].Value.ToString() : "";
                                //var lastname = de.Properties["sn"].Value != null ? de.Properties["sn"].Value.ToString() : "";
                                //var accountname = de.Properties["samAccountName"].Value != null ? de.Properties["samAccountName"].Value.ToString() : "";

                               // var compareName = "VZ\"" + accountname;

                                /*var existUser = (from e in crmContext.User select e).Where(x => x.UserDomainName == accountname).FirstOrDefault();
                                if (existUser != null)      //обновляем
                                {
                                    existUser.UserFullname = lastname + " " + firstname;
                                }
                                else            //добавляем нового пользователя
                                {
                                    crmContext.User.Add(new User() { 
                                        UserFullname = lastname + " " + firstname,
                                        UserDomainName = compareName
                                    });
                                }*/
                            }
                        //}

                        //crmContext.SaveChanges();
                    }                
                }
                
            }
            catch (Exception exc) { }
        }

        //Синхронизация баз объектов Андромеды и CRM, запускается автоматически и периодически в отдельном потоке
        public static void ObjectSync()
        {
            try
            {
                Logger.WriteString(@"D:/CRMService/Logs/", Logger.GetLogFileName(), DateTime.Now.ToString("HH:mm:ss.fff") + " || " + "START SYNC");

                var t = 0;

                using (var a_context = new A28Entities())
                {
                    using (var c_context = new CRMServiceEntities())
                    {
                        var currentUser = (from c in c_context.Systemuser select c).Where(x => x.ADName == Environment.UserDomainName + @"\" + Environment.UserName).FirstOrDefault();

                        var actualObject = (from a in a_context.Object select a).Where(x => x.RecordDeleted == false).AsEnumerable();

                        foreach(var obj in actualObject)
                        {
                            //проходим по бд crm
                            var actualCRMObject = (from c in c_context.GuardObject
                                                   join ce in c_context.GuardObjectExtention
                                                   on c.GuardObjectId equals ce.GuardObjectId
                                                   select new { c, ce })
                                                                                   .Where(x => x.ce.Number == obj.ObjectNumber).FirstOrDefault();

                            if (actualCRMObject != null)    //обновляем
                            {
                                actualCRMObject.ce.A28Id = obj.ObjectID.ToString();
                                actualCRMObject.ce.Number = obj.ObjectNumber;
                                actualCRMObject.ce.Name = obj.Name;
                                actualCRMObject.ce.Address = obj.Address;
                                actualCRMObject.ce.NumberHex = obj.ObjectNumber.ToString("X");
                                actualCRMObject.ce.IsArm = obj.IsArm;
                                actualCRMObject.ce.IsFire = obj.IsFire;
                                actualCRMObject.ce.IsPanic = obj.IsPanic;
                                actualCRMObject.ce.ObjTypeId = obj.ObjTypeID;
                                actualCRMObject.ce.EventTemplateId = obj.EventTemplateID;
                                actualCRMObject.ce.ControlTime = obj.ControlTime;
                                actualCRMObject.ce.ObjectPassword = obj.ObjectPassword;
                                actualCRMObject.ce.Phone1 = obj.Phone1;
                                actualCRMObject.ce.Phone2 = obj.Phone2;
                                actualCRMObject.ce.Phone3 = obj.Phone3;




                                actualCRMObject.c.DeletionStateCode = 0;
                                actualCRMObject.c.ModifiedOn = DateTime.Now;
                                actualCRMObject.c.ModifiedBy = currentUser.SystemuserId;
                            }
                            else    //иначе добавляем новый                            
                            {
                                var guardObjectId = Guid.NewGuid();

                                var objectBase = new GuardObject() 
                                {
                                    GuardObjectId = guardObjectId,
                                    CreatedOn = DateTime.Now,
                                    CreatedBy = currentUser.SystemuserId,
                                    ModifiedOn = DateTime.Now,
                                    ModifiedBy = currentUser.SystemuserId,
                                    DeletionStateCode = 0                                    
                                };

                                var objectExtensionBase = new GuardObjectExtention()
                                {
                                    GuardObjectId = guardObjectId,
                                    Number = obj.ObjectNumber,
                                    Name = obj.Name,
                                    Address = obj.Address,
                                    A28Id = obj.ObjectID.ToString(),
                                    NumberHex = obj.ObjectNumber.ToString("X"),
                                    IsArm = obj.IsArm,
                                    IsFire = obj.IsFire,
                                    IsPanic = obj.IsPanic,
                                    ObjTypeId = obj.ObjTypeID,                                    
                                    EventTemplateId = obj.EventTemplateID,
                                    ControlTime = obj.ControlTime,
                                    ObjectPassword = obj.ObjectPassword,
                                    Phone1 = obj.Phone1,
                                    Phone2 = obj.Phone2,
                                    Phone3 = obj.Phone3
                                };

                                c_context.GuardObject.Add(objectBase);
                                c_context.GuardObjectExtention.Add(objectExtensionBase);
                            }

                            t++;
                        }


                        
                        //заново пробегаем по crm базе и смотрим, есть ли старые обхекты, которых нет в андромеде, т.е. их надо удалить - DeletionStateCode = 1
                        var crmObject = (from c in c_context.GuardObject
                                         join ce in c_context.GuardObjectExtention
                                             on c.GuardObjectId equals ce.GuardObjectId
                                         select new { c, ce }).AsEnumerable();

                        foreach(var obj in crmObject)
                        {
                            var actualAObject = (from a in a_context.Object select a).Where(x => x.ObjectNumber == obj.ce.Number && x.RecordDeleted == false).FirstOrDefault();

                            if (actualAObject == null)   //тогда обхект надо пометить как удаленный
                            {
                                obj.c.DeletionStateCode = 1;
                            }
                        }

                        c_context.SaveChanges();
                    }
                }

                Logger.WriteString(@"D:/CRMService/Logs/", Logger.GetLogFileName(), DateTime.Now.ToString("HH:mm:ss.fff") + " || " + "END SYNC");
            }
            catch (Exception exc) { }
        }

        public static List<AreaModel> GetUserAreas()
        {
            try
            {
                var areasList = new List<AreaModel>();

                //var currentUser = System.Web.HttpContext.Current.User.Identity.Name;
                var currentUser = "VZ\\nik";

                using (var context = new CRMServiceEntities())
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


                        foreach (var actualArea in actualAreas)
                        {
                            areasList.Add(new AreaModel()
                            {
                                Name = actualArea.a.AreaName,
                                HrefLink = actualArea.a.AreaHrefLink
                            });
                        }
                    }
                }

                return areasList;
            }
            catch (Exception exc) { return null; }
        }
    }   

}