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
    
    public partial class inv_StockIssueDetail
    {
        public int StockIssueDetailID { get; set; }
        public Nullable<int> StockIssueID { get; set; }
        public Nullable<int> ItemID { get; set; }
        public Nullable<int> IssueQty { get; set; }
        public Nullable<int> RequestQty { get; set; }
        public Nullable<int> ReceiveQty { get; set; }
        public Nullable<int> StatusID { get; set; }
        public string LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
        public Nullable<System.DateTime> CreateOn { get; set; }
        public string CreatedBy { get; set; }
        public string Notes { get; set; }
    
        public virtual inv_StockIssue inv_StockIssue { get; set; }
        public virtual Item Item { get; set; }
    }
}
