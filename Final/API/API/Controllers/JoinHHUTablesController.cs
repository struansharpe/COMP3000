using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.Models;

namespace API.Controllers
{
    public class JoinHHUTablesController : ApiController
    {
        public IHttpActionResult GetTables()
        {
            COMP3000_SSharpeEntities db = new COMP3000_SSharpeEntities();
            List<User> lUser = db.Users.ToList();
            COMP3000_SSharpeEntities1 idb = new COMP3000_SSharpeEntities1();
            List<HouseHold> lHousehold = idb.HouseHolds.ToList();

            var query = from hh in lHousehold
                        join u in lUser on hh.AdminUser equals u.UID into table1
                        from u in table1.ToList()
                        select new JoinHHU { GetUser = u, GetHouseHold = hh };
            return Ok(query);

        }
    }
}
