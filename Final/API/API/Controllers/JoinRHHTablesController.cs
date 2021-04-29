using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.Models;

namespace API.Controllers
{
    public class JoinRHHTablesController : ApiController
    {
        public IHttpActionResult GetTables()
        {
            COMP3000_SSharpeEntities2 db = new COMP3000_SSharpeEntities2();
            List<Room> lRoom = db.Rooms.ToList();
            COMP3000_SSharpeEntities1 idb = new COMP3000_SSharpeEntities1();
            List<HouseHold> lHousehold = idb.HouseHolds.ToList();

            var query = from r in lRoom
                        join hh in lHousehold on r.HHID equals hh.HHID into table1
                        from hh in table1.ToList()
                        select new JoinRHH { GetRoom = r, GetHouseHold = hh };
            return Ok(query);

        }
    }
}
