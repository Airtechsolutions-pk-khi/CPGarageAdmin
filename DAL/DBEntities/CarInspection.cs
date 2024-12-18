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
    
    public partial class CarInspection
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CarInspection()
        {
            this.CarInspectionDetails = new HashSet<CarInspectionDetail>();
        }
    
        public int CarInspectionID { get; set; }
        public string Name { get; set; }
        public string AlternateName { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public Nullable<int> StatusID { get; set; }
        public int UserID { get; set; }
        public string LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CarInspectionDetail> CarInspectionDetails { get; set; }
        public virtual User User { get; set; }
    }
}
