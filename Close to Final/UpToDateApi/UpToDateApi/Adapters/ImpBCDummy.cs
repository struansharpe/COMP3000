using UpToDateApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UpToDateApi.Adapters;

namespace UpToDateApi.Adapters
{
    public class ImpBCDummy: IBarcodeAdapter
    {

        IDictionary<int, Item> BCValue = new Dictionary<int, Item>() {
            { 88887777, new Item(1234567, "Beans")},
            { 77776666, new Item(2345678, "Sausages")},
            { 88889999, new Item(3456789, "Bananas")}};



        private Item ReturnTranslate(object data)
        {

            return (Item)data;
        }

        public Item FindProductByBarcode(int shopping)
        {
            return ReturnTranslate(BCValue[shopping]);

        }
    }
}