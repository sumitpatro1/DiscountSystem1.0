using DiscountSystem1._0.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscountSystem1._0.Services
{
    class DiscountService: IDiscountService
    {
        public DiscountService()
        {
            //Set RateChart and DiscountList here:
        }
        public Dictionary<char,float> RateChart { get; set; }
        public List<Tuple<string, float, DiscountTypes>> DiscountList { get; set; }

        public float CalculateDiscount()
        {
            throw new NotImplementedException();
        }

    }
}
