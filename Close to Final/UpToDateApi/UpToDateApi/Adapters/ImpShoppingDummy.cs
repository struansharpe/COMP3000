using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UpToDateApi.Models;

namespace UpToDateApi.Adapters
{
    public class ImpShoppingDummy : IShoppingAdapter
    {

        IDictionary<int, HouseHoldItem> BCValue = new Dictionary<int, HouseHoldItem>() {
            { 88887777, new HouseHoldItem(1234567, 4)},
            { 88887776, new HouseHoldItem(2345678, 5)},
            { 88889999, new HouseHoldItem(3456789, 6)}};
        //ShoppingList IShoppingAdapter.GetShopping(int item)
        //{
        //    return ReturnTranslate(BCValue[item]);
        //}

        private HouseHoldItem ReturnTranslate(object data)
        {

            return (HouseHoldItem)data;
        }

        public HouseHoldItem GetShopping(int shopping)
        {
            return ReturnTranslate(BCValue[shopping]);

        }

       
    }
}