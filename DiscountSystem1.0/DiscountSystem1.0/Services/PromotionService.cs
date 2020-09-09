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

        public float CalculateTotalPrice(string purchase)
        {
            try
            {
                //get simplified purchase list e.g., 3A = ['A','A','A']
                List<char> purchaseList = GetPurchaseList(purchase);

                // get simplified promotion list e.g., 3A+B = ['A', 'A', 'A', 'B']
                var simplifiedPromotionList = GetPromotionSimplified();

                //Calculate the minimum price possible
                var val = CalculateMinimumPossiblePrice(purchaseList, simplifiedPromotionList);
                return val;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured:{ex.Message}");
                return 0;
            }

        }
        public void DisplayRateChart()
        {
            Console.WriteLine($"The Rate Chart : \n");
            foreach(var item in RateChart)
            {
                Console.WriteLine($"SKU : {item.Key} || Unit Price : {item.Value}\n");
            }
        }

        public void DisplayPromotionList()
        {
            Console.WriteLine($"The Rate Chart : \n");
            foreach (var item in PromotionList)
            {
                Console.WriteLine($"Promotion Rule : {item.Item1} || Promotion Price : {item.Item2} || Promotion Type : {item.Item3}\n");
            }
        }

        public List<char> GetPurchaseList(string purchase)
        {
            throw new NotImplementedException();
        }

        public Dictionary<List<char>, float> GetPromotionSimplified()
        {
            throw new NotImplementedException();
        }

        public float CalculateMinimumPossiblePrice(List<char> purchaseList, Dictionary<List<char>, float> simplifiedPromotionList)
        {
            throw new NotImplementedException();
        }
    }
}
