using System.Configuration;
using DAL.DBEntities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using DAL.Models;
using System.Web.Script.Serialization;

namespace BAL.Repositories
{
    public class BaseRepository : IDisposable
    {

        StreamWriter _sw;
        public Garage_LiveEntities DBContext;
        public BaseRepository()
        {
            DBContext = new Garage_LiveEntities();
        }

        public BaseRepository(Garage_LiveEntities ContextDB)
        {
            DBContext = ContextDB;
        }

        public void SaveChanges()
        {
            DBContext.SaveChanges();
        }

        #region IDisposable Support

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (DBContext != null)
                {
                    DBContext.Dispose();
                    DBContext = null;

                }
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        ~BaseRepository()
        {
            Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        public void ErrorLog(Exception e, string FnName, string FileName)
        {
            try
            {


                //ErrorLog1 Log = new ErrorLog1();
                //Log.Errorin = FnName + " : " + e.InnerException;
                //Log.ErrorMessage = e.Message;
                //Log.CreatedDate = DateTime.UtcNow;
                ////Log.UserID = 2;
                ////Log.CreatedBy= 2;
                //DBContext.ErrorLogs1.Attach(Log);
                //DBContext.ErrorLogs1.Add(Log);
                //DBContext.SaveChanges();
                //function
                //LogWrite(Log.ErrorMessage, FileName);
            }
            catch
            {
            }
        }
        public void ErrorLogV2(string msg, string FnName, string FileName)
        {
            try
            {
                //function
                LogWrite(msg, FileName);
            }
            catch
            {
            }
        }
        public void LogWrite(string msg, string fileName)
        {
            var logPath = "";
                //ConfigurationManager.AppSettings["LogPath"];
            _sw = new StreamWriter(@logPath + fileName + DateTime.UtcNow.ToString("yyyyMMdd") + ".txt", true);

            _sw.WriteLine(DateTime.UtcNow.ToLongTimeString() + " " + msg);
            _sw.Close();
        }

        public string CurrentDate(SessionInfo session)
        {
            #region timmings

            DateTime t1 = DateTime.UtcNow.AddMinutes(session.UTC);
            DateTime t2 = Convert.ToDateTime(session.OpenTime.ToString());
            DateTime t3 = Convert.ToDateTime(session.CloseTime.ToString());

            string startday;

            TimeSpan diff = t3 - t2;

            DateTime NewDate = t2.AddHours(diff.Hours <= 0 ? (24 - (-diff.Hours)) : diff.Hours);

            if (t3.Date != NewDate.Date)
            {
                int b = DateTime.Compare(t1, t3);

                if (b == 1)
                {
                    startday = DateTime.Today.ToString("yyyy-MM-dd hh:mm:ss");
                }
                else
                {
                    startday = DateTime.Today.AddDays(-1).ToString();
                }
            }
            else
            {
                startday = DateTime.Today.ToString("yyyy-MM-dd hh:mm:ss");
            }
            return startday;
            #endregion timmings
        }

        
        public string DateFormat(string Date)
        {
            if (Date != "")
                return Convert.ToDateTime(Date).ToString("yyyy-MM-dd hh:mm:ss");
            else
                return "";
        }
		public void PushNotificationAndroid(PushNoticationBLL obj)
		{
			try
			{
				var applicationID = "AAAAxl51rZs:APA91bHiGrGKga3ZYLPrzQmzYClRynk3448-mKPg-3IL8q6RJ3fE35OeV4yM2ohv6wjbsfe6LyolpwMD4kq1sp_jc7Swybi0f7jRshFdJj_5-DItwg9zGRpXK1JImoStU3mAO25CXZNG";
				var senderId = "851988295067";
				string deviceId = obj.DeviceID;
				WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
				tRequest.Method = "post";
				tRequest.ContentType = "application/json";
				var data = new
				{
					to = deviceId,
					notification = new
					{
						body = obj.Message,
						title = obj.Title,
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
