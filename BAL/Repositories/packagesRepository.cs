
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
    public class packagesRepository : BaseRepository
    {
        public static PackagesInfoViewModel repo;
        public static DataTable _dt;
        public static DataSet _ds;

        public packagesRepository()
             : base()
        {
            DBContext = new Garage_LiveEntities();
            repo = new PackagesInfoViewModel();
            _dt = new DataTable();
            _ds = new DataSet();
        }

        public packagesRepository(Garage_LiveEntities contextDB)
            : base(contextDB)
        {
            DBContext = contextDB;
            _dt = new DataTable();
            _ds = new DataSet();
        }
        public List<PackagesInfoViewModel> GetAll()
        {
            try
            {
                var lst = new List<PackagesInfoViewModel>();

                SqlParameter[] p = new SqlParameter[0];

                _dt = (new DBHelperGarageUAT().GetTableFromSP)("sp_GetPackageInfo_UAT", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<PackagesInfoViewModel>>();
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public PackagesInfoViewModel Get(int? id)
        {
            try
            {
                var _obj = new PackagesInfoViewModel();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@id", id);

                _dt = (new DBHelperGarageUAT().GetTableFromSP)("sp_GetPackagebyID_Admin", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        _obj = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<PackagesInfoViewModel>>().FirstOrDefault();
                    }
                }

                return _obj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int Insert(PackagesInfoViewModel data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[10];

                p[0] = new SqlParameter("@PackageName", data.PackageName);
                p[1] = new SqlParameter("@PackageDescription", data.PackageDescription);
                p[2] = new SqlParameter("@PackagePrice", data.PackagePrice);
                p[3] = new SqlParameter("@DeviceCount", data.DeviceCount);
                p[4] = new SqlParameter("@LocationsLimit", data.LocationsLimit);
                p[5] = new SqlParameter("@IsInventory", data.IsInventory);
                p[6] = new SqlParameter("@IsGarageGo", data.IsGarageGo);
                p[7] = new SqlParameter("@CreatedDate", data.CreatedDate);
                p[8] = new SqlParameter("@StatusID", data.StatusID);
                p[9] = new SqlParameter("@NoofDays", data.NoofDays);
                rtn = (new DBHelperGarageUAT().ExecuteNonQueryReturn)("sp_insertPackagesInfo_Admin", p);

                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public int Update(PackagesInfoViewModel data)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[11];

                p[0] = new SqlParameter("@PackageName", data.PackageName);
                p[1] = new SqlParameter("@PackageDescription", data.PackageDescription);
                p[2] = new SqlParameter("@PackagePrice", data.PackagePrice);
                p[3] = new SqlParameter("@DeviceCount", data.DeviceCount);
                p[4] = new SqlParameter("@LocationsLimit", data.LocationsLimit);
                p[5] = new SqlParameter("@IsInventory", data.IsInventory);
                p[6] = new SqlParameter("@IsGarageGo", data.IsGarageGo);
                p[7] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);
                p[8] = new SqlParameter("@StatusID", data.StatusID);
                p[9] = new SqlParameter("@PackageInfoID", data.PackageInfoID);
                p[10] = new SqlParameter("@NoofDays", data.NoofDays);
                rtn = (new DBHelperGarageUAT().ExecuteNonQueryReturn)("sp_UpdatePackagesInfo_Admin", p);

                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Delete(PackagesInfoViewModel data)
        {
            try
            {
                int _obj = 0;
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@id", data.PackageInfoID);
                p[1] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);

                _obj = (new DBHelperGarageUAT().ExecuteNonQueryReturn)("sp_DeletePackage", p);

                return _obj;
            }
            catch (Exception)
            {
                return 0;
            }
        }

    }
}
