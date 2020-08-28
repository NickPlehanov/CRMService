using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CRMService.Data.A28 {
    [Table("ObjExtField")]
    public class ObjExtField {
        [Key]
        public int RecordID { get; set; }
        public int ObjectID { get; set; }
        public int ExtFieldID { get; set; }
        public string ExtFieldValue { get; set; }
    }
    public class ObjExtFieldContext : DbContext {
        public ObjExtFieldContext() : base("A28Entity") { }
        public DbSet<ObjExtField> ObjExtField { get; set; }
    }
}