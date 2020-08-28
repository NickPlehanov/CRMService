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
    }
    public class ObjAdminContext : DbContext {
        public ObjAdminContext() : base("A28Entity") { }
        public DbSet<ObjAdmin> ObjAdmin { get; set; }
    }
}