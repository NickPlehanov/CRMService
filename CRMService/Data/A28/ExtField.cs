using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CRMService.Data.A28 {
    [Table("ExtField")]
    public class ExtField {
        public int ExtFieldID { get; set; }
        public int ExtFieldType { get; set; }
        public string ExtFieldName { get; set; }
        public bool RecordDeleted { get; set; }
    }
    public class ExtFieldContext : DbContext {
        public ExtFieldContext() : base("A28Entity") { }
        public DbSet<ExtField> ExtField { get; set; }
    }
}