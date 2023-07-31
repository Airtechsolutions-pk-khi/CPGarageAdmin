using BAL.Repositories;
using DAL.DBEntities;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Services.Description;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace CPGarageAdmin.Controllers
{
    public class packagesController : Controller
    {
        packagesRepository packagesRepo;
        public packagesController()
        {
            packagesRepo = new packagesRepository(new Garage_LiveEntities());

        }
        
        public ActionResult list()
        {
            var data = packagesRepo.GetAll();         
            return View(data);
        }

        [System.Web.Http.HttpGet]
        public ActionResult add(int? id)
        {
            try
            {
                if (id != null)
                {
                    var data = packagesRepo.Get(id);
                    return View(data);
                }
            }
            catch (Exception ex)
            {
            }
            return View();
        }
        [System.Web.Http.HttpGet]
        public JsonResult getByID(int? Id)
        {
            try
            {
                if (Id != null)
                {
                    var data = packagesRepo.Get(Id);
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Save(PackagesInfoViewModel data)
        {
            if (data.PackageInfoID == 0 || data.PackageInfoID == null)
            {
                data.CreatedDate = DateTime.Now;
                int rtn = packagesRepo.Insert(data);
                return Json(new { data = rtn }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                data.LastUpdatedDate = DateTime.Now;
                int rtn = packagesRepo.Update(data);
                return Json(new { data = rtn }, JsonRequestBehavior.AllowGet);
                
            }
        }

        

    }
    
}
