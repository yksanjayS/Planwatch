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
    
    public partial class tblRecordedData
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblRecordedData()
        {
            this.tblOveralldatas = new HashSet<tblOveralldata>();
        }
    
        public int RecordID { get; set; }
        public System.DateTime RecordedTime { get; set; }
        public string SetupID { get; set; }
        public string ParentID { get; set; }
        public Nullable<int> AlarmID { get; set; }
        public string OverallSensor1 { get; set; }
        public string OverallSensor2 { get; set; }
        public string OverallSensor3 { get; set; }
        public string OverallSensor4 { get; set; }
        public string OverallSensor5 { get; set; }
        public string OverallSensor6 { get; set; }
        public string OverallSensor7 { get; set; }
        public string OverallSensor8 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblOveralldata> tblOveralldatas { get; set; }
    }
}
