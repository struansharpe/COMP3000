using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.Models;

namespace API.Controllers
{
    public class JoinSLITablesController : ApiController
    {
        public IHttpActionResult GetTables()
        {
            COMP3000_SSharpeEntities14 db = new COMP3000_SSharpeEntities14();
            List<ShoppingListItem> lshoppingListItems = db.ShoppingListItems.ToList();
            COMP3000_SSharpeEntities5 idb = new COMP3000_SSharpeEntities5();
            List<Item> litems = idb.Items.ToList();

            var query = from sli in lshoppingListItems
                        join i in litems on sli.IID equals i.IID into table1
                        from i in table1.ToList()
                        select new JoinSLI { getItem = i, GetShoppingListItem = sli };
           
            return Ok(query);
        }
    }
}


