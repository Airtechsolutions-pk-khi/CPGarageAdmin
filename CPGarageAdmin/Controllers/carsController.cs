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

		//[HttpPost]
		//public ActionResult LoadData()
		//{

		//	var draw = Request.Form.GetValues("draw").FirstOrDefault();
		//	var start = Request.Form.GetValues("start").FirstOrDefault();
		//	var length = Request.Form.GetValues("length").FirstOrDefault();
		//	//Find Order Column
		//	var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
		//	var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();


		//	int pageSize = length != null ? Convert.ToInt32(length) : 0;
		//	int skip = start != null ? Convert.ToInt32(start) : 0;
		//	int recordsTotal = 0;
		//	using (Garage_LiveEntities dc = new Garage_LiveEntities())
		//	{
		//		// dc.Configuration.LazyLoadingEnabled = false; // if your table is relational, contain foreign key
		//		var v = carsRepo.GetCars();

		//		//SORT
		//		if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
		//		{
		//			//v = v.OrderBy(sortColumn + " " + sortColumnDir);
		//		}

		//		recordsTotal = v.Count();
		//		var data = v.Skip(skip).Take(pageSize).ToList();
		//		return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data }, JsonRequestBehavior.AllowGet);
		//	}
		//}
		public ActionResult List()
		{
			var data = carsRepo.GetCars();
			return View(data);
			//return View();
		}
		[HttpGet]
		public ActionResult add(int? id)
		{
			var Make = carsRepo.GetMake();
			ViewBag.Make = new SelectList(Make, "MakeID", "Name");

			var Customer = carsRepo.GetCustomer();
			ViewBag.Customer = new SelectList(Customer, "CustomerID", "FullName");
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
		//public ActionResult GetCustomer()
		//{
		//	var Customer = carsRepo.GetCustomer();
		//	ViewBag.Customer = new SelectList(Customer, "CustomerID", "FullName");
		//	return Json(Customer, JsonRequestBehavior.AllowGet);
		//}
		//public JsonResult GetMakes(string searchTerm)
		//{
		//	var makes = carsRepo.GetMake(searchTerm);
		//	return Json(makes, JsonRequestBehavior.AllowGet);
		//}
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
				return Json(new { data = rtn }, JsonRequestBehavior.AllowGet);

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
