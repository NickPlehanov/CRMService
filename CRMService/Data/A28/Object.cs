using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Dynamic;

namespace CRMService.Data.A28 {
    [Table("Object")]
    public class ObjectA28 {
        [Key]
        public int ObjectID { get; set; }
        public int ObjectNumber { get; set; }
        public int EventTemplateID { get; set; }
        public string Name { get; set; }
        public string ObjectPassword { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Phone3 { get; set; }
        public string Address { get; set; }
        public bool IsArm { get; set; }
        public bool IsFire { get; set; }
        public bool IsPanic { get; set; }
        public string TransmitterID { get; set; }
        public bool RecordDeleted { get; set; }
        public int ObjTypeID { get; set; }
        public int? ObjAdminID { get; set; }
        public int? ControlTime { get; set; }
        public bool ArmSchedule_EarlyArm { get; set; }
        public bool ArmSchedule_ControlArm { get; set; }
        public bool ArmSchedule_LaterArm { get; set; }
        public bool ArmSchedule_EarlyDisarm { get; set; }
        public bool ArmSchedule_ControlDisarm { get; set; }
        public bool ArmSchedule_LaterDisarm { get; set; }
    }
    public class ObjectContext : DbContext {
        public ObjectContext() : base("A28Entity") { }
        public DbSet<ObjectA28> ObjectA28 { get; set; }
    }
}