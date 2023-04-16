using BAL.Repositories;
using DAL.DBEntities;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace CPGarageAdmin.Controllers
{
    public class customersController : Controller
    {
        customersRepository customerRepo;
        packagesRepository packageRepo; 
        public customersController()
        {
            customerRepo = new customersRepository(new Garage_LiveEntities());
            packageRepo = new packagesRepository();
        }
        // GET: users
        public ActionResult list()
        {
            return View(customerRepo.GetCustomers());
        }
        [HttpGet]
        public ActionResult add(int? id)
        {
            var packages = packageRepo.GetAll();
            ViewBag.packages = new SelectList(packages, "PackageInfoID", "PackageName");

            try
            {
                if (id != 0 || id != null)
                {
                    //var data = customerRepo.GetCustomerbyid(id);
                    //return View(data);
                    var data = customerRepo.GetCustomerbyid(int.Parse(id.ToString()));
                    return View(data);
                }
            }
            catch (Exception ex)
            {

            }

            return View();
        }
        [HttpPost]
        public JsonResult Save(CustomerViewModel modal)
        {
            bool isSuccess = false;
            ModelState.Remove("UserID");
            if (ModelState.IsValid)
            {
                if (modal.UserID > 0)
                {
                    var data = customerRepo.edit(modal);

                    if (data == 1)
                        return Json(new { success = true });
                    else
                        return Json(new { success = false });

                }
                else
                {
                    var data = customerRepo.add(modal);

                    if (data == 1)
                        return Json(new { success = true });
                    else
                        return Json(new { success = false });
                }

            }
            else
            {

                return Json(new { success = isSuccess }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            bool isSuccess = false;
            if (ModelState.IsValid)
            {
                var data = customerRepo.delete(id, 0);

                return RedirectToAction("list", data);
            }
            else
            {

                return View();
            }
        }
        [HttpGet]
        public ActionResult Status(int id)
        {
            if (ModelState.IsValid)
            {
                var data = customerRepo.status(id);

                return RedirectToAction("list", data);
            }
            else
            {

                return View();
            }
        }
    }
}
