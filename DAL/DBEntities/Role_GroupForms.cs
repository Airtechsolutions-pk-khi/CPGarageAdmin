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
    
    public partial class Role_GroupForms
    {
        public int GroupFormID { get; set; }
        public Nullable<int> GroupID { get; set; }
        public Nullable<int> FormID { get; set; }
        public Nullable<bool> New { get; set; }
        public Nullable<bool> Edit { get; set; }
        public Nullable<bool> Remove { get; set; }
        public Nullable<bool> Access { get; set; }
        public Nullable<int> StatusID { get; set; }
    
        public virtual Role_Forms Role_Forms { get; set; }
        public virtual Role_Group Role_Group { get; set; }
    }
}
