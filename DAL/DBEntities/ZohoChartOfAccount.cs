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
    
    public partial class ZohoChartOfAccount
    {
        public int ChartOfAccountsID { get; set; }
        public string account_id { get; set; }
        public string account_name { get; set; }
        public string account_type { get; set; }
        public string AccountTypeZoho { get; set; }
        public string is_active { get; set; }
        public int UserID { get; set; }
        public string LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
    
        public virtual User User { get; set; }
    }
}
