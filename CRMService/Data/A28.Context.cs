﻿//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CRMService.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class A28Entities : DbContext
    {
        public A28Entities()
            : base("name=A28Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Object> Object { get; set; }
        public DbSet<ExtField> ExtField { get; set; }
        public DbSet<ObjExtField> ObjExtField { get; set; }
        public DbSet<ObjType> ObjType { get; set; }
        public DbSet<EventTemp> EventTemp { get; set; }
        public DbSet<ObjCust> ObjCust { get; set; }
        public DbSet<ObjAdmin> ObjAdmin { get; set; }
        public DbSet<EP> EP { get; set; }
        public DbSet<EPCustomer> EPCustomer { get; set; }
    }
}
