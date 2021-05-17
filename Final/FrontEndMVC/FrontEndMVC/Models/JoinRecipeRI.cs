using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEndMVC.Models
{
    public class JoinRecipeRI
    {
        public Recipe GetRecipe { get; set; }

        public RecipeItem GetRecipeItem { get; set; }

        public Item GetItem { get; set; }
    }
}