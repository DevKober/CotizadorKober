//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CotizadorKober.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Permission
    {
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public string PermissionKey { get; set; }
        public bool CreateOpt { get; set; }
        public bool ReadOpt { get; set; }
        public bool UpdateOpt { get; set; }
        public bool DeleteOpt { get; set; }
        public bool ExecuteOpt { get; set; }
        public string PermissionName { get; set; }
    }
}
