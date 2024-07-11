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
    public class carDeleteController : Controller
    {
        carDeleteRepository carsRepo;
        public carDeleteController()
        {
            carsRepo = new carDeleteRepository(new Garage_LiveEntities());
        }
        public ActionResult List()
        {
            var data = carsRepo.GetAll();
            return View(data);
        }
        [HttpGet]
        public ActionResult detail(int? id)
        {
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
        [HttpPost]
        public ActionResult Save(CarDeleteViewModel data)
        {
            try
            {
                data.LastUpdatedDate = DateTime.Now;
                int rtn = carsRepo.edit(data);
            }
            catch (Exception ex) { }
            return RedirectToAction("list", data);
        }
    }
}
