using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace CRMService.Data.A28 {
    [Table("ObjCust")]
    public class ObjCust {
        [Key]
        public int RecordID { get; set; }
        public int ObjectID { get; set; }
        public string ObjCustName { get; set; }
        public string ObjCustPhone1 { get; set; }
        public string ObjCustPhone2 { get; set; }
        public string ObjCustPhone3 { get; set; }
        public string ObjCustPhone4 { get; set; }
        public string ObjCustPhone5 { get; set; }
        public string ObjCustAddress { get; set; }
        public bool? MyAlarmPanicEnabled { get; set; }
        public int? MyAlarmUserRole { get; set; }
        public int? UserNumber { get; set; }
        public string ObjCustTitle { get; set; }
        public int? OrderNumber { get; set; }
        [NotMapped]
        public string Role {
            get => _Role;
            set {
                _Role = value;
            }
        }
        //TODO: переделать на запрос к ObjAdmin
        private string _Role {
            get {
                int param;
                if (int.TryParse(MyAlarmUserRole.ToString(), out param))
                    if (param == 1)
                        return "(Администратор)";
                    else if (param == 2)
                        return "(Пользователь)";
                    else
                        return null;
                //else {
                //    using (ObjectContext objectContext = new ObjectContext()) {
                //        if (objectContext.ObjectA28.Any(x => x.ObjectID == ObjectID)) {
                //            int? adminID = objectContext.ObjectA28.FirstOrDefault(x => x.ObjectID == ObjectID).ObjAdminID;
                //            if (!int.TryParse(adminID.ToString(), out _)) {
                //                return null;
                //            }
                //            else {
                //                using (ObjAdminContext objAdminContext = new ObjAdminContext()) {
                //                    ObjAdmin objAdmin = objAdminContext.ObjAdmin.SingleOrDefault(x => x.ObjAdminID == adminID);
                //                    if (objAdmin != null)
                //                        return "Адмнистратор(старый лк)";
                //                }
                //            }
                //            return !int.TryParse(adminID.ToString(), out _) ? null : "";
                //        }
                //        else
                //            return null;
                //    }
                //}
                else if (this.ObjCustPhone1 != null) {
                    if (this.ObjCustPhone1.Equals(" нет"))
                        return null;
                    else
                        return null;
                }
                else {
                    using (ObjectContext objectContext = new ObjectContext()) {

                        int? adminID = 0;
                        //if (objectContext.ObjectA28.Any(x => x.ObjectID == ObjectID)) {
                        if (ObjectID != 0) {
                            List<ObjectA28> t = objectContext.ObjectA28.Where(x => x.ObjectID == ObjectID && x.ObjAdminID != null).ToList();
                            foreach (var item in t) {
                                adminID = objectContext.ObjectA28.FirstOrDefault(x => x.ObjectID == ObjectID).ObjAdminID;

                                if (!int.TryParse(adminID.ToString(), out _)) {
                                    return null;
                                }
                                else {
                                    using (ObjAdminContext objAdminContext = new ObjAdminContext()) {
                                        ObjAdmin objAdmin = objAdminContext.ObjAdmin.SingleOrDefault(x => x.ObjAdminID == adminID);
                                        if (objAdmin != null)
                                            return "Адмнистратор(старый лк)";
                                    }
                                }
                            }
                            //}
                            //else
                            //    return null;
                        }

                        return !int.TryParse(adminID.ToString(), out _) ? null : "";
                    }
                }
            }
            set => value = string.IsNullOrEmpty(value) ? null : value;
        }

        [NotMapped]
        public string N_ObjCustPhone1 {
            get => _N_ObjCustPhone1;
            set {
                _N_ObjCustPhone1 = value;
            }
        }
        private string _N_ObjCustPhone1 {
            get => NormalizePhone(ObjCustPhone1);
            set => NormalizePhone(value);
        }
        [NotMapped]
        public string N_ObjCustPhone2 {
            get => _N_ObjCustPhone2;
            set {
                _N_ObjCustPhone2 = value;
            }
        }
        private string _N_ObjCustPhone2 {
            get => NormalizePhone(ObjCustPhone2);
            set => NormalizePhone(value);
        }
        [NotMapped]
        public string N_ObjCustPhone3 {
            get => _N_ObjCustPhone3;
            set {
                _N_ObjCustPhone3 = value;
            }
        }
        private string _N_ObjCustPhone3 {
            get => NormalizePhone(ObjCustPhone3);
            set => NormalizePhone(value);
        }
        [NotMapped]
        public string N_ObjCustPhone4 {
            get => _N_ObjCustPhone4;
            set {
                _N_ObjCustPhone4 = value;
            }
        }
        private string _N_ObjCustPhone4 {
            get => NormalizePhone(ObjCustPhone4);
            set => NormalizePhone(value);
        }
        [NotMapped]
        public string N_ObjCustPhone5 {
            get => _N_ObjCustPhone5;
            set {
                _N_ObjCustPhone5 = value;
            }
        }
        private string _N_ObjCustPhone5 {
            get => NormalizePhone(ObjCustPhone5);
            set => NormalizePhone(value);
        }
        [NotMapped]
        public string N_ObjCustTitle {
            get => _N_ObjCustTitle;
            set {
                _N_ObjCustTitle = value;
            }
        }
        private string _N_ObjCustTitle {
            get => NormalizePhone(ObjCustTitle);
            set => NormalizePhone(value);
        }

        [NotMapped]
        public int ObjectNumber {
            get => _ObjectNumber;
            set { _ObjectNumber = value; }
        }
        private int _ObjectNumber {
            get {
                using (ObjectContext objectContext = new ObjectContext()) {
                    if (ObjectID != 0)
                        if (objectContext.ObjectA28.Any(x => x.ObjectID == ObjectID && x.Disable == false))
                            return Convert.ToInt32(Convert.ToString(objectContext.ObjectA28.FirstOrDefault(x => x.ObjectID == ObjectID && x.Disable == false).ObjectNumber, 16));
                        else
                            return -1;
                    else
                        return -1;
                }
            }
            set {
                using (ObjectContext objectContext = new ObjectContext()) {
                    if (ObjectID != 0) {
                        int? p = objectContext.ObjectA28.FirstOrDefault(x => x.ObjectID == ObjectID).ObjectNumber;
                        value = int.TryParse(p.ToString(), out _) ? Convert.ToInt32(Convert.ToString(int.Parse(p.ToString()), 16)) : -1;
                    }
                }
            }
        }
        //public string NormalizePhone(string param) {
        //    if (!string.IsNullOrEmpty(param))
        //        return param.Replace("-", "").Replace(" ", "").Replace("+", "").ToLower();
        //    else
        //        return param;
        //}
        public string NormalizePhone(string param) {
            if (!string.IsNullOrEmpty(param) && !string.IsNullOrWhiteSpace(param)) {
                if (long.TryParse(param, out _)) {
                    string g = param.Replace("-", "").Replace(" ", "").Replace("+", "").ToLower();
                    if (g.Length == 11) {
                        if (g.Substring(0, 2) == "79")
                            return g.Remove(0, 1).Insert(0, "8");
                        else
                            return g;
                    }
                    return g;
                }
                else
                    return param.Replace("-", "").Replace(" ", "").Replace("+", "").ToLower();
            }
            else
                return param;
        }
    }
    public class ObjCustContext : DbContext {
        public ObjCustContext() : base("A28Entity") { }
        public DbSet<ObjCust> ObjCust { get; set; }
    }
}