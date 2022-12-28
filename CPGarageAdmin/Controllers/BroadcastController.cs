using BAL.Repositories;
using DAL.DBEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CPGarageAdmin.Controllers
{
    public class BroadcastController : Controller
    {
        customersRepository repo;
        public BroadcastController()
        {
            repo = new customersRepository(new Garage_LiveEntities());

        }
        // GET: itemmovement
        public ActionResult Broadcast(int? userid, string fDate, string tDate)
        {
            var listUsers = repo.getAllUsers();
            ViewBag.Users = listUsers;
            //ViewBag.Users = new MultiSelectList(listUsers, "UserID", "UserName");
            //int id = 0;

            //if (userid == 0 )
            //{
            //     id = listUsers.FirstOrDefault() != null ? listUsers.FirstOrDefault().UserID : 0;
            //}
            //ViewBag.Data = repo.GetCustomerSMSBills(id, fDate == null ? DateTime.Now.Date.AddDays(-5).ToString() : fDate, tDate == null ? DateTime.Now.Date.ToString() : tDate);
            return View();
        }


        //[HttpGet]
        //public ActionResult Search(int userid, string fDate, string tDate)
        //{
        //    var data = repo.GetCustomerSMSBills(userid, fDate, tDate);

        //    return View("list", "sms", data);

        //}


        //[HttpGet]
        //public ActionResult print(string companyname, string smscount, string fromdate, string todate, string total, string userid)
        //{
        //    var data = repo.GetInvoicePrint(companyname, smscount, fromdate, todate, total, int.Parse(userid));

        //    return Json(new { status = data == "" ? 0 : 1, url = data }, JsonRequestBehavior.AllowGet);

        //}
    }
}