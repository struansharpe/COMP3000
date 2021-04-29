using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class JoinHHI
    {
        public HouseHoldItem GetHouseHoldItem { get; set; }

        public Item GetItem { get; set; }

        public HouseHold GetHouseHold { get; set; }
    }
}