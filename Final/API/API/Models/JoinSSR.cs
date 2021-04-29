using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.Models;
namespace API.Models
{
    public class JoinSSR
    {
     public StorageSpace GetStorageSpace { get; set; }

        public Room GetRoom { get; set; }
    }
}