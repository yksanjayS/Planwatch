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
    
    public partial class GetPoint_Result
    {
        public int ID { get; set; }
        public string PointID { get; set; }
        public string PointName { get; set; }
        public string PointDetails { get; set; }
        public int NoOfSensors { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string ParentID { get; set; }
    }
}