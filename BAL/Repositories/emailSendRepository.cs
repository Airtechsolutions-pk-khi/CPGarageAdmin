
using DAL.DBEntities;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Twilio.Rest.Api.V2010.Account;
using Twilio;
using System.Web;
using System.IO;
using System.Configuration;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using System.Data;
using Twilio.TwiML.Messaging;
using WebAPICode.Helpers;
using System.Web.Razor.Tokenizer;

namespace BAL.Repositories
{
	public class emailSendRepository : BaseRepository
	{
		private DataTable _dt;
		private DataSet _ds;

		public emailSendRepository(Garage_LiveEntities garage_LiveEntities)
			 : base()
		{
			DBContext = new Garage_LiveEntities();
		}
		public List<User> GetCustomers()
		{
			try
			{
				var data = DBContext.Users.Where(x => x.StatusID == 1).ToList();
				return data;
			}
			catch (Exception ex)
			{
				return new List<User>();
			}
		}
		public DataSet GetTokens(string CustomerIDs)
		 {
			try
			{
				var _obj = new PushTokenBLL();
				SqlParameter[] p = new SqlParameter[1];
				p[0] = new SqlParameter("@ids", CustomerIDs);
				_ds = (new DBHelperGarageUAT().GetDatasetFromSP)("sp_GetToken_CP", p);
				return _ds;
			}
			catch (Exception)
			{
				return null;
			}
		}
		public List<Customer> GetCustomerNotification()
		{
			try
			{
				var lst = new List<Customer>();

				SqlParameter[] p = new SqlParameter[0];

				_dt = (new DBHelperGarageUAT().GetTableFromSP)("sp_GetCustomerNotification_CP", p);
				if (_dt != null)
				{
					if (_dt.Rows.Count > 0)
					{
						lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<Customer>>();
					}
				}
				return lst;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
		public List<User> GetCustomerEmailByID(int id)
		{
			try
			{
				var data = DBContext.Users.Where(x => x.UserID == id).ToList();
				return data;
			}
			catch (Exception ex)
			{
				return new List<User>();
			}
		}
		public DataSet GetCustomerContactByID(string Customers)
		{
			try
			{
				var _obj = new User();
				SqlParameter[] p = new SqlParameter[1];
				p[0] = new SqlParameter("@ids", Customers);
				_ds = (new DBHelperGarageUAT().GetDatasetFromSP)("sp_GetCustomerContact_CP", p);
				return _ds;
			}
			catch (Exception)
			{
				return null;
			}
		}
		public DataSet GetUserContactByID(string Users)
		{
			try
			{
				var _obj = new User();
				SqlParameter[] p = new SqlParameter[1];
				p[0] = new SqlParameter("@ids", Users);
				_ds = (new DBHelperGarageUAT().GetDatasetFromSP)("sp_GetUserContact_CP", p);
				return _ds;
			}
			catch (Exception)
			{
				return null;
			}
		}
	}
}
