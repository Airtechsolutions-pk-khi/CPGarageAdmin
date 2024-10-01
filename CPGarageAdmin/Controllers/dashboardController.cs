using BAL.Repositories;
using DAL.DBEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CPGarageAdmin.Controllers
{
    public class dashboardController : Controller
    {
        dashboardRepository dashboardRepo;
        public dashboardController()
        {
            dashboardRepo = new dashboardRepository(new Garage_LiveEntities());

        }
      

        // GET: dashboard
        public ActionResult dashboard(string fromdate, string todate)
        {
            try
            {                
                return View(dashboardRepo.GetDashboard(fromdate, todate));
            }
            catch (Exception)
            {
                return View();
            }
        }
    }
}