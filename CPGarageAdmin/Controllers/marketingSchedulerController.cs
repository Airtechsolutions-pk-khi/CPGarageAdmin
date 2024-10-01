using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CPGarageAdmin.Controllers
{
    public class marketingSchedulerController : Controller
    {
        // GET: marketingScheduler
        public ActionResult list()
        {
            return View();
        }
        public ActionResult add()
        {
            return View();
        }     
        public ActionResult whatsapp()
        {
            return View();
        }    
        public ActionResult notification()
        {
            return View();
        }
    }
}