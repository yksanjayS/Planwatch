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
    
    public partial class tblPlant
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblPlant()
        {
            this.tblPlantMasters = new HashSet<tblPlantMaster>();
        }
    
        public int ID { get; set; }
        public string PlantID { get; set; }
        public string PlantName { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string FaxNo { get; set; }
        public string Website { get; set; }
        public string PlantDetails { get; set; }
        public int NoOfArea { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string ParentID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblPlantMaster> tblPlantMasters { get; set; }
    }
}
