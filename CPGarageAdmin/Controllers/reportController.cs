using BAL.Repositories;
using DAL.DBEntities;
using DAL.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace CPGarageAdmin.Controllers
{
    public class reportController : Controller
    {
        reportRepository reportRepo;
        customersRepository Repo;
        public reportController()
        {
            reportRepo = new reportRepository(new Garage_LiveEntities());
            Repo = new customersRepository(new Garage_LiveEntities());

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
        public ActionResult GetDetailReport(string fromDate, string toDate, string statusFilter, JqueryDatatableParam param)
        {
            var data = reportRepo.GetLead(fromDate, toDate, statusFilter);
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
        public ActionResult CarVisitorReport(string fromdate, string todate)
        {
            return View();
        }
        public ActionResult DetailLogReport()
        {
            var User = Repo.DBContext.Users.Where(x => x.StatusID == 1).ToList();
            ViewBag.userList = new SelectList(User, "UserID", "UserName");
            return View();
        }
        public ActionResult LogReport(string fromDate, string toDate, string customerFilter, JqueryDatatableParam param)
        {
            var data = reportRepo.GetLog(fromDate, toDate, customerFilter);
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
    }
}