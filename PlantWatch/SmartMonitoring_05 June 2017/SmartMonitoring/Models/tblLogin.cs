//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SmartMonitoring.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblLogin
    {
        public string UserID { get; set; }
        public bool UserStatus { get; set; }
        public string LoginID { get; set; }
        public string Password { get; set; }
        public string UserRole { get; set; }
    
        public virtual tblUserRegister tblUserRegister { get; set; }
    }
}
