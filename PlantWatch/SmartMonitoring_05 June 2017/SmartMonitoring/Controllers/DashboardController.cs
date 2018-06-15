using SmartMonitoring.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartMonitoring.Filter;
using System.Collections;
using System.Data.Entity.Core.Objects;
using System.Data;

namespace SmartMonitoring.Controllers
{
    [InitializeSimpleMembershipAttribute]
    public class DashboardController : Controller
    {
        SmartMonitoringEntities db = new SmartMonitoringEntities();
        public ActionResult DashboardIndex()
        {
            return View();
        }

        public ActionResult GetPlantHierarchy()
        {

            return PartialView("_plantHierarchy");
        }

        public ActionResult GetChartForSelectedNode(string nodeid, string nodetext)
        {
            Plant plant = new Plant();
            try
            {
                int row;
                int a = getIndexofNumber(nodeid);
                string NodeNumber = nodeid.Substring(a, nodeid.Length - a);
                row = Convert.ToInt32(NodeNumber);
                string NodeType = nodeid.Substring(0, a);

                switch (NodeType)
                {
                    case "Plant":
                        {
                            ViewBag.Title = nodetext.ToString();
                            TempData["SelectedNodeID"] = nodeid;
                            ArrayList AreaList = GetAreasForPlant("Plant", nodeid);
                            return PartialView("_pieChart", Dashboard.GetPieChartData(NodeType, AreaList));

                        }
                    case "Area":
                        {
                            ViewBag.Title = nodetext.ToString();
                            TempData["SelectedNodeID"] = nodeid;
                            ArrayList TrainList = GetTrainForArea("Area", nodeid);
                            return PartialView("_pieChart", Dashboard.GetPieChartData(NodeType, TrainList));
                        }
                    case "Train":
                        {
                            ViewBag.Title = nodetext.ToString();
                            TempData["SelectedNodeID"] = nodeid;
                            ArrayList MachineList = GetMachinesForTrain("Train", nodeid);
                            return PartialView("_pieChart", Dashboard.GetPieChartData(NodeType, MachineList));
                        }
                    case "Machine":
                        {
                            ViewBag.Title = nodetext.ToString();
                            TempData["SelectedNodeID"] = nodeid;
                            ArrayList PointList = GetPointsForMachine("Machine", nodeid);
                            return PartialView("_pieChart", Dashboard.GetPieChartData(NodeType, PointList));
                        }
                    case "Point":
                        {
                            ViewBag.Title = nodetext.ToString();
                            TempData["SelectedNodeID"] = nodeid;
                            List<RecordedData> lstRecordData = GetRecordDataForPoint(nodeid);
                            return PartialView("_pointView", TrendDashboard.GetLineChartDataWithNullValues(lstRecordData));
                        }
                    default:
                        {
                            ArrayList AreaList = GetAreasForPlant("Plant", nodeid);
                            return PartialView("_pieChart", Dashboard.GetPieChartData(NodeType, AreaList));
                        }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //return PartialView("_pieChart", Dashboard.GetPieChartData());
        }

        public ArrayList GetAreasForPlant(string NodeType, string NodeID)
        {
            ArrayList areas = new ArrayList();
            ArrayList TrainList = new ArrayList();
            int i = 0;
            int Counter = 0;
            using (SmartMonitoringEntities context = new SmartMonitoringEntities())
            {
                var AllArea = context.GetAreaByPlantID(NodeID).ToList();
                string[] areaID = new string[AllArea.Count];
                string[] areaName = new string[AllArea.Count];
                int[] AlarmFlag = new int[AllArea.Count()];
                foreach (var area in AllArea)
                {
                    string Areaid = area.AreaID;
                    var AllSensors = context.GetAllSensorsByAreaID(Areaid).ToList();
                    int token = 0;
                    foreach (var sensors in AllSensors)
                    {
                        var RecordedDataforAreas = context.GetSensorAvgOverallValueForArea(sensors.SensorID).ToList();
                        float avgValue = 0;
                        int alarmID = 0, ctr = 0;
                        foreach (var value1 in RecordedDataforAreas)
                        {
                            avgValue += (float)(value1.OverallValue);
                            alarmID = (int)(value1.AlarmID);
                            ctr++;
                        }
                        avgValue = avgValue / RecordedDataforAreas.Count;
                        i = GetAlarmForNode(Convert.ToDouble(avgValue), sensors.SensorID, alarmID);
                        if (i == 2)
                        {
                            token = 2;
                            break;
                        }
                        else if (i == 1)
                        {
                            token = 1;
                        }
                    }
                    if (token == 2)
                    {
                        areaID[Counter] = area.AreaID;
                        areaName[Counter] = area.AreaName;
                        AlarmFlag[Counter] = token;
                        Counter++;
                    }
                    else if (token == 1)
                    {
                        areaID[Counter] = area.AreaID;
                        areaName[Counter] = area.AreaName;
                        AlarmFlag[Counter] = token;
                        Counter++;
                    }
                    else if (token == 0)
                    {
                        areaID[Counter] = area.AreaID;
                        areaName[Counter] = area.AreaName;
                        AlarmFlag[Counter] = token;
                        Counter++;
                    }
                }
                areas.Add(areaID);
                areas.Add(areaName);
                areas.Add(AlarmFlag);
            }

            return areas;
        }

        public ArrayList GetTrainForArea(string NodeType, string NodeID)
        {
            ArrayList trains = new ArrayList();
            int i = 0;
            int Counter = 0;
            using (SmartMonitoringEntities context = new SmartMonitoringEntities())
            {
                var AllTrains = context.GetTrainByAreaID(NodeID).ToList();
                if (AllTrains.Count > 0)
                {
                    string[] trainID = new string[AllTrains.Count];
                    string[] trainName = new string[AllTrains.Count];
                    int[] AlarmFlag = new int[AllTrains.Count()];
                    foreach (var train in AllTrains)
                    {
                        string Trainid = train.TrainID;
                        var AllSensors = context.GetAllSensorsByTrainID(Trainid).ToList();
                        int token = 0;
                        foreach (var sensors in AllSensors)
                        {
                            var RecordedDataforTrains = context.GetSensorAvgOverallValueForTrain(sensors.SensorID).ToList();
                            float avgValue = 0;
                            int alarmID = 0;
                            foreach (var trainsVal in RecordedDataforTrains)
                            {
                                avgValue += (float)(trainsVal.OverallValue);
                                alarmID = (int)(trainsVal.AlarmID);
                            }
                            avgValue = avgValue / RecordedDataforTrains.Count;
                            i = GetAlarmForNode(Convert.ToDouble(avgValue), sensors.SensorID, alarmID);
                            if (i == 2)
                            {
                                token = 2;
                                break;
                            }
                            else if (i == 1)
                            {
                                token = 1;
                            }
                        }
                        if (token == 2)
                        {
                            trainID[Counter] = train.TrainID;
                            trainName[Counter] = train.TrainName;
                            AlarmFlag[Counter] = token;
                            Counter++;
                        }
                        else if (token == 1)
                        {
                            trainID[Counter] = train.TrainID;
                            trainName[Counter] = train.TrainName;
                            AlarmFlag[Counter] = token;
                            Counter++;
                        }
                        else if (token == 0)
                        {
                            trainID[Counter] = train.TrainID;
                            trainName[Counter] = train.TrainName;
                            AlarmFlag[Counter] = token;
                            Counter++;
                        }
                    }
                    trains.Add(trainID);
                    trains.Add(trainName);
                    trains.Add(AlarmFlag);
                }

            }

            return trains;
        }

        public ArrayList GetMachinesForTrain(string NodeType, string NodeID)
        {
            ArrayList machines = new ArrayList();
            int i = 0;
            int Counter = 0;
            using (SmartMonitoringEntities context = new SmartMonitoringEntities())
            {
                var AllMachines = context.GetMachineByTrainID(NodeID).ToList();
                string[] machineID = new string[AllMachines.Count];
                string[] machineName = new string[AllMachines.Count];
                int[] AlarmFlag = new int[AllMachines.Count()];
                foreach (var machine in AllMachines)
                {
                    string Machineid = machine.MachineID;
                    var AllSensors = context.GetAllSensorsByMachineID(Machineid).ToList();
                    int token = 0;
                    foreach (var sensors in AllSensors)
                    {
                        var RecordedDataforMachines = context.GetSensorAvgOverallValueForMachine(sensors.SensorID).ToList();
                        float avgValue = 0;
                        int alarmID = 0;
                        foreach (var machineVal in RecordedDataforMachines)
                        {
                            avgValue += (float)(machineVal.OverallValue);
                            alarmID = (int)(machineVal.AlarmID);
                        }
                        avgValue = avgValue / RecordedDataforMachines.Count;
                        i = GetAlarmForNode(Convert.ToDouble(avgValue), sensors.SensorID, alarmID);
                        if (i == 2)
                        {
                            token = 2;
                            break;
                        }
                        else if (i == 1)
                        {
                            token = 1;
                        }
                    }
                    if (token == 2)
                    {
                        machineID[Counter] = machine.MachineID;
                        machineName[Counter] = machine.MachineName;
                        AlarmFlag[Counter] = token;
                        Counter++;
                    }
                    else if (token == 1)
                    {
                        machineID[Counter] = machine.MachineID;
                        machineName[Counter] = machine.MachineName;
                        AlarmFlag[Counter] = token;
                        Counter++;
                    }
                    else if (token == 0)
                    {
                        machineID[Counter] = machine.MachineID;
                        machineName[Counter] = machine.MachineName;
                        AlarmFlag[Counter] = token;
                        Counter++;
                    }
                }
                machines.Add(machineID);
                machines.Add(machineName);
                machines.Add(AlarmFlag);
            }

            return machines;
        }

        public ArrayList GetPointsForMachine(string NodeType, string NodeID)
        {
            ArrayList points = new ArrayList();
            int i = 0;
            int Counter = 0;
            using (SmartMonitoringEntities context = new SmartMonitoringEntities())
            {
                var AllPoints = context.GetPointByMachineID(NodeID).ToList();
                string[] pointID = new string[AllPoints.Count];
                string[] pointName = new string[AllPoints.Count];
                int[] AlarmFlag = new int[AllPoints.Count()];
                foreach (var point in AllPoints)
                {
                    string Pointid = point.PointID;
                    var AllSensors = context.GetAllSensorsByPointID(Pointid).ToList();
                    int token = 0;
                    foreach (var sensors in AllSensors)
                    {
                        var RecordedDataforPoints = context.GetSensorAvgOverallValueForPoint(sensors.SensorID).ToList();
                        float avgValue = 0;
                        int alarmID = 0;
                        foreach (var pointVal in RecordedDataforPoints)
                        {
                            avgValue += (float)(pointVal.OverallValue);
                            alarmID = (int)(pointVal.AlarmID);
                        }
                        avgValue = avgValue / RecordedDataforPoints.Count;
                        i = GetAlarmForNode(Convert.ToDouble(avgValue), sensors.SensorID, alarmID);
                        if (i == 2)
                        {
                            token = 2;
                            break;
                        }
                        else if (i == 1)
                        {
                            token = 1;
                        }

                    }
                    if (token == 2)
                    {
                        pointID[Counter] = point.PointID;
                        pointName[Counter] = point.PointName;
                        AlarmFlag[Counter] = token;
                        Counter++;
                    }
                    else if (token == 1)
                    {
                        pointID[Counter] = point.PointID;
                        pointName[Counter] = point.PointName;
                        AlarmFlag[Counter] = token;
                        Counter++;
                    }
                    else if (token == 0)
                    {
                        pointID[Counter] = point.PointID;
                        pointName[Counter] = point.PointName;
                        AlarmFlag[Counter] = token;
                        Counter++;
                    }
                }
                points.Add(pointID);
                points.Add(pointName);
                points.Add(AlarmFlag);
            }
            return points;
        }

        public List<RecordedData> GetSensorDetailForPoint(string NodeID)
        {
            List<RecordedData> sensorData = new List<RecordedData>();
            try
            {
                using (SmartMonitoringEntities context = new SmartMonitoringEntities())
                {
                    var AllSensors = context.GetAllSensorsByPointIDNew(NodeID).ToList();
                    string[] SensorList = new string[AllSensors.Count];
                    int str = 0;
                    foreach (var sensors in AllSensors)
                    {
                        SensorList[str] = sensors.SensorName;
                        var RecordedData = context.GetRecordedDataBuSensorID(sensors.SensorID).ToList();
                        float[] OverAllList = new float[RecordedData.Count];
                        int rtr = 0;
                        foreach (var data in RecordedData)
                        {
                            //OverAllList[rtr] = (float)data.OverallValue;
                            //sensorData.Add(new RecordedData
                            //{
                            //    SensorID = data.SensorID,
                            //   // TimeStamp = Convert.ToString(data.RecordTime),
                            //    OverAllValue = (float)data.OverallValue,
                            //    OverAllUnit = data.OverallDataUnit
                            //});
                            //rtr++;
                        }
                        str++;
                    }
                }
            }
            catch { }
            return sensorData;
        }

        public List<RecordedData> GetRecordDataForPoint(string NodeID)
        {
            List<RecordedData> RecordedData = new List<RecordedData>();
            List<RecordedData> sensorList = new List<RecordedData>();
            TempData["SensorsCount"] = 0;
            try
            {
                using (SmartMonitoringEntities context = new SmartMonitoringEntities())
                {
                    var AllrecordedData = context.GetRecordedTimeByPointID(NodeID).ToList();
                    sensorList.Add(new RecordedData
                    {
                        SensorName1 = AllrecordedData[0].OverallSensor1,
                        SensorName2 = AllrecordedData[0].OverallSensor2,
                        SensorName3 = AllrecordedData[0].OverallSensor3,
                        SensorName4 = AllrecordedData[0].OverallSensor4,
                        SensorName5 = AllrecordedData[0].OverallSensor5,
                        SensorName6 = AllrecordedData[0].OverallSensor6,
                        SensorName7 = AllrecordedData[0].OverallSensor7,
                        SensorName8 = AllrecordedData[0].OverallSensor8
                    });

                    ViewBag.SensorList = sensorList;
                    foreach( var records in AllrecordedData)
                    {
                        int recordid = records.RecordID;
                        DateTime recordtime = records.RecordedTime;
                        var OverallData = context.GetOverallByRecordID(recordid, NodeID).ToList();
                        TempData["SensorsCount"] = OverallData.Count;
                        foreach (var overall in OverallData)
                        {
                            RecordedData.Add(new RecordedData
                            {
                              TimeStamp = recordtime,
                              ParentID = overall.ParentID,
                              SensorID = overall.SensorID,
                              OverAllValue = overall.Overallvalue
                        });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RecordedData;
        }

        public ActionResult GetOverallData()
        {
            string NodeID = TempData["SelectedNodeID"].ToString();
            TrendDashboard.RecordedDataList = GetRecordDataForPoint(NodeID);
            return PartialView("_gridViewSensorData", TrendDashboard.RecordedDataList);
        }

        
        private int getIndexofNumber(string cell)
        {
            int indexofNum = -1;
            foreach (char c in cell)
            {
                indexofNum++;
                if (Char.IsDigit(c))
                {
                    return indexofNum;
                }
            }
            return indexofNum;
        }

        public int GetAlarmForNode(double recordData, string SensorID, int alarmID)
        {
            int i = 0;
            double HighValue, LowValue;
            try
            {
                using (SmartMonitoringEntities context = new SmartMonitoringEntities())
                {
                    var Alarmdata = context.GetAlarmData(Convert.ToInt32(alarmID)).ToList();
                    foreach (var alarm in Alarmdata)
                    {
                        LowValue = Convert.ToDouble(alarm.AlarmValue_Low);
                        HighValue = Convert.ToDouble(alarm.AlarmValue_High);

                        if (recordData > HighValue)
                        {
                            i = 2;
                        }
                        else if (recordData > LowValue)
                        {
                            i = 1;
                        }
                        else
                        {
                            i = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return i;
        }

        //public ActionResult LineChartDateTimeAxis()
        //{
        //    return PartialView("_pointView",DateTimeXAxisChartData.GetLineChartDataWithNullValues());
        //}

        public JsonResult Piechart()
        {

            var chartsdata = new List<Alarmdata>();

            chartsdata.Add(new Alarmdata("High Alarm", 2));
            chartsdata.Add(new Alarmdata("Low Alarm", 1));
            chartsdata.Add(new Alarmdata("No Alarm", 1));

            return Json(chartsdata, JsonRequestBehavior.AllowGet); // returning list from here.
        }
    }
}