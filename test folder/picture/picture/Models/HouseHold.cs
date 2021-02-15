//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace picture.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class HouseHold
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HouseHold()
        {
            this.Rooms = new HashSet<Room>();
            this.HouseHoldItems = new HashSet<HouseHoldItem>();
        }
    
        public int HHID { get; set; }
        public string HHName { get; set; }
        public int AdminUser { get; set; }
        public Nullable<int> RestrictedUsers { get; set; }
    
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Room> Rooms { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HouseHoldItem> HouseHoldItems { get; set; }
        public virtual User User1 { get; set; }
    }
}
