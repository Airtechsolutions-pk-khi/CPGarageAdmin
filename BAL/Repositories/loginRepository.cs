
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Repositories
{
    public class loginRepository 
    {
        public BaseRepository _baseRepo;


        public loginRepository()
            : base()
        {
            _baseRepo = new BaseRepository();
            //_baseRepo.DBContext = new Garage_LiveEntities();

        }

        //public loginRepository(Garage_LiveEntities contextDB)
        //  : base(contextDB)
        //{
        //    _baseRepo.DBContext = contextDB;
        //}

        public login GetLoginInfo(string email,string password)
        {
            try
            {
                login obj = new login();
                obj.UserName = "cpadmin@garage.sa";
                obj.Password = "admin";
                obj.Status = 1;
                obj.UserID = 1;
                    //_baseRepo.DBContext.sp_apiGetSuperUserInfo(CompanyCode).ToList();
                return obj;
            }
            catch (Exception ex)
            {
                _baseRepo.ErrorLog(ex, "loginRepository/GetLoginInfo", "Exception");
                return null;
            }
        }

        
    }
}
