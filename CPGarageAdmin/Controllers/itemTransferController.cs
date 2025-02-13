using BAL.Repositories;
using DAL.DBEntities;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CPGarageAdmin.Controllers
{
    public class itemTransferController : Controller
    {
        customersRepository userRepo;
        public itemTransferController()
        {
            userRepo = new customersRepository(new Garage_LiveEntities());
        }
        //public ActionResult list()
        //{
        //    return View();
        //}
        [HttpGet]
        public ActionResult add(int? id)
        {
            var userList = userRepo.DBContext.Users
                            .Where(x => x.StatusID == 1)
                            .OrderBy(x => x.Email)
                            .ToList();
            ViewBag.userList = new SelectList(userList, "UserID", "UserName");
            return View();
        }
        public JsonResult GetLocationsByUser(int userId)
        {
            var locations = userRepo.DBContext.Locations
                              .Where(l => l.UserID == userId)
                              .OrderBy(l => l.Name)
                              .Select(l => new SelectListItem
                              {
                                  Value = l.LocationID.ToString(),
                                  Text = l.Name
                              }).ToList();

            return Json(locations, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Save(ItemTransferViewModel modal)
        {
            bool isSuccess = false;
            if (modal != null)
            {
                var data = userRepo.itemTransferRequest(modal);
                return Json(new { success = true });
            }
            else
            {

                return Json(new { success = isSuccess }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}