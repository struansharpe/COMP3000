using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Models;

namespace API.Adapters
{
    interface IShoppingAdapter
    {

        HouseHoldItem GetShopping(int shopping);
    }
}
