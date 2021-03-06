﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CRMService.Data.A28 {
    [Table("DeviceType")]
    public class DeviceTypes {
        [Key]
        public int? DeviceTypeID { get; set; }
        public string Name { get; set; }
    }

    public class DeviceTypeContext : DbContext {
        public DeviceTypeContext() : base("A28Entity") { }
        public DbSet<DeviceTypes> DeviceTypes { get; set; }
    }
}