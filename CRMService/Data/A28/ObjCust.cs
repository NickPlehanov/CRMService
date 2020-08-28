using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

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
        public bool MyAlarmPanicEnabled { get; set; }
        public int? MyAlarmUserRole { get; set; }
        public int? UserNumber { get; set; }
        public string ObjCustTitle { get; set; }
        [NotMapped]
        public string Role {
            get => _Role;
            set {
                _Role = value;
            }
        }
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
                else if (this.ObjCustPhone1.Equals(" нет"))
                    return null;
                else
                    return "(Администратор(старый ЛК))";
            }
            set => value = string.IsNullOrEmpty(value) ? null : value;
        }
    }
    public class ObjCustContext : DbContext {
        public ObjCustContext() : base("A28Entity") { }
        public DbSet<ObjCust> ObjCust { get; set; }
    }
}