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
    
    public partial class tblTrain
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblTrain()
        {
            this.tblPlantMasters = new HashSet<tblPlantMaster>();
        }
    
        public int ID { get; set; }
        public string TrainID { get; set; }
        public string TrainName { get; set; }
        public int NumberOfMachines { get; set; }
        public string DriveUnitName { get; set; }
        public string IntermediateUnitName { get; set; }
        public string DrivenUnitName { get; set; }
        public System.DateTime Createdate { get; set; }
        public string ParentID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblPlantMaster> tblPlantMasters { get; set; }
    }
}
