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
    public class carDeleteRepository : BaseRepository
    {
        private DataTable _dt;
        public carDeleteRepository()
             : base()
        {
            DBContext = new Garage_LiveEntities();
        }
        public carDeleteRepository(Garage_LiveEntities contextDB)
            : base(contextDB)
        {
            DBContext = contextDB;
        }
        public List<CarDeleteViewModel> GetAll()
        {
            try
            {
                var lst = new List<CarDeleteViewModel>();

                SqlParameter[] p = new SqlParameter[0];

                _dt = (new DBHelperGarageUAT().GetTableFromSP)("sp_GetCarDeleteRequest_CP", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<CarDeleteViewModel>>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public CarDeleteViewModel GetCarByID(int? id)
        {
            try
            {
                var _obj = new CarDeleteViewModel();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@id", id);

                _dt = (new DBHelperGarageUAT().GetTableFromSP)("sp_GetDeleteCarsbyID_Admin", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        _obj = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<CarDeleteViewModel>>().FirstOrDefault();
                    }
                }

                return _obj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int edit(CarDeleteViewModel modal)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[4];
                p[0] = new SqlParameter("@RequestStatus", modal.RequestStatus);
                p[1] = new SqlParameter("@UpdatedDate", modal.LastUpdatedDate);
                p[2] = new SqlParameter("@UpdatedBy", "Admin");
                p[3] = new SqlParameter("@RequestID", modal.RequestID);

                rtn = (new DBHelperGarageUAT().ExecuteNonQueryReturn)("sp_UpdateDeleteCarStatus_CP", p);
                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
