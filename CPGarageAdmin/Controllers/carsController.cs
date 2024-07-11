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
        private object totalRecordCount;
        private object filteredRecordCount;

        public carsController()
        {
            carsRepo = new carsRepository(new Garage_LiveEntities());

        }
        public ActionResult List()
        {
            return View();
        }
        public ActionResult GetData(JqueryDatatableParam param)
        {
            var cars = carsRepo.GetCars(param);
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
        public ActionResult add(int? id, int? ModelID)
        {
            var Make = carsRepo.GetMake();
            ViewBag.Make = new SelectList(Make, "MakeID", "Name");

            //var Customer = carsRepo.GetCustomer();
            //ViewBag.Customer = new SelectList(Customer, "CustomerID", "FullName");
            try
            {
                if (id != 0 || id != null)
                {
                    var data = carsRepo.GetCarByID(id);
                    return View(data);
                }
            }
            catch (Exception ex)
            {

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
            if (data.CarID == 0 || data.CarID == null)
            {
                data.CreatedOn = DateTime.Now;
                int rtn = carsRepo.add(data);
                return Json(new { data = rtn }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                data.LastUpdateDate = DateTime.Now.ToString();
                int rtn = carsRepo.edit(data);
                //return Json(new { data = rtn }, JsonRequestBehavior.AllowGet);
                return Json(new { redirectTo = Url.Action("list") }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            int rtn = carsRepo.Delete(id);
            return Json(new { data = rtn }, JsonRequestBehavior.AllowGet);
        }
    }
}
