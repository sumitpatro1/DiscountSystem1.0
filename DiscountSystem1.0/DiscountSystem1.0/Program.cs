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

            Console.WriteLine("Please enter the items you want to purchase in following format: '3A,B,2C'. \nEnter your items here: ");
            var purchase = Console.ReadLine();
            do
            {
                //Calculate Total Price
                var total = promotionSvc.CalculateTotalPrice(purchase);
                Console.WriteLine($"Total: \t {total} \n");
                Console.WriteLine("Pleae enter the items in following format: 3A,B,2C. Or, to exit Please type 'Exit'. \nEnter here: ");
                purchase = Console.ReadLine().Replace(" ","").ToUpper(); //Remove unnecessary spaces in between text if any
            }
            while (purchase != "EXIT");

        }
    }
}
