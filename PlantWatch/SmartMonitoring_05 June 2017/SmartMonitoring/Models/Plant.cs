using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartMonitoring.Models
{
    public class Plant
    {
        public string PlantID { get; set; }

        [Required]
        [Display(Name ="Plant Name")]
        public string PlantName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [Display(Name ="Contact No")]
        public string ContactNo { get; set; }
        [Display(Name ="Fax No")]
        public string FaxNo { get; set; }
        public string Website { get; set; }
        [Required]
        [Display(Name = "No of Area")]
        public int NumberOfArea { get; set; }
        [Display(Name ="Plant Detail")]
        public string PlantDetails { get; set; }

        public string UserID { get; set; }
        public string AID { get; set; }
        public string TID { get; set; }
        public string MID { get; set; }
        public string PID { get; set; }
        public string SID { get; set; }

        public List<Plant> lstPlant { get; set; }
        public List<Area> lstArea { get; set; }
        public List<Train> lstTrain { get; set; }
        public List<Machine> lstMachine { get; set; }
        public List<Point> lstPoint { get; set; }
        public List<Sensor> lstSensor { get; set; }

    }
    public partial class TFileStructure
    {
        public List<TreeViewModel> Childs { get; set; }
    }
    public class TreeViewModel
    {
        public string id { get; set; }
        public string parent { get; set; }
        public string text { get; set; }
        public string icon { get; set; }
       
        //public string li_attr { get; set; }
        //public string a_attr { get; set; }



        //[JsonIgnore]
        //public int NodeId { get; set; }
        //public string icon { get { return "glyphicon glyphicon-user"; } }
        //[JsonIgnore]
        //public int ParentId { get; set; }
        //public string nodeText { get; set; }
        //[JsonProperty("nodes")]
        //public List<TreeViewModel> ChildRoles { get; set; }
    }

    public class state
    {
        public bool opened { get; set; }
        public bool disabled { get; set; }
        public bool selected { get; set; }
    }

    public class Area
    {
        public string AreaID { get; set; }
       
        [Display(Name = "Plant ID")]
        public string ParentID { get; set; }
        [Required]
        [Display(Name ="Area Name")]
        public string AreaName { get; set; }
        [Display(Name ="Area Detail")]
        public string AreaDetail { get; set; }
        [Required]
        [Display(Name = "No of Trains")]
        public int NumberOfTrains { get; set; }
    }

    public class Train
    {
        public string TrainID { get; set; }
        [Display(Name = "Area ID")]
        public string ParentID { get; set; }
        [Required]
        [Display(Name ="Train Name")]
        public string TrainName { get; set; }
        [Required]
        [Display(Name ="No of Machine")]
        public int NumberOfMachines { get; set; }
        [Required]
        [Display(Name ="Drive Unit")]
        public string DriveUnitName { get; set; }
       
        [Display(Name ="Intermediate Unit")]
        public string IntermediateUnitName { get; set; }
        [Required]
        [Display(Name ="Driven Unit")]
        public string DrivenUnitName { get; set; }
    }

    public class Machine
    {
        public string MachineID { get; set; }
        [Display(Name = "Train ID")]
        public string ParentID { get; set; }
        [Required]
        [Display(Name = "Machine Name")]
        public string MachineName { get; set; }
        [Required]
        [Display(Name = "Machine Details")]
        public string MachineDetails { get; set; }
        [Required]
        [Display(Name = "RPM Driven")]
        public int RPMDriven { get; set; }
        [Required]
        [Display(Name = "Pulse Revolution")]
        public int PulseRevolution { get; set; }
        [Display(Name = "Machine Image")]
        public string machineImage { get; set; }
    }

    public class Point
    {
        
        public string PointID { get; set; }
        [Display(Name = "Machine ID")]
        public string ParentID { get; set; }
        [Required]
        [Display(Name = "Point Name")]
        public string PointName { get; set; }
       
        [Display(Name = "Point Details")]
        public string PointDetails { get; set; }
        [Required]
        [Display(Name = "No Of Sensors")]
        public int NoOfSensors { get; set; }

    }

    public class Sensor
    {
        
        public string SensorID { get; set; }
        [Display(Name = "Point ID")]
        public string ParentID { get; set; }
        [Required]
        [Display(Name = "Sensor Name")]
        public string SensorName { get; set; }
        [Required]
        [Display(Name = "Sensor Direction")]
        public string SensorDirection { get; set; }
        [Required]
        [Display(Name = "Sensor Type")]
        public string SensorType { get; set; }
        [Required]
        [Display(Name = "Acquisition ID")]
        public string AcquisitionID { get; set; }
    }

    public class MeasurementSetup
    {
        [Required]
        [Display(Name = "Setup ID")]
        public int SetupID { get; set; }
        [Required]
        [Display(Name = "Setup Name")]
        public string SetupName { get; set; }
        [Required]
        [Display(Name = "Setup Type")]
        public string SetupType { get; set; }
        [Required]
        [Display(Name = "Spectrum Schedule")]
        public string SpectrumSchedule { get; set; }
        [Required]
        [Display(Name = "Band Alarm ID")]
        public int BandAlarmID { get; set; }
        [Required]
        [Display(Name = "Envelope Alarm ID")]
        public int EnvelopeAlarmID { get; set; }
        [Required]
        [Display(Name = "Trend Alarm ID")]
        public int TrendAlarmID { get; set; }
        [Required]
        [Display(Name = "Trend Schedule")]
        public string TrendSchedule { get; set; }
        [Required]
        [Display(Name = "Spectrum Unit")]
        public string SpectrumUnit { get; set; }
        [Required]
        [Display(Name = "Time Unit")]
        public string TimeUnit { get; set; }
        [Required]
        [Display(Name = "Spectrum Data Unit")]
        public string SpectrumDataUnit { get; set; }
        [Required]
        [Display(Name = "Time Data Unit")]
        public string TimeDataUnit { get; set; }
        [Required]
        [Display(Name = "Fault Frequency")]
        public string FaultFrequency { get; set; }
    }
}