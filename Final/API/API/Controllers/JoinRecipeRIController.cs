using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.Models;
namespace API.Controllers
{
    public class JoinRecipeRIController : ApiController
    {
        public IHttpActionResult GetTables()
        {
            COMP3000_SSharpeEntities9 db = new COMP3000_SSharpeEntities9();
            List<Recipe> lRecipes = db.Recipes.ToList();
            COMP3000_SSharpeEntities10 dbi = new COMP3000_SSharpeEntities10();
            List<RecipeItem> lRecipeItems = dbi.RecipeItems.ToList();
            COMP3000_SSharpeEntities5 sd = new COMP3000_SSharpeEntities5();
            List<Item> litems = sd.Items.ToList();

            var query = from ri in lRecipeItems
                        join r in lRecipes on ri.RecipeID equals r.RecipeID into table1
                        from r in table1.ToList()
                        
                        join i in litems on ri.Item equals i.IID into table3
                        from i in table3.ToList()
                        select new JoinRecipeRI { GetRecipe = r, GetRecipeItem = ri, GetItem = i};
            return Ok(query);
        }
    }
}

