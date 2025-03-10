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
    
    public partial class SessionInfo
    {
        public int RowId { get; set; }
        public string SessionId { get; set; }
        public int SubUserId { get; set; }
        public int StatusID { get; set; }
        public int UTC { get; set; }
        public Nullable<System.DateTime> StartDT { get; set; }
        public Nullable<System.DateTime> EndDT { get; set; }
        public Nullable<System.TimeSpan> OpenTime { get; set; }
        public Nullable<System.TimeSpan> CloseTime { get; set; }
        public Nullable<System.DateTime> LogoutDT { get; set; }
        public Nullable<System.DateTime> LoginDT { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int LocationID { get; set; }
        public string Currency { get; set; }
    
        public virtual Location Location { get; set; }
        public virtual SubUser SubUser { get; set; }
    }
}
