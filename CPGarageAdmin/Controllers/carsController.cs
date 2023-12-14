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
			var employees = carsRepo.GetCars(param);
			if (!string.IsNullOrEmpty(param.sSearch))
			{
				employees = employees.Where(x => x.RegistrationNo.ToLower().Contains(param.sSearch.ToLower())
											  || x.MakeName.ToLower().Contains(param.sSearch.ToLower())
											  || x.ModelName.ToLower().Contains(param.sSearch.ToLower())
											  || x.Year.ToString().Contains(param.sSearch.ToLower())
											  || x.CarTypeName.ToString().Contains(param.sSearch.ToLower())).ToList();
			}
			var sortColumnIndex = Convert.ToInt32(HttpContext.Request.QueryString["iSortCol_0"]);
			var sortDirection = HttpContext.Request.QueryString["sSortDir_0"];
			if (sortColumnIndex == 3)
			{
				//employees = sortDirection == "asc" ? employees.OrderBy(c => c.Name) : employees.OrderByDescending(c => c.);
			}
			else if (sortColumnIndex == 4)
			{
				//employees = sortDirection == "asc" ? employees.OrderBy(c => c.CarTypeName) : employees.OrderByDescending(c => c.CarTypeName);
			}
			else if (sortColumnIndex == 5)
			{
			//	employees = sortDirection == "asc" ? employees.OrderBy(c => c.Color) : employees.OrderByDescending(c => c.Salary);
			}
			else
			{
				//Func<Car, string> orderingFunction = e => sortColumnIndex == 0 ? e.Name : sortColumnIndex == 1 ? e.RegistrationNo : e.EngineType;
				//employees = sortDirection == "asc" ? employees.OrderBy(orderingFunction) : employees.OrderByDescending(orderingFunction);
			}
			var displayResult = employees.Skip(param.iDisplayStart)
			   .Take(param.iDisplayLength).ToList();
			var totalRecords = employees.Count();
			return Json(new
			{
				param.sEcho,
				iTotalRecords = totalRecords,
				iTotalDisplayRecords = totalRecords,
				aaData = displayResult
			}, JsonRequestBehavior.AllowGet);
		}

		//public ActionResult List(int page = 1, int pageSize = 10)
		//{
		//	int skip = (page - 1) * pageSize;
		//	var data = carsRepo.GetCars(skip, pageSize);
		//	return View(data);
		//}

		//public ActionResult List()
		//{
		//	var data = carsRepo.GetCars();
		//	return View(data);
		//	//return View();
		//}
		[HttpGet]
		public ActionResult add(int? id)
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
