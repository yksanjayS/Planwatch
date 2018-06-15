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
    
    public partial class tblSensor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblSensor()
        {
            this.tblPlantMasters = new HashSet<tblPlantMaster>();
        }
    
        public int ID { get; set; }
        public string SensorID { get; set; }
        public string SensorName { get; set; }
        public string SensorDirection { get; set; }
        public string SensorType { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string SetupID { get; set; }
        public string Sensitivity { get; set; }
        public string VMUName { get; set; }
        public string NCAID { get; set; }
        public string ParentID { get; set; }
        public string SensorUnit { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblPlantMaster> tblPlantMasters { get; set; }
    }
}