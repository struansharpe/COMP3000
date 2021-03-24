using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEndMVC.Models
{
    public class ShoppingListItem
    {
        public int SLIID { get; set; }
        public Nullable<int> SLID { get; set; }
        public int IID { get; set; }
        public Nullable<int> ITID { get; set; }
        public int QTY { get; set; }
    }
}