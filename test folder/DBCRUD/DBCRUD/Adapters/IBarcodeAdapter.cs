using DBCRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBCRUD.Adapters
{
    interface IBarcodeAdapter
    {

        Item ReadBarcode(int barCode);

         Item ReturnTranslate(object data);
        
          
    }
}
