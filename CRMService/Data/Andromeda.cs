using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CRMService.Data {
    [Table("New_andromedaExtensionBase")]
    public class Andromeda {
        [Key]
        public Guid New_andromedaId { get; set; }
        public string New_name { get; set; }
        public int? New_number { get; set; }
        public string New_objtype { get; set; }
        public string New_address { get; set; }
    }
    public class AndromedaContext : DbContext {
        public AndromedaContext() : base("VityazMSCRMEntity") { }
        public DbSet<Andromeda> Andromeda { get; set; }
    }
}