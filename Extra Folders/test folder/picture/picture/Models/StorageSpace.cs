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
    
    public partial class StorageSpace
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StorageSpace()
        {
            this.HouseHoldItems = new HashSet<HouseHoldItem>();
        }
    
        public int SSID { get; set; }
        public string StorageName { get; set; }
        public Nullable<int> RID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HouseHoldItem> HouseHoldItems { get; set; }
        public virtual Room Room { get; set; }
    }
}
