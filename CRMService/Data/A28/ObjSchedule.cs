using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CRMService.Data.A28 {
    [Table("ObjSchedule")]
    public class ObjSchedule {
        [Key]
        public int RecordID { get; set; }
        public int ObjectID { get; set; }
        public int DayNumber { get; set; }
        public int IntervalNumber { get; set; }
        public DateTime StartDT { get; set; }
        public DateTime StopDT { get; set; }
    }
    public class ObjScheduleContext : DbContext {
        public ObjScheduleContext() : base("A28Entity") { }
        public DbSet<ObjSchedule> ObjSchedule { get; set; }
    }
}