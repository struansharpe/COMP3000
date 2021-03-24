using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpToDateApi.Models;

namespace UpToDateApi.Adapters
{
    interface IShoppingAdapter
    {

        HouseHoldItem GetShopping(int shopping);
    }
}
