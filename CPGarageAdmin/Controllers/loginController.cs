using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CPGarageAdmin.Controllers
{
    public class loginController : baseController
    {
        // GET: login
        public ActionResult login()
        {
            return View();
        }



        [HttpPost]
        public ActionResult login(string username, string password)
        {
            //UserSession sess = new UserSession();

            //Session.Add("UserSession", null);
            //var user = accessControlRepo.authinticateUser(username, password);

            //if (user != null)
            //{
            //    sess.tblUser = user;
            //    sess.listPagePermissions = accessControlRepo.getAuthorizePageList(Convert.ToInt32(user.TBLROLE.ROLEID));

            //    specialPermissionsViewModal sp = new specialPermissionsViewModal();
            //    var permissions = user.TBLROLE.TBLPERMISSIONSMAPPINGs;
            //    var spProperties = sp.GetType().GetProperties();

            //    if (user.TBLROLE.ROLENAME.ToLower() == "admin" || user.ROLEID == 1)
            //    {

            //        foreach (var prop in spProperties)
            //        {
            //            prop.SetValue(sp, true, null);
            //        }
            //    }
            //    else
            //    {
            //        foreach (string rule in Enum.GetNames(typeof(specialPermissionEnum)).ToList())
            //        {
            //            string permissionName = Regex.Replace(rule, "(\\B[A-Z])", " $1");
            //            permissionName = permissionName.ToLower();

            //            var permission = permissions.Where(p => p.TBLSPECIALPERMISSION.SPECIALPERMISSION.ToLower().Contains(permissionName)).FirstOrDefault();
            //            var spProperty = spProperties.Where(pr => pr.Name.ToLower() == permissionName.Replace(" ", "")).FirstOrDefault();
            //            if (spProperty != null && permission != null)
            //            {
            //                spProperty.SetValue(sp, permission.ISACTIVE, null);
            //            }
            //            else if (spProperty != null)
            //            {
            //                spProperty.SetValue(sp, false, null);
            //            }
            //        }
            //    }
            //    sess.specialPermission = sp;

            //    Session["UserSession"] = sess;
            if (username == "cpadmin@garage.sa" && password == "admin")
            {
                ViewBag.welcome = "Welcome: '" + username + "'";
                return RedirectToAction("dashboard", "dashboard");
            }

            else
            {
                return View();
            }
            //}
            //else
            //{
            //    ViewBag.PageMessage = "Invalid UserName/Password";
            //    return View();
            //}
        }
    }
}