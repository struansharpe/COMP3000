//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DBCRUD.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Item
    {
        public int IID { get; set; }
        public string ItemName { get; set; }
        public Nullable<int> ITID { get; set; }
        public Item(){}
        public Item(int ID, string Name)
        {
            IID = ID;
            ItemName = Name;
            ITID = null;
        }

        public Item(int ID, string Name,int TypeID)
        {
            IID = ID;
            ItemName = Name;
            ITID = TypeID;
        }
    }
}
