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
using System.Web.Mvc;

namespace BAL.Repositories
{
    public class carsRepository : BaseRepository
    {
        private DataTable _dt;
        private DataSet _ds;
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
        public CarsPaginationResult GetCars(JqueryDatatableParam param)
        {
            try
            {
                CarsPaginationResult result = new CarsPaginationResult();
                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("@start", param.iDisplayStart);
                p[1] = new SqlParameter("@end", param.iDisplayLength);
                p[2] = new SqlParameter("@searchTerm", param.sSearch);
                _ds = (new DBHelperGarageUAT().GetDatasetFromSP)("sp_GetCars_CP", p);
                if (_ds != null && _ds.Tables.Count > 0)
                {
                    if (_ds.Tables[0] != null)
                    {
                        result.Cars = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_ds.Tables[0])).ToObject<List<CarsViewModel>>();
                    }
                    if (_ds.Tables[1] != null && _ds.Tables[1].Rows.Count > 0)
                    {
                        result.TotalRecords = Convert.ToInt32(_ds.Tables[1].Rows[0][0]);
                    }
                }
                return result;
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
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[17];

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
