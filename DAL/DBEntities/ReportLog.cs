//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.DBEntities
{
    using System;
    using System.Collections.Generic;
    
    public partial class ReportLog
    {
        public int ReportID { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<int> StaffID { get; set; }
        public string SessionID { get; set; }
        public Nullable<double> CashAmount { get; set; }
        public Nullable<double> CardAmount { get; set; }
        public Nullable<double> CouponAmount { get; set; }
        public Nullable<double> ShortAmount { get; set; }
        public Nullable<double> ExcessAmount { get; set; }
        public Nullable<double> ExpenseAmount { get; set; }
        public Nullable<int> LocationID { get; set; }
        public string LastUpdatedeBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}
