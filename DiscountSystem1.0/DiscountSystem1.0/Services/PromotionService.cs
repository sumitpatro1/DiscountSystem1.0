using PromotionSystem1._0.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionSystem1._0.Services
{
    public class PromotionService: IPromotionService
    {
        public PromotionService()
        {
            //Set RateChart and DiscountList here. bewlow values are random update the list as per requirement
            RateChart = new Dictionary<char, float>()
            {
                { 'A', 1 },
                { 'B', 2 },
                { 'C', 3 },
                { 'D', 4 }
            };

            PromotionList = new List<Tuple<string, float, PromotionTypes>>()
            {
                Tuple.Create<string, float, PromotionTypes>("3A", 2 , PromotionTypes.MULTI ),
                Tuple.Create<string, float, PromotionTypes>( "A+B+C", 4, PromotionTypes.CUMULATIVE )
            };
        }
        public Dictionary<char,float> RateChart { get; set; }
        public List<Tuple<string, float, PromotionTypes>> PromotionList { get; set; }

        public float CalculatePromotion()
        {
            throw new NotImplementedException();
        }
        public void DisplayRateChart()
        {

        }

        public void DisplayPromotionList()
        {

        }


    }
}
