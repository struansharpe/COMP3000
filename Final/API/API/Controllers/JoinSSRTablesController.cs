using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.Models;

namespace API.Controllers
{
    public class JoinSSRTablesController : ApiController
    {
        public IHttpActionResult GetTables()
        {
            COMP3000_SSharpeEntities2 db = new COMP3000_SSharpeEntities2();
            List<Room> lRoom = db.Rooms.ToList();
            COMP3000_SSharpeEntities3 idb = new COMP3000_SSharpeEntities3();
            List<StorageSpace> lStorageSpace= idb.StorageSpaces.ToList();

            var query = from ss in lStorageSpace
                        join r in lRoom on ss.RID equals r.RID into table1
                        from r in table1.ToList()
                        select new JoinSSR { GetRoom = r, GetStorageSpace = ss };
            return Ok(query);

        }
    }
}
