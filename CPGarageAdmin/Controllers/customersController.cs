using BAL.Repositories;
using DAL.DBEntities;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace CPGarageAdmin.Controllers
{
    public class customersController : Controller
    {

        customersRepository customerRepo;
        packagesRepository packageRepo;
        public customersController()
        {
            customerRepo = new customersRepository(new Garage_LiveEntities());
            packageRepo = new packagesRepository();

        }
        // GET: users
        public ActionResult list()
        {
            return View(customerRepo.GetAll());
        }
        [HttpGet]
        public ActionResult add(int? id)
      
        {
            var packages = packageRepo.GetAll();
            ViewBag.packages = new SelectList(packages, "PackageInfoID", "PackageName");

            var countries = customerRepo.GetCountries();
            ViewBag.CountryList = new SelectList(countries, "Value", "Text");
            

            try
            {
                if (id != 0 || id != null)
                {
                    //var data = customerRepo.GetCustomerbyid(id);
                    //return View(data);
                    var data = customerRepo.GetCustomerbyid(int.Parse(id.ToString()));
                    return View(data);
                }
            }
            catch (Exception ex)
            {

            }

            return View();
        }
        public JsonResult GetCitiesByCountry(string countryCode)
        {
            // Replace with your logic to fetch cities by country code
            var cities = packageRepo.GetCitiesByCountry(countryCode);
            var cityList = cities.Select(c => new { ID = c.CityID, Name = c.Name }).ToList();
            return Json(cityList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Save(CustomerViewModel modal)
        {
            bool isSuccess = false;
            ModelState.Remove("UserID");
            if (ModelState.IsValid)
            {
                if (modal.UserID > 0)
                {
                    var data = customerRepo.edit(modal);
                    if (modal.PackageInfoID != 1)
                    {
                        try
                        {

                            string ToEmail, SubJect, cc, Bcc;
                            cc = "";
                            Bcc = "";
                            ToEmail = modal.Email;
                            SubJect = "Your Package has been updated";
                            string BodyEmail = System.IO.File.ReadAllText(Server.MapPath("~/Template") + "\\" + "package-update.txt");
                            DateTime dateTime = DateTime.UtcNow.Date;
                            BodyEmail = BodyEmail.Replace("#Date#", dateTime.ToString("dd/MMM/yyyy"))
                            .Replace("#Name#", modal.FirstName.ToString())
                            .Replace("#Email#", modal.Email.ToString())
                            .Replace("#Password#", modal.Password.ToString())
                            .Replace("#Contact#", modal.ContactNo.ToString())
                            .Replace("#Company#", modal.Company.ToString())
                            .Replace("#Package#", modal.PackageInfoID.ToString());
                            //rafi
                            //SendEmail(SubJect, BodyEmail, ToEmail);
                        }
                        catch { }
                    }
                    else
                    {
                        if (modal.PackageInfoID == 1)
                        {
                            try
                            {
                                string ToEmail, SubJect, cc, Bcc;
                                cc = "";
                                Bcc = "";
                                ToEmail = modal.Email;
                                SubJect = "Thank you For Registered in Garage";
                                string BodyEmail = System.IO.File.ReadAllText(Server.MapPath("~/Template") + "\\" + "Trial_template.txt");
                                //rafi
                                //SendEmail(SubJect, BodyEmail, ToEmail);
                            }
                            catch { }
                            if (data == 1)
                                return Json(new { success = true });
                            else
                                return Json(new { success = false });
                        }
                    }
                    if (data == 1)
                        return Json(new { success = true });
                    else
                        return Json(new { success = false });

                }
                else
                {
                    var data = customerRepo.add(modal);
                    if (modal.PackageInfoID == 1)
                    {
                        try
                        {
                            string ToEmail, SubJect, cc, Bcc;
                            cc = "";
                            Bcc = "";
                            ToEmail = modal.Email;
                            SubJect = "Thank you For Registered in Garage";
                            string BodyEmail = System.IO.File.ReadAllText(Server.MapPath("~/Template") + "\\" + "Trial_template.txt");
                            //rafi
                            //SendEmail(SubJect, BodyEmail, ToEmail);
                        }
                        catch { }
                        if (data == 1)
                            return Json(new { success = true });
                        else
                            return Json(new { success = false });
                    }
                    else
                    {
                        try
                        {
                            string ToEmail, SubJect, cc, Bcc;
                            cc = "";
                            Bcc = "";
                            ToEmail = modal.Email;
                            SubJect = "Thank you For Registered in Garage";
                            string BodyEmail = System.IO.File.ReadAllText(Server.MapPath("~/Template") + "\\" + "new-customer.txt");
                            DateTime dateTime = DateTime.UtcNow.Date;
                            BodyEmail = BodyEmail.Replace("#Date#", dateTime.ToString("dd/MMM/yyyy"))
                            .Replace("#Name#", modal.FirstName.ToString())
                            .Replace("#Email#", modal.Email.ToString())
                            .Replace("#Password#", modal.Password.ToString())
                            .Replace("#Contact#", modal.ContactNo.ToString())
                            .Replace("#Company#", modal.Company.ToString())
                            .Replace("#Package#", modal.PackageInfoID.ToString()
                            .Replace("#PackageName#", modal.PackageInfoID == 2 ? "Professional" : null));


                            //rafi
                            //SendEmail(SubJect, BodyEmail, ToEmail);
                        }
                        catch { }
                        if (data == 1)
                            return Json(new { success = true });
                        else
                            return Json(new { success = false });
                    }
                }

            }
            else
            {

                return Json(new { success = isSuccess }, JsonRequestBehavior.AllowGet);
            }
        }
        public void SendEmail(string Subject, string BodyEmail, string To)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(To);


                mail.From = new MailAddress(ConfigurationManager.AppSettings["From"].ToString());
                mail.Subject = Subject;
                string Body = BodyEmail;
                mail.Body = Body;
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient();
                smtp.UseDefaultCredentials = false;
                smtp.Port = int.Parse(ConfigurationManager.AppSettings["SmtpPort"].ToString());
                smtp.Host = ConfigurationManager.AppSettings["SmtpServer"].ToString(); //Or Your SMTP Server Address
                smtp.Credentials = new System.Net.NetworkCredential
                     (ConfigurationManager.AppSettings["From"].ToString(), ConfigurationManager.AppSettings["Password"].ToString());

                smtp.EnableSsl = true;

                smtp.Send(mail);
            }
            catch (Exception ex)
            {

            }
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            bool isSuccess = false;
            if (ModelState.IsValid)
            {
                var data = customerRepo.delete(id, 0);

                return RedirectToAction("list", data);
            }
            else
            {

                return View();
            }
        }
        [HttpGet]
        public ActionResult Status(int id)
        {
            if (ModelState.IsValid)
            {
                var data = customerRepo.status(id);

                return RedirectToAction("list", data);
            }
            else
            {

                return View();
            }
        }
        [HttpGet]
        public ActionResult PackageUpdate()
        {
            var data = customerRepo.GetUserPackage();

            //SendEmailCustomer(modal);
            string ToEmail, SubJect, cc, Bcc;
            cc = "";
            Bcc = "";
            ToEmail = ConfigurationManager.AppSettings["To"].ToString();
            SubJect = "Your Starter Package Is About To Expire";
            string BodyEmail = System.IO.File.ReadAllText(Server.MapPath("~/Template") + "\\" + "package-reminder.txt");
            DateTime dateTime = DateTime.UtcNow.Date;

            //rafi
            //SendEmail(SubJect, BodyEmail, ToEmail);
            return View(data);
        }
    }
}
