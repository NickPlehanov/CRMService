using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CRMService.Data.A28 {
    [Table("ObjAdmin")]
    public class ObjAdmin {
        public int ObjAdminID { get; set; }
        public string AdminName { get; set; }
        public string AdminEmail { get; set; }
        public string AdminPhone { get; set; }
        public bool RecordDeleted { get; set; }

        [NotMapped]
        public string N_AdminPhone {
            get => _N_AdminPhone;
            set {
                _N_AdminPhone = value;
            }
        }
        private string _N_AdminPhone {
            get => NormalizePhone(AdminPhone);
            set => NormalizePhone(value);
        }

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
    public class ObjAdminContext : DbContext {
        public ObjAdminContext() : base("A28Entity") { }
        public DbSet<ObjAdmin> ObjAdmin { get; set; }
    }
}