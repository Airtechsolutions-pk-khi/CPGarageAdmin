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
    
    public partial class sp_GetIntegrationSetttings_Result
    {
        public int IntegrationID { get; set; }
        public string Name { get; set; }
        public string IntegrationKey { get; set; }
        public string Token { get; set; }
        public string Value { get; set; }
        public Nullable<int> StatusID { get; set; }
        public Nullable<int> UserID { get; set; }
        public string URL { get; set; }
    }
}