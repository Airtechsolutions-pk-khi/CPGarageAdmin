using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace DAL.Models
{
    public class ViewModel
    {
    }
    #region login

    public class login
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Nullable<int> Status { get; set; }
    }
    #endregion login
    public class MakeViewModel
    {
        public int MakeID { get; set; }
        public Nullable<int> RowID { get; set; }
        [Required(ErrorMessage = "Input is required")]
        public string Name { get; set; }
        public string ArabicName { get; set; }
        public string ImagePath { get; set; }
        public string LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
        public Nullable<int> StatusID { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        
    }
    public class ModelsViewModel
    {
        public int ModelID { get; set; }
        public Nullable<int> RowID { get; set; }
        public int MakeID { get; set; }
        [Required(ErrorMessage = "Input is required")]
        public string Name { get; set; }
        public string ArabicName { get; set; }
        [Required(ErrorMessage = "Input is required")]
        public Nullable<short> Year { get; set; }
        [Required(ErrorMessage = "Input is required")]
        public string EngineNo { get; set; }
        public string RecommendedLitres { get; set; }
        public string ImagePath { get; set; }
        public string LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
        public Nullable<int> StatusID { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public List<SelectListItem> Makes { get; set; }
    }
    #region customer
    public class CustomerViewModel
    {
        public int UserID { get; set; }
        public int RowID { get; set; }
        public string UserName { get; set; }
        [Required(ErrorMessage = "Input is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Input is required")]
        public string LastName { get; set; }
        public string ImagePath { get; set; }
        [Required(ErrorMessage = "Input is required")]
        public string Password { get; set; }
        public string Company { get; set; }
        public string BusinessType { get; set; }
        [Required(ErrorMessage = "Input is required")]
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public Nullable<int> CityID { get; set; }
        public string CountryID { get; set; }
        public string Website { get; set; }
        public Nullable<bool> Subscribe { get; set; }
        public Nullable<int> RoleID { get; set; }
        public Nullable<int> TimeZoneID { get; set; }
        public string LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
        public Nullable<int> StatusID { get; set; }
        public string CompanyCode { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string States { get; set; }
        public string Zipcode { get; set; }
        public string VATNO { get; set; }
        public Nullable<double> Tax { get; set; }
        public bool IsSMSActivate { get; set; }

        public int LocationID { get; set; }
        [Required(ErrorMessage = "Input is required")]
        public string LocationName { get; set; }
        public string LocationAddress { get; set; }
        public string LocationContactNo { get; set; }
        [Required(ErrorMessage = "Input is required")]
        public string LocationEmail { get; set; }

    }
    #endregion customer


    public class DasboardViewModel
    {
        public string TotalCustomers { get; set; }
        public string TotalTransactions { get; set; }
        public string TotalLocations { get; set; }
        public string TotalSubusers { get; set; }
    }
    public  class SmsBilling
    {
        public int SmsBillingID { get; set; }
        public string CompanyName { get; set; }
        public Nullable<int> SMSCount { get; set; }
        public string FromDate { get; set; }
        public string ToDate{ get; set; }
        public Nullable<double> Rate { get; set; }
        public Nullable<double> Total { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
        public int UserID { get; set; }
        public Nullable<int> Status { get; set; }
        
    }
}
