using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FrontEndMVC.Models;

namespace FrontEndMVC.Models
{
    public class JoinSSR
    {
     public StorageSpace GetStorageSpace { get; set; }

        public Room GetRoom { get; set; }
    }
}