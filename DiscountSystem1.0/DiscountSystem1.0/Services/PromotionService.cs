using PromotionSystem1._0.Core;
using System;
using System.Collections.Generic;
using System.Linq;
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
            List<char> purchaseList = new List<char>();
            try
            {
                var purchaseItemsSeparated = new List<string>(purchase.Split(','));
                purchaseItemsSeparated.ForEach(item =>
                {
                    int multiple;
                    var multipleValue = item.Length > 1 ? item.Substring(0, item.Length - 1) : "1";
                    if (Int32.TryParse(multipleValue, out multiple) && RateChart.ContainsKey(item[item.Length - 1]))
                    {
                        purchaseList.AddRange(Enumerable.Repeat(item[item.Length - 1], multiple));
                    }
                    else
                    {
                        throw new Exception();
                    }
                });
                return purchaseList;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to process, please make sure you mention the purchase list in the format mentioned, also do not enter items that are not available in rate chart.");
                throw ex;
            }
        }

        public Dictionary<List<char>, float> GetPromotionSimplified()
        {
            var simplifiedPromotionList = new Dictionary<List<char>, float>();
            List<char> individualPromotionItemsList;
            PromotionList.ForEach(promotion => {
                individualPromotionItemsList = new List<char>();
                if (promotion.Item3 == PromotionTypes.MULTI)
                {
                    var multiple = promotion.Item1.Length > 1 ? Convert.ToInt32(promotion.Item1.Substring(0, promotion.Item1.Length - 1)) : 1;
                    individualPromotionItemsList.AddRange(Enumerable.Repeat(Char.ToUpper(promotion.Item1[promotion.Item1.Length - 1]), multiple));
                }
                else if (promotion.Item3 == PromotionTypes.CUMULATIVE)
                {
                    var discountSeparated = promotion.Item1.Split('+').ToList();
                    discountSeparated.ForEach(discount =>
                    {
                        var multiple = discount.Length > 1 ? Convert.ToInt32(discount.Substring(0, discount.Length - 1)) : 1;
                        individualPromotionItemsList.AddRange(Enumerable.Repeat(Char.ToUpper(discount[discount.Length - 1]), multiple));
                    });
                }
                simplifiedPromotionList.Add(individualPromotionItemsList, promotion.Item2);
            });
            return simplifiedPromotionList;
        }

        public float CalculateMinimumPossiblePrice(List<char> purchaseList, Dictionary<List<char>, float> promotions)
        {
            var clonePurchaseList = new List<char>(purchaseList);
            float total = 0;
            //If discounts exist then only try to apply discount
            if (promotions.Any())
            {
                var promotionValue = promotions.First().Value;
                foreach (var key in promotions.First().Key)
                {
                    //If current promotion cannot be applied because items does not exist in the purchase list then, remove current promotion from the promotion list
                    if (!clonePurchaseList.Remove(key))
                    {
                        promotionValue = 0;
                        clonePurchaseList = new List<char>(purchaseList);
                        promotions = promotions.Skip(1).ToDictionary(key => key.Key, value => value.Value);
                        break;
                    };
                }

                // Recurssive Implementation
                // if discount applied then items are removed from the purchase list
                var innerTotal = promotionValue + CalculateMinimumPossiblePrice(clonePurchaseList, promotions);

                // if promotionValue > 0 then promotion appplied successfully and promotion also exists in the promotion list
                // for such condition we will remove the current promotion from the promotion list and we will pass the unaltered purchase items
                // this is done to cover the scenario where we skip a discount and check whether the amont will be lesser if we skip a promotion rule.
                var innerTotalSkipDiscount = CalculateMinimumPossiblePrice(purchaseList, promotionValue > 0 ? promotions.Skip(1).ToDictionary(key => key.Key, value => value.Value) : promotions);

                total += innerTotal <= innerTotalSkipDiscount ? innerTotal : innerTotalSkipDiscount;
            }
            else
            {
                purchaseList.ForEach(item => total += RateChart.ContainsKey(item) ? RateChart[item] : 0);
            }
            return total;
        }
    }
}
