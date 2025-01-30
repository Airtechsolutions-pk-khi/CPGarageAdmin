using DAL.DBEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Parser.SyntaxTree;


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
		public int CustomerID { get; set; }
		public int? UserID { get; set; }
		public int? PackageInfoID { get; set; }
		public int RowID { get; set; }
		public string FullName { get; set; }
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
		public int? CityID { get; set; }
        public int? ID { get; set; }
        public string CountryID { get; set; }
		public string Website { get; set; }
		public Nullable<bool> Subscribe { get; set; }
		public Nullable<int> RoleID { get; set; }
		public Nullable<int> TimeZoneID { get; set; }
		public string LastUpdatedBy { get; set; }
		public string BusinessTypes { get; set; }
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
		public bool IsGarageGo { get; set; }
		public bool IsCashier { get; set; }
		public bool AllowNegativeInventory { get; set; }
		public bool IsOdoo { get; set; }
		public bool IsAccountingAddons { get; set; }
		public bool IsYakeen { get; set; }
		public bool IsMojaz { get; set; }
		public bool IsDefaultCar { get; set; }
		public int? LocationID { get; set; }
		[Required(ErrorMessage = "Input is required")]
		public string LocationName { get; set; }
		public string LocationAddress { get; set; }
		public string LocationContactNo { get; set; }
		[Required(ErrorMessage = "Input is required")]
		public string LocationEmail { get; set; }
		public string Currency { get; set; }
		public string RemainingDays { get; set; }
		public Nullable<System.DateTime> PackageExpiry { get; set; }

		public string SelectedCountry { get; set; } // For the selected value
        public IEnumerable<SelectListItem> CountryList { get; set; } // For the dropdown options
	}

    
    public class CityViewModel
    {
        public int ID { get; set; }
        public int CityID { get; set; }
        public string Name { get; set; }
        public string CountryCode { get; set; }
        public string District { get; set; }        
        public string Population { get; set; }
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
	#region EmailModel
	public class EmailSendViewModel
	{
		//public string[] UserIDs { get; set; }
		public string UserIDs { get; set; } = null;
		public string Users { get; set; } = null;
        public string Customers { get; set; } = null;
        public string CustomerIDs { get; set; } = null;
        public string Title { get; set; } = null;
        public string Title1 { get; set; } = null;
        public string Title2 { get; set; } = null;
        public string Subject { get; set; } = null;
        [AllowHtml]
		public string Descripiton { get; set; } = null;
        public string PushDescription { get; set; } = null;
        [AllowHtml]
		public string WhatsappDescription { get; set; } = null;
        public string Type { get; set; } = null;
        public string WhatsappType { get; set; } = null;
        [NotMapped]
		public IEnumerable<Customer> customerList { get; set; }
	}
	#endregion
	#region cars
	public class CarsViewModel
	{
		public int CarID { get; set; }
		public int? RowID { get; set; }
		public int? CustomerID { get; set; }
		public string CustomerPhone { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string VinNo { get; set; }
		public int? MakeID { get; set; }
		public int? ModelID { get; set; }
		public int? Year { get; set; }
		public string Color { get; set; }
		public string RegistrationNo { get; set; }
		public Nullable<float> CheckLitre { get; set; }
		public string EngineType { get; set; }
		public string RecommendedAmount { get; set; }
		public int StatusID { get; set; }
		public string BinaryImage { get; set; }
		public string LastUpdateBy { get; set; }
		public string LastUpdateDate { get; set; }
		public DateTime? CreatedOn { get; set; }
		public string CreatedBy { get; set; }
		public int? LocationID { get; set; }
		public int? UserID { get; set; }
		public string ImagePath { get; set; }
		public string Gender { get; set; }
		public string CarTypeName { get; set; }
		public string MakeName { get; set; }
		public string ModelName { get; set; }
		public int? CarType { get; set; }

    }
	public class CartypeViewModel
	{
		public int BodyTypeID { get; set; }
		public string Name { get; set; }
		public string ArabicName { get; set; }
		public string Image { get; set; }
		public int StatusID { get; set; }
	}
	public class JqueryDatatableParam
	{
		public string sEcho { get; set; }
		public string sSearch { get; set; }
		public int iDisplayLength { get; set; }
		public int iDisplayStart { get; set; }
		public int iColumns { get; set; }
		public int iSortCol_0 { get; set; }
		public string sSortDir_0 { get; set; }
		public int iSortingCols { get; set; }
		public string sColumns { get; set; }
    }
    public class CarsPaginationResult
    {
        public List<CarsViewModel> Cars { get; set; }
        public int TotalRecords { get; set; }
    }

    #endregion
    public class PushTokenBLL
	{
		public int TokenID { get; set; }
		public string Token { get; set; }
		public int? CustomerID { get; set; }
		public int? StatusID { get; set; }
		public int? DeviceID { get; set; }
	}
	public class PushNoticationBLL
	{
		public string DeviceID { get; set; }
		public string Type { get; set; }
		public string Link { get; set; }
		public string Title { get; set; }
		public string Message { get; set; }
	}
	public class CarDeleteViewModel
	{
		public int RequestID { get; set; }
		public int CarID { get; set; }
		public string CarName { get; set; }
		public int CustomerID { get; set; }
		public string CustomerName { get; set; }
		public string Reason { get; set; }
		public string RequestFrom { get; set; }
		public int RequestStatus { get; set; }
		public DateTime? CreatedDate { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? LastUpdatedDate { get; set; }
		public string LastUpdatedBy { get; set; }
	}

	public class summaryViewModel
	{
		public string TotalTransaction { get; set; }
		public string TotalLocation { get; set; }
		public string TotalCars { get; set; }
		public string TotalCustomers { get; set; }
		public string TotalProfessionalCustomers { get; set; }
		public string TotalTrialCustomers { get; set; }
	}
	public class leadReportViewModel
    {
		public int UserID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string ContactNo { get; set; }
		public int PackageInfoID { get; set; }
		public string Company { get; set; }
		public int? RemainingDays { get; set; }	
		public string BusinessType { get; set; }	
		public string PackageName { get; set; }	
		public string PackageNameAr { get; set; }	
		public DateTime? CreatedDate { get; set; }	
		public DateTime? ExpiryDate { get; set; }	
    }

    public class IntegrationStatsViewModel
    {
        public int IntegraionStatsID { get; set; }
        public int? IntegraionID { get; set; }
        public int? UserID { get; set; }
        public int? CustomerID { get; set; }
        public DateTime? Date { get; set; }
        public int? StatusID { get; set; }
        public string IntegrationType { get; set; }
        public string CustomerName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? ExternalReferenceID { get; set; }
        public string Remarks { get; set; }
        public double? ServiceCharges { get; set; }
    }
}

