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
    
    public partial class sp_rptCreditCustomer_Result
    {
        public string RegistrationNo { get; set; }
        public string ModelName { get; set; }
        public string MakeName { get; set; }
        public string CustomerContact { get; set; }
        public string CustomerName { get; set; }
        public int OrderID { get; set; }
        public Nullable<int> TransactionNo { get; set; }
        public Nullable<int> OrderNo { get; set; }
        public Nullable<double> AmountTotal { get; set; }
        public Nullable<double> AmountDiscount { get; set; }
        public Nullable<double> Tax { get; set; }
        public Nullable<double> GrandTotal { get; set; }
        public Nullable<double> RefundedAmount { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
    }
}
