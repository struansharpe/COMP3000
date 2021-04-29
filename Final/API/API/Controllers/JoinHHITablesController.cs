using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class JoinHHITablesController : ApiController
    {
        public IHttpActionResult GetTables()
        {
            //COMP3000_SSharpeEntities4 db = new COMP3000_SSharpeEntities4();
            //List<HouseHoldItem> lHouseHoldItems = db.HouseHoldItems.ToList();

            //COMP3000_SSharpeEntities5 idb = new COMP3000_SSharpeEntities5();
            //List<Item> litems = idb.Items.ToList();

            //var query = from hhi in lHouseHoldItems
            //            join i in litems on hhi.IID equals i.IID into table1
            //            from i in table1.ToList()
            //            select new JoinHHI { GetItem = i, GetHouseHoldItem = hhi };
            //return Ok(query);

            COMP3000_SSharpeEntities5 sd = new COMP3000_SSharpeEntities5();
            List<Item> litems = sd.Items.ToList();

            COMP3000_SSharpeEntities4 mp = new COMP3000_SSharpeEntities4();
            List<HouseHoldItem> lHouseHoldItems = mp.HouseHoldItems.ToList();

            COMP3000_SSharpeEntities1 hh = new COMP3000_SSharpeEntities1();
            List<HouseHold> lHouseHolds = hh.HouseHolds.ToList();



            var query = from hhi in lHouseHoldItems
                        join i in litems on hhi.IID equals i.IID into table1
                        from i in table1.ToList()

                        join h in lHouseHolds on hhi.HHID equals h.HHID into table3
                        from h in table3.ToList()
                        select new JoinHHI { GetItem = i, GetHouseHoldItem = hhi, GetHouseHold = h };

            return Ok(query);
        }

    }
}

