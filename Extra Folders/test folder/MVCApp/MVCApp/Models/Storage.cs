using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCApp.Models
{
    public class Storage
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public Nullable<int> Weight { get; set; }
        public int QTY { get; set; }
        public string Cupboard { get; set; }
        public string Room { get; set; }
    }
}