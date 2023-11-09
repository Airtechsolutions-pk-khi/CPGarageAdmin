using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace CPGarageAdmin.Controllers
{
    public class baseController : Controller
    {
        // GET: base
        public ActionResult Index()
        {
            return View();
        }
		//var applicationID = "AAAAPqbVywM:APA91bG1NTaDGJKRGJZdoWMtzQBgU86r9_dqTLUWAPbF_giLTvFS8j6yeM9vzF4MQiwsxKGhqn05BNhUIgNkiDFoDkGV5Ev8zBGPTwoxTIE3zJ3SD6uf2FHIjNrPCmnJFyVhy-vXMZ8d";

		//var senderId = "269087001347";

		[HttpGet]
		[Route("api/push/android")]
		public void PushNotification()

		{
			try
			{
				var applicationID = "AAAAPqbVywM:APA91bG1NTaDGJKRGJZdoWMtzQBgU86r9_dqTLUWAPbF_giLTvFS8j6yeM9vzF4MQiwsxKGhqn05BNhUIgNkiDFoDkGV5Ev8zBGPTwoxTIE3zJ3SD6uf2FHIjNrPCmnJFyVhy-vXMZ8d";
				var senderId = "269087001347";
				string deviceId = "cfu1sCzaTQiG4xK7MVgn98:APA91bG-sfNevqv_XlQeNrSfdJ0jWVv9BXCeTE6EDmmo_PBkl8xLqshLY9KxeIbrIH-yAexVok9OO4hVQzGR0n0-2Q3fFoT0ZsUJ5SsPHhS-xWoHGXMrnOYg5q0QY0x8C9cIU7_xQP17";
				WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
				tRequest.Method = "post";
				tRequest.ContentType = "application/json";
				var data = new
				{
					to = deviceId,
					notification = new
					{
						body = "test",
						title = "test",
						icon = "myicon",
						sound = "default"

					}
				};
				var serializer = new JavaScriptSerializer();
				var json = serializer.Serialize(data);
				Byte[] byteArray = Encoding.UTF8.GetBytes(json);
				tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
				tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
				tRequest.ContentLength = byteArray.Length;
				using (Stream dataStream = tRequest.GetRequestStream())
				{
					dataStream.Write(byteArray, 0, byteArray.Length);
					using (WebResponse tResponse = tRequest.GetResponse())
					{
						using (Stream dataStreamResponse = tResponse.GetResponseStream())
						{
							using (StreamReader tReader = new StreamReader(dataStreamResponse))
							{
								String sResponseFromServer = tReader.ReadToEnd();
								string str = sResponseFromServer;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				string str = ex.Message;
			}
		}
	}
}