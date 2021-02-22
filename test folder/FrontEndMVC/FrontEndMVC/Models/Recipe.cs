using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEndMVC.Models
{
    public class Recipe
    {
        public int RecipeID { get; set; }
        public string Details { get; set; }
        public string Instructions { get; set; }
        public Nullable<int> UserID { get; set; }
    }
}