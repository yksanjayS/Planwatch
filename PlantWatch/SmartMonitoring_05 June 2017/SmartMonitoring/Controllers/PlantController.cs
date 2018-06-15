using SmartMonitoring.Filter;
using SmartMonitoring.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;


namespace SmartMonitoring.Controllers
{
    [InitializeSimpleMembershipAttribute]
    public class PlantController : Controller
    {
        private SmartMonitoringEntities context = new SmartMonitoringEntities();
        // GET: Plant Information

        public ActionResult PlantIndex()
        {
            Plant plant = new Plant();
            plant.lstPlant = PlantList(Convert.ToString(Session["UserID"]));
            plant.lstArea = AreaList(plant.PlantID);
            plant.lstTrain = TrainList(plant.AID);
            plant.lstMachine = MachineList(plant.TID);
            plant.lstPoint = PointList(plant.TID);
            plant.lstSensor = SensorList(plant.TID);
            return View();
        }

        public ActionResult GetPlantHierarchy()
        {
            return PartialView("_plantHierarchy");
        }

        public JsonResult CreateTreeView()
        {
            List<TreeViewModel> TreeList = new List<TreeViewModel>();
            try
            {
                using (SmartMonitoringEntities context = new SmartMonitoringEntities())
                {
                    var plantData = context.GetAllPlantDetail(Session["UserID"].ToString()).ToList();
                    ViewBag.NodeCount = plantData.Count;
                    foreach (var node in plantData)
                    {
                        TreeList.Add(new TreeViewModel
                        {
                            id = node.NodeID,
                            text = node.NodeText,
                            parent = node.ParentID
                        });
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return Json(TreeList, "True", JsonRequestBehavior.AllowGet);
            //return new JsonResult { Data = new { TreeList }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult DeleteSelectedNode()
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("PlantIndex");
        }

        #region Plant Section.    

        /// <summary>  
        /// GET: /Plant/PlantList    
        /// </summary>  
        /// <returns>Return all plants associtaed with current user</returns>  

        private List<Plant> PlantList(string UserID)
        {
            List<Plant> plant = new List<Plant>();
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return plant;
        }

        /// <summary>  
        /// GET: /Plant/AddPlant  
        /// </summary>  
        /// <returns>Add new plant associtaed with current user</returns>  
        public ActionResult AddPlant()
        {
            try
            {
                return PartialView("_NewPlant");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>  
        /// POST: /Plant/AddPlant  
        /// </summary>  
        /// <returns>Add new plant associtaed with current user</returns>  
        [HttpPost]
        public ActionResult AddPlant(Plant plant)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (SmartMonitoringEntities context = new SmartMonitoringEntities())
                    {
                        int i = context.AddPlant("INSERT", Session["UserID"].ToString(), plant.PlantID, plant.PlantName, plant.Address, plant.ContactNo, plant.FaxNo, plant.Website, plant.PlantDetails, Convert.ToInt32(plant.NumberOfArea), Session["UserRole"].ToString());
                        if (i > 0)
                        {
                            return RedirectToAction("PlantIndex");
                        }
                        var list = new System.Collections.Generic.List<Object> { "1',2" };
                    }
                    return RedirectToAction("PlantIndex");
                }
                else
                {
                    ModelState.AddModelError("", "Fill all fields correctly !");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("PlantIndex");
        }

        /// <summary>  
        /// GET: /Plant/PlantDetail  
        /// </summary>  
        /// <returns>Show detail of the selected plant by current user</returns>  
        /// 
        [HttpPost]
        public ActionResult PlantDetail(string nodeid)
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
                            using (SmartMonitoringEntities context = new SmartMonitoringEntities())
                            {
                                var plantRecord = context.GetPlant(nodeid).ToList();
                                if (plantRecord.Count() > 0)
                                {
                                    foreach (var plantitem in plantRecord)
                                    {
                                        plant.PlantID = plantitem.PlantID;
                                        plant.PlantName = plantitem.PlantName;
                                        plant.Address = plantitem.Address;
                                        plant.ContactNo = plantitem.ContactNo;
                                        plant.FaxNo = plantitem.FaxNo;
                                        plant.Website = plantitem.Website;
                                        plant.PlantDetails = plantitem.PlantDetails;
                                        plant.NumberOfArea = plantitem.NoOfArea;
                                        Session["SelectedNodeID"] = plantitem.PlantID;
                                    }
                                    return PartialView("_PlantDetail", plant);
                                }
                                else
                                {
                                    TempData["NodeAvailability"] = "There is no any 'Plant' for display ! Please click on 'Add New Plant' button for create new 'Plant'.";
                                    return PartialView("_PlantDetail", plant);
                                }
                            }
                        }
                    case "Area":
                        {
                            using (SmartMonitoringEntities context = new SmartMonitoringEntities())
                            {
                                Area area = new Area();
                                var areaRecord = context.GetArea(nodeid).ToList();
                                if (areaRecord.Count() > 0)
                                {
                                    foreach (var areaitem in areaRecord)
                                    {
                                        area.AreaID = areaitem.AreaID;
                                        area.AreaName = areaitem.AreaName;
                                        area.AreaDetail = areaitem.AreaDetail;
                                        area.NumberOfTrains = areaitem.NumberOfTrains;
                                        Session["SelectedNodeID"] = areaitem.AreaID;
                                    }
                                    return PartialView("_AreaDetail", area);
                                }
                                else
                                {
                                    TempData["NodeAvailability"] = "There is no any 'Plant' for display ! Please click on 'Add New Plant' button for create new 'Plant'.";
                                    return PartialView("_AreaDetail", area);
                                }
                            }
                        }
                    case "Train":
                        {
                            using (SmartMonitoringEntities context = new SmartMonitoringEntities())
                            {
                                Train train = new Train();
                                var trainRecord = context.GetTrain(nodeid).ToList();
                                if (trainRecord.Count() > 0)
                                {
                                    foreach (var trainitem in trainRecord)
                                    {
                                        train.TrainID = trainitem.TrainID;
                                        train.TrainName = trainitem.TrainName;
                                        train.NumberOfMachines = trainitem.NumberOfMachines;
                                        train.DriveUnitName = trainitem.DriveUnitName;
                                        train.DrivenUnitName = trainitem.DrivenUnitName;
                                        train.IntermediateUnitName = trainitem.IntermediateUnitName;
                                        Session["SelectedNodeID"] = trainitem.TrainID;
                                    }
                                    return PartialView("_TrainDetail", train);
                                }
                                else
                                {
                                    TempData["NodeAvailability"] = "There is no any 'Train' for display ! Please click on 'Add New Plant' button for create new 'Plant'.";
                                    return PartialView("_TrainDetail", train);
                                }
                            }
                        }
                    case "Machine":
                        {
                            using (SmartMonitoringEntities context = new SmartMonitoringEntities())
                            {
                                Machine machine = new Machine();
                                var machineRecord = context.GetMachine(nodeid).ToList();
                                if (machineRecord.Count() > 0)
                                {
                                    foreach (var machineitem in machineRecord)
                                    {
                                        machine.MachineID = machineitem.MachineID;
                                        machine.MachineName = machineitem.MachineName;
                                        machine.MachineDetails = machineitem.MachineDetails;
                                        machine.RPMDriven = Convert.ToInt32(machineitem.RPMDriven);
                                        machine.PulseRevolution = Convert.ToInt32(machineitem.PulseRevolution);
                                        Session["SelectedNodeID"] = machineitem.MachineID;
                                    }
                                    return PartialView("_MachineDetail", machine);
                                }
                                else
                                {
                                    TempData["NodeAvailability"] = "There is no any 'Plant' for display ! Please click on 'Add New Plant' button for create new 'Plant'.";
                                    return PartialView("_MachineDetail", machine);
                                }
                            }
                        }
                    case "Point":
                        {
                            using (SmartMonitoringEntities context = new SmartMonitoringEntities())
                            {
                                Point point = new Point();
                                var pointRecord = context.GetPoint(nodeid).ToList();
                                if (pointRecord.Count() > 0)
                                {
                                    foreach (var pointitem in pointRecord)
                                    {
                                        point.PointID = pointitem.PointID;
                                        point.PointName = pointitem.PointName;
                                        point.PointDetails = pointitem.PointDetails;
                                        point.NoOfSensors = pointitem.NoOfSensors;
                                        Session["SelectedNodeID"] = pointitem.PointID;
                                    }
                                    return PartialView("_PointDetails", point);
                                }
                                else
                                {
                                    TempData["NodeAvailability"] = "There is no any 'Plant' for display ! Please click on 'Add New Plant' button for create new 'Plant'.";
                                    return PartialView("_PointDetails", point);
                                }
                            }
                        }
                    default:
                        {
                            return PartialView("_PlantDetail", plant);
                        }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
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

        /// <summary>  
        /// POST: /Plant/PlantDetail  
        /// </summary>  
        /// <returns>Save all the changes in selected plant by current user and return to main page.</returns>  
        [HttpPost]
        public ActionResult PlantDetail1(Plant plant, FormCollection form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (SmartMonitoringEntities context = new SmartMonitoringEntities())
                    {


                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Fill all fields correctly !");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EditPlant(Plant plant)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (SmartMonitoringEntities context = new SmartMonitoringEntities())
                    {
                        int i = context.UpdatePlant(Session["UserID"].ToString(), Session["SelectedNodeID"].ToString(), plant.PlantName, plant.Address, plant.ContactNo, plant.FaxNo, plant.Website, plant.PlantDetails, Convert.ToInt32(plant.NumberOfArea), Session["UserRole"].ToString());
                        if (i > 0)
                        {
                            return RedirectToAction("PlantIndex");
                        }

                    }
                    return RedirectToAction("PlantIndex");
                }
                else
                {
                    ModelState.AddModelError("", "Your data is not changed successfully! Please try again.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("PlantIndex");
        }


        #endregion

        #region Area List method.    
        /// <summary>  
        /// GET: /Plant/AreaList    
        /// </summary>  
        /// <returns>Return all 'Area' associtaed with this 'Plant'</returns>  

        private List<Area> AreaList(string plantID)
        {
            List<Area> area = new List<Area>();
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return area;
        }

        [HttpGet]
        public ActionResult AddArea(string PlantID)
        {
            try
            {
                Area area = new Area();
                area.ParentID = Session["SelectedNodeID"].ToString();
                return PartialView("_NewArea", area);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult AddArea(Area area, FormCollection form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string paranetid = form["parentid"];
                    using (SmartMonitoringEntities context = new SmartMonitoringEntities())
                    {
                        int i = context.AddArea("INSERT", Session["UserID"].ToString(), area.AreaID, area.AreaName, area.AreaDetail, area.NumberOfTrains, Session["UserRole"].ToString(), paranetid);
                        if (i > 0)
                        {
                            return RedirectToAction("PlantIndex");
                        }
                        var list = new System.Collections.Generic.List<Object> { "1',2" };
                    }
                    return RedirectToAction("PlantIndex");
                }
                else
                {
                    ModelState.AddModelError("", "Fill all fields correctly !");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("PlantIndex");
        }

        [HttpPost]
        public ActionResult EditArea(Area area)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (SmartMonitoringEntities context = new SmartMonitoringEntities())
                    {
                        int i = context.AddArea("UPDATE", Session["UserID"].ToString(), Session["SelectedNodeID"].ToString(), area.AreaName, area.AreaDetail, area.NumberOfTrains, Session["UserRole"].ToString(), area.ParentID);
                        if (i > 0)
                        {
                            return RedirectToAction("PlantIndex");
                        }
                    }
                    return RedirectToAction("PlantIndex");
                }
                else
                {
                    ModelState.AddModelError("", "Your data is not changed successfully! Please try again.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("PlantIndex");
        }

        #endregion

        #region Train List method.    
        /// <summary>  
        /// GET: /Plant/TrainList 
        /// </summary>  
        /// <returns>Return all 'Train' associtaed with this 'Area'</returns>  

        private List<Train> TrainList(string areaID)
        {
            List<Train> train = new List<Train>();
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return train;
        }

        [HttpGet]
        public ActionResult AddTrain(string AreaID)
        {
            try
            {
                Train train = new Train();
                train.ParentID = Session["SelectedNodeID"].ToString();
                return PartialView("_NewTrain", train);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult AddTrain(Train train, FormCollection form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string paranetid = form["parentid"];
                    using (SmartMonitoringEntities context = new SmartMonitoringEntities())
                    {
                        int i = context.AddTrain("INSERT", Session["UserID"].ToString(), train.TrainID, train.TrainName, train.NumberOfMachines, train.DriveUnitName, train.IntermediateUnitName, train.DrivenUnitName, Session["UserRole"].ToString(), paranetid);
                        if (i > 0)
                        {
                            return RedirectToAction("PlantIndex");
                        }
                        var list = new System.Collections.Generic.List<Object> { "1',2" };
                    }
                    return RedirectToAction("PlantIndex");
                }
                else
                {
                    ModelState.AddModelError("", "Fill all fields correctly !");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("PlantIndex");
        }

        [HttpPost]
        public ActionResult EditTrain(Train train)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (SmartMonitoringEntities context = new SmartMonitoringEntities())
                    {
                        int i = context.AddTrain("UPDATE", Session["UserID"].ToString(), Session["SelectedNodeID"].ToString(), train.TrainName, train.NumberOfMachines, train.DriveUnitName, train.IntermediateUnitName, train.DrivenUnitName, Session["UserRole"].ToString(), train.ParentID);
                        if (i > 0)
                        {
                            return RedirectToAction("PlantIndex");
                        }
                    }
                    return RedirectToAction("PlantIndex");
                }
                else
                {
                    ModelState.AddModelError("", "Your data is not changed successfully! Please try again.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("PlantIndex");
        }


        #endregion

        #region Machine List method.    
        /// <summary>  
        /// GET: /Plant/MachineList    
        /// </summary>  
        /// <returns>Return all 'Machine' associtaed with this 'Train'</returns>  

        private List<Machine> MachineList(string trainID)
        {
            List<Machine> machine = new List<Machine>();
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return machine;
        }

        [HttpGet]
        public ActionResult AddMachine(string TrainID)
        {
            try
            {
                Machine machine = new Machine();
                machine.ParentID = Session["SelectedNodeID"].ToString();
                return PartialView("_NewMachine", machine);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult AddMachine(Machine machine, FormCollection form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string parentid = form["parentid"];
                    using (SmartMonitoringEntities context = new SmartMonitoringEntities())
                    {
                        int i = context.AddMachine("INSERT", Session["UserID"].ToString(), machine.MachineID, machine.MachineName, machine.MachineDetails, machine.RPMDriven, machine.PulseRevolution, machine.machineImage, Session["UserRole"].ToString(), parentid);
                        if (i > 0)
                        {
                            return RedirectToAction("PlantIndex");
                        }
                        var list = new System.Collections.Generic.List<Object> { "1',2" };
                    }
                    return RedirectToAction("PlantIndex");
                }
                else
                {
                    ModelState.AddModelError("", "Fill all fields correctly !");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("PlantIndex");
        }

        [HttpPost]
        public ActionResult EditMachine(Machine machine)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (SmartMonitoringEntities context = new SmartMonitoringEntities())
                    {
                        int i = context.AddMachine("UPDATE", Session["UserID"].ToString(), Session["SelectedNodeID"].ToString(), machine.MachineName, machine.MachineDetails, machine.RPMDriven, machine.PulseRevolution, machine.machineImage, Session["UserRole"].ToString(), machine.ParentID);
                        if (i > 0)
                        {
                            return RedirectToAction("PlantIndex");
                        }
                    }
                    return RedirectToAction("PlantIndex");
                }
                else
                {
                    ModelState.AddModelError("", "Your data is not changed successfully! Please try again.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("PlantIndex");
        }


        #endregion

        #region Point List method.    
        /// <summary>  
        /// GET: /Plant/PointList    
        /// </summary>  
        /// <returns>Return all 'Points' associtaed with this 'Machine'</returns>  

        private List<Point> PointList(string machineID)
        {
            List<Point> point = new List<Point>();
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return point;
        }

        [HttpGet]
        public ActionResult AddPoint(string MachineID)
        {
            try
            {
                Point point = new Point();
                point.ParentID = Session["SelectedNodeID"].ToString();
                return PartialView("_Point", point);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public ActionResult AddPoint(Point point, FormCollection form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string parentid = form["parentid"];
                    using (SmartMonitoringEntities context = new SmartMonitoringEntities())
                    {
                        int i = context.AddPoint("INSERT", Session["UserID"].ToString(), point.PointID, point.PointName, point.PointDetails, point.NoOfSensors, Session["UserRole"].ToString(), parentid);
                        if (i > 0)
                        {
                            return RedirectToAction("PlantIndex");
                        }
                        var list = new System.Collections.Generic.List<Object> { "1',2" };
                    }
                    return RedirectToAction("PlantIndex");
                }
                else
                {
                    ModelState.AddModelError("", "Fill all fields correctly !");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("PlantIndex");
        }

        [HttpPost]
        public ActionResult EditPoint(Point point)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (SmartMonitoringEntities context = new SmartMonitoringEntities())
                    {
                        int i = context.AddPoint("UPDATE", Session["UserID"].ToString(), Session["SelectedNodeID"].ToString(), point.PointName, point.PointDetails, point.NoOfSensors, Session["UserRole"].ToString(), point.ParentID);
                        if (i > 0)
                        {
                            return RedirectToAction("PlantIndex");
                        }
                    }
                    return RedirectToAction("PlantIndex");
                }
                else
                {
                    ModelState.AddModelError("", "Your data is not changed successfully! Please try again.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("PlantIndex");
        }
        #endregion


        #region Sensor List method.    
        /// <summary>  
        /// GET: /Plant/SensorList    
        /// </summary>  
        /// <returns>Return all 'Sensors' associtaed with this 'Point'</returns>  

        private List<Sensor> SensorList(string pointID)
        {
            List<Sensor> sensor = new List<Sensor>();
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return sensor;
        }
        #endregion
    }
}
