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
    
    public partial class SalesItem
    {
        public int SalesItemID { get; set; }
        public Nullable<int> ItemID { get; set; }
        public Nullable<int> LocationID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
    
        public virtual Location Location { get; set; }
    }
}
