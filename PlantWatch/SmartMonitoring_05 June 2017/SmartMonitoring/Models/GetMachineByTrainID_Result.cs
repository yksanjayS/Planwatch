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
    
    public partial class GetMachineByTrainID_Result
    {
        public int ID { get; set; }
        public string MachineID { get; set; }
        public string MachineName { get; set; }
        public string MachineDetails { get; set; }
        public Nullable<int> RPMDriven { get; set; }
        public Nullable<int> PulseRevolution { get; set; }
        public byte[] MachineImages { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string ParentID { get; set; }
    }
}
