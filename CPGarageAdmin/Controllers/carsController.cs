using BAL.Repositories;
using DAL.DBEntities;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace CPGarageAdmin.Controllers
{
    public class carsController : Controller
    {

        carsRepository carsRepo;
        modelRepository modelRepo;
        customersRepository custRepo;
        private object totalRecordCount;
        private object filteredRecordCount;

        public carsController()
        {
            carsRepo = new carsRepository(new Garage_LiveEntities());
            modelRepo = new modelRepository(new Garage_LiveEntities());
            custRepo = new customersRepository(new Garage_LiveEntities());
        }
        public ActionResult List()
        {
            return View();
        }
        public ActionResult GetData(JqueryDatatableParam param, int? statusID)
        {
            var cars = carsRepo.GetCars(param, statusID);
            var totalRecords = cars.TotalRecords;
            var jsonResponse = new
            {
                param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = cars.Cars
            };
            return Json(jsonResponse, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult GetData(JqueryDatatableParam param)
        //{
        //	var employees = carsRepo.GetCars(param);
        //	var displayResult = employees.Skip(param.iDisplayStart)
        //	   .Take(param.iDisplayLength).ToList();
        //	var totalRecords = employees.Count();
        //	return Json(new
        //	{
        //		param.sEcho,
        //		iTotalRecords = totalRecords,
        //		iTotalDisplayRecords = totalRecords,
        //		aaData = displayResult
        //	}, JsonRequestBehavior.AllowGet);
        //}

        [HttpGet]
        public ActionResult add(int? id)
        {
            var Make = carsRepo.GetMake();
            ViewBag.Make = new SelectList(Make, "MakeID", "Name");
            try
            {
                if (id != 0 || id != null)
                {
                    var data = carsRepo.GetCarByID(id);
                    var Model = modelRepo.DBContext.Models.Where(x => x.MakeID == data.MakeID).ToList();
                    ViewBag.Model = new SelectList(Model, "ModelID", "Name");
                    return View(data);
                }
            }
            catch (Exception ex)
            {
                //carid = 245624
                //180433
            }
            return View();
        }
        [HttpGet]
        public ActionResult GetCustomer(string searchTerm)
        {
            var customers = carsRepo.GetCustomer();
            // Convert the filtered customers to a format that Select2 expects.
            var results = customers
                .Where(c => c != null && c.FullName != null && c.FullName.Contains(searchTerm))
                .Select(c => new
                {
                    id = c.CustomerID,
                    text = c.FullName,
                });
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Models(int makeId)
        {
            var models = carsRepo.GetModel(makeId);
            ViewBag.Model = new SelectList(models, "ModelID", "Name");
            return Json(models, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Save(CarsViewModel data)
        {
            try
            {
                if (data.CarID == 0 || data.CarID == null)
                {
                    data.CreatedOn = DateTime.Now;
                    int rtn = carsRepo.add(data);
                    return Json(new { success = true, data = rtn, redirectUrl = Url.Action("List", "Cars") }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    data.LastUpdateDate = DateTime.Now.ToString();
                    int rtn = carsRepo.edit(data);
                    return Json(new { success = true, redirectUrl = Url.Action("List", "Cars") }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred: " + ex.Message });
            }
        }

        //public JsonResult Save(CarsViewModel data)
        //{
        //    if (data.CarID == 0 || data.CarID == null)
        //    {
        //        data.CreatedOn = DateTime.Now;
        //        int rtn = carsRepo.add(data);
        //        return Json(new { success = true, data = rtn, redirectUrl = Url.Action("List", "Cars") }, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        data.LastUpdateDate = DateTime.Now.ToString();
        //        int rtn = carsRepo.edit(data);
        //        return Json(new { success = true}, JsonRequestBehavior.AllowGet);
        //    }
        //}

        [HttpPost]
        public ActionResult Delete(int id)
        {
            int rtn = carsRepo.Delete(id);
            return Json(new { data = rtn }, JsonRequestBehavior.AllowGet);
        }
    }
}
