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
    
    public partial class GetRecordedDataBuSensorID_Result
    {
        public System.DateTime RecordTime { get; set; }
        public string SetupID { get; set; }
        public string ParentID { get; set; }
        public double OverallValue { get; set; }
        public string OverallDataUnit { get; set; }
        public string Time_X { get; set; }
        public string Time_Y { get; set; }
        public string FFT_X { get; set; }
        public string FFT_Y { get; set; }
        public string SensorID { get; set; }
        public Nullable<int> AlarmID { get; set; }
    }
}
