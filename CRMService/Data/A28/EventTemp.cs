using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CRMService.Data.A28 {
    [Table("EventTemp")]
    public class EventTemp {
        [Key]
        public int EventTemplateID { get; set; }
        public string EventTemplateName { get; set; }
        public string Description { get; set; }
        public bool RecordDeleted { get; set; }
    }
    public class EventTempContext : DbContext {
        public EventTempContext() : base("A28Entity") { }
        public DbSet<EventTemp> EventTemp { get; set; }
    }
}