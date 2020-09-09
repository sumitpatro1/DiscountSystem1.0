using PromotionSystem1._0.Services;
using System;
using System.Collections.Generic;

namespace PromotionSystem1._0
{
    class Program
    {
        static void Main(string[] args)
        {
            //Display rate chart and Promotion list
            var promotionSvc = new PromotionService();
            promotionSvc.DisplayRateChart();
            promotionSvc.DisplayPromotionList();

            Console.WriteLine("Pleae enter the items in following format: 3A,B,2C. \nEnter your items: ");
            var purchase = Console.ReadLine();
            
            //Calculate Total Price
            var total = promotionSvc.CalculateTotalPrice(purchase);
            Console.WriteLine($"total: \t {total}");
        }
    }
}
