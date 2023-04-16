using BAL.Repositories;
using DAL.DBEntities;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        [System.Web.Http.HttpGet]
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
                if (id != 0 || id != null)
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
        [HttpPost]
        [Route("Save")]
        public int Save([FromBody] PackagesInfoViewModel obj)
        {
            if (obj.PackageInfoID == 0 || obj.PackageInfoID == null)
            {
                obj.CreatedDate = DateTime.Now;
                return packagesRepo.Insert(obj);
            }
            else
            {
                obj.LastUpdatedDate = DateTime.Now;
                return packagesRepo.Update(obj);
                //if (data == 1)
                //    return RedirectToAction("list");
                //else
                //    return RedirectToAction("list");
            }
        }
    }
}
