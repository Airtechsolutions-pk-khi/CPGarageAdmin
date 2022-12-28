using DAL.DBEntities;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Repositories
{
    public class countryRepository : BaseRepository
    {
        public countryRepository()
            : base()
        {
            DBContext = new Garage_LiveEntities();
        }

        public countryRepository(Garage_LiveEntities contextDB)
            : base(contextDB)
        {
            DBContext = contextDB;
        }

        //public IEnumerable<CountryList> GetCountries()
        //{
        //    try
        //    {
        //        var modal = DBContext.Countries.OrderBy(x => x.Name)
        //         .AsEnumerable().Select(x => new CountryList
        //         {
        //             Code = x.Code,
        //             Name = x.Name,
        //             Continent = x.Continent,
        //             Region = x.Region,
        //             SurfaceArea = x.SurfaceArea,
        //             IndepYear = x.IndepYear,
        //             LifeExpectancy = x.LifeExpectancy,
        //             GNP = x.GNP,
        //             GNPOld = x.GNPOld,
        //             LocalName = x.LocalName,
        //             GovernmentForm = x.GovernmentForm,
        //             HeadOfState = x.HeadOfState,
        //             Capital = x.Capital,
        //             PhonePrefix = x.PhonePrefix,
        //             Code2 = x.Code2,
        //             Currency = x.Currency,
        //             Curr_Code = x.Curr_Code,
        //             Population = x.Population
        //         }).ToList();
        //        return modal;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLog(ex, "CountryRepository/GetCountries", "Exception");
        //        return null;
        //    }
        //}


        //public IEnumerable<CitiesList> GetCities(string Code)
        //{           
        //    try
        //    {
        //        var modal = DBContext.Cities.OrderBy(x => x.Name)
        //            .Where(x=>x.CountryCode==Code)
        //        .AsEnumerable().Select(x => new CitiesList
        //        {
        //            Name = x.Name,
        //            ID = x.ID,
        //            CountryCode = x.CountryCode,
        //            District = x.District,
        //            Population = x.Population
        //        }).ToList();

        //        return modal;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLog(ex, "CountryRepository/GetCities", "Exception");
        //        return null;
        //    }
        //}
        //public IEnumerable<Country> CountryByCodeGet(string Country_Code)
        //{
        //    var country_List = (from temp in DBContext.Countries
        //                        where temp.Code == Country_Code
        //                        select temp).ToList();

        //    return country_List;
        //}

        //public IEnumerable<City> CityByCodeGet(string id)
        //{
        //    var city_List = (from temp in DBContext.Cities
        //                     where temp.CountryCode == id
        //                     select temp).ToList();

        //    return city_List;
        //}
    }
}
