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
    
    public partial class sp_StockHistoryV2_rpt_Result
    {
        public int StockID { get; set; }
        public string StoreName { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public int CurrentStock { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string RefrenceNo { get; set; }
        public int StockIn { get; set; }
        public int StockOut { get; set; }
        public int ItemID { get; set; }
    }
}
