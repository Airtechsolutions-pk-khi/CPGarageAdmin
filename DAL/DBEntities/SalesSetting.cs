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
    
    public partial class SalesSetting
    {
        public int SalesSettingID { get; set; }
        public Nullable<double> SalesMonthlyTarget { get; set; }
        public Nullable<int> CarsMonthlyTarget { get; set; }
        public Nullable<double> SalesDailyTarget { get; set; }
        public Nullable<int> CarsDailyTarget { get; set; }
        public Nullable<int> StatusID { get; set; }
        public Nullable<int> LocationID { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
        public Nullable<int> UserID { get; set; }
    
        public virtual Location Location { get; set; }
    }
}
