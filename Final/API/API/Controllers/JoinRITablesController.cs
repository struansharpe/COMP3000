using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.Models;

namespace API.Controllers
{
    public class JoinRITablesController : ApiController
    {

        public IHttpActionResult GetTables()
        {
            COMP3000_SSharpeEntities10 db = new COMP3000_SSharpeEntities10();
            List<RecipeItem> lrecipeItems = db.RecipeItems.ToList();
            COMP3000_SSharpeEntities5 idb = new COMP3000_SSharpeEntities5();
            List<Item> litems = idb.Items.ToList();

            var query = from ri in lrecipeItems
                        join i in litems on ri.Item equals i.IID into table1
                        from i in table1.ToList()
                        select new JoinRecipeItem{ getItem = i, GetRecipeItem = ri };

            return Ok(query);
        }
    }
}
