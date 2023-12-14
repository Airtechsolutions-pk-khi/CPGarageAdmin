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
using System.Reflection;

namespace BAL.Repositories
{
	public class carsRepository : BaseRepository
	{
		private DataTable _dt;
		public carsRepository()
			 : base()
		{
			DBContext = new Garage_LiveEntities();
		}
		public carsRepository(Garage_LiveEntities contextDB)
			: base(contextDB)
		{
			DBContext = contextDB;
		}
		//public List<CarsViewModel> GetCars(int skip, int take)
		//{
		//	try
		//	{
		//		var lst = new List<CarsViewModel>();

		//		// Modify your data access logic to include the skip and take parameters
		//		var parameters = new List<SqlParameter>
		//{
		//	new SqlParameter("@Skip", skip),
		//	new SqlParameter("@Take", take)
		//};

		//		_dt = (new DBHelperGarageUAT().GetTableFromSP)("sp_GetCars_CP", parameters.ToArray());

		//		if (_dt != null && _dt.Rows.Count > 0)
		//		{
		//			// Convert DataTable to a list of CarsViewModel
		//			lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<CarsViewModel>>();
		//		}

		//		return lst;
		//	}
		//	catch (Exception ex)
		//	{
		//		// Handle exceptions appropriately (throw, log, etc.)
		//		throw new Exception("Error fetching paginated cars.", ex);
		//	}
		//}


		public List<CarsViewModel> GetCars(JqueryDatatableParam param)
		{
			try
			{
				var lst = new List<CarsViewModel>();
				SqlParameter[] p = new SqlParameter[2];
				if(param.iDisplayStart == 0)
				{
					param.iDisplayStart = 1;
				}
				p[0] = new SqlParameter("@start", param.iDisplayStart);
				p[1] = new SqlParameter("@end", param.iDisplayLength);
				_dt = (new DBHelperGarageUAT().GetTableFromSP)("sp_GetCars_CP",p);
				if (_dt != null)
				{
					if (_dt.Rows.Count > 0)
					{
						lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<CarsViewModel>>();
					}
				}

				return lst;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
		public List<CustomerViewModel> GetCustomer()
		{
			try
			{
				var lst = new List<CustomerViewModel>();

				_dt = (new DBHelperGarageUAT().GetTableFromSP)("sp_GetCustomersForCars_CP");
				if (_dt != null)
				{
					if (_dt.Rows.Count > 0)
					{
						lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<CustomerViewModel>>();
					}
				}

				return lst;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
		//public List<CarsViewModel> GetPaginatedCars(int skip, int take)
		//{
		//	try
		//	{
		//		// Your existing logic for fetching all data
		//		var allData = GetCars();

		//		// Paginate the data using Skip and Take
		//		var paginatedData = allData.Skip(skip).Take(take).ToList();

		//		return paginatedData;
		//	}
		//	catch (Exception ex)
		//	{
		//		// Handle exceptions appropriately
		//		return null;
		//	}
		//}
		public List<CarsViewModel> GetPaginatedCars(int skip, int take)
		{
			try
			{
				var lst = new List<CarsViewModel>();

				// Modify your data access logic to call the stored procedure with pagination parameters
				var parameters = new List<SqlParameter>
		{
			new SqlParameter("@Skip", skip),
			new SqlParameter("@Take", take)
            // Add other parameters as needed
        };

				_dt = (new DBHelperGarageUAT().GetTableFromSP)("sp_GetCars_CP", parameters.ToArray());
				_dt = (new DBHelperGarageUAT().GetTableFromSP)("sp_GetCars_CP", parameters.ToArray());

				if (_dt != null && _dt.Rows.Count > 0)
				{
					// Convert DataTable to a list of CarsViewModel
					lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<CarsViewModel>>();
				}

				return lst;
			}
			catch (Exception ex)
			{
				// Handle exceptions appropriately
				return null;
			}
		}

		public List<MakeViewModel> GetMake()
		{
			try
			{
				var lst = new List<MakeViewModel>();

				_dt = (new DBHelperGarageUAT().GetTableFromSP)("sp_GetMakeforCars_CP");
				if (_dt != null)
				{
					if (_dt.Rows.Count > 0)
					{
						lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<MakeViewModel>>();
					}
				}

				return lst;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
		public List<ModelsViewModel> GetModel(int? MakeID)
		{
			try
			{
				var lst = new List<ModelsViewModel>();
				SqlParameter[] p = new SqlParameter[1];
				p[0] = new SqlParameter("@id", MakeID);
				_dt = (new DBHelperGarageUAT().GetTableFromSP)("sp_GetModelforCars_CP", p);
				if (_dt != null)
				{
					if (_dt.Rows.Count > 0)
					{
						lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<ModelsViewModel>>();
					}
				}

				return lst;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
		public List<CartypeViewModel> GetCarType()
		{
			try
			{
				var lst = new List<CartypeViewModel>();

				_dt = (new DBHelperGarageUAT().GetTableFromSP)("sp_GetCarType_CP");
				if (_dt != null)
				{
					if (_dt.Rows.Count > 0)
					{
						lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<CartypeViewModel>>();
					}
				}
				return lst;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
		public CarsViewModel GetCarByID(int? id)
		{
			try
			{
				var _obj = new CarsViewModel();
				SqlParameter[] p = new SqlParameter[1];
				p[0] = new SqlParameter("@id", id);

				_dt = (new DBHelperGarageUAT().GetTableFromSP)("sp_GetCarsbyID_Admin", p);
				if (_dt != null)
				{
					if (_dt.Rows.Count > 0)
					{
						_obj = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<CarsViewModel>>().FirstOrDefault();
					}
				}

				return _obj;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
		public int edit(CarsViewModel modal)
		{
			try
			{
				var _obj = new CustomerViewModel();
				SqlParameter[] a = new SqlParameter[1];
				a[0] = new SqlParameter("@Mobile", modal.CustomerPhone);
				_dt = (new DBHelperGarageUAT().GetTableFromSP)("sp_GetCustomerByPhone_CADMIN", a);
				if (_dt != null)
				{
					if (_dt.Rows.Count > 0)
					{
						_obj = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<CustomerViewModel>>().FirstOrDefault();
					}
				}
				int rtn = 0;
				SqlParameter[] p = new SqlParameter[17];

				p[0] = new SqlParameter("@CustomerID", _obj.CustomerID);
				p[1] = new SqlParameter("@MakeID", modal.MakeID);
				p[2] = new SqlParameter("@Name", modal.Name);
				p[3] = new SqlParameter("@ModelID", modal.ModelID);
				p[4] = new SqlParameter("@Description", modal.Description);
				p[5] = new SqlParameter("@Year", modal.Year);
				p[6] = new SqlParameter("@RegistrationNo", modal.RegistrationNo);
				p[7] = new SqlParameter("@ImagePath", modal.ImagePath);
				p[8] = new SqlParameter("@StatusID", modal.StatusID);
				p[9] = new SqlParameter("@Color", modal.Color);
				p[10] = new SqlParameter("@RecommendedAmount", modal.RecommendedAmount);
				p[11] = new SqlParameter("@CheckLitres", modal.CheckLitre);
				p[12] = new SqlParameter("@CarType", modal.CarType);
				p[13] = new SqlParameter("@EngineType", modal.EngineType);
				p[14] = new SqlParameter("@CarID", modal.CarID);
				p[15] = new SqlParameter("@LastUpdateDate", modal.LastUpdateDate);
				p[16] = new SqlParameter("@LastUpdateBy", "Admin");

				rtn = (new DBHelperGarageUAT().ExecuteNonQueryReturn)("sp_UpdateCars_CP", p);

				return rtn;
			}
			catch (Exception ex)
			{
				return 0;
			}
		}
		public int add(CarsViewModel modal)
		{
			try
			{
				int rtn = 0;
				SqlParameter[] p = new SqlParameter[16];

				p[0] = new SqlParameter("@CustomerID", modal.CustomerID);
				p[1] = new SqlParameter("@MakeID", modal.MakeID);
				p[2] = new SqlParameter("@Name", modal.Name);
				p[3] = new SqlParameter("@ModelID", modal.ModelID);
				p[4] = new SqlParameter("@Description", modal.Description);
				p[5] = new SqlParameter("@Year", modal.Year);
				p[6] = new SqlParameter("@RegistrationNo", modal.RegistrationNo);
				p[7] = new SqlParameter("@ImagePath", modal.ImagePath);
				p[8] = new SqlParameter("@StatusID", modal.StatusID);
				p[9] = new SqlParameter("@Color", modal.Color);
				p[10] = new SqlParameter("@RecommendedAmount", modal.RecommendedAmount);
				p[11] = new SqlParameter("@CheckLitres", modal.CheckLitre);
				p[12] = new SqlParameter("@CarType", modal.CarType);
				p[13] = new SqlParameter("@EngineType", modal.EngineType);
				p[14] = new SqlParameter("@CreatedOn", modal.CreatedOn);
				p[15] = new SqlParameter("@CreatedBy", "Admin");

				rtn = (new DBHelperGarageUAT().ExecuteNonQueryReturn)("sp_InsertCars_CP", p);

				return rtn;
			}
			catch (Exception ex)
			{
				return 0;
			}
		}
		public int Delete(int id)
		{
			try
			{
				int rtn = 0;
				SqlParameter[] p = new SqlParameter[4];

				p[0] = new SqlParameter("@StatusID", 3);
				p[1] = new SqlParameter("@CarID", id);
				p[2] = new SqlParameter("@LastUpdateDate", DateTime.Now);
				p[3] = new SqlParameter("@LastUpdateBy", "Admin");

				rtn = (new DBHelperGarageUAT().ExecuteNonQueryReturn)("sp_DeleteCar_CP", p);
				return rtn;
			}
			catch (Exception ex)
			{
				return 0;
			}
		}
	}
}
