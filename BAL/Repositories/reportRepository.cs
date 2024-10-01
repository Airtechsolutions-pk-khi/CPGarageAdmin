using DAL.DBEntities;
using BAL.Repositories;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Twilio.Rest.Api.V2010.Account;
using Twilio;
using System.Web;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Razor.Tokenizer;
using System.Data;
using WebAPICode.Helpers;
using Newtonsoft.Json.Linq;

namespace BAL.Repositories
{
    public class reportRepository : BaseRepository
    {
        public static summaryViewModel repo;
        public static DataTable _dt;
        public static DataSet _ds;

        public reportRepository()
             : base()
        {
            DBContext = new Garage_LiveEntities();
            repo = new summaryViewModel();
            _dt = new DataTable();
            _ds = new DataSet();
        }

        public reportRepository(Garage_LiveEntities contextDB)
            : base(contextDB)
        {
            DBContext = contextDB;
        }

        public summaryViewModel Get(string fromdate, string todate)
        {
            try
            {
                var _obj = new summaryViewModel();
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@fromdate", fromdate);
                p[1] = new SqlParameter("@todate", todate);

                _dt = (new DBHelperGarageUAT().GetTableFromSP)("sp_GetRptSummary_Admin", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        _obj = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<summaryViewModel>>().FirstOrDefault();
                    }
                }

                return _obj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<leadReportViewModel> GetLead(string fromdate, string todate, string statusFilter)

        {
            try
            {
                var lst = new List<leadReportViewModel>();

                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("@fromdate", fromdate);
                p[1] = new SqlParameter("@todate", todate);
                p[2] = new SqlParameter("@statusFilter", statusFilter);

                _dt = (new DBHelperGarageUAT().GetTableFromSP)("sp_GetCustomerPackageDetail_CP", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<leadReportViewModel>>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<IntegrationStatsViewModel> GetLog(string fromdate, string todate, string statusFilter)
        {
            try
            {
                var lst = new List<IntegrationStatsViewModel>();

                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("@fromdate", fromdate);
                p[1] = new SqlParameter("@todate", todate);
                p[2] = new SqlParameter("@statusFilter", statusFilter);

                _dt = (new DBHelperGarageUAT().GetTableFromSP)("sp_GetDetailLogReport_CP", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<IntegrationStatsViewModel>>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
