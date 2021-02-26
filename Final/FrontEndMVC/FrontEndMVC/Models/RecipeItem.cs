using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEndMVC.Models
{
    public class RecipeItem
    {
        public int RecipeIID { get; set; }
        public Nullable<int> Item { get; set; }
        public Nullable<int> RecipeID { get; set; }
        public int QTY { get; set; }
        public Nullable<int> Weight { get; set; }
    }
}