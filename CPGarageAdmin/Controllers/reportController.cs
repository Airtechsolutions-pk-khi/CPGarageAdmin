using BAL.Repositories;
using DAL.DBEntities;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace CPGarageAdmin.Controllers
{
    public class reportController : Controller
    {
        reportRepository reportRepo;
        public reportController()
        {
            reportRepo = new reportRepository(new Garage_LiveEntities());

        }
        // GET: report
        public ActionResult SummaryReport()
        {
            return View();
        }
        [System.Web.Mvc.HttpGet]
        public JsonResult Get(string fromdate, string todate)
        {
            try
            {
                if (fromdate != null)
                {
                    var data = reportRepo.Get(fromdate, todate);
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
            }
            return Json(JsonRequestBehavior.AllowGet);
        }
        public ActionResult LeadDetailReport()
        {
            return View();
        }
        public ActionResult GetDetailReport(string fromDate, string toDate, JqueryDatatableParam param)
        {
            var data = reportRepo.GetLead(fromDate, toDate);
            var totalrecord = data.Count();
            var jsonResponse = new
            {
                param.sEcho,
                iTotalRecords = totalrecord,
                iTotalDisplayRecords = totalrecord,
                aaData = data
            };
            return Json(jsonResponse, JsonRequestBehavior.AllowGet);
        }
        //public ActionResult LeadDetailReport(string fromdate, string todate)
        //{
        //    var data = reportRepo.GetLead(fromdate, todate);
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //    //return View(data);
        //}
    }
}