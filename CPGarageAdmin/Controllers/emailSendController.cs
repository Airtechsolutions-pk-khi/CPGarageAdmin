using BAL.Repositories;
using DAL.DBEntities;
using DAL.Models;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.Security.AntiXss;
using System.Web.Services.Description;

namespace CPGarageAdmin.Controllers
{
	public class emailSendController : Controller
	{

		emailSendRepository emailSendRepo;
		customersRepository repo;
		BaseRepository baserepo;
		public emailSendController()
		{
			emailSendRepo = new emailSendRepository(new Garage_LiveEntities());
			repo = new customersRepository();
			baserepo = new BaseRepository();
		}
		public ActionResult add()
		{
			var AllCustomers = emailSendRepo.GetCustomers();
			ViewBag.AllCustomers = new SelectList(AllCustomers, "UserID", "UserName");

			var Customers = emailSendRepo.GetCustomerNotification();
			ViewBag.Customers = new SelectList(Customers, "CustomerID", "FullName");

			var  CustomerMarketing = emailSendRepo.GetCustomerMarketing();
			ViewBag.CustomerMarketing = new SelectList(CustomerMarketing, "CustomerID", "FullName");
			return View();
		}
		[HttpGet]
        public ActionResult sendemail()
        {
            var AllCustomers = emailSendRepo.GetCustomers();
            ViewBag.AllCustomers = new SelectList(AllCustomers, "UserID", "UserName");

            var Customers = emailSendRepo.GetCustomerNotification();
            ViewBag.Customers = new SelectList(Customers, "CustomerID", "FullName");

            var CustomerMarketing = emailSendRepo.GetCustomerMarketing();
            ViewBag.CustomerMarketing = new SelectList(CustomerMarketing, "CustomerID", "FullName");
            return View();
        }
        [HttpPost]
		public ActionResult EmailSend(EmailSendViewModel model)
		{
			try
			{
				if (model.Type == "Email")
				{
					foreach (var id in model.UserIDs)
					{
						var customerEmail = emailSendRepo.GetCustomerEmailByID(id);
						model.Descripiton = model.Descripiton;

						try
						{
							string SubJect, ToEmail;
							SubJect = model.Subject;
							ToEmail = customerEmail[0].Email;
							string BodyEmail = System.IO.File.ReadAllText(Server.MapPath("~/Template") + "\\" + "marketing.txt");
							BodyEmail = BodyEmail.Replace("#Title#", model.Title.ToString())
												 .Replace("#Description#", model.Descripiton.ToString());
							SendEmail(SubJect, BodyEmail, ToEmail);						
						}
						catch (Exception ex)
						{
							return Json(new { success = false });
						}
					}
					return Json(new { success = true });
				}
				else if (model.Type == "WhatsApp")
				{
					if (model.WhatsappType == "User")
					{
						var ds = emailSendRepo.GetUserContactByID(model.Users);
						var lstcustomer = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[0])).ToObject<List<User>>();
						foreach (var item in lstcustomer)
						{
							try
							{
								var Contact = item.ContactNo;
								var Title = model.Title1;
								var Description = model.WhatsappDescription;

								string apiUrl = "https://graph.facebook.com/v16.0/104897916024556/messages";
								string accessToken = "Bearer EAAXW0hLwIIwBOzNXZCdaaeqg7fdsBZB8IWeb7Dl2hh5qWBPvZCxpyC0lbKEJvafy6gKF3dvep9nhzHxukBooe5GZBg6p6BNZB6BAlZBuJK7TMsu4kpMNWQcjhYZBS7DcuANJcZA7F9XiHZAwngMdFWKYSuk8g0BbA7qpgsZAUu9y0Unz2RceZAaUfUmUOJfKFrSpRKAJ9ZCMZASP54AxO7aZAH";

								string jsonBody = "{" +
									"\"messaging_product\": \"whatsapp\"," +
									"\"recipient_type\": \"individual\"," +
									"\"to\": \"" + Contact + "\"," +
									"\"type\": \"template\"," +
									"\"template\": {" +
									"    \"name\": \"variable_template\"," +
									"    \"language\": {" +
									"        \"code\": \"ar\"" +
									"    }," +
									"    \"components\": [" +
									"        {" +
									"            \"type\": \"header\"," +
									"            \"parameters\": [" +
									"                {" +
									"                    \"type\": \"text\"," +
									"                    \"text\": \"" + Title + "\"" +
									"                }" +
									"            ]" +
									"        }," +
									"        {" +
									"            \"type\": \"body\"," +
									"            \"parameters\": [" +
									"                {" +
									"                    \"type\": \"text\"," +
									"                    \"text\": \"" + Description + "\"" +
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
									return Json(new { success = true });
								}
								catch (WebException ex)
								{
									// Handle any exceptions here
									Console.WriteLine(ex.Message);
									return Json(new { success = false });
								}
							}
							catch (Exception ex)
							{

							}
						}
					}
					else if (model.WhatsappType == "Customer")
					{
						var ds = emailSendRepo.GetCustomerContactByID(model.Customers);
						var lstcustomer = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[0])).ToObject<List<Customer>>();
						foreach (var item in lstcustomer)
						{
							try
							{
								var Contact = item.Mobile;
								var Title = model.Title1;
								var Description = model.WhatsappDescription;

								string apiUrl = "https://graph.facebook.com/v16.0/104897916024556/messages";
								string accessToken = "Bearer EAAXW0hLwIIwBOzNXZCdaaeqg7fdsBZB8IWeb7Dl2hh5qWBPvZCxpyC0lbKEJvafy6gKF3dvep9nhzHxukBooe5GZBg6p6BNZB6BAlZBuJK7TMsu4kpMNWQcjhYZBS7DcuANJcZA7F9XiHZAwngMdFWKYSuk8g0BbA7qpgsZAUu9y0Unz2RceZAaUfUmUOJfKFrSpRKAJ9ZCMZASP54AxO7aZAH";

								string jsonBody = "{" +
									"\"messaging_product\": \"whatsapp\"," +
									"\"recipient_type\": \"individual\"," +
									"\"to\": \"" + Contact + "\"," +
									"\"type\": \"template\"," +
									"\"template\": {" +
									"    \"name\": \"variable_template\"," +
									"    \"language\": {" +
									"        \"code\": \"ar\"" +
									"    }," +
									"    \"components\": [" +
									"        {" +
									"            \"type\": \"header\"," +
									"            \"parameters\": [" +
									"                {" +
									"                    \"type\": \"text\"," +
									"                    \"text\": \"" + Title + "\"" +
									"                }" +
									"            ]" +
									"        }," +
									"        {" +
									"            \"type\": \"body\"," +
									"            \"parameters\": [" +
									"                {" +
									"                    \"type\": \"text\"," +
									"                    \"text\": \"" + Description + "\"" +
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
									return Json(new { success = true });
								}
								catch (WebException ex)
								{
									// Handle any exceptions here
									Console.WriteLine(ex.Message);
									return Json(new { success = false });
								}

							}
							catch (Exception ex)
							{

							}
						}
					}
				}
				else if (model.Type == "PushNotification")
				{
					try
					{
						var ds = emailSendRepo.GetTokens(model.CustomerIDs);
						var lsttoken = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[0])).ToObject<List<PushTokenBLL>>();

						foreach (var item in lsttoken)
						{
							var token = new PushNoticationBLL();
							token.Title = model.Title2;
							token.Message = model.PushDescription;
							token.DeviceID = item.Token;
							baserepo.PushNotificationAndroid(token);

						}
						return Json(new { success = true });
					}
					catch (Exception ex)
					{
						return Json(new { success = false });
					}
				}
			}
			catch (Exception ex)
			{
			}
			return Json(new { success = true });
		}
		public void SendEmail(string Subject, string BodyEmail, string To)
		{
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(To);
                mail.From = new MailAddress(ConfigurationManager.AppSettings["From"].ToString());
                mail.Subject = Subject;
                mail.Body = BodyEmail;
                mail.IsBodyHtml = true;
                SmtpClient smtp1 = new SmtpClient("mail.alfalahchemical.com", 587);
                smtp1.EnableSsl = true;
                smtp1.UseDefaultCredentials = false;
                smtp1.Credentials = new System.Net.NetworkCredential("support@garage.sa", "gwsjtdptdehujbza");
                smtp1.Send(mail);
            }
            catch (Exception ex)
            {

            }
        }
	}
}
