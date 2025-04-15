using BAL.Repositories;
using DAL.DBEntities;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.Json.Nodes;
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
        //public ActionResult list()
        //{
        //    return View(customerRepo.GetAll());
        //}

        public ActionResult list(int? filter = 0) // Default to "All"
        {
            IEnumerable<DAL.DBEntities.User> customers;

            switch (filter)
            {
                case 1: // Active
                    customers = customerRepo.GetAll().Where(c => c.StatusID == 1);
                    break;
                case 2: // Deactive
                    customers = customerRepo.GetAll().Where(c => c.StatusID != 1);
                    break;
                default: // All
                    customers = customerRepo.GetAll();
                    break;
            }

            ViewBag.Filter = filter; // Pass the selected filter value to the view
            return View(customers);
        }

        [HttpGet]
        public ActionResult add(int? id)
        {
            var packages = packageRepo.GetAll();
            ViewBag.Packages = new SelectList(packages, "PackageInfoID", "PackageName");

            var countries = customerRepo.GetCountries();
            ViewBag.CountryList = new SelectList(countries, "Value", "Text");
            ViewBag.CityList = new SelectList(new List<SelectListItem>(), "Value", "Text"); // Empty initially

            CustomerViewModel data = new CustomerViewModel(); // Create an empty model

            try
            {
                if (id.HasValue && id.Value != 0) // Only fetch data if id is valid
                {
                    data = customerRepo.GetCustomerbyid(id.Value);

                    if (data != null)
                    {
                        // Fetch cities based on the customer's saved country
                        var cities = packageRepo.GetCitiesByCountry(data.CountryID);
                        ViewBag.CityList = new SelectList(cities, "ID", "Name", data.CityID); // Bind selected city
                    }
                }
            }
            catch (Exception ex)
            {
                // Log exception if needed
                ViewBag.ErrorMessage = "An error occurred while fetching customer data.";
            }

            return View(data); // Always return a model
        }



        //[HttpGet]
        //public ActionResult add(int? id)

        //{
        //    var packages = packageRepo.GetAll();
        //    ViewBag.packages = new SelectList(packages, "PackageInfoID", "PackageName");

        //    var countries = customerRepo.GetCountries();
        //    ViewBag.CountryList = new SelectList(countries, "Value", "Text");
        //    ViewBag.CityList = new SelectList(new List<SelectListItem>(), "Value", "Text");
        //    try
        //    {
        //        if (id != 0 || id != null)
        //        {
        //            //var data = customerRepo.GetCustomerbyid(id);
        //            //return View(data);
        //            var data = customerRepo.GetCustomerbyid(int.Parse(id.ToString()));
        //            // Fetch cities based on the customer's saved country
        //            var cities = packageRepo.GetCitiesByCountry(data.CountryID);
        //            ViewBag.CityList = new SelectList(cities, "ID", "Name", data.ID);

        //            return View(data);
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //    return View();
        //}
        public JsonResult GetCitiesByCountry(string countryCode)
        {
            // Replace with your logic to fetch cities by country code
            var cities = packageRepo.GetCitiesByCountry(countryCode);
            var cityList = cities.Select(c => new { ID = c.ID, Name = c.Name }).ToList();
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

                    if (data == -1)
                    {
                        return Json(new { success = false, message = "This email is already taken." });
                    }


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
                    if (data == -1)
                    {
                        return Json(new { success = false, message = "This email is already taken." });
                    }

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
        //public JsonResult SendWhatsAppMessage(int userId)
        //{
        //    var user = customerRepo.GetCustomerbyid(userId);
        //    if (user != null && !string.IsNullOrEmpty(user.ContactNo))
        //    {
        //        string message = $@"‏
        //             *عملينا العزيز، شكراً لاختيارك نظام كراج. أدناه بيانات حسابكم:*

        //             👤 *اسم العميل:* {user.FirstName} {user.LastName}  
        //             🏢 *المركز:* {user.Company}  

        //             🔹 *بيانات حسابكم في كراج:*  

        //             🌐 *للدخول إلى لوحة التحكم من خلال الرابط:*  
        //             https://admin.garage.sa/  

        //             *بعد دخولكم الى لوحة التحكم يمكنكم الدخول باستخدام:*

        //             📧 *الإيميل:* {user.Email}  
        //             🔑 *كلمة المرور:* {user.Password}  

        //             🎥 *فيديو شرح لوحة التحكم:*  
        //             https://youtu.be/EXCEQMy6RPQ  

        //             📚 *مكتبة المساعدة:*  
        //             https://garagelibrary.notion.site/5cd30a5bc7474380928471949e4735fe?pvs=4  

        //             📲 *تحميل تطبيق كراج:*  

        //             🛒 *نسخة الاي او اس (ايباد):*  
        //             https://apps.apple.com/sa/app/garage-pos/id1454372626  

        //             🤖 *نسخة الاندرويد:*  
        //             https://play.google.com/store/apps/details?id=sa.garage  

        //             *بعد تحميل التطبيق يمكنكم الدخول باستخدام:*  
        //             \u200E
        //             🏷️ *رمز المنشأة:* {user.CompanyCode}  
        //             🏷️ *الكود:* {user.Passcode} ";

        //        string whatsappUrl = $"https://api.whatsapp.com/send?phone={user.ContactNo}&text={Uri.EscapeDataString(message)}";
        //        return Json(new { success = true, url = whatsappUrl }, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(new { success = false, message = "المستخدم غير موجود أو لا يحتوي على رقم هاتف." }, JsonRequestBehavior.AllowGet);
        //}
        public JsonResult SendWhatsAppMessage(int userId)
        {
            var user = customerRepo.GetCustomerbyid(userId);
            if (user != null && !string.IsNullOrEmpty(user.ContactNo))
            {
                try
                {
                    var company = user.Company;
                    var name = user.FirstName + user.LastName;
                    var companycode = user.CompanyCode;
                    var passcode = user.Passcode;
                    var username = user.Email;
                    var password = user.Password;

                    string apiUrl = "https://graph.facebook.com/v16.0/104897916024556/messages";
                    string accessToken = "Bearer EAADqZBje1EysBO3Ylo9hXm74a0utJr9K3MxTziNVlgkrmSVO6fvtQzF06RoogNSa8YBGfZANaXZAkiQUHIPrIbroONJtYg53nSSPDMoNeJEgEroZArJJnI7zVVPCxiRlz8B51E2TXZCPS5QKLZBSYZACkz93Aur0hssH7K82n4KX9RuO9NcFFjtJnilR3ZCf5saernwGxlZCXHZAEH2EjYzroZAQv97CXuROwfDInZCbiZBZATbBh9IJF5WOVaGhHutmSP";

                    //string jsonBody = $@"‏
                    //             *عملينا العزيز، شكراً لاختيارك نظام كراج. أدناه بيانات حسابكم:*

                    //             👤 *اسم العميل:* {user.FirstName} {user.LastName}  
                    //             🏢 *المركز:* {user.Company}  

                    //             🔹 *بيانات حسابكم في كراج:*  

                    //             🌐 *للدخول إلى لوحة التحكم من خلال الرابط:*  
                    //             https://admin.garage.sa/  

                    //             *بعد دخولكم الى لوحة التحكم يمكنكم الدخول باستخدام:*

                    //             📧 *الإيميل:* {user.Email}  
                    //             🔑 *كلمة المرور:* {user.Password}  

                    //             🎥 *فيديو شرح لوحة التحكم:*  
                    //             https://youtu.be/EXCEQMy6RPQ  

                    //             📚 *مكتبة المساعدة:*  
                    //             https://garagelibrary.notion.site/5cd30a5bc7474380928471949e4735fe?pvs=4  

                    //             📲 *تحميل تطبيق كراج:*  

                    //             🛒 *نسخة الاي او اس (ايباد):*  
                    //             https://apps.apple.com/sa/app/garage-pos/id1454372626  

                    //             🤖 *نسخة الاندرويد:*  
                    //             https://play.google.com/store/apps/details?id=sa.garage  

                    //             *بعد تحميل التطبيق يمكنكم الدخول باستخدام:*  
                    //             \u200E
                    //             🏷️ *رمز المنشأة:* {user.CompanyCode}  
                    //             🏷️ *الكود:* {user.Passcode} ";


                    string jsonBody = "{" +
                        "\"messaging_product\": \"whatsapp\"," +
                        "\"recipient_type\": \"individual\"," +
                        "\"to\": \"" + user.ContactNo + "\"," +
                        "\"type\": \"template\"," +
                        "\"template\": {" +
                        "    \"name\": \"customer_account\"," +
                        "    \"language\": {" +
                        "        \"code\": \"ar\"" +
                        "    }," +
                        "    \"components\": [" +
                        "        {" +
                        "            \"type\": \"header\"," +
                        "            \"parameters\": [" +
                        "                {" +
                        "                    \"type\": \"text\"," +
                        "                    \"text\": \"👤 *اسم العميل:* " + user.FirstName + " " + user.LastName + "\"" +
                        "                }" +
                        "            ]" +
                        "        }," +
                        "        {" +
                        "            \"type\": \"body\"," +
                        "            \"parameters\": [" +
                        "                {" +
                        "                    \"type\": \"text\"," +
                        "                    \"text\": " +
                        "                     \"🏢 *المركز:* " + user.Company + "\\n\\n" +
                        "                     🔹 *بيانات حسابكم في كراج:* \\n\\n" +
                        "                     🌐 *للدخول إلى لوحة التحكم:* \\nhttps://admin.garage.sa/ \\n\\n" +
                        "                     📧 *الإيميل:* " + user.Email + "\\n" +
                        "                     🔑 *كلمة المرور:* " + user.Password + "\\n\\n" +
                        "                     🎥 *فيديو شرح لوحة التحكم:* \\nhttps://youtu.be/EXCEQMy6RPQ \\n\\n" +
                        "                     📚 *مكتبة المساعدة:* \\nhttps://garagelibrary.notion.site/5cd30a5bc7474380928471949e4735fe?pvs=4 \\n\\n" +
                        "                     📲 *تحميل تطبيق كراج:* \\n\\n" +
                        "                     🛒 *نسخة الاي او اس:* \\nhttps://apps.apple.com/sa/app/garage-pos/id1454372626 \\n\\n" +
                        "                     🤖 *نسخة الاندرويد:* \\nhttps://play.google.com/store/apps/details?id=sa.garage \\n\\n" +
                        "                     🏷️ *رمز المنشأة:* " + user.CompanyCode + "\\n" +
                        "                     🏷️ *الكود:* " + user.Passcode + "\"" +
                        "                }" +
                        "            ]" +
                        "        }," +
                        "        {" +
                        "            \"type\": \"button\"," +
                        "            \"sub_type\": \"url\"," +
                        "            \"index\": \"0\"," +
                        "            \"parameters\": [" +
                        "                {" +
                        "                    \"type\": \"text\"," +
                        "                    \"text\": \"home\"" +
                        "                }" +
                        "            ]" +
                        "        }" +
                        "    ]" +
                        "}}";
                    try
                    {
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
                        request.Method = "POST";
                        request.ContentType = "application/json";
                        request.Headers.Add("Authorization", accessToken);

                        using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                        {
                            streamWriter.Write(jsonBody);
                            streamWriter.Flush();
                            streamWriter.Close();
                        }

                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        using (var streamReader = new StreamReader(response.GetResponseStream()))
                        {
                            string responseData = streamReader.ReadToEnd();
                        }
                      
                    }
                    catch (WebException ex)
                    {
                        // Handle any exceptions here
                        Console.WriteLine(ex.Message);
                        return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}
