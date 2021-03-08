using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Adapters
{
    interface IBarcodeAdapter
    {

        Item FindProductByBarcode(int barCode);


    }
}
