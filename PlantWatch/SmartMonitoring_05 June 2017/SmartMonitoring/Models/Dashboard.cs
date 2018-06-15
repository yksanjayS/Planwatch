using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartMonitoring.Models
{
    public class Dashboard
    {

        public static List<Dashboard> GetTrendChart()
        {
            var data = new List<Dashboard>();
            data.Add(new Dashboard("A", 56, 62));
            data.Add(new Dashboard("B", 30, 70));
            data.Add(new Dashboard("C", 58, 68));
            data.Add(new Dashboard("D", 65, 54));
            data.Add(new Dashboard("E", 40, 52));
            data.Add(new Dashboard("F", 36, 60));
            data.Add(new Dashboard("D", 70, 48));

            return data;
        }


        public static List<Dashboard> GetPieChartData(string NodeType, ArrayList alarmData)
        {
            var data = new List<Dashboard>();
            if (alarmData.Count > 0)
            {
                double[] chartdata = GetChartData(alarmData);
                data.Add(new Dashboard("High Alarm", chartdata[2]));
                data.Add(new Dashboard("Low Alarm", chartdata[1]));
                data.Add(new Dashboard("No Alarm", chartdata[0]));
            }
            else
            {
                data.Add(new Dashboard("High Alarm", 0));
                data.Add(new Dashboard("Low Alarm", 0));
                data.Add(new Dashboard("No Alarm", 1));
            }
            return data;
        }


        public static List<Dashboard> GetLogarithmicSampleDashboard()
        {
            var data = new List<Dashboard>();

            data.Add(new Dashboard("A", 5));
            data.Add(new Dashboard("B", 50));
            data.Add(new Dashboard("C", 500));
            data.Add(new Dashboard("D", 5000));
            data.Add(new Dashboard("E", 50000));

            return data;
        }

        public Dashboard(string label, double value1)
        {
            this.Label = label;
            this.Value1 = value1;
        }

        public Dashboard(string label, double value1, double value2)
        {
            this.Label = label;
            this.Value1 = value1;
            this.Value2 = value2;
        }

        public string Label { get; set; }
        public double Value1 { get; set; }
        public double Value2 { get; set; }
        public static string Title { get; set; }
        public string TitleID { get; set; }
        public static List<string> TitleList { get; set; }
        public List<string> LabelList { get; set; }
        public List<float> ValueList { get; set; }

        public List<Dashboard> GetTitleList(string[] titles)
        {
            List<Dashboard> titleList = new List<Dashboard>();

            //for (int Count = 0; Count < titles.Length; Count++)
            //{
            //    titleList.Add(new Dashboard
            //    {
            //        Title = titles[Count].ToString()
            //    });
            //}
            return titleList;
        }

        public List<Dashboard> GetLabelList(string[] labels)
        {
            List<Dashboard> labelList = new List<Dashboard>();

            //for (int Count = 0; Count < labels.Length; Count++)
            //{
            //    labelList.Add(new Dashboard
            //    {
            //        Label = labels[Count].ToString()
            //    });
            //}
            return labelList;
        }

        public List<Dashboard> GetValueList(int[] values)
        {
            List<Dashboard> valueList = new List<Dashboard>();

            //for (int Count = 0; Count < values.Length; Count++)
            //{
            //    valueList.Add(new Dashboard
            //    {
            //        Value1 = values[Count]
            //    });
            //}
            return valueList;
        }

        public static double[] GetChartData(ArrayList alarmList)
        {
            double[] chartdata = new double[3];
            int[] alarmflag = (int[])(alarmList[2]);
            int red = 0, yellow = 0, green = 0;

            for (int Counter = 0; Counter < alarmflag.Length; Counter++)
            {
                if (alarmflag[Counter] == 0)
                {
                    green = green + 1;
                }
                else if (alarmflag[Counter] == 1)
                {
                    yellow = yellow + 1;
                }
                else if (alarmflag[Counter] == 2)
                {
                    red = red + 1;
                }
            }
            chartdata[0] = green;
            chartdata[1] = yellow;
            chartdata[2] = red;
            return chartdata;
        }

        public string SensorID { get; set; }
        public string SensorName { get; set; }
        public string ParentID { get; set; }
        //public static List<RecordedData> SensorList { get; set; }

    }

    public class RecordedData
    {
        public string SensorID { get; set; }
        public string SensorName { get; set; }


        public string SensorName1 { get; set; }

        public string SensorName2 { get; set; }

        public string SensorName3 { get; set; }

        public string SensorName4 { get; set; }

        public string SensorName5 { get; set; }

        public string SensorName6 { get; set; }

        public string SensorName7 { get; set; }

        public string SensorName8 { get; set; }


        public float OA1 { get; set; }

        public float OA2 { get; set; }

        public float OA3 { get; set; }

        public float OA4 { get; set; }

        public float OA5 { get; set; }

        public float OA6 { get; set; }

        public float OA7 { get; set; }

        public float OA8 { get; set; }


        public string ParentID { get; set; }
        public DateTime TimeStamp { get; set; }
        public string OverAllUnit { get; set; }
        public double OverAllValue { get; set; }
        public List<RecordedData> OverallList { get; set; }
        public List<RecordedData> SensorList { get; set; }

    }

    public class Alarmdata
    {
        public Alarmdata(string label, int value1)
        {
            this.AlarmName = label;
            this.DataValue = value1;
        }
        public string AlarmName { get; set; }

        public int DataValue { get; set; }
    }

    public class TrendDashboard
    {
        public static List<TrendDashboard> GetLineChartDataWithNullValues(List<RecordedData> lstRecordData)
        {
            var data = new List<TrendDashboard>();
            foreach (var item in lstRecordData)
            {
              data.Add(new TrendDashboard(item.TimeStamp, item.SensorID, item.OverAllValue));
            }
            return data;
        }
        public TrendDashboard(List<RecordedData> lstSensors, DateTime valueX, double? valueY1, double? valueY2, double? valueY3, double? valueY4, double? valueY5, double? valueY6, double? valueY7, double? valueY8)
        {
            this.SensorList = SensorList;
            ValueX = valueX;
            ValueY1 = valueY1;
            ValueY2 = valueY2;
            ValueY3 = valueY3;
            ValueY4 = valueY4;
            ValueY5 = valueY5;
            ValueY6 = valueY6;
            ValueY7 = valueY7;
            ValueY8 = valueY8;
        }

        public TrendDashboard(DateTime valueX, string sensorName, double? ValueY1)
        {
            ValueX = valueX;
            this.Sesnorname = sensorName;
            ValueY1 = ValueY1;
        }

        public List<RecordedData> SensorList { get; set; }
        public string Sesnorname { get; set; }
        public static DateTime ValueX { get; set; }
        public static double? ValueY1 { get; set; }
        public static double? ValueY2 { get; set; }
        public static double? ValueY3 { get; set; }
        public static double? ValueY4 { get; set; }
        public static double? ValueY5 { get; set; }
        public static double? ValueY6 { get; set; }
        public static double? ValueY7 { get; set; }
        public static double? ValueY8 { get; set; }

        public List<RecordedData> OverallList { get; set; }

        public static List<RecordedData> RecordedDataList { get; set; }
        
    }
}
