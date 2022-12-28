using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CPGarageAdmin.Controllers
{
    public class baseController : Controller
    {
        // GET: base
        public ActionResult Index()
        {
            return View();
        }
    }
}