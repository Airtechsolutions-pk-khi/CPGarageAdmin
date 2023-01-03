using DAL.DBEntities;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Repositories
{
    public class dashboardRepository : BaseRepository
    {
        public dashboardRepository()
            : base()
        {
            DBContext = new Garage_LiveEntities();
        }

        public dashboardRepository(Garage_LiveEntities contextDB)
            : base(contextDB)
        {
            DBContext = contextDB;
        }

        public DasboardViewModel GetDashboard()
        {
            try
            {
                DasboardViewModel obj = new DasboardViewModel();
                obj.TotalCustomers = DBContext.Users.Where(x => x.StatusID == 1).Count().ToString();
                obj.TotalLocations = DBContext.Locations.Where(x => x.StatusID == 1).Count().ToString();
                obj.TotalSubusers = DBContext.SubUsers.Where(x => x.StatusID == 1).Count().ToString();
                obj.TotalTransactions = DBContext.OrderCheckouts.Where(x => x.OrderStatus == 103).Count().ToString();
                obj.TotalCars = DBContext.Cars.Where(x => x.StatusID == 1).Count().ToString();
                obj.TotalProducts = DBContext.Items.Where(x => x.StatusID == 1).Count().ToString();
                return obj;
            }
            catch (Exception ex)
            {
                //_baseRepo.ErrorLog(ex, "loginRepository/GetLoginInfo", "Exception");
                return new DasboardViewModel();
            }
        }


    }
}
