using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmartMonitoring.Models;

namespace SmartMonitoring.Controllers
{
    public class tblPlantMastersController : Controller
    {
        private SmartMonitoringEntities db = new SmartMonitoringEntities();

        // GET: tblPlantMasters
        public ActionResult Index()
        {
            var tblPlantMasters = db.tblPlantMasters.Include(t => t.tblArea).Include(t => t.tblPlant).Include(t => t.tblTrain).Include(t => t.tblUserRegister);
            return View(tblPlantMasters.ToList());
        }

        // GET: tblPlantMasters/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblPlantMaster tblPlantMaster = db.tblPlantMasters.Find(id);
            if (tblPlantMaster == null)
            {
                return HttpNotFound();
            }
            return View(tblPlantMaster);
        }

        // GET: tblPlantMasters/Create
        public ActionResult Create()
        {
            ViewBag.AreaID = new SelectList(db.tblAreas, "AreaID", "AreaName");
            ViewBag.PlantID = new SelectList(db.tblPlants, "PlantID", "PlantName");
            ViewBag.TrainID = new SelectList(db.tblTrains, "TrainID", "TrainName");
            ViewBag.UserID = new SelectList(db.tblUserRegisters, "UserID", "FirstName");
            return View();
        }

        // POST: tblPlantMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,SensorID,PointID,MachineID,TrainID,AreaID,PlantID,UserID")] tblPlantMaster tblPlantMaster)
        {
            if (ModelState.IsValid)
            {
                db.tblPlantMasters.Add(tblPlantMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AreaID = new SelectList(db.tblAreas, "AreaID", "AreaName", tblPlantMaster.AreaID);
            ViewBag.PlantID = new SelectList(db.tblPlants, "PlantID", "PlantName", tblPlantMaster.PlantID);
            ViewBag.TrainID = new SelectList(db.tblTrains, "TrainID", "TrainName", tblPlantMaster.TrainID);
            ViewBag.UserID = new SelectList(db.tblUserRegisters, "UserID", "FirstName", tblPlantMaster.UserID);
            return View(tblPlantMaster);
        }

        // GET: tblPlantMasters/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblPlantMaster tblPlantMaster = db.tblPlantMasters.Find(id);
            if (tblPlantMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.AreaID = new SelectList(db.tblAreas, "AreaID", "AreaName", tblPlantMaster.AreaID);
            ViewBag.PlantID = new SelectList(db.tblPlants, "PlantID", "PlantName", tblPlantMaster.PlantID);
            ViewBag.TrainID = new SelectList(db.tblTrains, "TrainID", "TrainName", tblPlantMaster.TrainID);
            ViewBag.UserID = new SelectList(db.tblUserRegisters, "UserID", "FirstName", tblPlantMaster.UserID);
            return View(tblPlantMaster);
        }

        // POST: tblPlantMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,SensorID,PointID,MachineID,TrainID,AreaID,PlantID,UserID")] tblPlantMaster tblPlantMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblPlantMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AreaID = new SelectList(db.tblAreas, "AreaID", "AreaName", tblPlantMaster.AreaID);
            ViewBag.PlantID = new SelectList(db.tblPlants, "PlantID", "PlantName", tblPlantMaster.PlantID);
            ViewBag.TrainID = new SelectList(db.tblTrains, "TrainID", "TrainName", tblPlantMaster.TrainID);
            ViewBag.UserID = new SelectList(db.tblUserRegisters, "UserID", "FirstName", tblPlantMaster.UserID);
            return View(tblPlantMaster);
        }

        // GET: tblPlantMasters/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblPlantMaster tblPlantMaster = db.tblPlantMasters.Find(id);
            if (tblPlantMaster == null)
            {
                return HttpNotFound();
            }
            return View(tblPlantMaster);
        }

        // POST: tblPlantMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tblPlantMaster tblPlantMaster = db.tblPlantMasters.Find(id);
            db.tblPlantMasters.Remove(tblPlantMaster);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
