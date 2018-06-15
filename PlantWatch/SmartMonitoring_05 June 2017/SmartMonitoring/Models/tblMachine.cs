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
    
    public partial class tblMachine
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblMachine()
        {
            this.tblPlantMasters = new HashSet<tblPlantMaster>();
        }
    
        public int ID { get; set; }
        public string MachineID { get; set; }
        public string MachineName { get; set; }
        public string MachineDetails { get; set; }
        public Nullable<int> RPMDriven { get; set; }
        public Nullable<int> PulseRevolution { get; set; }
        public byte[] MachineImages { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string ParentID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblPlantMaster> tblPlantMasters { get; set; }
    }
}