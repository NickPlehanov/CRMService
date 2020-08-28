using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CRMService.Data.A28 {
    [Table("ObjType")]
    public class ObjType {
        [Key]
        public int ObjTypeID { get; set; }
        public string ObjTypeName { get; set; }
        public string Description { get; set; }
        public bool RecordDeleted { get; set; }
    }
    public class ObjTypeContext : DbContext {
        public ObjTypeContext() : base("A28Entity") { }
        public DbSet<ObjType> ObjType { get; set; }
    }
}