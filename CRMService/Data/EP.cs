//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class EP
    {
        public int ProcID { get; set; }
        public int ProcGroupID { get; set; }
        public Nullable<int> OrderNumber { get; set; }
        public string ComponentGUID { get; set; }
        public Nullable<int> ProcType { get; set; }
        public string Title { get; set; }
        public Nullable<int> InternalNumber { get; set; }
        public Nullable<bool> Enabled { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<int> RCFilterInterval { get; set; }
        public Nullable<bool> UseAnyEvent { get; set; }
        public Nullable<int> ActivationMode { get; set; }
        public Nullable<bool> AnyEventForActivate { get; set; }
        public Nullable<int> ActivationInterval { get; set; }
        public Nullable<bool> AnyEventForDeactivate { get; set; }
        public string Params { get; set; }
    }
}
