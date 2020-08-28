using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CRMService.Data.A28 {
    [Table("EPCustomer")]
    public class EPCustomer {
        [Key]
        public int RecordID { get; set; }
        public int RecordType { get; set; }
        public int OwnerRecordID { get; set; }
        public int CustomerGroupID { get; set; }
        public int BeginningNumber { get; set; }
        public int EndNumber { get; set; }
    }
    public class EPCustomerContext : DbContext {
        public EPCustomerContext() : base("A28Entity") { }
        public DbSet<EPCustomer> EPCustomer { get; set; }
    }
}