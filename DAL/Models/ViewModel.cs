using DAL.DBEntities;
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
		public int? PackageInfoID { get; set; }
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
		public Nullable<System.DateTime> ExpiryDate { get; set; }
		public bool StatusID { get; set; }
		public string CompanyCode { get; set; }
		public Nullable<System.DateTime> CreatedDate { get; set; }
		public string States { get; set; }
		public string Zipcode { get; set; }
		public string VATNO { get; set; }
		public Nullable<double> Tax { get; set; }
		public bool IsSMSActivate { get; set; }
		public bool AllowNegativeInventory { get; set; }
		public bool IsOdoo { get; set; }

		public bool IsAccountingAddons { get; set; }
		public int? LocationID { get; set; }
		[Required(ErrorMessage = "Input is required")]
		public string LocationName { get; set; }
		public string LocationAddress { get; set; }
		public string LocationContactNo { get; set; }
		[Required(ErrorMessage = "Input is required")]
		public string LocationEmail { get; set; }
		public string Currency { get; set; }
		public Nullable<System.DateTime> PackageExpiry { get; set; }
	}

	public class RoleGroup
	{
		public int GroupID { get; set; }
		public string GroupName { get; set; }
		public int StatusID { get; set; }
		public int UserID { get; set; }
		public Nullable<DateTime> LastUpdatedDate { get; set; }
		public string LastUpdatedBy { get; set; }
	}
	public class RoleGroupForms
	{
		public int GroupFormID { get; set; }
		public int GroupID { get; set; }
		public int FormID { get; set; }
		public bool New { get; set; }
		public bool Edit { get; set; }
		public bool Remove { get; set; }
		public bool Access { get; set; }
		public int StatusID { get; set; }
	}
	#endregion customer


	public class DasboardViewModel
	{
		public string TotalCustomers { get; set; }
		public string TotalTransactions { get; set; }
		public string TotalLocations { get; set; }
		public string TotalSubusers { get; set; }
		public string TotalCars { get; set; }
		public string TotalProducts { get; set; }
		public string TotalTrialCustomer { get; set; }
		public string TotalProfessionalCustomer { get; set; }

	}
	public class SmsBilling
	{
		public int SmsBillingID { get; set; }
		public string CompanyName { get; set; }
		public Nullable<int> SMSCount { get; set; }
		public string FromDate { get; set; }
		public string ToDate { get; set; }
		public Nullable<double> Rate { get; set; }
		public Nullable<double> Total { get; set; }
		public Nullable<System.DateTime> LastUpdatedDate { get; set; }
		public int UserID { get; set; }
		public Nullable<int> Status { get; set; }

	}
	#region Packages
	public class PackagesInfoViewModel
	{
		public int PackageInfoID { get; set; }
		public string PackageName { get; set; }
		public Nullable<decimal> PackagePrice { get; set; }
		public string PackageDescription { get; set; }
		public string NoofDays { get; set; }
		public Nullable<int> DeviceCount { get; set; }
		public Nullable<int> LocationsLimit { get; set; }
		public bool StatusID { get; set; }
		public bool IsInventory { get; set; }
		public bool IsGarageGo { get; set; }
		public Nullable<System.DateTime> CreatedDate { get; set; }
		public Nullable<System.DateTime> LastUpdatedDate { get; set; }

	}
	public class UserPackageBLL
	{
		public int UserPackageDetailID { get; set; }
		public Nullable<int> PackageInfoID { get; set; }
		public Nullable<int> UserID { get; set; }
		public Nullable<int> StatusID { get; set; }
		public Nullable<System.DateTime> CreatedDate { get; set; }
		public Nullable<System.DateTime> LastUpdatedDate { get; set; }
		public string FirstName { get; set; }
		public string Name { get; set; }
	}
	#endregion
}

